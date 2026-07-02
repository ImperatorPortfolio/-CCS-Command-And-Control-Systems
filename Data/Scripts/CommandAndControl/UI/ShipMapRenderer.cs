using System;
using System.Collections.Generic;
using VRage.Game.GUI.TextPanel;
using VRageMath;

namespace AGS
{
    // Blueprint-style wireframe of the construct hull. Every block contributes its true
    // surface geometry — cubes from cell occupancy, slopes/corners from oriented shape
    // templates whose conventions are taken from CubeBlocks_Armor.sbc mount points — and
    // touching blocks are merged by FACE CANCELLATION: a face emitted by two solids is an
    // interior interface and vanishes. The surviving surface is then reduced to feature
    // lines: an edge is drawn only where the surface folds (its bordering faces are not
    // coplanar) or where it borders a single face (an open silhouette). Flat sheets —
    // decks, walls, long ramp runs of 2x1 base/tip pairs — therefore collapse into one
    // collated outline instead of a grid of individual blocks.
    //
    // The feature-line set is view-independent and cached per ship geometry version.
    // Per view, HIDDEN-LINE REMOVAL then makes the hull opaque: every line is sampled
    // and ray-tested (orthographically, toward the camera) against the surviving faces;
    // covered sub-segments are dropped, so the wireframe reads as a solid object rather
    // than glass. Visible segments are cached per view, so per frame only projection,
    // fitting and drawing run. Views are true orthographic projections along explicit
    // camera bases (naval drawing conventions: TOP = bow up, starboard right; LEFT =
    // bow left; FRONT = seen from ahead of the bow). Lines are depth-shaded. Specialty
    // (non-armour) systems and security features are drawn as icons on top.
    public static class ShipMapRenderer
    {
        private struct Line3
        {
            public Vector3 A;
            public Vector3 B;
        }

        private struct ViewBasis
        {
            public Vector3 Right; // screen +x
            public Vector3 Up;    // screen +y
            public Vector3 Dir;   // camera look direction (into the scene)
        }

        // ── Feature-line cache ───────────────────────────────────────────────────────
        // Extraction is geometry-only (no view dependence), so it reruns only when the
        // ship model is rebuilt (CopyFrom/Clear bump Version). The surviving surface
        // faces are kept alongside the lines: they are the occluders for the per-view
        // hidden-line pass.
        private static ShipVoxelModel s_cachedShip;
        private static int s_cachedVersion = -1;
        private static readonly List<Line3> s_lines = new List<Line3>();
        private static readonly List<SurfFace> s_faces = new List<SurfFace>();

        // Per-view visible-segment cache (index = ShipView value).
        private const int ViewCount = 8;
        private static readonly List<Line3>[] s_viewSegments = new List<Line3>[ViewCount];
        private static readonly ShipVoxelModel[] s_viewShips = new ShipVoxelModel[ViewCount];
        private static readonly int[] s_viewVersions = new int[ViewCount];

        // Reused projection buffers (two endpoints per line).
        private static float[] s_px = new float[512];
        private static float[] s_py = new float[512];
        private static float[] s_pd = new float[512];

        public static void Render(MySpriteDrawFrame frame, Vector2 origin, UiRect rect, UiContext context, SecurityEntity selected)
        {
            UiRenderer.DrawPanel(frame, origin, new UiPanel { Bounds = rect, Background = context.Theme.Background1 });
            UiRenderer.DrawOutline(frame, origin, rect, context.Theme.Border, Math.Max(1f, context.Theme.StrokeThin));

            var model = context.Frame.Security;
            var ship = model != null ? model.Ship : null;
            if (ship == null || !ship.HasCells)
            {
                DrawEmpty(frame, origin, rect, context, "No construct geometry available");
                return;
            }

            EnsureLines(ship);
            if (s_lines.Count == 0)
            {
                DrawEmpty(frame, origin, rect, context, "Construct hull not visible");
                return;
            }

            var basis = GetViewBasis(model.View);

            // Hidden-line removal is view-dependent; visible segments are cached per view
            // until the ship geometry version changes.
            var lines = EnsureVisibleSegments(ship, model.View);

            // 1) Project every line endpoint; track screen-space and depth bounds.
            var count = lines.Count;
            EnsureCapacity(count * 2);
            float minX = float.MaxValue, minY = float.MaxValue, maxX = float.MinValue, maxY = float.MinValue;
            float minD = float.MaxValue, maxD = float.MinValue;
            for (var i = 0; i < count; i++)
            {
                var line = lines[i];
                var a = i * 2;
                var b = a + 1;
                ProjectPoint(ref basis, line.A.X, line.A.Y, line.A.Z, out s_px[a], out s_py[a], out s_pd[a]);
                ProjectPoint(ref basis, line.B.X, line.B.Y, line.B.Z, out s_px[b], out s_py[b], out s_pd[b]);
                if (s_px[a] < minX) minX = s_px[a];
                if (s_px[a] > maxX) maxX = s_px[a];
                if (s_py[a] < minY) minY = s_py[a];
                if (s_py[a] > maxY) maxY = s_py[a];
                if (s_pd[a] < minD) minD = s_pd[a];
                if (s_pd[a] > maxD) maxD = s_pd[a];
                if (s_px[b] < minX) minX = s_px[b];
                if (s_px[b] > maxX) maxX = s_px[b];
                if (s_py[b] < minY) minY = s_py[b];
                if (s_py[b] > maxY) maxY = s_py[b];
                if (s_pd[b] < minD) minD = s_pd[b];
                if (s_pd[b] > maxD) maxD = s_pd[b];
            }

            // 2) Fit the projected model into the plot rect, scaled by zoom.
            var zoomFactor = MathHelper.Clamp(1f + (model.ZoomStep * 0.18f), 0.4f, 3.5f);
            var pad = Math.Max(6f, Math.Min(rect.Width, rect.Height) * 0.08f);
            var plot = rect.Deflate(new UiThickness(pad));
            var spanX = Math.Max(0.001f, maxX - minX);
            var spanY = Math.Max(0.001f, maxY - minY);
            var scale = Math.Min(plot.Width / spanX, plot.Height / spanY) * zoomFactor;
            var modelCenterX = (minX + maxX) * 0.5f;
            var modelCenterY = (minY + maxY) * 0.5f;
            var centerX = plot.X + (plot.Width * 0.5f);
            var centerY = plot.Y + (plot.Height * 0.5f);

            // 3) Draw the wireframe, shading near lines brighter than far ones.
            var depthRange = Math.Max(0.001f, maxD - minD);
            for (var i = 0; i < count; i++)
            {
                var a = i * 2;
                var b = a + 1;
                var pa = new Vector2(centerX + ((s_px[a] - modelCenterX) * scale), centerY - ((s_py[a] - modelCenterY) * scale));
                var pb = new Vector2(centerX + ((s_px[b] - modelCenterX) * scale), centerY - ((s_py[b] - modelCenterY) * scale));
                var t = ((s_pd[a] + s_pd[b]) * 0.5f - minD) / depthRange; // 0 = farthest, 1 = nearest
                UiRenderer.DrawLine(frame, origin, pa, pb, ShadeByDepth(context, t), 1f);
            }

            // 4) Specialty-block icons, then security feature markers, over the wireframe.
            DrawDevices(frame, origin, context, ship, ref basis, scale, modelCenterX, modelCenterY, centerX, centerY);
            DrawMarkers(frame, origin, context, model, selected, ref basis, scale, modelCenterX, modelCenterY, centerX, centerY);

            // 5) View label.
            UiRenderer.DrawLabel(frame, origin, new UiLabel
            {
                Bounds = new UiRect(rect.X + 4f, rect.Y + 4f, rect.Width - 8f, Math.Max(14f, rect.Height * 0.08f)),
                Padding = new UiThickness(0f),
                Text = GetViewLabel(model.View) + " VIEW",
                Font = "Monospace",
                Scale = Math.Max(context.Theme.TextSm, 0.32f),
                Color = context.Theme.ForegroundPrimary,
                Alignment = TextAlignment.CENTER,
                VerticalAlignment = UiVerticalAlignment.Center
            });
        }

