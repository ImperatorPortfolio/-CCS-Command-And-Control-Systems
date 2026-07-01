using System;
using System.Collections.Generic;
using System.Globalization;
using Sandbox.Game.GameSystems.TextSurfaceScripts;
using VRage.Game.GUI.TextPanel;
using VRageMath;

namespace AGS
{
    // Mutually-exclusive selector. Selection is owned by the app: OnClick emits the
    // control's GroupValue, and the app marks exactly one button in the group selected.
    public sealed class RadioButton : Control
    {
        public string Text { get; set; }
        public bool IsSelected { get; set; }
        public int GroupValue { get; set; }
        public UiCommand Command { get; set; }

        public RadioButton()
        {
            Focusable = true;
        }

        protected override Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            return new Vector2(Width >= 0f ? Width : availableSize.X, Height >= 0f ? Height : 34f * context.Theme.LayoutScale);
        }

        protected override void RenderSelf(MySpriteDrawFrame frame, Vector2 origin, UiContext context)
        {
            var diameter = Math.Min(ArrangedRect.Height, 16f * context.Theme.LayoutScale);
            var radius = diameter * 0.5f;
            var center = new Vector2(ArrangedRect.X + radius, ArrangedRect.Y + (ArrangedRect.Height * 0.5f));

            UiRenderer.DrawCircle(frame, origin, center, radius, context.Theme.Border);
            UiRenderer.DrawCircle(frame, origin, center, Math.Max(1f, radius - Math.Max(1f, context.Theme.StrokeThin)), context.Theme.Background2);
            if (IsSelected)
            {
                UiRenderer.DrawCircle(frame, origin, center, radius * 0.5f, context.Theme.Accent);
            }

            var labelX = center.X + radius + Math.Max(4f, context.Theme.SpacingSm);
            UiRenderer.DrawLabel(frame, origin, new UiLabel
            {
                Bounds = new UiRect(labelX, ArrangedRect.Y, Math.Max(0f, ArrangedRect.Right - labelX), ArrangedRect.Height),
                Padding = new UiThickness(0f),
                Text = Text ?? string.Empty,
                Font = "Monospace",
                Scale = context.Theme.TextSm,
                Color = context.Theme.ForegroundPrimary,
                Alignment = TextAlignment.LEFT,
                VerticalAlignment = UiVerticalAlignment.Center
            });
        }

