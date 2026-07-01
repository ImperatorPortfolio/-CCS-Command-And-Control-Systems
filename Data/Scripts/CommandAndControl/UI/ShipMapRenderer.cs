using System;
using System.Collections.Generic;
using VRage.Game.GUI.TextPanel;
using VRageMath;

namespace AGS
{
    // Isometric 3D wireframe of the construct hull, in the blueprint style. Rather than
    // drawing every cell face (which looks like a grid of squares), the hull is reduced to
    // its FEATURE edges — silhouette and creases only: an edge is drawn where the surface
    // actually folds, and the internal lines of a flat face are dropped. Edges are projected
    // from the selected view direction and depth-shaded. View buttons orbit the camera
    // (yaw/pitch); zoom scales. Specialty (non-armour) systems are drawn as category icons.
    //
    // Armour shape (slope/corner) is currently treated as a voxel for the outline; drawing
    // slopes as true oriented diagonals from their Forward/Up is the remaining piece.
    public static class ShipMapRenderer
    {
        // ── Orientation calibration knob ─────────────────────────────────────────────
        // Bump this 0..9 in game until the 2x1 slopes sit correctly; no vertex editing.
        //   0 = none (same as the 1x1 slope)   1 = yaw +90   2 = yaw 180   3 = yaw -90
        //   4 = pitch +90                       5 = pitch -90
        //   6 = pitch +90 then yaw 180          7 = pitch +90 then yaw +90
        //   8 = pitch +90 then yaw -90          9 = pitch -90 then yaw 180
        private const int Slope2x1Calibration = 6;

        private struct Corner
        {
            public float X;
            public float Y;
            public float Z;
        }

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

            float yaw, pitch;
            GetViewAngles(model.View, out yaw, out pitch);

            // 1) Occupancy of every cell (cubes AND slopes) — used so cube feature edges
            // still cull against a slope cell as if it were full.
            var occupied = new HashSet<long>();
            var cellOwner = new Dictionary<long, int>();
            for (var i = 0; i < ship.Cells.Count; i++)
            {
                var cell = ship.Cells[i];
                var key = Pack(cell.X, cell.Y, cell.Z);
                occupied.Add(key);
                cellOwner[key] = cell.OwnerIndex;
            }

            // Single-cell sloped blocks (slope/corner/inverted corner) are drawn as their
            // true oriented triangle geometry instead of voxel cubes; record their cells so
            // the voxel pass skips them.
            var shapedCells = new HashSet<long>();
            var shapeEdges = new List<ShapeEdge>();
            for (var i = 0; i < ship.Blocks.Count; i++)
            {
                var block = ship.Blocks[i];
                if (block == null || !IsTemplatedShape(block.ShapeId) || !IsSingleCell(block))
                {
                    continue;
                }

                shapedCells.Add(Pack(block.MinX, block.MinY, block.MinZ));
                CollectShapeEdges(block, occupied, cellOwner, ship.Blocks, shapeEdges);
            }

            var cornerIndex = new Dictionary<long, int>();
            var corners = new List<Corner>();
            var edgeSet = new HashSet<long>();
            var edges = new List<int>(); // flattened (a,b) corner-index pairs

            for (var i = 0; i < ship.Cells.Count; i++)
            {
                var cell = ship.Cells[i];
                if (shapedCells.Contains(Pack(cell.X, cell.Y, cell.Z)))
                {
                    continue; // drawn as oriented geometry below
                }

                AddCellEdges(cell.X, cell.Y, cell.Z, occupied, cornerIndex, corners, edgeSet, edges);
            }

            if (edges.Count == 0 && shapeEdges.Count == 0)
            {
                DrawEmpty(frame, origin, rect, context, "Construct hull not visible");
                return;
            }

            // 2) Project every corner once; track screen-space and depth bounds.
            var px = new float[corners.Count];
            var py = new float[corners.Count];
            var pd = new float[corners.Count];
            float minX = float.MaxValue, minY = float.MaxValue, maxX = float.MinValue, maxY = float.MinValue;
            float minD = float.MaxValue, maxD = float.MinValue;
            for (var i = 0; i < corners.Count; i++)
            {
                float x, y, depth;
                Project(corners[i].X, corners[i].Y, corners[i].Z, yaw, pitch, out x, out y, out depth);
                px[i] = x;
                py[i] = y;
                pd[i] = depth;
                if (x < minX) minX = x;
                if (x > maxX) maxX = x;
                if (y < minY) minY = y;
                if (y > maxY) maxY = y;
                if (depth < minD) minD = depth;
                if (depth > maxD) maxD = depth;
            }