        public static string GetViewLabel(ShipView view)
        {
            switch (view)
            {
                case ShipView.Bottom: return "BOTTOM";
                case ShipView.Left: return "LEFT";
                case ShipView.Right: return "RIGHT";
                case ShipView.Front: return "FRONT";
                case ShipView.Back: return "BACK";
                case ShipView.Iso: return "ISO";
                case ShipView.IsoRear: return "ISO REAR";
                default: return "TOP";
            }
        }

        // ── Views ────────────────────────────────────────────────────────────────────
        // Model space is the primary controller's local frame: +X starboard, +Y up,
        // -Z bow (VRage forward). Each view is an explicit orthographic camera basis.
        private static ViewBasis GetViewBasis(ShipView view)
        {
            Vector3 dir, up;
            switch (view)
            {
                case ShipView.Bottom: dir = new Vector3(0f, 1f, 0f); up = new Vector3(0f, 0f, -1f); break;   // bow up, mirrored
                case ShipView.Front: dir = new Vector3(0f, 0f, 1f); up = new Vector3(0f, 1f, 0f); break;     // seen from ahead
                case ShipView.Back: dir = new Vector3(0f, 0f, -1f); up = new Vector3(0f, 1f, 0f); break;     // seen from astern
                case ShipView.Left: dir = new Vector3(1f, 0f, 0f); up = new Vector3(0f, 1f, 0f); break;      // port side, bow left
                case ShipView.Right: dir = new Vector3(-1f, 0f, 0f); up = new Vector3(0f, 1f, 0f); break;    // starboard side, bow right
                case ShipView.Iso: dir = Vector3.Normalize(new Vector3(-1f, -1f, 1f)); up = new Vector3(0f, 1f, 0f); break;     // from above front-starboard
                case ShipView.IsoRear: dir = Vector3.Normalize(new Vector3(1f, -1f, -1f)); up = new Vector3(0f, 1f, 0f); break; // from above rear-port
                default: dir = new Vector3(0f, -1f, 0f); up = new Vector3(0f, 0f, -1f); break;               // Top: bow up, starboard right
            }

            // Orthonormalise up against the view direction (matters for the iso views).
            up = up - (dir * Vector3.Dot(up, dir));
            up = Vector3.Normalize(up);
            return new ViewBasis { Right = Vector3.Cross(dir, up), Up = up, Dir = dir };
        }

        // Orthographic projection onto the view basis. Depth grows toward the camera
        // (larger = nearer) so it feeds ShadeByDepth directly.
        private static void ProjectPoint(ref ViewBasis basis, float x, float y, float z, out float sx, out float sy, out float depth)
        {
            sx = (basis.Right.X * x) + (basis.Right.Y * y) + (basis.Right.Z * z);
            sy = (basis.Up.X * x) + (basis.Up.Y * y) + (basis.Up.Z * z);
            depth = -((basis.Dir.X * x) + (basis.Dir.Y * y) + (basis.Dir.Z * z));
        }

        private static void EnsureCapacity(int needed)
        {
            if (s_px.Length >= needed)
            {
                return;
            }

            var size = s_px.Length;
            while (size < needed)
            {
                size *= 2;
            }

            s_px = new float[size];
            s_py = new float[size];
            s_pd = new float[size];
        }

        // ── Hidden-line removal ──────────────────────────────────────────────────────
        // Makes the hull opaque for a given view: every feature line is subdivided into
        // short sub-segments and each sample is ray-tested toward the camera against the
        // surviving surface faces. Covered runs are dropped, visible runs are merged back
        // into segments. Orthographic projection means a sample's screen position equals
        // its ray's, so faces are binned on a screen-space grid and each sample only
        // tests the few faces overlapping its bin.

        private const float SampleStep = 0.45f; // world units between visibility samples
        private const float RayEpsilon = 0.05f; // ignore faces this close along the ray (the line's own faces)
        private const int OcclusionBins = 48;

        private sealed class SurfFace
        {
            public Vector3[] Verts;
            public Vector3 Normal; // unit length
            public float PlaneC;   // Normal · Verts[0]
            // Per-view scratch: projected screen bounds + nearest depth, for binning.
            public float MinX;
            public float MinY;
            public float MaxX;
            public float MaxY;
            public float MaxD;
        }

