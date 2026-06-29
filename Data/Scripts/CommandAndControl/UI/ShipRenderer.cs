using System;
using VRage.Game.GUI.TextPanel;
using VRageMath;

namespace AGS
{
    public static class ShipRenderer
    {
        public static void Render(MySpriteDrawFrame frame, Vector2 origin, UiRect rect, UiContext context, SecurityEntity selected)
        {
            var model = context.Frame.Security;
            var ship = model != null ? model.Ship : null;

            UiRenderer.DrawPanel(frame, origin, new UiPanel { Bounds = rect, Background = context.Theme.Background1 });
            UiRenderer.DrawOutline(frame, origin, rect, context.Theme.Border, Math.Max(1f, context.Theme.StrokeThin));

            if (ship == null || !ship.HasCells)
            {
                DrawEmpty(frame, origin, rect, context, "No construct geometry available");
                return;
            }

            var resolvedLayer = ResolveLayer(ship, model.View, model.Layer);
            var projection = ShipProjection.Build(ship, model.View, resolvedLayer);
            if (!projection.HasCells)
            {
                DrawEmpty(frame, origin, rect, context, "Selected layer has no geometry");
                return;
            }

            var zoomPad = MathHelper.Clamp(0.12f - (model.ZoomStep * 0.018f), 0.02f, 0.22f);
            var pad = new UiThickness(rect.Width * zoomPad, rect.Height * zoomPad, rect.Width * zoomPad, rect.Height * zoomPad);
            var plot = rect.Deflate(pad);
            var widthCells = Math.Max(1, (projection.MaxU - projection.MinU) + 1);
            var heightCells = Math.Max(1, (projection.MaxV - projection.MinV) + 1);
            var cellSize = Math.Min(plot.Width / widthCells, plot.Height / heightCells);
            cellSize = MathHelper.Clamp(cellSize, 2.5f, 36f);

            var drawWidth = widthCells * cellSize;
            var drawHeight = heightCells * cellSize;
            var offsetX = plot.X + ((plot.Width - drawWidth) * 0.5f);
            var offsetY = plot.Y + ((plot.Height - drawHeight) * 0.5f);
            var silhouetteStroke = MathHelper.Clamp(cellSize * 0.12f, 1f, 4f);
            foreach (var pair in projection.VisibleCells)
            {
                var cell = pair.Value;
                DrawVisibleCell(frame, origin, cell, projection, context, offsetX, offsetY, cellSize, silhouetteStroke, resolvedLayer);
            }

            DrawSlopeHighlights(frame, origin, ship, projection, context, offsetX, offsetY, cellSize);
            DrawMarkers(frame, origin, context, model, selected, projection, offsetX, offsetY, cellSize, resolvedLayer);

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

        public static int GetLayerCount(ShipVoxelModel ship, ShipView view)
        {
            if (ship == null || !ship.HasCells) return 0;
            return GetDepthMax(ship, view) - GetDepthMin(ship, view) + 1;
        }

        public static int ResolveLayer(ShipVoxelModel ship, ShipView view, int requestedLayer)
        {
            if (ship == null || !ship.HasCells) return 0;
            var min = GetDepthMin(ship, view);
            var max = GetDepthMax(ship, view);
            if (requestedLayer == int.MaxValue) return max;
            if (requestedLayer < min) return min;
            if (requestedLayer > max) return max;
            return requestedLayer;
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
                default: return "TOP";
            }
        }

        public static ShipView CycleView(ShipView view, int delta)
        {
            var values = 6;
            var next = ((int)view + delta) % values;
            if (next < 0) next += values;
            return (ShipView)next;
        }

        private static void DrawVisibleCell(MySpriteDrawFrame frame, Vector2 origin, ShipProjectedCell cell, ShipProjectionResult projection, UiContext context, float offsetX, float offsetY, float cellSize, float stroke, int resolvedLayer)
        {
            var color = ResolveDepthColor(context, cell.Depth, resolvedLayer, projection.MinDepth, projection.MaxDepth);
            var left = offsetX + ((cell.U - projection.MinU) * cellSize);
            var top = offsetY + ((cell.V - projection.MinV) * cellSize);
            var right = left + cellSize;
            var bottom = top + cellSize;
            var thickness = cell.Depth == resolvedLayer ? Math.Max(stroke, 2f) : stroke;

            if (!HasNeighbor(projection.VisibleCells, cell.U, cell.V - 1))
            {
                UiRenderer.DrawLine(frame, origin, new Vector2(left, top), new Vector2(right, top), color, thickness);
            }
            if (!HasNeighbor(projection.VisibleCells, cell.U + 1, cell.V))
            {
                UiRenderer.DrawLine(frame, origin, new Vector2(right, top), new Vector2(right, bottom), color, thickness);
            }
            if (!HasNeighbor(projection.VisibleCells, cell.U, cell.V + 1))
            {
                UiRenderer.DrawLine(frame, origin, new Vector2(left, bottom), new Vector2(right, bottom), color, thickness);
            }
            if (!HasNeighbor(projection.VisibleCells, cell.U - 1, cell.V))
            {
                UiRenderer.DrawLine(frame, origin, new Vector2(left, top), new Vector2(left, bottom), color, thickness);
            }
        }