            // 2b) Project sloped-block edge endpoints into the same screen/depth bounds.
            for (var i = 0; i < shapeEdges.Count; i++)
            {
                var se = shapeEdges[i];
                Project(se.A.X, se.A.Y, se.A.Z, yaw, pitch, out se.SAx, out se.SAy, out se.SAd);
                Project(se.B.X, se.B.Y, se.B.Z, yaw, pitch, out se.SBx, out se.SBy, out se.SBd);
                minX = Math.Min(minX, Math.Min(se.SAx, se.SBx));
                maxX = Math.Max(maxX, Math.Max(se.SAx, se.SBx));
                minY = Math.Min(minY, Math.Min(se.SAy, se.SBy));
                maxY = Math.Max(maxY, Math.Max(se.SAy, se.SBy));
                minD = Math.Min(minD, Math.Min(se.SAd, se.SBd));
                maxD = Math.Max(maxD, Math.Max(se.SAd, se.SBd));
            }

            // 3) Fit the projected model into the plot rect, scaled by zoom.
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

            // 4) Draw the wireframe, shading near edges brighter than far ones.
            var depthRange = Math.Max(0.001f, maxD - minD);
            for (var e = 0; e + 1 < edges.Count; e += 2)
            {
                var a = edges[e];
                var b = edges[e + 1];
                var pa = new Vector2(centerX + ((px[a] - modelCenterX) * scale), centerY - ((py[a] - modelCenterY) * scale));
                var pb = new Vector2(centerX + ((px[b] - modelCenterX) * scale), centerY - ((py[b] - modelCenterY) * scale));
                var t = ((pd[a] + pd[b]) * 0.5f - minD) / depthRange; // 0 = farthest, 1 = nearest
                UiRenderer.DrawLine(frame, origin, pa, pb, ShadeByDepth(context, t), 1f);
            }

            // 4b) Draw the oriented slope/corner geometry in the same fit transform.
            // DEBUG: only the 2x1 slopes are drawn in cyan; every other shape uses normal
            // depth shading. If no cyan appears, the 2x1 blocks aren't being detected as 2x1
            // (subtype name / single-cell mismatch) and are falling back to voxel cubes.
            var debug2x1Color = new Color(90, 210, 255);
            for (var i = 0; i < shapeEdges.Count; i++)
            {
                var se = shapeEdges[i];
                var pa = new Vector2(centerX + ((se.SAx - modelCenterX) * scale), centerY - ((se.SAy - modelCenterY) * scale));
                var pb = new Vector2(centerX + ((se.SBx - modelCenterX) * scale), centerY - ((se.SBy - modelCenterY) * scale));
                Color color;
                if (se.Is2x1)
                {
                    color = debug2x1Color;
                }
                else
                {
                    var t = ((se.SAd + se.SBd) * 0.5f - minD) / depthRange;
                    color = ShadeByDepth(context, t);
                }

                UiRenderer.DrawLine(frame, origin, pa, pb, color, 1f);
            }

            // 5) Specialty-block icons, then security feature markers, over the wireframe.
            DrawDevices(frame, origin, context, ship, yaw, pitch, scale, modelCenterX, modelCenterY, centerX, centerY);
            DrawMarkers(frame, origin, context, model, selected, yaw, pitch, scale, modelCenterX, modelCenterY, centerX, centerY);

            // 6) View label.
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

        // The six directional views are true orthographic 2D projections (camera looks
        // straight down an axis); Iso/IsoRear are angled axonometric "perspective" views.
        private static void GetViewAngles(ShipView view, out float yaw, out float pitch)
        {
            switch (view)
            {
                case ShipView.Bottom: yaw = 0f; pitch = MathHelper.ToRadians(-90f); break;
                case ShipView.Front: yaw = 0f; pitch = 0f; break;
                case ShipView.Back: yaw = MathHelper.ToRadians(180f); pitch = 0f; break;
                case ShipView.Left: yaw = MathHelper.ToRadians(-90f); pitch = 0f; break;
                case ShipView.Right: yaw = MathHelper.ToRadians(90f); pitch = 0f; break;
                case ShipView.Iso: yaw = MathHelper.ToRadians(35f); pitch = MathHelper.ToRadians(35f); break;
                case ShipView.IsoRear: yaw = MathHelper.ToRadians(215f); pitch = MathHelper.ToRadians(35f); break;
                default: yaw = 0f; pitch = MathHelper.ToRadians(90f); break; // Top: orthographic top-down
            }
        }