        private static List<Line3> EnsureVisibleSegments(ShipVoxelModel ship, ShipView view)
        {
            var vi = (int)view;
            if (vi < 0 || vi >= ViewCount)
            {
                vi = 0;
            }

            if (s_viewSegments[vi] != null && ReferenceEquals(s_viewShips[vi], ship) && s_viewVersions[vi] == ship.Version)
            {
                return s_viewSegments[vi];
            }

            var basis = GetViewBasis(view);
            var segments = ComputeVisibleSegments(ref basis);
            s_viewSegments[vi] = segments;
            s_viewShips[vi] = ship;
            s_viewVersions[vi] = ship.Version;
            return segments;
        }

        private static List<Line3> ComputeVisibleSegments(ref ViewBasis basis)
        {
            var result = new List<Line3>(s_lines.Count);
            if (s_faces.Count == 0)
            {
                result.AddRange(s_lines);
                return result;
            }

            // Project every face's bounds into view space and bin them on a screen grid.
            float minX = float.MaxValue, minY = float.MaxValue, maxX = float.MinValue, maxY = float.MinValue;
            for (var i = 0; i < s_faces.Count; i++)
            {
                var face = s_faces[i];
                float fMinX = float.MaxValue, fMinY = float.MaxValue, fMaxX = float.MinValue, fMaxY = float.MinValue, fMaxD = float.MinValue;
                var verts = face.Verts;
                for (var v = 0; v < verts.Length; v++)
                {
                    float sx, sy, sd;
                    ProjectPoint(ref basis, verts[v].X, verts[v].Y, verts[v].Z, out sx, out sy, out sd);
                    if (sx < fMinX) fMinX = sx;
                    if (sx > fMaxX) fMaxX = sx;
                    if (sy < fMinY) fMinY = sy;
                    if (sy > fMaxY) fMaxY = sy;
                    if (sd > fMaxD) fMaxD = sd;
                }

                face.MinX = fMinX;
                face.MinY = fMinY;
                face.MaxX = fMaxX;
                face.MaxY = fMaxY;
                face.MaxD = fMaxD;
                if (fMinX < minX) minX = fMinX;
                if (fMaxX > maxX) maxX = fMaxX;
                if (fMinY < minY) minY = fMinY;
                if (fMaxY > maxY) maxY = fMaxY;
            }

            var spanX = Math.Max(0.001f, maxX - minX);
            var spanY = Math.Max(0.001f, maxY - minY);
            var bins = new List<int>[OcclusionBins * OcclusionBins];
            for (var i = 0; i < s_faces.Count; i++)
            {
                var face = s_faces[i];
                var bx0 = BinIndex(face.MinX, minX, spanX);
                var bx1 = BinIndex(face.MaxX, minX, spanX);
                var by0 = BinIndex(face.MinY, minY, spanY);
                var by1 = BinIndex(face.MaxY, minY, spanY);
                for (var by = by0; by <= by1; by++)
                {
                    for (var bx = bx0; bx <= bx1; bx++)
                    {
                        var b = (by * OcclusionBins) + bx;
                        if (bins[b] == null)
                        {
                            bins[b] = new List<int>();
                        }

                        bins[b].Add(i);
                    }
                }
            }

            // Sample each line; keep the visible runs as merged sub-segments.
            for (var i = 0; i < s_lines.Count; i++)
            {
                var line = s_lines[i];
                var delta = line.B - line.A;
                var steps = Math.Max(1, (int)Math.Ceiling(delta.Length() / SampleStep));
                var runStart = -1;
                for (var s = 0; s < steps; s++)
                {
                    var mid = line.A + (delta * ((s + 0.5f) / steps));
                    var visible = !IsOccluded(ref basis, mid, bins, minX, minY, spanX, spanY);
                    if (visible && runStart < 0)
                    {
                        runStart = s;
                    }

                    if ((!visible || s == steps - 1) && runStart >= 0)
                    {
                        var runEnd = visible ? s + 1 : s;
                        result.Add(new Line3
                        {
                            A = line.A + (delta * (runStart / (float)steps)),
                            B = line.A + (delta * (runEnd / (float)steps))
                        });
                        runStart = -1;
                    }
                }
            }

            return result;
        }

        private static int BinIndex(float value, float min, float span)
        {
            return MathHelper.Clamp((int)((value - min) / span * OcclusionBins), 0, OcclusionBins - 1);
        }

        private static bool IsOccluded(ref ViewBasis basis, Vector3 p, List<int>[] bins, float minX, float minY, float spanX, float spanY)
        {
            float px, py, pd;
            ProjectPoint(ref basis, p.X, p.Y, p.Z, out px, out py, out pd);
            var bin = bins[(BinIndex(py, minY, spanY) * OcclusionBins) + BinIndex(px, minX, spanX)];
            if (bin == null)
            {
                return false;
            }

            for (var i = 0; i < bin.Count; i++)
            {
                var face = s_faces[bin[i]];
                if (px < face.MinX || px > face.MaxX || py < face.MinY || py > face.MaxY || face.MaxD <= pd + RayEpsilon)
                {
                    continue; // no screen overlap, or the face is not in front of the sample
                }

                // Orthographic ray from the sample toward the camera: x(t) = p - t * Dir.
                var denom = (face.Normal.X * basis.Dir.X) + (face.Normal.Y * basis.Dir.Y) + (face.Normal.Z * basis.Dir.Z);
                if (denom > -0.0001f && denom < 0.0001f)
                {
                    continue; // edge-on to the view; zero projected area
                }

                var t = (((face.Normal.X * p.X) + (face.Normal.Y * p.Y) + (face.Normal.Z * p.Z)) - face.PlaneC) / denom;
                if (t < RayEpsilon)
                {
                    continue; // behind the sample, or a face the line itself lies on
                }

                if (PointInFace(face, p - (basis.Dir * t)))
                {
                    return true;
                }
            }

            return false;
        }