        public override UiCommand OnClick(UiContext context)
        {
            return Command == null ? null : UiCommand.Create(Command.IntentType, Command.AppId, GroupValue, Command.Data);
        }
    }

    // [-] value [+] integer stepper. Tapping a side button emits the clamped new value.
    public sealed class Stepper : Control
    {
        public string Label { get; set; }
        public string ValueSuffix { get; set; }
        public int Value { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public int Step { get; set; }
        public UiCommand Command { get; set; }

        public Stepper()
        {
            Focusable = true;
            Step = 1;
            MaxValue = 100;
        }

        protected override Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            return new Vector2(Width >= 0f ? Width : availableSize.X, Height >= 0f ? Height : 34f * context.Theme.LayoutScale);
        }

        private float ButtonWidth()
        {
            return Math.Min(ArrangedRect.Height, ArrangedRect.Width * 0.3f);
        }

        protected override void RenderSelf(MySpriteDrawFrame frame, Vector2 origin, UiContext context)
        {
            var style = Style ?? context.Theme.ShellButtonStyle;
            var btnW = ButtonWidth();
            var minusRect = new UiRect(ArrangedRect.X, ArrangedRect.Y, btnW, ArrangedRect.Height);
            var plusRect = new UiRect(ArrangedRect.Right - btnW, ArrangedRect.Y, btnW, ArrangedRect.Height);
            var centerRect = new UiRect(minusRect.Right, ArrangedRect.Y, Math.Max(0f, ArrangedRect.Width - (btnW * 2f)), ArrangedRect.Height);

            var x = context.PointerLocal.X;
            var hoverMinus = IsHovered && x <= ArrangedRect.X + btnW;
            var hoverPlus = IsHovered && x >= ArrangedRect.Right - btnW;

            DrawKey(frame, origin, context, style, minusRect, "-", Value <= MinValue, hoverMinus, hoverMinus && IsPressed);
            DrawKey(frame, origin, context, style, plusRect, "+", Value >= MaxValue, hoverPlus, hoverPlus && IsPressed);

            UiRenderer.DrawPanel(frame, origin, new UiPanel { Bounds = centerRect, Background = context.Theme.Background2 });
            UiRenderer.DrawOutline(frame, origin, centerRect, context.Theme.Border, context.Theme.StrokeThin);

            var text = string.IsNullOrEmpty(Label) ? string.Empty : Label + " ";
            text += Value.ToString(CultureInfo.InvariantCulture);
            if (!string.IsNullOrEmpty(ValueSuffix))
            {
                text += ValueSuffix;
            }

            UiRenderer.DrawLabel(frame, origin, new UiLabel
            {
                Bounds = centerRect,
                Padding = new UiThickness(Math.Max(2f, context.Theme.SpacingSm), 0f),
                Text = text,
                Font = "Monospace",
                Scale = context.Theme.TextSm,
                Color = context.Theme.ForegroundPrimary,
                Alignment = TextAlignment.CENTER,
                VerticalAlignment = UiVerticalAlignment.Center
            });
        }

        private static void DrawKey(MySpriteDrawFrame frame, Vector2 origin, UiContext context, UiStyle style, UiRect rect, string glyph, bool atLimit, bool hovered, bool pressed)
        {
            UiRenderer.DrawButton(frame, origin, new UiButton
            {
                Bounds = rect,
                Padding = new UiThickness(0f),
                Text = glyph,
                Font = "Monospace",
                Scale = context.Theme.TextMd,
                IsHovered = hovered && !atLimit,
                IsPressed = pressed && !atLimit,
                IsDisabled = atLimit,
                Background = atLimit ? context.Theme.Background1 : style.Background,
                HoverBackground = style.HoverBackground,
                PressedBackground = style.PressedBackground,
                Foreground = atLimit ? context.Theme.ForegroundDim : style.Foreground,
                Alignment = TextAlignment.CENTER
            });
        }

        public override UiCommand OnClick(UiContext context)
        {
            if (Command == null)
            {
                return null;
            }

            var btnW = ButtonWidth();
            var x = context.PointerLocal.X;
            var step = Math.Max(1, Step);
            int newValue;
            if (x <= ArrangedRect.X + btnW)
            {
                newValue = Value - step;
            }
            else if (x >= ArrangedRect.Right - btnW)
            {
                newValue = Value + step;
            }
            else
            {
                return null;
            }

            if (newValue < MinValue)
            {
                newValue = MinValue;
            }
            if (newValue > MaxValue)
            {
                newValue = MaxValue;
            }
            if (newValue == Value)
            {
                return null;
            }

            return UiCommand.Create(Command.IntentType, Command.AppId, newValue, Command.Data);
        }
    }

    // Read-only fill bar. Use MetricBar instead when you want the label/value row.
    public sealed class ProgressBar : Control
    {
        public float Ratio { get; set; }
        public string ValueText { get; set; }
        public Color FillColor { get; set; }

        protected override Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            return new Vector2(Width >= 0f ? Width : availableSize.X, Height >= 0f ? Height : Math.Max(10f, 14f * context.Theme.LayoutScale));
        }

        protected override void RenderSelf(MySpriteDrawFrame frame, Vector2 origin, UiContext context)
        {
            var ratio = MathHelper.Clamp(Ratio, 0f, 1f);
            UiRenderer.DrawPanel(frame, origin, new UiPanel { Bounds = ArrangedRect, Background = context.Theme.Background2 });
            UiRenderer.DrawOutline(frame, origin, ArrangedRect, context.Theme.Border, context.Theme.StrokeThin);

            var inset = Math.Max(1f, ArrangedRect.Height * 0.16f);
            var fillWidth = Math.Max(0f, (ArrangedRect.Width - (inset * 2f)) * ratio);
            var fillRect = new UiRect(ArrangedRect.X + inset, ArrangedRect.Y + inset, fillWidth, Math.Max(1f, ArrangedRect.Height - (inset * 2f)));
            UiRenderer.DrawPanel(frame, origin, new UiPanel { Bounds = fillRect, Background = FillColor.A > 0 ? FillColor : context.Theme.Accent });

            if (!string.IsNullOrEmpty(ValueText))
            {
                UiRenderer.DrawLabel(frame, origin, new UiLabel
                {
                    Bounds = ArrangedRect,
                    Padding = new UiThickness(Math.Max(2f, context.Theme.SpacingSm), 0f),
                    Text = ValueText,
                    Font = "Monospace",
                    Scale = context.Theme.TextXs,
                    Color = context.Theme.ForegroundPrimary,
                    Alignment = TextAlignment.CENTER,
                    VerticalAlignment = UiVerticalAlignment.Center
                });
            }
        }
    }

    // Thin divider line. Horizontal by default; set Vertical for a column rule.
    public sealed class Separator : Control
    {
        public bool Vertical { get; set; }
        public Color Color { get; set; }
        public float Thickness { get; set; }

        public Separator()
        {
            Thickness = 1f;
        }

        protected override Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            var t = Math.Max(1f, Thickness);
            if (Vertical)
            {
                return new Vector2(Width >= 0f ? Width : t, Height >= 0f ? Height : availableSize.Y);
            }

            return new Vector2(Width >= 0f ? Width : availableSize.X, Height >= 0f ? Height : t);
        }

        protected override void RenderSelf(MySpriteDrawFrame frame, Vector2 origin, UiContext context)
        {
            var color = Color.A > 0 ? Color : context.Theme.Border;
            var t = Math.Max(1f, Thickness);
            UiRect line;
            if (Vertical)
            {
                var x = ArrangedRect.X + ((ArrangedRect.Width - t) * 0.5f);
                line = new UiRect(x, ArrangedRect.Y, t, ArrangedRect.Height);
            }
            else
            {
                var y = ArrangedRect.Y + ((ArrangedRect.Height - t) * 0.5f);
                line = new UiRect(ArrangedRect.X, y, ArrangedRect.Width, t);
            }

            UiRenderer.DrawPanel(frame, origin, new UiPanel { Bounds = line, Background = color });
        }
    }

    // Arbitrary sprite (texture or icon) drawn to fill the element bounds.
    public sealed class ImageControl : Control
    {
        public string Sprite { get; set; }
        public Color Tint { get; set; }
        public UiCommand Command { get; set; }

        public ImageControl()
        {
            Tint = Color.White;
        }

        protected override Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            return new Vector2(Width >= 0f ? Width : availableSize.X, Height >= 0f ? Height : availableSize.Y);
        }

        protected override void RenderSelf(MySpriteDrawFrame frame, Vector2 origin, UiContext context)
        {
            UiRenderer.DrawImage(frame, origin, ArrangedRect, Sprite, Tint.A > 0 ? Tint : Color.White);
        }

        public override UiCommand OnClick(UiContext context)
        {
            return Command;
        }
    }

    // On-screen numeric keypad. The only practical text-entry path on an LCD: the app
    // keeps an input buffer and applies each key. Digits emit 0-9; ClearCode/BackspaceCode
    // are emitted for the C and < keys.
    public sealed class Keypad : Control
    {
        public const int ClearCode = -1;
        public const int BackspaceCode = -2;

        private static readonly string[] Labels = { "7", "8", "9", "4", "5", "6", "1", "2", "3", "C", "0", "<" };
        private static readonly int[] Codes = { 7, 8, 9, 4, 5, 6, 1, 2, 3, ClearCode, 0, BackspaceCode };

        public UiCommand Command { get; set; }

        public Keypad()
        {
            Focusable = true;
        }

        protected override Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            return new Vector2(Width >= 0f ? Width : availableSize.X, Height >= 0f ? Height : availableSize.Y);
        }

        private void CellMetrics(out float cellW, out float cellH, out float gap)
        {
            gap = Math.Max(2f, ArrangedRect.Width * 0.02f);
            cellW = (ArrangedRect.Width - (gap * 2f)) / 3f;
            cellH = (ArrangedRect.Height - (gap * 3f)) / 4f;
        }

        private int IndexAt(Vector2 point)
        {
            float cellW, cellH, gap;
            CellMetrics(out cellW, out cellH, out gap);
            if (cellW <= 0f || cellH <= 0f)
            {
                return -1;
            }

            var col = (int)((point.X - ArrangedRect.X) / (cellW + gap));
            var row = (int)((point.Y - ArrangedRect.Y) / (cellH + gap));
            if (col < 0 || col > 2 || row < 0 || row > 3)
            {
                return -1;
            }

            var index = (row * 3) + col;
            return index >= 0 && index < Codes.Length ? index : -1;
        }

        protected override void RenderSelf(MySpriteDrawFrame frame, Vector2 origin, UiContext context)
        {
            var style = Style ?? context.Theme.ShellButtonStyle;
            float cellW, cellH, gap;
            CellMetrics(out cellW, out cellH, out gap);
            if (cellW <= 0f || cellH <= 0f)
            {
                return;
            }

            var pressed = IsPressed;
            var activeIndex = IsHovered ? IndexAt(context.PointerLocal) : -1;

            for (var i = 0; i < Labels.Length; i++)
            {
                var col = i % 3;
                var row = i / 3;
                var rect = new UiRect(ArrangedRect.X + (col * (cellW + gap)), ArrangedRect.Y + (row * (cellH + gap)), cellW, cellH);
                UiRenderer.DrawButton(frame, origin, new UiButton
                {
                    Bounds = rect,
                    Padding = new UiThickness(0f),
                    Text = Labels[i],
                    Font = "Monospace",
                    Scale = context.Theme.TextMd,
                    IsHovered = i == activeIndex,
                    IsPressed = i == activeIndex && pressed,
                    Background = style.Background,
                    HoverBackground = style.HoverBackground,
                    PressedBackground = style.PressedBackground,
                    Foreground = style.Foreground,
                    Alignment = TextAlignment.CENTER
                });
            }
        }

        public override UiCommand OnClick(UiContext context)
        {
            if (Command == null)
            {
                return null;
            }

            var index = IndexAt(context.PointerLocal);
            if (index < 0)
            {
                return null;
            }

            return UiCommand.Create(Command.IntentType, Command.AppId, Codes[index], Command.Data);
        }
    }

    // Horizontal selectable tab row. Emits the tapped tab index; the app swaps content.
    public sealed class TabStrip : Control
    {
        private readonly List<string> _tabs = new List<string>();

        public IList<string> Tabs { get { return _tabs; } }
        public int SelectedIndex { get; set; }
        public UiCommand Command { get; set; }

        public TabStrip()
        {
            Focusable = true;
        }

        public void SetTabs(string csv)
        {
            _tabs.Clear();
            if (string.IsNullOrEmpty(csv))
            {
                return;
            }

            var parts = csv.Split(',');
            for (var i = 0; i < parts.Length; i++)
            {
                var token = parts[i].Trim();
                if (token.Length > 0)
                {
                    _tabs.Add(token);
                }
            }
        }

        protected override Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            return new Vector2(Width >= 0f ? Width : availableSize.X, Height >= 0f ? Height : 32f * context.Theme.LayoutScale);
        }

        private float TabWidth()
        {
            var count = _tabs.Count > 0 ? _tabs.Count : 1;
            return ArrangedRect.Width / count;
        }

        private int IndexAt(Vector2 point)
        {
            var tabW = TabWidth();
            if (tabW <= 0f || _tabs.Count == 0)
            {
                return -1;
            }

            var index = (int)((point.X - ArrangedRect.X) / tabW);
            return index >= 0 && index < _tabs.Count ? index : -1;
        }

        protected override void RenderSelf(MySpriteDrawFrame frame, Vector2 origin, UiContext context)
        {
            if (_tabs.Count == 0)
            {
                return;
            }

            var style = Style ?? context.Theme.ShellButtonStyle;
            var tabW = TabWidth();
            var activeIndex = IsHovered ? IndexAt(context.PointerLocal) : -1;
            for (var i = 0; i < _tabs.Count; i++)
            {
                var rect = new UiRect(ArrangedRect.X + (i * tabW), ArrangedRect.Y, tabW, ArrangedRect.Height);
                var selected = i == SelectedIndex;
                UiRenderer.DrawPanel(frame, origin, new UiPanel { Bounds = rect, Background = selected || i == activeIndex ? style.HoverBackground : style.Background });
                if (selected)
                {
                    var accentHeight = Math.Max(1f, rect.Height * 0.1f);
                    UiRenderer.DrawPanel(frame, origin, new UiPanel { Bounds = new UiRect(rect.X, rect.Bottom - accentHeight, rect.Width, accentHeight), Background = context.Theme.Accent });
                }

                UiRenderer.DrawLabel(frame, origin, new UiLabel
                {
                    Bounds = rect,
                    Padding = new UiThickness(Math.Max(2f, context.Theme.SpacingSm), 0f),
                    Text = _tabs[i],
                    Font = "Monospace",
                    Scale = context.Theme.TextSm,
                    Color = selected ? context.Theme.ForegroundPrimary : context.Theme.ForegroundMuted,
                    Alignment = TextAlignment.CENTER,
                    VerticalAlignment = UiVerticalAlignment.Center
                });
            }
        }

        public override UiCommand OnClick(UiContext context)
        {
            if (Command == null)
            {
                return null;
            }

            var index = IndexAt(context.PointerLocal);
            if (index < 0)
            {
                return null;
            }

            return UiCommand.Create(Command.IntentType, Command.AppId, index, Command.Data);
        }
    }

    // Collapsed select control with a drop-open option list. Open state is app-owned
    // (bind IsOpen): tapping the header emits ToggleCommand, tapping an option emits
    // SelectCommand with the chosen index. Defaults to a high ZIndex so the open list
    // paints over and out-hit-tests its siblings.
    public sealed class Dropdown : Control
    {
        private readonly List<string> _options = new List<string>();

        public IList<string> Options { get { return _options; } }
        public int SelectedIndex { get; set; }
        public bool IsOpen { get; set; }
        public string Placeholder { get; set; }
        public UiCommand ToggleCommand { get; set; }
        public UiCommand SelectCommand { get; set; }

        public Dropdown()
        {
            Focusable = true;
            ZIndex = 100;
        }

        public void SetOptions(string csv)
        {
            _options.Clear();
            if (string.IsNullOrEmpty(csv))
            {
                return;
            }

            var parts = csv.Split(',');
            for (var i = 0; i < parts.Length; i++)
            {
                var token = parts[i].Trim();
                if (token.Length > 0)
                {
                    _options.Add(token);
                }
            }
        }

        protected override Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            return new Vector2(Width >= 0f ? Width : availableSize.X, Height >= 0f ? Height : 30f * context.Theme.LayoutScale);
        }

        private float RowHeight()
        {
            return ArrangedRect.Height;
        }

        private UiRect PopupRect()
        {
            return new UiRect(ArrangedRect.X, ArrangedRect.Bottom, ArrangedRect.Width, RowHeight() * _options.Count);
        }

        private string SelectedText()
        {
            if (SelectedIndex >= 0 && SelectedIndex < _options.Count)
            {
                return _options[SelectedIndex];
            }

            return Placeholder ?? string.Empty;
        }

        protected override void RenderSelf(MySpriteDrawFrame frame, Vector2 origin, UiContext context)
        {
            UiRenderer.DrawPanel(frame, origin, new UiPanel { Bounds = ArrangedRect, Background = context.Theme.Background2 });
            UiRenderer.DrawOutline(frame, origin, ArrangedRect, IsOpen ? context.Theme.Accent : context.Theme.Border, IsOpen ? Math.Max(1f, context.Theme.StrokeNormal) : context.Theme.StrokeThin);

            var arrowWidth = Math.Min(ArrangedRect.Width * 0.2f, ArrangedRect.Height);
            var pad = Math.Max(4f, context.Theme.SpacingSm);
            UiRenderer.DrawLabel(frame, origin, new UiLabel
            {
                Bounds = new UiRect(ArrangedRect.X + pad, ArrangedRect.Y, Math.Max(0f, ArrangedRect.Width - arrowWidth - pad), ArrangedRect.Height),
                Padding = new UiThickness(0f),
                Text = SelectedText(),
                Font = "Monospace",
                Scale = context.Theme.TextSm,
                Color = SelectedIndex >= 0 && SelectedIndex < _options.Count ? context.Theme.ForegroundPrimary : context.Theme.ForegroundDim,
                Alignment = TextAlignment.LEFT,
                VerticalAlignment = UiVerticalAlignment.Center
            });
            UiRenderer.DrawLabel(frame, origin, new UiLabel
            {
                Bounds = new UiRect(ArrangedRect.Right - arrowWidth, ArrangedRect.Y, arrowWidth, ArrangedRect.Height),
                Padding = new UiThickness(0f),
                Text = IsOpen ? "^" : "v",
                Font = "Monospace",
                Scale = context.Theme.TextSm,
                Color = context.Theme.ForegroundMuted,
                Alignment = TextAlignment.CENTER,
                VerticalAlignment = UiVerticalAlignment.Center
            });

            if (!IsOpen || _options.Count == 0)
            {
                return;
            }

            var popup = PopupRect();
            var rowH = RowHeight();
            UiRenderer.DrawPanel(frame, origin, new UiPanel { Bounds = popup, Background = context.Theme.Background1 });
            for (var i = 0; i < _options.Count; i++)
            {
                var rowRect = new UiRect(popup.X, popup.Y + (i * rowH), popup.Width, rowH);
                if (i == SelectedIndex)
                {
                    UiRenderer.DrawPanel(frame, origin, new UiPanel { Bounds = rowRect, Background = context.Theme.Background2 });
                }

                UiRenderer.DrawLabel(frame, origin, new UiLabel
                {
                    Bounds = new UiRect(rowRect.X + pad, rowRect.Y, Math.Max(0f, rowRect.Width - (pad * 2f)), rowRect.Height),
                    Padding = new UiThickness(0f),
                    Text = _options[i],
                    Font = "Monospace",
                    Scale = context.Theme.TextSm,
                    Color = context.Theme.ForegroundPrimary,
                    Alignment = TextAlignment.LEFT,
                    VerticalAlignment = UiVerticalAlignment.Center
                });
            }

            UiRenderer.DrawOutline(frame, origin, popup, context.Theme.Border, context.Theme.StrokeThin);
        }

        public override UiElement HitTest(Vector2 point)
        {
            if (Visibility == UiVisibility.Collapsed || !IsEnabled)
            {
                return null;
            }

            if (ArrangedRect.Contains(point))
            {
                return this;
            }

            if (IsOpen && _options.Count > 0 && PopupRect().Contains(point))
            {
                return this;
            }

            return null;
        }

        public override UiCommand OnClick(UiContext context)
        {
            var point = context.PointerLocal;
            if (ArrangedRect.Contains(point))
            {
                return ToggleCommand;
            }

            if (IsOpen && SelectCommand != null && _options.Count > 0)
            {
                var popup = PopupRect();
                if (popup.Contains(point))
                {
                    var rowH = RowHeight();
                    if (rowH <= 0f)
                    {
                        return null;
                    }

                    var index = (int)((point.Y - popup.Y) / rowH);
                    if (index < 0 || index >= _options.Count)
                    {
                        return null;
                    }

                    return UiCommand.Create(SelectCommand.IntentType, SelectCommand.AppId, index, SelectCommand.Data);
                }
            }

            return null;
        }
    }
}