        // Rotate ship-local (yaw about up, then pitch about right) into camera space.
        // Returns screen x/y (model units, pre-fit) and a depth where larger = nearer.
        private static void Project(float x, float y, float z, float yaw, float pitch, out float sx, out float sy, out float depth)
        {
            var cy = (float)Math.Cos(yaw);
            var syaw = (float)Math.Sin(yaw);
            var cp = (float)Math.Cos(pitch);
            var sp = (float)Math.Sin(pitch);

            var camX = (cy * x) + (syaw * z);
            var camY = (sp * syaw * x) + (cp * y) - (sp * cy * z);
            var camZ = (-cp * syaw * x) + (sp * y) + (cp * cy * z);

            sx = camX;
            sy = camY;
            depth = -camZ;
        }

        // Emit only this cell's FEATURE edges — silhouette and creases — not every face
        // boundary. An edge is shared by up to four cells in the plane perpendicular to it;
        // the occupancy of those four decides whether the surface actually folds there.
        // This yields a clean outline instead of a grid of every cell face.
        private static void AddCellEdges(int x, int y, int z, HashSet<long> occupied, Dictionary<long, int> cornerIndex, List<Corner> corners, HashSet<long> edgeSet, List<int> edges)
        {
            // The cell's four X-axis edges, four Y-axis edges, four Z-axis edges.
            TryEdge(0, x, y, z, occupied, cornerIndex, corners, edgeSet, edges);
            TryEdge(0, x, y + 1, z, occupied, cornerIndex, corners, edgeSet, edges);
            TryEdge(0, x, y, z + 1, occupied, cornerIndex, corners, edgeSet, edges);
            TryEdge(0, x, y + 1, z + 1, occupied, cornerIndex, corners, edgeSet, edges);

            TryEdge(1, x, y, z, occupied, cornerIndex, corners, edgeSet, edges);
            TryEdge(1, x + 1, y, z, occupied, cornerIndex, corners, edgeSet, edges);
            TryEdge(1, x, y, z + 1, occupied, cornerIndex, corners, edgeSet, edges);
            TryEdge(1, x + 1, y, z + 1, occupied, cornerIndex, corners, edgeSet, edges);

            TryEdge(2, x, y, z, occupied, cornerIndex, corners, edgeSet, edges);
            TryEdge(2, x + 1, y, z, occupied, cornerIndex, corners, edgeSet, edges);
            TryEdge(2, x, y + 1, z, occupied, cornerIndex, corners, edgeSet, edges);
            TryEdge(2, x + 1, y + 1, z, occupied, cornerIndex, corners, edgeSet, edges);
        }

        private static void TryEdge(int axis, int cx, int cy, int cz, HashSet<long> occupied, Dictionary<long, int> cornerIndex, List<Corner> corners, HashSet<long> edgeSet, List<int> edges)
        {
            if (!IsFeatureEdge(axis, cx, cy, cz, occupied))
            {
                return;
            }

            var bx = cx;
            var by = cy;
            var bz = cz;
            if (axis == 0) bx = cx + 1;
            else if (axis == 1) by = cy + 1;
            else bz = cz + 1;

            var a = GetCorner(cornerIndex, corners, cx, cy, cz);
            var b = GetCorner(cornerIndex, corners, bx, by, bz);
            AddEdge(edgeSet, edges, a, b);
        }