        // Convex point-in-polygon on the dominant axis plane of the face normal; winding
        // agnostic (all cross products must share a sign). On-edge counts as inside so
        // seams between coplanar faces do not leak lines from behind.
        private static bool PointInFace(SurfFace face, Vector3 q)
        {
            var n = face.Normal;
            var ax = Math.Abs(n.X);
            var ay = Math.Abs(n.Y);
            var az = Math.Abs(n.Z);
            var drop = ax >= ay && ax >= az ? 0 : (ay >= az ? 1 : 2);
            float qu, qv;
            Map2(q, drop, out qu, out qv);

            var verts = face.Verts;
            var pos = false;
            var neg = false;
            for (var i = 0; i < verts.Length; i++)
            {
                float au, av, bu, bv;
                Map2(verts[i], drop, out au, out av);
                Map2(verts[(i + 1) % verts.Length], drop, out bu, out bv);
                var cross = ((bu - au) * (qv - av)) - ((bv - av) * (qu - au));
                if (cross > 0.00001f)
                {
                    pos = true;
                }
                else if (cross < -0.00001f)
                {
                    neg = true;
                }

                if (pos && neg)
                {
                    return false;
                }
            }

            return true;
        }

        private static void Map2(Vector3 p, int drop, out float u, out float v)
        {
            if (drop == 0)
            {
                u = p.Y;
                v = p.Z;
            }
            else if (drop == 1)
            {
                u = p.X;
                v = p.Z;
            }
            else
            {
                u = p.X;
                v = p.Y;
            }
        }

        // ── Feature-line extraction ──────────────────────────────────────────────────

        private static void EnsureLines(ShipVoxelModel ship)
        {
            if (ReferenceEquals(ship, s_cachedShip) && ship.Version == s_cachedVersion)
            {
                return;
            }

            s_cachedShip = ship;
            s_cachedVersion = ship.Version;
            BuildLines(ship, s_lines);
            for (var i = 0; i < ViewCount; i++)
            {
                s_viewSegments[i] = null;
                s_viewShips[i] = null;
            }
        }

        private static void BuildLines(ShipVoxelModel ship, List<Line3> lines)
        {
            lines.Clear();

            // Occupancy of every cell. Cells owned by a templated single-cell block are
            // drawn from that block's true shape instead of as voxel cubes.
            var occupied = new HashSet<long>();
            for (var i = 0; i < ship.Cells.Count; i++)
            {
                var cell = ship.Cells[i];
                occupied.Add(Pack(cell.X, cell.Y, cell.Z));
            }

            var shapedCells = new HashSet<long>();
            for (var i = 0; i < ship.Blocks.Count; i++)
            {
                var block = ship.Blocks[i];
                if (block != null && IsTemplatedShape(block.ShapeId) && IsSingleCell(block))
                {
                    shapedCells.Add(Pack(block.MinX, block.MinY, block.MinZ));
                }
            }

            // 1) Collect the surface faces of every solid. A face emitted twice — once by
            // each of two touching solids — is an interior interface and cancels (parity),
            // which is what merges adjacent blocks into one shape: a 2x1 base meeting its
            // tip drops the shared half-square, a slope resting on a cube drops the square
            // between them, and so on. Faces are keyed by their exact corner set, so only
            // truly coincident geometry cancels.
            var faces = new Dictionary<FaceKey, PendingFace>();
            for (var i = 0; i < ship.Cells.Count; i++)
            {
                var cell = ship.Cells[i];
                if (shapedCells.Contains(Pack(cell.X, cell.Y, cell.Z)))
                {
                    continue;
                }

                EmitCellFaces(cell.X, cell.Y, cell.Z, occupied, shapedCells, faces);
            }

            for (var i = 0; i < ship.Blocks.Count; i++)
            {
                var block = ship.Blocks[i];
                if (block != null && IsTemplatedShape(block.ShapeId) && IsSingleCell(block))
                {
                    EmitShapeFaces(block, faces);
                }
            }

            // 2) Reduce the surviving surface to feature lines. Every face boundary edge is
            // accumulated with the face's normal; an edge is drawn when the surface folds
            // there (bordering faces not coplanar — including the opposite-normal pinch of
            // two diagonally touching blocks) or when it borders a single face (open
            // silhouette). Edges interior to a flat sheet border two same-normal faces and
            // are skipped, which removes all internal grid lines and ramp cross-rails.
            var edges = new Dictionary<EdgeKey, EdgeInfo>();
            s_faces.Clear();
            foreach (var face in faces.Values)
            {
                if ((face.Count & 1) == 0)
                {
                    continue; // cancelled interior interface
                }

                // Surviving faces double as the occluders for the hidden-line pass.
                s_faces.Add(new SurfFace
                {
                    Verts = face.Verts,
                    Normal = face.Normal,
                    PlaneC = Vector3.Dot(face.Normal, face.Verts[0])
                });

                var verts = face.Verts;
                for (var v = 0; v < verts.Length; v++)
                {
                    AccumulateEdge(edges, verts[v], verts[(v + 1) % verts.Length], face.Normal);
                }
            }

            foreach (var edge in edges.Values)
            {
                if (edge.FaceCount == 1 || edge.Crease)
                {
                    lines.Add(new Line3 { A = edge.A, B = edge.B });
                }
            }
        }

