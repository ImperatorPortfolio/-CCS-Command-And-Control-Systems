using System;
using System.Collections.Generic;
using System.Globalization;
using Sandbox.Game.GameSystems.TextSurfaceScripts;
using VRage.Game.GUI.TextPanel;
using VRageMath;

namespace AGS
{
    public enum ChartKind
    {
        Line,
        Bar
    }

    // Filled/stroked rectangle.
    public sealed class RectangleShape : Control
    {
        public Color Fill { get; set; }
        public Color Stroke { get; set; }
        public float StrokeThickness { get; set; }

        protected override Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            return new Vector2(Width >= 0f ? Width : availableSize.X, Height >= 0f ? Height : availableSize.Y);
        }

        protected override void RenderSelf(MySpriteDrawFrame frame, Vector2 origin, UiContext context)
        {
            if (Fill.A > 0)
            {
                UiRenderer.DrawPanel(frame, origin, new UiPanel { Bounds = ArrangedRect, Background = Fill });
            }
            if (Stroke.A > 0 && StrokeThickness > 0f)
            {
                UiRenderer.DrawOutline(frame, origin, ArrangedRect, Stroke, StrokeThickness);
            }
        }
    }

    // Filled/stroked ellipse (fills the element bounds).
    public sealed class EllipseShape : Control
    {
        public Color Fill { get; set; }
        public Color Stroke { get; set; }
        public float StrokeThickness { get; set; }

        protected override Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            return new Vector2(Width >= 0f ? Width : availableSize.X, Height >= 0f ? Height : availableSize.Y);
        }

        protected override void RenderSelf(MySpriteDrawFrame frame, Vector2 origin, UiContext context)
        {
            if (Stroke.A > 0 && StrokeThickness > 0f)
            {
                UiRenderer.DrawEllipse(frame, origin, ArrangedRect, Stroke);
                var inner = ArrangedRect.Deflate(new UiThickness(Math.Max(1f, StrokeThickness)));
                UiRenderer.DrawEllipse(frame, origin, inner, Fill.A > 0 ? Fill : context.Theme.Background1);
            }
            else if (Fill.A > 0)
            {
                UiRenderer.DrawEllipse(frame, origin, ArrangedRect, Fill);
            }
        }
    }

    // Straight line across the element. Endpoints are fractions (0..1) of the bounds;
    // defaults to a horizontal line through the middle.
    public sealed class LineShape : Control
    {
        public Color Stroke { get; set; }
        public float StrokeThickness { get; set; }
        public float X1 { get; set; }
        public float Y1 { get; set; }
        public float X2 { get; set; }
        public float Y2 { get; set; }

        public LineShape()
        {
            StrokeThickness = 1f;
            X1 = 0f;
            Y1 = 0.5f;
            X2 = 1f;
            Y2 = 0.5f;
        }

        protected override Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            return new Vector2(Width >= 0f ? Width : availableSize.X, Height >= 0f ? Height : availableSize.Y);
        }

        protected override void RenderSelf(MySpriteDrawFrame frame, Vector2 origin, UiContext context)
        {
            var color = Stroke.A > 0 ? Stroke : context.Theme.Border;
            var a = new Vector2(ArrangedRect.X + (ArrangedRect.Width * X1), ArrangedRect.Y + (ArrangedRect.Height * Y1));
            var b = new Vector2(ArrangedRect.X + (ArrangedRect.Width * X2), ArrangedRect.Y + (ArrangedRect.Height * Y2));
            UiRenderer.DrawLine(frame, origin, a, b, color, Math.Max(1f, StrokeThickness));
        }
    }

    // Line or bar chart over a numeric series. Min/Max auto-scale from the data when left
    // equal. Built for telemetry sparklines.
    public sealed class Chart : Control
    {
        private readonly List<float> _values = new List<float>();

        public IList<float> Values { get { return _values; } }
        public ChartKind Kind { get; set; }
        public float Minimum { get; set; }
        public float Maximum { get; set; }
        public Color LineColor { get; set; }
        public Color FillColor { get; set; }
        public bool ShowFrame { get; set; }

        public Chart()
        {
            ShowFrame = true;
        }

        public void SetValues(string csv)
        {
            _values.Clear();
            if (string.IsNullOrEmpty(csv))
            {
                return;
            }

            var parts = csv.Split(',');
            for (var i = 0; i < parts.Length; i++)
            {
                float value;
                if (float.TryParse(parts[i].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out value))
                {
                    _values.Add(value);
                }
            }
        }

        protected override Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            return new Vector2(Width >= 0f ? Width : availableSize.X, Height >= 0f ? Height : Math.Max(40f, 60f * context.Theme.LayoutScale));
        }

        protected override void RenderSelf(MySpriteDrawFrame frame, Vector2 origin, UiContext context)
        {
            UiRenderer.DrawPanel(frame, origin, new UiPanel { Bounds = ArrangedRect, Background = context.Theme.Background0 });
            if (ShowFrame)
            {
                UiRenderer.DrawOutline(frame, origin, ArrangedRect, context.Theme.Border, context.Theme.StrokeThin);
            }

            if (_values.Count == 0)
            {
                return;
            }

            float min = Minimum;
            float max = Maximum;
            if (max <= min)
            {
                min = _values[0];
                max = _values[0];
                for (var i = 1; i < _values.Count; i++)
                {
                    if (_values[i] < min) min = _values[i];
                    if (_values[i] > max) max = _values[i];
                }
            }

            var range = Math.Max(0.0001f, max - min);
            var inset = Math.Max(2f, ArrangedRect.Height * 0.06f);
            var plot = ArrangedRect.Deflate(new UiThickness(inset));
            var line = LineColor.A > 0 ? LineColor : context.Theme.Accent;
            var fill = FillColor.A > 0 ? FillColor : context.Theme.AccentSoft;

            if (Kind == ChartKind.Bar)
            {
                var gap = _values.Count > 1 ? Math.Max(1f, plot.Width * 0.01f) : 0f;
                var barW = Math.Max(1f, (plot.Width - (gap * (_values.Count - 1))) / _values.Count);
                for (var i = 0; i < _values.Count; i++)
                {
                    var ratio = MathHelper.Clamp((_values[i] - min) / range, 0f, 1f);
                    var barH = plot.Height * ratio;
                    var x = plot.X + (i * (barW + gap));
                    UiRenderer.DrawPanel(frame, origin, new UiPanel { Bounds = new UiRect(x, plot.Bottom - barH, barW, Math.Max(1f, barH)), Background = fill });
                }
                return;
            }

            var thickness = Math.Max(1f, plot.Height * 0.02f);
            var stepX = _values.Count > 1 ? plot.Width / (_values.Count - 1) : 0f;
            var prev = Vector2.Zero;
            for (var i = 0; i < _values.Count; i++)
            {
                var ratio = MathHelper.Clamp((_values[i] - min) / range, 0f, 1f);
                var point = new Vector2(plot.X + (i * stepX), plot.Bottom - (plot.Height * ratio));
                if (i > 0)
                {
                    UiRenderer.DrawLine(frame, origin, prev, point, line, thickness);
                }
                prev = point;
            }
        }
    }

    // Read-only rows x columns. Columns: comma-separated headers. Rows: ';'-separated
    // rows, each '|'-separated cells. Columns share width equally.
    public sealed class Table : Control
    {
        private readonly List<string> _columns = new List<string>();
        private readonly List<string[]> _rows = new List<string[]>();

        public bool ShowHeader { get; set; }
        public float RowHeight { get; set; }

        public Table()
        {
            ShowHeader = true;
            RowHeight = 22f;
        }

        public void SetColumns(string csv)
        {
            _columns.Clear();
            if (string.IsNullOrEmpty(csv))
            {
                return;
            }

            var parts = csv.Split(',');
            for (var i = 0; i < parts.Length; i++)
            {
                _columns.Add(parts[i].Trim());
            }
        }

        public void AddRow(string[] cells)
        {
            if (cells != null)
            {
                _rows.Add(cells);
            }
        }

        public void SetRows(string spec)
        {
            _rows.Clear();
            if (string.IsNullOrEmpty(spec))
            {
                return;
            }

            var rows = spec.Split(';');
            for (var i = 0; i < rows.Length; i++)
            {
                var trimmed = rows[i].Trim();
                if (trimmed.Length > 0)
                {
                    _rows.Add(trimmed.Split('|'));
                }
            }
        }

        private int ColumnCount()
        {
            var count = _columns.Count;
            for (var i = 0; i < _rows.Count; i++)
            {
                if (_rows[i].Length > count)
                {
                    count = _rows[i].Length;
                }
            }
            return Math.Max(1, count);
        }

        protected override Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            var rowH = RowHeight * context.Theme.LayoutScale;
            var lines = _rows.Count + (ShowHeader ? 1 : 0);
            var desired = lines * rowH;
            return new Vector2(Width >= 0f ? Width : availableSize.X, Height >= 0f ? Height : desired);
        }

        protected override void RenderSelf(MySpriteDrawFrame frame, Vector2 origin, UiContext context)
        {
            var cols = ColumnCount();
            var colW = ArrangedRect.Width / cols;
            var rowH = RowHeight * context.Theme.LayoutScale;
            var y = ArrangedRect.Y;
            var pad = Math.Max(2f, context.Theme.SpacingSm);

            if (ShowHeader && _columns.Count > 0)
            {
                var headerRect = new UiRect(ArrangedRect.X, y, ArrangedRect.Width, rowH);
                UiRenderer.DrawPanel(frame, origin, new UiPanel { Bounds = headerRect, Background = context.Theme.Background2 });
                for (var c = 0; c < _columns.Count; c++)
                {
                    DrawCell(frame, origin, new UiRect(ArrangedRect.X + (c * colW) + pad, y, colW - (pad * 2f), rowH), _columns[c], context.Theme.ForegroundMuted, context);
                }
                y += rowH;
            }

            for (var r = 0; r < _rows.Count; r++)
            {
                if (y >= ArrangedRect.Bottom)
                {
                    break;
                }

                if ((r & 1) == 1)
                {
                    UiRenderer.DrawPanel(frame, origin, new UiPanel { Bounds = new UiRect(ArrangedRect.X, y, ArrangedRect.Width, rowH), Background = context.Theme.Background1 });
                }

                var row = _rows[r];
                for (var c = 0; c < cols; c++)
                {
                    var text = c < row.Length ? row[c] : string.Empty;
                    DrawCell(frame, origin, new UiRect(ArrangedRect.X + (c * colW) + pad, y, colW - (pad * 2f), rowH), text, context.Theme.ForegroundPrimary, context);
                }
                y += rowH;
            }
        }

        private static void DrawCell(MySpriteDrawFrame frame, Vector2 origin, UiRect rect, string text, Color color, UiContext context)
        {
            UiRenderer.DrawLabel(frame, origin, new UiLabel
            {
                Bounds = rect,
                Padding = new UiThickness(0f),
                Text = text ?? string.Empty,
                Font = "Monospace",
                Scale = context.Theme.TextSm,
                Color = color,
                Alignment = TextAlignment.LEFT,
                VerticalAlignment = UiVerticalAlignment.Center
            });
        }
    }

    // Key -> value inspector list. Rows: ';'-separated, each 'key|value'.
    public sealed class DetailsView : Control
    {
        private readonly List<string> _keys = new List<string>();
        private readonly List<string> _values = new List<string>();

        public float RowHeight { get; set; }
        public float LabelWeight { get; set; }

        public DetailsView()
        {
            RowHeight = 20f;
            LabelWeight = 0.45f;
        }

        public void AddRow(string key, string value)
        {
            _keys.Add(key ?? string.Empty);
            _values.Add(value ?? string.Empty);
        }

        public void SetRows(string spec)
        {
            _keys.Clear();
            _values.Clear();
            if (string.IsNullOrEmpty(spec))
            {
                return;
            }

            var rows = spec.Split(';');
            for (var i = 0; i < rows.Length; i++)
            {
                var trimmed = rows[i].Trim();
                if (trimmed.Length == 0)
                {
                    continue;
                }

                var cells = trimmed.Split('|');
                _keys.Add(cells.Length > 0 ? cells[0] : string.Empty);
                _values.Add(cells.Length > 1 ? cells[1] : string.Empty);
            }
        }

        protected override Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            var rowH = RowHeight * context.Theme.LayoutScale;
            return new Vector2(Width >= 0f ? Width : availableSize.X, Height >= 0f ? Height : _keys.Count * rowH);
        }

        protected override void RenderSelf(MySpriteDrawFrame frame, Vector2 origin, UiContext context)
        {
            var rowH = RowHeight * context.Theme.LayoutScale;
            var labelW = ArrangedRect.Width * MathHelper.Clamp(LabelWeight, 0.1f, 0.9f);
            var pad = Math.Max(2f, context.Theme.SpacingSm);
            var y = ArrangedRect.Y;

            for (var i = 0; i < _keys.Count; i++)
            {
                if (y >= ArrangedRect.Bottom)
                {
                    break;
                }

                UiRenderer.DrawLabel(frame, origin, new UiLabel
                {
                    Bounds = new UiRect(ArrangedRect.X + pad, y, labelW - pad, rowH),
                    Padding = new UiThickness(0f),
                    Text = _keys[i],
                    Font = "Monospace",
                    Scale = context.Theme.TextSm,
                    Color = context.Theme.ForegroundMuted,
                    Alignment = TextAlignment.LEFT,
                    VerticalAlignment = UiVerticalAlignment.Center
                });
                UiRenderer.DrawLabel(frame, origin, new UiLabel
                {
                    Bounds = new UiRect(ArrangedRect.X + labelW, y, ArrangedRect.Width - labelW - pad, rowH),
                    Padding = new UiThickness(0f),
                    Text = _values[i],
                    Font = "Monospace",
                    Scale = context.Theme.TextSm,
                    Color = context.Theme.ForegroundPrimary,
                    Alignment = TextAlignment.RIGHT,
                    VerticalAlignment = UiVerticalAlignment.Center
                });
                y += rowH;
            }
        }
    }
}