        // The four cells sharing the edge live in the plane perpendicular to the edge axis,
        // indexed (-1,-1),(-1,0),(0,-1),(0,0) on the two perpendicular axes.
        private static bool IsFeatureEdge(int axis, int cx, int cy, int cz, HashSet<long> occupied)
        {
            bool o0, o1, o2, o3;
            if (axis == 0) // edge along X; perpendicular plane is Y,Z
            {
                o0 = occupied.Contains(Pack(cx, cy - 1, cz - 1));
                o1 = occupied.Contains(Pack(cx, cy - 1, cz));
                o2 = occupied.Contains(Pack(cx, cy, cz - 1));
                o3 = occupied.Contains(Pack(cx, cy, cz));
            }
            else if (axis == 1) // edge along Y; perpendicular plane is X,Z
            {
                o0 = occupied.Contains(Pack(cx - 1, cy, cz - 1));
                o1 = occupied.Contains(Pack(cx - 1, cy, cz));
                o2 = occupied.Contains(Pack(cx, cy, cz - 1));
                o3 = occupied.Contains(Pack(cx, cy, cz));
            }
            else // edge along Z; perpendicular plane is X,Y
            {
                o0 = occupied.Contains(Pack(cx - 1, cy - 1, cz));
                o1 = occupied.Contains(Pack(cx - 1, cy, cz));
                o2 = occupied.Contains(Pack(cx, cy - 1, cz));
                o3 = occupied.Contains(Pack(cx, cy, cz));
            }

            return IsCrease(o0, o1, o2, o3);
        }

        // Given the four cells around an edge: 0 or 4 occupied means the edge is fully
        // outside or fully inside the hull (skip). One or three occupied is a convex or
        // concave 90° fold (draw). Exactly two occupied is a flat face the edge runs through
        // when they are adjacent (skip — removes the internal grid lines), or a pinch when
        // they are diagonal (draw).
        private static bool IsCrease(bool a, bool b, bool c, bool d)
        {
            var count = (a ? 1 : 0) + (b ? 1 : 0) + (c ? 1 : 0) + (d ? 1 : 0);
            if (count == 0 || count == 4)
            {
                return false;
            }

            if (count == 2)
            {
                return (a && d) || (b && c); // diagonal pair = pinch; adjacent pair = flat
            }

            return true;
        }

        private sealed class ShapeEdge
        {
            public Vector3 A;
            public Vector3 B;
            public float SAx;
            public float SAy;
            public float SAd;
            public float SBx;
            public float SBy;
            public float SBd;
            public bool Is2x1;
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