        // A cube cell emits a face toward empty space always, toward a shaped
        // (slope/corner) cell so exactly coincident interfaces can cancel, and never
        // toward another plain solid cell (both full cubes; the interface is interior).
        private static void EmitCellFaces(int x, int y, int z, HashSet<long> occupied, HashSet<long> shapedCells, Dictionary<FaceKey, PendingFace> faces)
        {
            float x0 = x, y0 = y, z0 = z;
            float x1 = x + 1f, y1 = y + 1f, z1 = z + 1f;

            if (WantsFace(x - 1, y, z, occupied, shapedCells))
            {
                AddFace(faces, new[] { new Vector3(x0, y0, z0), new Vector3(x0, y1, z0), new Vector3(x0, y1, z1), new Vector3(x0, y0, z1) }, new Vector3(-1f, 0f, 0f));
            }

            if (WantsFace(x + 1, y, z, occupied, shapedCells))
            {
                AddFace(faces, new[] { new Vector3(x1, y0, z0), new Vector3(x1, y1, z0), new Vector3(x1, y1, z1), new Vector3(x1, y0, z1) }, new Vector3(1f, 0f, 0f));
            }

            if (WantsFace(x, y - 1, z, occupied, shapedCells))
            {
                AddFace(faces, new[] { new Vector3(x0, y0, z0), new Vector3(x1, y0, z0), new Vector3(x1, y0, z1), new Vector3(x0, y0, z1) }, new Vector3(0f, -1f, 0f));
            }

            if (WantsFace(x, y + 1, z, occupied, shapedCells))
            {
                AddFace(faces, new[] { new Vector3(x0, y1, z0), new Vector3(x1, y1, z0), new Vector3(x1, y1, z1), new Vector3(x0, y1, z1) }, new Vector3(0f, 1f, 0f));
            }

            if (WantsFace(x, y, z - 1, occupied, shapedCells))
            {
                AddFace(faces, new[] { new Vector3(x0, y0, z0), new Vector3(x1, y0, z0), new Vector3(x1, y1, z0), new Vector3(x0, y1, z0) }, new Vector3(0f, 0f, -1f));
            }

            if (WantsFace(x, y, z + 1, occupied, shapedCells))
            {
                AddFace(faces, new[] { new Vector3(x0, y0, z1), new Vector3(x1, y0, z1), new Vector3(x1, y1, z1), new Vector3(x0, y1, z1) }, new Vector3(0f, 0f, 1f));
            }
        }

        private static bool WantsFace(int nx, int ny, int nz, HashSet<long> occupied, HashSet<long> shapedCells)
        {
            var key = Pack(nx, ny, nz);
            return !occupied.Contains(key) || shapedCells.Contains(key);
        }

        // Emit a templated block's faces in world space, oriented by its Right/Up/Forward.
        // Templates are exact, so no per-shape culling or calibration is needed — the face
        // cancellation and coplanarity tests above do all the merging.
        private static void EmitShapeFaces(ShipBlockGeometry block, Dictionary<FaceKey, PendingFace> faces)
        {
            Vector3[] verts;
            int[][] faceIndices;
            Vector3[] faceNormals;
            if (!GetTemplate(block.ShapeId, out verts, out faceIndices, out faceNormals))
            {
                return;
            }

            var rightI = DirToVec(block.Right);
            var upI = DirToVec(block.Up);
            var fwdI = DirToVec(block.Forward);
            var right = new Vector3(rightI.X, rightI.Y, rightI.Z);
            var up = new Vector3(upI.X, upI.Y, upI.Z);
            var forward = new Vector3(fwdI.X, fwdI.Y, fwdI.Z);
            var center = new Vector3(block.MinX + 0.5f, block.MinY + 0.5f, block.MinZ + 0.5f);

            var world = new Vector3[verts.Length];
            for (var i = 0; i < verts.Length; i++)
            {
                world[i] = center + (verts[i].X * right) + (verts[i].Y * up) + (verts[i].Z * forward);
            }

            for (var f = 0; f < faceIndices.Length; f++)
            {
                var idx = faceIndices[f];
                var poly = new Vector3[idx.Length];
                for (var i = 0; i < idx.Length; i++)
                {
                    poly[i] = world[idx[i]];
                }

                var n = faceNormals[f];
                var normal = Vector3.Normalize((n.X * right) + (n.Y * up) + (n.Z * forward));
                AddFace(faces, poly, normal);
            }
        }

        private sealed class PendingFace
        {
            public Vector3[] Verts;
            public Vector3 Normal;
            public int Count;
        }

        private static void AddFace(Dictionary<FaceKey, PendingFace> faces, Vector3[] verts, Vector3 normal)
        {
            var key = FaceKeyOf(verts);
            PendingFace existing;
            if (faces.TryGetValue(key, out existing))
            {
                existing.Count++;
            }
            else
            {
                faces[key] = new PendingFace { Verts = verts, Normal = normal, Count = 1 };
            }
        }

        private sealed class EdgeInfo
        {
            public Vector3 A;
            public Vector3 B;
            public Vector3 Normal;
            public bool HasNormal;
            public bool Crease;
            public int FaceCount;
        }

        private static void AccumulateEdge(Dictionary<EdgeKey, EdgeInfo> edges, Vector3 a, Vector3 b, Vector3 normal)
        {
            var ka = CornerKey(a);
            var kb = CornerKey(b);
            if (ka == kb)
            {
                return;
            }

            var key = ka <= kb ? new EdgeKey(ka, kb) : new EdgeKey(kb, ka);
            EdgeInfo info;
            if (!edges.TryGetValue(key, out info))
            {
                info = new EdgeInfo { A = a, B = b };
                edges[key] = info;
            }

            info.FaceCount++;
            if (!info.HasNormal)
            {
                info.Normal = normal;
                info.HasNormal = true;
            }
            else if (Vector3.Dot(normal, info.Normal) < 0.999f)
            {
                // The surface folds across this edge: any non-coplanar pair of faces,
                // including the opposite-normal pinch of two diagonally touching solids.
                info.Crease = true;
            }
        }

        // Canonical, order-independent face identity: the sorted quantised corner keys
        // (triangles pad the fourth slot). Two coincident faces from different blocks map
        // to the same key regardless of vertex order or winding.
        private static FaceKey FaceKeyOf(Vector3[] verts)
        {
            var a = CornerKey(verts[0]);
            var b = CornerKey(verts[1]);
            var c = CornerKey(verts[2]);
            var d = verts.Length > 3 ? CornerKey(verts[3]) : long.MaxValue;
            long t;
            if (a > b) { t = a; a = b; b = t; }
            if (c > d) { t = c; c = d; d = t; }
            if (a > c) { t = a; a = c; c = t; }
            if (b > d) { t = b; b = d; d = t; }
            if (b > c) { t = b; b = c; c = t; }
            return new FaceKey(a, b, c, d);
        }

        private static long CornerKey(Vector3 p)
        {
            // All vertices sit on the half-cell lattice; x2 makes them exact integers.
            return Pack((int)Math.Round(p.X * 2f), (int)Math.Round(p.Y * 2f), (int)Math.Round(p.Z * 2f));
        }

        private struct FaceKey : IEquatable<FaceKey>
        {
            public readonly long A;
            public readonly long B;
            public readonly long C;
            public readonly long D;