        private static bool HasNeighbor(System.Collections.Generic.Dictionary<long, ShipProjectedCell> visibleCells, int u, int v)
        {
            return visibleCells.ContainsKey(Pack(u, v));
        }

        private static void DrawMarkers(MySpriteDrawFrame frame, Vector2 origin, UiContext context, SecurityScreenModel model, SecurityEntity selected, ShipProjectionResult projection, float offsetX, float offsetY, float cellSize, int visibleLayer)
        {
            for (var i = 0; i < model.Markers.Count; i++)
            {
                var marker = model.Markers[i];
                int u, v, depth;
                ShipProjection.ProjectCell((int)Math.Round(marker.X), (int)Math.Round(marker.Y), (int)Math.Round(marker.Z), model.View, out u, out v, out depth);
                if (depth > visibleLayer)
                {
                    continue;
                }

                var x = offsetX + (((float)u - projection.MinU + 0.5f) * cellSize);
                var y = offsetY + (((float)v - projection.MinV + 0.5f) * cellSize);
                var side = MathHelper.Clamp(cellSize * 1.4f, 6f, 18f);
                var bounds = new UiRect(x - (side * 0.5f), y - (side * 0.5f), side, side);
                UiRenderer.DrawOutline(frame, origin, bounds, GetMarkerColor(marker.Kind, context), Math.Max(1f, side * 0.14f));

                if (selected != null && string.Equals(selected.Id, marker.Id, StringComparison.OrdinalIgnoreCase))
                {
                    var inner = bounds.Deflate(new UiThickness(Math.Max(1f, side * 0.18f)));
                    UiRenderer.DrawOutline(frame, origin, inner, context.Theme.ForegroundPrimary, 1f);
                }
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

        private static Vector2 ToScreenPoint(float u, float v, int minU, int minV, float offsetX, float offsetY, float cellSize)
        {
            return new Vector2(offsetX + ((u - minU + 0.5f) * cellSize), offsetY + ((v - minV + 0.5f) * cellSize));
        }

        private static void DrawSlopeHighlights(MySpriteDrawFrame frame, Vector2 origin, ShipVoxelModel ship, ShipProjectionResult projection, UiContext context, float offsetX, float offsetY, float cellSize)
        {
            if (ship == null)
            {
                return;
            }

            foreach (var pair in projection.VisibleCells)
            {
                var cell = pair.Value;
                if (cell.OwnerIndex < 0 || cell.OwnerIndex >= ship.Blocks.Count)
                {
                    continue;
                }

                var block = ship.Blocks[cell.OwnerIndex];
                if (block == null || block.ShapeId != ShipShapeId.Slope || !IsPlainSlopeSubtype(block.SubtypeName))
                {
                    continue;
                }

                var left = offsetX + ((cell.U - projection.MinU) * cellSize);
                var top = offsetY + ((cell.V - projection.MinV) * cellSize);
                var inset = MathHelper.Clamp(cellSize * 0.22f, 1f, 6f);
                var bounds = new UiRect(left + inset, top + inset, Math.Max(2f, cellSize - (inset * 2f)), Math.Max(2f, cellSize - (inset * 2f)));
                UiRenderer.DrawOutline(frame, origin, bounds, context.Theme.Warning, Math.Max(1f, cellSize * 0.12f));
            }
        }

        private static bool IsPlainSlopeSubtype(string subtypeName)
        {
            var lowered = (subtypeName ?? string.Empty).ToLowerInvariant();
            return lowered.Contains("slope") &&
                   !lowered.Contains("2x1") &&
                   !lowered.Contains("tip") &&
                   !lowered.Contains("corner") &&
                   !lowered.Contains("invcorner") &&
                   !lowered.Contains("invertedcorner");
        }
        private static Color ResolveDepthColor(UiContext context, int depth, int resolvedLayer, int minDepth, int maxDepth)
        {
            if (depth == resolvedLayer) return context.Theme.Accent;
            var range = Math.Max(1, maxDepth - minDepth);
            var ratio = (depth - minDepth) / (float)range;
            var blend = Color.Lerp(context.Theme.ForegroundDim, context.Theme.Border, ratio);
            return new Color(blend.R, blend.G, blend.B, 170);
        }

        private static int GetDepthMin(ShipVoxelModel ship, ShipView view)
        {
            switch (view)
            {
                case ShipView.Top:
                case ShipView.Bottom:
                    return ship.MinY;
                case ShipView.Left:
                case ShipView.Right:
                    return ship.MinX;
                default:
                    return ship.MinZ;
            }
        }

        private static int GetDepthMax(ShipVoxelModel ship, ShipView view)
        {
            switch (view)
            {
                case ShipView.Top:
                case ShipView.Bottom:
                    return ship.MaxY;
                case ShipView.Left:
                case ShipView.Right:
                    return ship.MaxX;
                default:
                    return ship.MaxZ;
            }
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

        private static long Pack(int x, int y)
        {
            return ((long)x << 32) ^ (uint)y;
        }
    }
}