        // Emit a block's shape outline as world-space edges, oriented by its Forward/Up/Right,
        // culling any edge whose both bordering faces abut a solid neighbour — so packed
        // slopes show only their exposed sloped surfaces, not a full wedge mesh.
        // Template coords are centred in [-0.5,0.5] with X=right, Y=up, Z=forward. Each edge
        // carries the two face codes it borders (see FaceOffset); code 6 = always exposed
        // (the hypotenuse/cut). If a shape faces the wrong way in game, flip the sign of the
        // relevant axis in its template — the transform here is otherwise convention-agnostic.
        private static void CollectShapeEdges(ShipBlockGeometry block, HashSet<long> occupied, Dictionary<long, int> cellOwner, List<ShipBlockGeometry> blocks, List<ShapeEdge> output)
        {
            Vector3[] verts;
            int[] edgePairs;
            int[] edgeFaces;
            if (!GetTemplate(block.ShapeId, out verts, out edgePairs, out edgeFaces))
            {
                return;
            }

            var rightI = DirToVec(block.Right);
            var upI = DirToVec(block.Up);
            var fwdI = DirToVec(block.Forward);

            // Slope convention (shared by the 1x1 slope and the 2x1 base/tip, which use the
            // same SE Front/Bottom geometry and MirroringY=Z/MirroringZ=Y): -90° pitch about
            // Right (up<--forward, forward<-up).
            var rotRightI = rightI;
            var rotUpI = new Vector3I(-fwdI.X, -fwdI.Y, -fwdI.Z);
            var rotFwdI = upI;

            // Extra per-shape calibration (a single 0-5 setting) so an orientation can be
            // dialed in by eye without editing vertices.
            var is2x1 = block.ShapeId == ShipShapeId.Slope2x1 || block.ShapeId == ShipShapeId.Slope2x1Tip;
            if (is2x1)
            {
                ApplyCalibration(Slope2x1Calibration, ref rotRightI, ref rotUpI, ref rotFwdI);
            }

            var right = new Vector3(rotRightI.X, rotRightI.Y, rotRightI.Z);
            var up = new Vector3(rotUpI.X, rotUpI.Y, rotUpI.Z);
            var forward = new Vector3(rotFwdI.X, rotFwdI.Y, rotFwdI.Z);
            var center = new Vector3(block.MinX + 0.5f, block.MinY + 0.5f, block.MinZ + 0.5f);

            var world = new Vector3[verts.Length];
            for (var i = 0; i < verts.Length; i++)
            {
                world[i] = center + (verts[i].X * right) + (verts[i].Y * up) + (verts[i].Z * forward);
            }

            for (var e = 0; e + 1 < edgePairs.Length; e += 2)
            {
                var faceA = edgeFaces[e];
                var faceB = edgeFaces[e + 1];
                bool draw;

                if (faceA == 6 || faceB == 6)
                {
                    var ortho = faceA == 6 ? faceB : faceA;
                    var off = FaceOffset(ortho, rotRightI, rotUpI, rotFwdI);
                    var neighborKey = Pack(block.MinX + off.X, block.MinY + off.Y, block.MinZ + off.Z);
                    if (ortho == 0 || ortho == 1)
                    {
                        // Diagonal (triangle-side) edge — the line that makes a slope read as
                        // a slope. Keep it against cubes, space, and differently-angled
                        // slopes; hide it only where an identically-oriented slope continues
                        // the same flat surface (a smooth ramp seam).
                        draw = !IsSameOrientationNeighbor(neighborKey, block, cellOwner, blocks);
                    }
                    else
                    {
                        // Edge along a full base/back face — hide it when that face is buried
                        // against any solid (e.g. a slope's back against the hull core).
                        draw = FaceExposed(ortho, block, rotRightI, rotUpI, rotFwdI, occupied);
                    }
                }
                else
                {
                    // Axis-aligned edge: keep it if either bordering face is open to space.
                    draw = FaceExposed(faceA, block, rotRightI, rotUpI, rotFwdI, occupied)
                        || FaceExposed(faceB, block, rotRightI, rotUpI, rotFwdI, occupied);
                }

                if (draw)
                {
                    output.Add(new ShapeEdge { A = world[edgePairs[e]], B = world[edgePairs[e + 1]], Is2x1 = is2x1 });
                }
            }
        }

        // True when the neighbouring cell belongs to a block of the same shape AND the same
        // orientation — a continuation of this exact sloped surface, whose shared seam should
        // not be drawn. (Differently-angled neighbours and cubes return false, so their
        // creases stay.)
        private static bool IsSameOrientationNeighbor(long neighborKey, ShipBlockGeometry block, Dictionary<long, int> cellOwner, List<ShipBlockGeometry> blocks)
        {
            int ownerIndex;
            if (!cellOwner.TryGetValue(neighborKey, out ownerIndex) || ownerIndex < 0 || ownerIndex >= blocks.Count)
            {
                return false;
            }

            var neighbor = blocks[ownerIndex];
            return neighbor != null
                && neighbor.ShapeId == block.ShapeId
                && neighbor.Forward == block.Forward
                && neighbor.Up == block.Up;
        }

        // A face is exposed if it is the hypotenuse/cut (code 6) or the neighbouring cell in
        // that face's grid direction is empty.
        private static bool FaceExposed(int faceCode, ShipBlockGeometry block, Vector3I right, Vector3I up, Vector3I forward, HashSet<long> occupied)
        {
            if (faceCode == 6)
            {
                return true;
            }

            var offset = FaceOffset(faceCode, right, up, forward);
            return !occupied.Contains(Pack(block.MinX + offset.X, block.MinY + offset.Y, block.MinZ + offset.Z));
        }

        // Applies an extra rotation (selected by setting 0..9) to the already-pitched block
        // frame, so orientation can be tuned by eye via the Slope2x1Calibration knob.
        private static void ApplyCalibration(int setting, ref Vector3I right, ref Vector3I up, ref Vector3I forward)
        {
            switch (setting)
            {
                case 1: Yaw(ref right, ref up, ref forward, 1); break;
                case 2: Yaw(ref right, ref up, ref forward, 2); break;
                case 3: Yaw(ref right, ref up, ref forward, 3); break; // -90
                case 4: Pitch(ref right, ref up, ref forward, 1); break;
                case 5: Pitch(ref right, ref up, ref forward, 3); break; // -90
                case 6: Pitch(ref right, ref up, ref forward, 1); Yaw(ref right, ref up, ref forward, 2); break;
                case 7: Pitch(ref right, ref up, ref forward, 1); Yaw(ref right, ref up, ref forward, 1); break;
                case 8: Pitch(ref right, ref up, ref forward, 1); Yaw(ref right, ref up, ref forward, 3); break;
                case 9: Pitch(ref right, ref up, ref forward, 3); Yaw(ref right, ref up, ref forward, 2); break;
                default: break; // 0: no extra rotation
            }
        }