            public FaceKey(long a, long b, long c, long d)
            {
                A = a;
                B = b;
                C = c;
                D = d;
            }

            public bool Equals(FaceKey other)
            {
                return A == other.A && B == other.B && C == other.C && D == other.D;
            }

            public override bool Equals(object obj)
            {
                return obj is FaceKey && Equals((FaceKey)obj);
            }

            public override int GetHashCode()
            {
                var hash = A.GetHashCode();
                hash = unchecked((hash * 397) ^ B.GetHashCode());
                hash = unchecked((hash * 397) ^ C.GetHashCode());
                hash = unchecked((hash * 397) ^ D.GetHashCode());
                return hash;
            }
        }

        private struct EdgeKey : IEquatable<EdgeKey>
        {
            public readonly long A;
            public readonly long B;

            public EdgeKey(long a, long b)
            {
                A = a;
                B = b;
            }

            public bool Equals(EdgeKey other)
            {
                return A == other.A && B == other.B;
            }

            public override bool Equals(object obj)
            {
                return obj is EdgeKey && Equals((EdgeKey)obj);
            }

            public override int GetHashCode()
            {
                return unchecked((A.GetHashCode() * 397) ^ B.GetHashCode());
            }
        }

        private static bool IsTemplatedShape(ShipShapeId shape)
        {
            return shape == ShipShapeId.Slope
                || shape == ShipShapeId.Corner
                || shape == ShipShapeId.InvertedCorner
                || shape == ShipShapeId.Slope2x1
                || shape == ShipShapeId.Slope2x1Tip;
        }

        private static bool IsSingleCell(ShipBlockGeometry block)
        {
            return block.MinX == block.MaxX && block.MinY == block.MaxY && block.MinZ == block.MaxZ;
        }

        private static Vector3I DirToVec(int direction)
        {
            return Base6Directions.GetIntVector((Base6Directions.Direction)direction);
        }

        // ── Shape templates ──────────────────────────────────────────────────────────
        // Local coords: X = +Right, Y = +Up, Z = +Forward; the cell spans [-0.5, 0.5].
        // Orientation conventions are taken from CubeBlocks_Armor.sbc mount points:
        //   Slope      — full Front + Bottom faces; the slope ascends toward +Forward.
        //   Slope2Tip  — full Bottom, lower-half Front; the thin end of the 2:1 ramp.
        //   Slope2Base — full Bottom + Front, lower-half Back; the tall end. Base in
        //                front of tip (same orientation) continues the same plane, so
        //                their seam cancels and a ramp run merges into one sheet.
        //   Corner     — tetrahedron; right angle where Front, Right and Bottom meet.
        //   InvCorner  — cube minus that tetrahedron (full Back, Left, Top faces).
        private static bool GetTemplate(ShipShapeId shape, out Vector3[] verts, out int[][] faceIndices, out Vector3[] faceNormals)
        {
            switch (shape)
            {
                case ShipShapeId.Slope:
                    verts = SlopeVerts;
                    faceIndices = SlopeFaces;
                    faceNormals = SlopeNormals;
                    return true;
                case ShipShapeId.Slope2x1:
                    verts = Slope2BaseVerts;
                    faceIndices = Slope2BaseFaces;
                    faceNormals = Slope2BaseNormals;
                    return true;
                case ShipShapeId.Slope2x1Tip:
                    verts = Slope2TipVerts;
                    faceIndices = Slope2TipFaces;
                    faceNormals = Slope2TipNormals;
                    return true;
                case ShipShapeId.Corner:
                    verts = CornerVerts;
                    faceIndices = CornerFaces;
                    faceNormals = CornerNormals;
                    return true;
                case ShipShapeId.InvertedCorner:
                    verts = InvCornerVerts;
                    faceIndices = InvCornerFaces;
                    faceNormals = InvCornerNormals;
                    return true;
                default:
                    verts = null;
                    faceIndices = null;
                    faceNormals = null;
                    return false;
            }
        }

        // 1x1 slope: solid Front + Bottom; sloped quad from the back-bottom edge up to the
        // front-top edge.
        private static readonly Vector3[] SlopeVerts =
        {
            new Vector3(-0.5f, -0.5f, -0.5f), // 0 back-bottom-left
            new Vector3(0.5f, -0.5f, -0.5f),  // 1 back-bottom-right
            new Vector3(0.5f, -0.5f, 0.5f),   // 2 front-bottom-right
            new Vector3(-0.5f, -0.5f, 0.5f),  // 3 front-bottom-left
            new Vector3(-0.5f, 0.5f, 0.5f),   // 4 front-top-left
            new Vector3(0.5f, 0.5f, 0.5f),    // 5 front-top-right
        };

        private static readonly int[][] SlopeFaces =
        {
            new[] { 0, 1, 2, 3 }, // bottom
            new[] { 3, 2, 5, 4 }, // front
            new[] { 0, 1, 5, 4 }, // sloped
            new[] { 0, 3, 4 },    // left triangle
            new[] { 1, 2, 5 },    // right triangle
        };

        private static readonly Vector3[] SlopeNormals =
        {
            new Vector3(0f, -1f, 0f),
            new Vector3(0f, 0f, 1f),
            new Vector3(0f, 1f, -1f),
            new Vector3(-1f, 0f, 0f),
            new Vector3(1f, 0f, 0f),
        };

        // 2x1 tip: the thin end. Solid Bottom, lower-half Front; sloped quad from the
        // back-bottom edge up to the front-mid edge.
        private static readonly Vector3[] Slope2TipVerts =
        {
            new Vector3(-0.5f, -0.5f, -0.5f), // 0 back-bottom-left
            new Vector3(0.5f, -0.5f, -0.5f),  // 1 back-bottom-right
            new Vector3(0.5f, -0.5f, 0.5f),   // 2 front-bottom-right
            new Vector3(-0.5f, -0.5f, 0.5f),  // 3 front-bottom-left
            new Vector3(-0.5f, 0.0f, 0.5f),   // 4 front-mid-left
            new Vector3(0.5f, 0.0f, 0.5f),    // 5 front-mid-right
        };