        // +90° yaw about Up, applied 'quarters' times: (right, forward) -> (-forward, right).
        private static void Yaw(ref Vector3I right, ref Vector3I up, ref Vector3I forward, int quarters)
        {
            for (var i = 0; i < quarters; i++)
            {
                var r = Neg(forward);
                forward = right;
                right = r;
            }
        }

        // +90° pitch about Right, applied 'quarters' times: (up, forward) -> (forward, -up).
        private static void Pitch(ref Vector3I right, ref Vector3I up, ref Vector3I forward, int quarters)
        {
            for (var i = 0; i < quarters; i++)
            {
                var u = forward;
                forward = Neg(up);
                up = u;
            }
        }

        private static Vector3I Neg(Vector3I v)
        {
            return new Vector3I(-v.X, -v.Y, -v.Z);
        }

        private static Vector3I FaceOffset(int faceCode, Vector3I right, Vector3I up, Vector3I forward)
        {
            switch (faceCode)
            {
                case 0: return new Vector3I(-right.X, -right.Y, -right.Z);
                case 1: return right;
                case 2: return new Vector3I(-up.X, -up.Y, -up.Z);
                case 3: return up;
                case 4: return new Vector3I(-forward.X, -forward.Y, -forward.Z);
                default: return forward; // 5
            }
        }

        private static Vector3I DirToVec(int direction)
        {
            return Base6Directions.GetIntVector((Base6Directions.Direction)direction);
        }

        private static bool GetTemplate(ShipShapeId shape, out Vector3[] verts, out int[] edges, out int[] edgeFaces)
        {
            switch (shape)
            {
                case ShipShapeId.Slope: verts = SlopeVerts; edges = SlopeEdges; edgeFaces = SlopeEdgeFaces; return true;
                case ShipShapeId.Slope2x1: verts = Slope2x1BaseVerts; edges = Slope2x1Edges; edgeFaces = Slope2x1EdgeFaces; return true;
                case ShipShapeId.Slope2x1Tip: verts = Slope2x1TipVerts; edges = Slope2x1Edges; edgeFaces = Slope2x1EdgeFaces; return true;
                case ShipShapeId.Corner: verts = CornerVerts; edges = CornerEdges; edgeFaces = CornerEdgeFaces; return true;
                case ShipShapeId.InvertedCorner: verts = InvCornerVerts; edges = InvCornerEdges; edgeFaces = InvCornerEdgeFaces; return true;
                default: verts = null; edges = null; edgeFaces = null; return false;
            }
        }

        // Slope wedge: full -Up (bottom) and -Forward (back) faces; hypotenuse rising from the
        // front-bottom edge to the back-top edge.
        private static readonly Vector3[] SlopeVerts =
        {
            new Vector3(-0.5f, -0.5f, -0.5f), // 0 back-bottom-left
            new Vector3(0.5f, -0.5f, -0.5f),  // 1 back-bottom-right
            new Vector3(0.5f, -0.5f, 0.5f),   // 2 front-bottom-right
            new Vector3(-0.5f, -0.5f, 0.5f),  // 3 front-bottom-left
            new Vector3(-0.5f, 0.5f, -0.5f),  // 4 back-top-left
            new Vector3(0.5f, 0.5f, -0.5f),   // 5 back-top-right
        };

        // A slope reads as its slanted rectangular face — a parallelogram in view — so we
        // draw ONLY those four edges, not the triangular sides/base/back of the wedge.
        // Rectangle corners: 3 front-bottom-left -> 4 back-top-left -> 5 back-top-right ->
        // 2 front-bottom-right.
        private static readonly int[] SlopeEdges =
        {
            3, 4, 4, 5, 5, 2, 2, 3,
        };