        private static readonly int[][] Slope2TipFaces =
        {
            new[] { 0, 1, 2, 3 }, // bottom
            new[] { 3, 2, 5, 4 }, // front (lower half)
            new[] { 0, 1, 5, 4 }, // sloped
            new[] { 0, 3, 4 },    // left triangle
            new[] { 1, 2, 5 },    // right triangle
        };

        private static readonly Vector3[] Slope2TipNormals =
        {
            new Vector3(0f, -1f, 0f),
            new Vector3(0f, 0f, 1f),
            new Vector3(0f, 2f, -1f),
            new Vector3(-1f, 0f, 0f),
            new Vector3(1f, 0f, 0f),
        };

        // 2x1 base: the tall end. Solid Bottom + Front, lower-half Back; sloped quad from
        // the back-mid edge up to the front-top edge (continues the tip's plane).
        private static readonly Vector3[] Slope2BaseVerts =
        {
            new Vector3(-0.5f, -0.5f, -0.5f), // 0 back-bottom-left
            new Vector3(0.5f, -0.5f, -0.5f),  // 1 back-bottom-right
            new Vector3(0.5f, -0.5f, 0.5f),   // 2 front-bottom-right
            new Vector3(-0.5f, -0.5f, 0.5f),  // 3 front-bottom-left
            new Vector3(-0.5f, 0.5f, 0.5f),   // 4 front-top-left
            new Vector3(0.5f, 0.5f, 0.5f),    // 5 front-top-right
            new Vector3(-0.5f, 0.0f, -0.5f),  // 6 back-mid-left
            new Vector3(0.5f, 0.0f, -0.5f),   // 7 back-mid-right
        };

        private static readonly int[][] Slope2BaseFaces =
        {
            new[] { 0, 1, 2, 3 }, // bottom
            new[] { 3, 2, 5, 4 }, // front (full)
            new[] { 0, 1, 7, 6 }, // back (lower half)
            new[] { 6, 7, 5, 4 }, // sloped
            new[] { 0, 3, 4, 6 }, // left trapezoid
            new[] { 1, 2, 5, 7 }, // right trapezoid
        };

        private static readonly Vector3[] Slope2BaseNormals =
        {
            new Vector3(0f, -1f, 0f),
            new Vector3(0f, 0f, 1f),
            new Vector3(0f, 0f, -1f),
            new Vector3(0f, 2f, -1f),
            new Vector3(-1f, 0f, 0f),
            new Vector3(1f, 0f, 0f),
        };

        // Corner tetrahedron: right angle at the front-bottom-right corner (where the
        // Front, Right and Bottom mount faces meet); cut triangle facing up-back-left.
        private static readonly Vector3[] CornerVerts =
        {
            new Vector3(0.5f, -0.5f, 0.5f),   // 0 right-angle corner (front-bottom-right)
            new Vector3(-0.5f, -0.5f, 0.5f),  // 1 front-bottom-left
            new Vector3(0.5f, 0.5f, 0.5f),    // 2 front-top-right
            new Vector3(0.5f, -0.5f, -0.5f),  // 3 back-bottom-right
        };

        private static readonly int[][] CornerFaces =
        {
            new[] { 0, 1, 2 }, // front triangle
            new[] { 0, 1, 3 }, // bottom triangle
            new[] { 0, 2, 3 }, // right triangle
            new[] { 1, 2, 3 }, // cut triangle
        };

        private static readonly Vector3[] CornerNormals =
        {
            new Vector3(0f, 0f, 1f),
            new Vector3(0f, -1f, 0f),
            new Vector3(1f, 0f, 0f),
            new Vector3(-1f, 1f, -1f),
        };

        // Inverted corner: full cube minus that same tetrahedron. Full Back, Left and Top
        // faces; half faces on Front, Right and Bottom; cut triangle facing down-front-right.
        private static readonly Vector3[] InvCornerVerts =
        {
            new Vector3(-0.5f, -0.5f, -0.5f), // 0 back-bottom-left
            new Vector3(0.5f, -0.5f, -0.5f),  // 1 back-bottom-right
            new Vector3(0.5f, 0.5f, -0.5f),   // 2 back-top-right
            new Vector3(-0.5f, 0.5f, -0.5f),  // 3 back-top-left
            new Vector3(-0.5f, -0.5f, 0.5f),  // 4 front-bottom-left
            new Vector3(0.5f, 0.5f, 0.5f),    // 5 front-top-right
            new Vector3(-0.5f, 0.5f, 0.5f),   // 6 front-top-left
        };

        private static readonly int[][] InvCornerFaces =
        {
            new[] { 0, 1, 2, 3 }, // back
            new[] { 0, 4, 6, 3 }, // left
            new[] { 3, 2, 5, 6 }, // top
            new[] { 0, 1, 4 },    // bottom triangle
            new[] { 1, 2, 5 },    // right triangle
            new[] { 4, 5, 6 },    // front triangle
            new[] { 1, 5, 4 },    // cut triangle
        };

        private static readonly Vector3[] InvCornerNormals =
        {
            new Vector3(0f, 0f, -1f),
            new Vector3(-1f, 0f, 0f),
            new Vector3(0f, 1f, 0f),
            new Vector3(0f, -1f, 0f),
            new Vector3(1f, 0f, 0f),
            new Vector3(0f, 0f, 1f),
            new Vector3(1f, -1f, 1f),
        };

        // ── Overlays ─────────────────────────────────────────────────────────────────

        // Specialty systems drawn as category glyphs at their block centre, each on a
        // small dark backing chip so it stays readable over the line work.
        private static void DrawDevices(MySpriteDrawFrame frame, Vector2 origin, UiContext context, ShipVoxelModel ship, ref ViewBasis basis, float scale, float modelCenterX, float modelCenterY, float centerX, float centerY)
        {
            if (ship.Devices == null)
            {
                return;
            }

            var bg = context.Theme.Background1;
            var backing = new Color(bg.R, bg.G, bg.B, (byte)220);
            for (var i = 0; i < ship.Devices.Count; i++)
            {
                var device = ship.Devices[i];
                float dx, dy, depth;
                ProjectPoint(ref basis, device.X + 0.5f, device.Y + 0.5f, device.Z + 0.5f, out dx, out dy, out depth);
                var x = centerX + ((dx - modelCenterX) * scale);
                var y = centerY - ((dy - modelCenterY) * scale);

                string sprite;
                Color color;
                float sizeFactor;
                GetDeviceIcon(device.Category, out sprite, out color, out sizeFactor);
                var side = MathHelper.Clamp(scale * sizeFactor, 4f, 14f);
                UiRenderer.DrawImage(frame, origin, new UiRect(x - (side * 0.65f), y - (side * 0.65f), side * 1.3f, side * 1.3f), "SquareSimple", backing);
                UiRenderer.DrawImage(frame, origin, new UiRect(x - (side * 0.5f), y - (side * 0.5f), side, side), sprite, color);
            }
        }