        // Face codes per edge (0:-right 1:+right 2:-up 3:+up 4:-forward 5:+forward 6:always).
        // Left/right edges are seams shared with side neighbours (hide where an identical
        // slope continues); top/bottom edges always draw so the rectangle stays complete.
        private static readonly int[] SlopeEdgeFaces =
        {
            0, 6, // left edge  -> same-orientation cull against the -right neighbour
            6, 6, // top edge   -> always draw
            1, 6, // right edge -> same-orientation cull against the +right neighbour
            6, 6, // bottom edge-> always draw
        };

        // 2x1 slopes are single cells whose slanted face is a shallow (2:1) parallelogram.
        // Same convention as the basic slope (back at -forward/high, descending toward
        // +forward). Base: back-top down to mid-height at the front. Tip: back at mid-height
        // down to the bottom at the front. Corners ordered front-left, back-left, back-right,
        // front-right so the edge/face tables below match the basic slope's.
        private static readonly Vector3[] Slope2x1BaseVerts =
        {
            new Vector3(-0.5f, 0.0f, 0.5f),   // 0 front-left  (mid height)
            new Vector3(-0.5f, 0.5f, -0.5f),  // 1 back-top-left
            new Vector3(0.5f, 0.5f, -0.5f),   // 2 back-top-right
            new Vector3(0.5f, 0.0f, 0.5f),    // 3 front-right (mid height)
        };

        private static readonly Vector3[] Slope2x1TipVerts =
        {
            new Vector3(-0.5f, -0.5f, 0.5f),  // 0 front-left  (bottom)
            new Vector3(-0.5f, 0.0f, -0.5f),  // 1 back-left   (mid height)
            new Vector3(0.5f, 0.0f, -0.5f),   // 2 back-right  (mid height)
            new Vector3(0.5f, -0.5f, 0.5f),   // 3 front-right (bottom)
        };

        private static readonly int[] Slope2x1Edges =
        {
            0, 1, 1, 2, 2, 3, 3, 0,
        };

        // Left/right edges are side seams (same-orientation cull); top/bottom always draw.
        private static readonly int[] Slope2x1EdgeFaces =
        {
            0, 6, // left edge
            6, 6, // top/back edge
            1, 6, // right edge
            6, 6, // bottom/front edge
        };

        // Corner tetrahedron: right-angle corner at (-right,-up,-forward); slanted face out.
        private static readonly Vector3[] CornerVerts =
        {
            new Vector3(-0.5f, -0.5f, -0.5f), // 0 corner
            new Vector3(0.5f, -0.5f, -0.5f),  // 1 +right
            new Vector3(-0.5f, -0.5f, 0.5f),  // 2 +forward
            new Vector3(-0.5f, 0.5f, -0.5f),  // 3 +up
        };

        private static readonly int[] CornerEdges =
        {
            0, 1, 0, 2, 0, 3, 1, 2, 1, 3, 2, 3,
        };

        private static readonly int[] CornerEdgeFaces =
        {
            2, 4, 2, 0, 4, 0, 2, 6, 4, 6, 0, 6,
        };

        // Inverted corner: full cube minus the corner tetra at (-right,-up,-forward).
        private static readonly Vector3[] InvCornerVerts =
        {
            new Vector3(0.5f, -0.5f, -0.5f),  // 0 B
            new Vector3(-0.5f, -0.5f, 0.5f),  // 1 C
            new Vector3(-0.5f, 0.5f, -0.5f),  // 2 D
            new Vector3(0.5f, -0.5f, 0.5f),   // 3 E
            new Vector3(0.5f, 0.5f, -0.5f),   // 4 F
            new Vector3(-0.5f, 0.5f, 0.5f),   // 5 G
            new Vector3(0.5f, 0.5f, 0.5f),    // 6 H
        };

        private static readonly int[] InvCornerEdges =
        {
            0, 1, 0, 2, 1, 2, // cut triangle
            0, 3, 0, 4,       // B-E, B-F
            1, 3, 1, 5,       // C-E, C-G
            2, 4, 2, 5,       // D-F, D-G
            3, 6, 4, 6, 5, 6, // E-H, F-H, G-H
        };

        private static readonly int[] InvCornerEdgeFaces =
        {
            6, 2, 6, 4, 6, 0, // cut triangle edges: always + the cube face they lie on
            2, 1, 4, 1,       // B-E (-up,+right), B-F (-forward,+right)
            2, 5, 0, 5,       // C-E (-up,+forward), C-G (-right,+forward)
            4, 3, 0, 3,       // D-F (-forward,+up), D-G (-right,+up)
            1, 5, 1, 3, 5, 3, // E-H (+right,+forward), F-H (+right,+up), G-H (+forward,+up)
        };

        private static int GetCorner(Dictionary<long, int> cornerIndex, List<Corner> corners, int x, int y, int z)
        {
            var key = Pack(x, y, z);
            int index;
            if (cornerIndex.TryGetValue(key, out index))
            {
                return index;
            }

            index = corners.Count;
            corners.Add(new Corner { X = x, Y = y, Z = z });
            cornerIndex[key] = index;
            return index;
        }

        private static void AddEdge(HashSet<long> edgeSet, List<int> edges, int a, int b)
        {
            if (a == b)
            {
                return;
            }

            var lo = Math.Min(a, b);
            var hi = Math.Max(a, b);
            var key = ((long)lo << 32) | (uint)hi;
            if (edgeSet.Add(key))
            {
                edges.Add(a);
                edges.Add(b);
            }
        }

        // Specialty systems drawn as small category icons at their block centre.
        private static void DrawDevices(MySpriteDrawFrame frame, Vector2 origin, UiContext context, ShipVoxelModel ship, float yaw, float pitch, float scale, float modelCenterX, float modelCenterY, float centerX, float centerY)
        {
            if (ship.Devices == null)
            {
                return;
            }

            var side = MathHelper.Clamp(scale * 0.9f, 5f, 14f);
            for (var i = 0; i < ship.Devices.Count; i++)
            {
                var device = ship.Devices[i];
                float dx, dy, depth;
                Project(device.X, device.Y, device.Z, yaw, pitch, out dx, out dy, out depth);
                var x = centerX + ((dx - modelCenterX) * scale);
                var y = centerY - ((dy - modelCenterY) * scale);

                string sprite;
                Color color;
                GetDeviceIcon(device.Category, context, out sprite, out color);
                UiRenderer.DrawImage(frame, origin, new UiRect(x - (side * 0.5f), y - (side * 0.5f), side, side), sprite, color);
            }
        }

        // Maps a category to a built-in LCD sprite + colour. Shapes/colours are easily
        // swapped; these are all stock sprites guaranteed to exist on text surfaces.
        private static void GetDeviceIcon(ShipDeviceCategory category, UiContext context, out string sprite, out Color color)
        {
            switch (category)
            {
                case ShipDeviceCategory.Thruster: sprite = "Triangle"; color = context.Theme.Warning; return;
                case ShipDeviceCategory.Reactor: sprite = "Circle"; color = context.Theme.Success; return;
                case ShipDeviceCategory.Battery: sprite = "SquareSimple"; color = context.Theme.Accent; return;
                case ShipDeviceCategory.Tank: sprite = "Circle"; color = context.Theme.AccentSoft; return;
                case ShipDeviceCategory.Controller: sprite = "SemiCircle"; color = context.Theme.ForegroundPrimary; return;
                case ShipDeviceCategory.Weapon: sprite = "Cross"; color = context.Theme.Danger; return;
                case ShipDeviceCategory.Cargo: sprite = "SquareSimple"; color = context.Theme.ForegroundMuted; return;
                case ShipDeviceCategory.Gyro: sprite = "Circle"; color = context.Theme.ForegroundDim; return;
                default: sprite = "SquareSimple"; color = context.Theme.Border; return;
            }
        }

        private static void DrawMarkers(MySpriteDrawFrame frame, Vector2 origin, UiContext context, SecurityScreenModel model, SecurityEntity selected, float yaw, float pitch, float scale, float modelCenterX, float modelCenterY, float centerX, float centerY)
        {
            if (model.Markers == null)
            {
                return;
            }

            for (var i = 0; i < model.Markers.Count; i++)
            {
                var marker = model.Markers[i];
                float mx, my, depth;
                Project(marker.X + 0.5f, marker.Y + 0.5f, marker.Z + 0.5f, yaw, pitch, out mx, out my, out depth);
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