        // Maps a category to a stock LCD sprite + a fixed palette colour, so systems are
        // recognisable at a glance regardless of theme. Conveyors are numerous, so they
        // draw smaller and dimmer than the primary systems.
        private static void GetDeviceIcon(ShipDeviceCategory category, out string sprite, out Color color, out float sizeFactor)
        {
            sizeFactor = 0.9f;
            switch (category)
            {
                case ShipDeviceCategory.Thruster: sprite = "Triangle"; color = new Color(255, 150, 50); return;      // orange triangle
                case ShipDeviceCategory.Reactor: sprite = "Circle"; color = new Color(255, 220, 80); return;         // yellow disc
                case ShipDeviceCategory.Battery: sprite = "IconEnergy"; color = new Color(110, 230, 120); return;    // green bolt
                case ShipDeviceCategory.Tank: sprite = "IconHydrogen"; color = new Color(90, 200, 255); return;      // cyan H2
                case ShipDeviceCategory.Controller: sprite = "SemiCircle"; color = new Color(255, 255, 255); return; // white dome
                case ShipDeviceCategory.Weapon: sprite = "Danger"; color = new Color(255, 80, 80); return;           // red warning
                case ShipDeviceCategory.Cargo: sprite = "SquareSimple"; color = new Color(210, 180, 130); return;    // tan crate
                case ShipDeviceCategory.Gyro: sprite = "CircleHollow"; color = new Color(150, 150, 170); return;     // grey ring
                case ShipDeviceCategory.Conveyor: sprite = "SquareHollow"; color = new Color(110, 125, 150); sizeFactor = 0.55f; return; // small slate square
                case ShipDeviceCategory.Connector: sprite = "SquareTapered"; color = new Color(230, 120, 230); return; // magenta clamp
                case ShipDeviceCategory.Antenna: sprite = "Arrow"; color = new Color(120, 220, 220); return;         // teal arrow
                case ShipDeviceCategory.Medical: sprite = "Cross"; color = new Color(240, 240, 240); return;         // white cross
                case ShipDeviceCategory.Production: sprite = "RightTriangle"; color = new Color(190, 140, 255); return; // violet wedge
                default: sprite = "SquareSimple"; color = new Color(120, 120, 120); sizeFactor = 0.6f; return;
            }
        }

        private static void DrawMarkers(MySpriteDrawFrame frame, Vector2 origin, UiContext context, SecurityScreenModel model, SecurityEntity selected, ref ViewBasis basis, float scale, float modelCenterX, float modelCenterY, float centerX, float centerY)
        {
            if (model.Markers == null)
            {
                return;
            }

            for (var i = 0; i < model.Markers.Count; i++)
            {
                var marker = model.Markers[i];
                float mx, my, depth;
                ProjectPoint(ref basis, marker.X + 0.5f, marker.Y + 0.5f, marker.Z + 0.5f, out mx, out my, out depth);
                var x = centerX + ((mx - modelCenterX) * scale);
                var y = centerY - ((my - modelCenterY) * scale);
                var side = MathHelper.Clamp(scale * 1.1f, 6f, 18f);
                var bounds = new UiRect(x - (side * 0.5f), y - (side * 0.5f), side, side);
                UiRenderer.DrawOutline(frame, origin, bounds, GetMarkerColor(marker.Kind, context), Math.Max(1f, side * 0.14f));

                if (selected != null && string.Equals(selected.Id, marker.Id, StringComparison.OrdinalIgnoreCase))
                {
                    UiRenderer.DrawOutline(frame, origin, bounds.Deflate(new UiThickness(Math.Max(1f, side * 0.18f))), context.Theme.ForegroundPrimary, 1f);
                }
            }
        }

        private static Color ShadeByDepth(UiContext context, float t)
        {
            t = MathHelper.Clamp(t, 0f, 1f);
            var color = Color.Lerp(context.Theme.ForegroundDim, context.Theme.Accent, t);
            return new Color(color.R, color.G, color.B, (byte)(150 + (int)(105 * t)));
        }

        private static Color GetMarkerColor(SecurityFeatureKind kind, UiContext context)
        {
            switch (kind)
            {
                case SecurityFeatureKind.Access: return context.Theme.Warning;
                case SecurityFeatureKind.Airlocks: return context.Theme.Accent;
                case SecurityFeatureKind.Turrets: return context.Theme.Danger;
                case SecurityFeatureKind.Sensors: return context.Theme.Success;
                case SecurityFeatureKind.Cameras: return context.Theme.ForegroundPrimary;
                case SecurityFeatureKind.Alarms: return context.Theme.AccentSoft;
                default: return context.Theme.Border;
            }
        }

        private static void DrawEmpty(MySpriteDrawFrame frame, Vector2 origin, UiRect rect, UiContext context, string text)
        {
            UiRenderer.DrawLabel(frame, origin, new UiLabel
            {
                Bounds = rect,
                Padding = new UiThickness(0f),
                Text = text,
                Font = "Monospace",
                Scale = Math.Max(context.Theme.TextSm, 0.34f),
                Color = context.Theme.ForegroundMuted,
                Alignment = TextAlignment.CENTER,
                VerticalAlignment = UiVerticalAlignment.Center
            });
        }

        // 21 bits per axis (two's-complement masked), matching ShipGeometryBuilder's packing.
        private static long Pack(int x, int y, int z)
        {
            var packedX = ((long)(x & 0x1FFFFF)) << 42;
            var packedY = ((long)(y & 0x1FFFFF)) << 21;
            var packedZ = (long)(z & 0x1FFFFF);
            return packedX | packedY | packedZ;
        }
    }
}
