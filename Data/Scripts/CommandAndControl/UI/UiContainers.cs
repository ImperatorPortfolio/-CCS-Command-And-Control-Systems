using System;
using Sandbox.Game.GameSystems.TextSurfaceScripts;
using VRage.Game.GUI.TextPanel;
using VRageMath;

namespace AGS
{
    // Modal dialog. Fills its arranged area with a dim backdrop and centres a titled
    // panel that hosts Content. Designed to be added to UiView.Overlays so it covers the
    // whole surface, but also works as a normal child. Clicking the close glyph or the
    // backdrop emits CloseCommand.
    public sealed class Dialog : ContentControl
    {
        private UiRect _panelRect;
        private UiRect _contentRect;
        private UiRect _closeRect;
        private bool _hasClose;

        public string Title { get; set; }
        public UiCommand CloseCommand { get; set; }
        public bool ShowClose { get; set; }
        public bool DismissOnBackdrop { get; set; }
        public float PanelWidth { get; set; }
        public float PanelHeight { get; set; }
        public Color BackdropColor { get; set; }
        public Color Background { get; set; }
        public Color BorderColor { get; set; }
        public UiThickness Padding { get; set; }

        public Dialog()
        {
            ShowClose = true;
            DismissOnBackdrop = true;
            PanelWidth = -1f;
            PanelHeight = -1f;
            Padding = new UiThickness(8f);
        }

        private float TitleHeight()
        {
            return string.IsNullOrEmpty(Title) ? 0f : Math.Max(20f, _panelRect.Height * 0.12f);
        }

        protected override Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            if (Content != null)
            {
                Content.Measure(availableSize, context);
            }
            return availableSize;
        }

        protected override void ArrangeCore(UiRect rect, UiContext context)
        {
            var w = Math.Min(rect.Width, PanelWidth > 0f ? PanelWidth : rect.Width * 0.6f);
            var h = Math.Min(rect.Height, PanelHeight > 0f ? PanelHeight : rect.Height * 0.6f);
            _panelRect = new UiRect(rect.X + ((rect.Width - w) * 0.5f), rect.Y + ((rect.Height - h) * 0.5f), w, h);

            var titleH = TitleHeight();
            var closeSize = Math.Max(16f, titleH > 0f ? titleH : 22f);
            _hasClose = ShowClose && CloseCommand != null;
            _closeRect = _hasClose ? new UiRect(_panelRect.Right - closeSize, _panelRect.Y, closeSize, closeSize) : new UiRect();

            var innerTop = _panelRect.Y + titleH;
            _contentRect = new UiRect(
                _panelRect.X + Padding.Left,
                innerTop + Padding.Top,
                Math.Max(0f, _panelRect.Width - Padding.Horizontal),
                Math.Max(0f, _panelRect.Bottom - innerTop - Padding.Vertical));

            if (Content != null)
            {
                Content.Arrange(_contentRect, context);
            }
        }

        protected override void RenderSelf(MySpriteDrawFrame frame, Vector2 origin, UiContext context)
        {
            var backdrop = BackdropColor.A > 0 ? BackdropColor : new Color(0, 0, 0, 150);
            UiRenderer.DrawPanel(frame, origin, new UiPanel { Bounds = ArrangedRect, Background = backdrop });

            UiRenderer.DrawPanel(frame, origin, new UiPanel { Bounds = _panelRect, Background = Background.A > 0 ? Background : context.Theme.Background1 });
            UiRenderer.DrawOutline(frame, origin, _panelRect, BorderColor.A > 0 ? BorderColor : context.Theme.Border, Math.Max(1f, context.Theme.StrokeNormal));

            var titleH = TitleHeight();
            if (titleH > 0f)
            {
                var titleRect = new UiRect(_panelRect.X, _panelRect.Y, _panelRect.Width, titleH);
                UiRenderer.DrawPanel(frame, origin, new UiPanel { Bounds = titleRect, Background = context.Theme.Background2 });
                UiRenderer.DrawLabel(frame, origin, new UiLabel
                {
                    Bounds = titleRect.Deflate(new UiThickness(Math.Max(4f, context.Theme.SpacingSm), 0f)),
                    Padding = new UiThickness(0f),
                    Text = Title,
                    Font = "Monospace",
                    Scale = context.Theme.TextMd,
                    Color = context.Theme.ForegroundPrimary,
                    Alignment = TextAlignment.LEFT,
                    VerticalAlignment = UiVerticalAlignment.Center
                });
            }

            if (_hasClose)
            {
                UiRenderer.DrawLabel(frame, origin, new UiLabel
                {
                    Bounds = _closeRect,
                    Padding = new UiThickness(0f),
                    Text = "X",
                    Font = "Monospace",
                    Scale = context.Theme.TextMd,
                    Color = context.Theme.ForegroundMuted,
                    Alignment = TextAlignment.CENTER,
                    VerticalAlignment = UiVerticalAlignment.Center
                });
            }
        }

        public override UiElement HitTest(Vector2 point)
        {
            if (Visibility == UiVisibility.Collapsed || !IsEnabled)
            {
                return null;
            }

            if (Content != null)
            {
                var hit = Content.HitTest(point);
                if (hit != null)
                {
                    return hit;
                }
            }

            return ArrangedRect.Contains(point) ? this : null;
        }

        public override UiCommand OnClick(UiContext context)
        {
            var p = context.PointerLocal;
            if (_hasClose && _closeRect.Contains(p))
            {
                return CloseCommand;
            }

            if (DismissOnBackdrop && !_panelRect.Contains(p))
            {
                return CloseCommand;
            }

            return null;
        }
    }

    // Titled bordered container around a single child.
    public sealed class GroupBox : ContentControl
    {
        public string Header { get; set; }
        public Color Background { get; set; }
        public Color BorderColor { get; set; }
        public UiThickness Padding { get; set; }

        public GroupBox()
        {
            Padding = new UiThickness(6f);
        }

        private float HeaderHeight(UiContext context)
        {
            return Math.Max(18f, 22f * context.Theme.LayoutScale);
        }

        protected override Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            if (Content != null)
            {
                var hH = HeaderHeight(context);
                Content.Measure(new Vector2(Math.Max(0f, availableSize.X - Padding.Horizontal), Math.Max(0f, availableSize.Y - hH - Padding.Vertical)), context);
            }
            return availableSize;
        }

        protected override void ArrangeCore(UiRect rect, UiContext context)
        {
            if (Content != null)
            {
                var hH = HeaderHeight(context);
                Content.Arrange(new UiRect(rect.X + Padding.Left, rect.Y + hH + Padding.Top, Math.Max(0f, rect.Width - Padding.Horizontal), Math.Max(0f, rect.Height - hH - Padding.Vertical)), context);
            }
        }

        protected override void RenderSelf(MySpriteDrawFrame frame, Vector2 origin, UiContext context)
        {
            var hH = HeaderHeight(context);
            UiRenderer.DrawPanel(frame, origin, new UiPanel { Bounds = ArrangedRect, Background = Background.A > 0 ? Background : context.Theme.Background1 });
            UiRenderer.DrawOutline(frame, origin, ArrangedRect, BorderColor.A > 0 ? BorderColor : context.Theme.Border, context.Theme.StrokeThin);
            var headerRect = new UiRect(ArrangedRect.X, ArrangedRect.Y, ArrangedRect.Width, hH);
            UiRenderer.DrawPanel(frame, origin, new UiPanel { Bounds = headerRect, Background = context.Theme.Background2 });
            UiRenderer.DrawLabel(frame, origin, new UiLabel
            {
                Bounds = headerRect.Deflate(new UiThickness(Math.Max(4f, context.Theme.SpacingSm), 0f)),
                Padding = new UiThickness(0f),
                Text = Header ?? string.Empty,
                Font = "Monospace",
                Scale = context.Theme.TextSm,
                Color = context.Theme.ForegroundPrimary,
                Alignment = TextAlignment.LEFT,
                VerticalAlignment = UiVerticalAlignment.Center
            });
        }
    }

    // Collapsible section. Clicking the header emits ToggleCommand; the app flips
    // IsExpanded. Content is only measured/arranged/rendered when expanded.
    public sealed class Expander : ContentControl
    {
        public string Header { get; set; }
        public bool IsExpanded { get; set; }
        public UiCommand ToggleCommand { get; set; }

        public Expander()
        {
            Focusable = true;
        }

        private float HeaderHeight(UiContext context)
        {
            return Math.Max(22f, 26f * context.Theme.LayoutScale);
        }

        protected override Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            var hH = HeaderHeight(context);
            if (Content != null)
            {
                Content.Visibility = IsExpanded ? UiVisibility.Visible : UiVisibility.Collapsed;
            }

            if (IsExpanded && Content != null)
            {
                Content.Measure(new Vector2(availableSize.X, Math.Max(0f, availableSize.Y - hH)), context);
                return new Vector2(availableSize.X, hH + Content.DesiredSize.Y);
            }

            return new Vector2(availableSize.X, hH);
        }

        protected override void ArrangeCore(UiRect rect, UiContext context)
        {
            var hH = HeaderHeight(context);
            if (IsExpanded && Content != null)
            {
                Content.Arrange(new UiRect(rect.X, rect.Y + hH, rect.Width, Math.Max(0f, rect.Height - hH)), context);
            }
        }

        protected override void RenderSelf(MySpriteDrawFrame frame, Vector2 origin, UiContext context)
        {
            var hH = HeaderHeight(context);
            var headerRect = new UiRect(ArrangedRect.X, ArrangedRect.Y, ArrangedRect.Width, hH);
            var style = Style ?? context.Theme.ShellButtonStyle;
            var hovered = State == UiControlState.Hover || State == UiControlState.Pressed || State == UiControlState.Focused;
            UiRenderer.DrawPanel(frame, origin, new UiPanel { Bounds = headerRect, Background = hovered ? style.HoverBackground : style.Background });

            var arrowW = Math.Min(headerRect.Height, headerRect.Width * 0.15f);
            UiRenderer.DrawLabel(frame, origin, new UiLabel
            {
                Bounds = new UiRect(headerRect.X, headerRect.Y, arrowW, headerRect.Height),
                Padding = new UiThickness(0f),
                Text = IsExpanded ? "-" : "+",
                Font = "Monospace",
                Scale = context.Theme.TextMd,
                Color = context.Theme.ForegroundPrimary,
                Alignment = TextAlignment.CENTER,
                VerticalAlignment = UiVerticalAlignment.Center
            });
            UiRenderer.DrawLabel(frame, origin, new UiLabel
            {
                Bounds = new UiRect(headerRect.X + arrowW, headerRect.Y, Math.Max(0f, headerRect.Width - arrowW), headerRect.Height),
                Padding = new UiThickness(0f),
                Text = Header ?? string.Empty,
                Font = "Monospace",
                Scale = context.Theme.TextSm,
                Color = context.Theme.ForegroundPrimary,
                Alignment = TextAlignment.LEFT,
                VerticalAlignment = UiVerticalAlignment.Center
            });
        }

        public override UiCommand OnClick(UiContext context)
        {
            return context.PointerLocal.Y <= ArrangedRect.Y + HeaderHeight(context) ? ToggleCommand : null;
        }
    }

    // One page of a TabControl.
    public sealed class TabItem : ContentControl
    {
        public string Header { get; set; }
    }

    // Tab strip header + swapped body. Children are TabItems; only the selected one is
    // measured, arranged, rendered and hit-tested. Tapping a header emits the tab index.
    public sealed class TabControl : Control
    {
        private UiRect _headerRect;
        private UiRect _bodyRect;

        public int SelectedIndex { get; set; }
        public UiCommand Command { get; set; }

        public TabControl()
        {
            Focusable = true;
        }

        public void AddTab(TabItem item)
        {
            AddChild(item);
        }

        private float HeaderHeight(UiContext context)
        {
            return Math.Max(26f, 30f * context.Theme.LayoutScale);
        }

        private TabItem Selected()
        {
            return SelectedIndex >= 0 && SelectedIndex < Children.Count ? Children[SelectedIndex] as TabItem : null;
        }

        private int HeaderIndexAt(Vector2 point)
        {
            var count = Children.Count;
            if (count == 0 || !_headerRect.Contains(point))
            {
                return -1;
            }

            var tabW = _headerRect.Width / count;
            if (tabW <= 0f)
            {
                return -1;
            }

            var index = (int)((point.X - _headerRect.X) / tabW);
            return index >= 0 && index < count ? index : -1;
        }

        protected override Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            var sel = Selected();
            if (sel != null)
            {
                sel.Measure(new Vector2(availableSize.X, Math.Max(0f, availableSize.Y - HeaderHeight(context))), context);
            }
            return availableSize;
        }

        protected override void ArrangeCore(UiRect rect, UiContext context)
        {
            var hH = HeaderHeight(context);
            _headerRect = new UiRect(rect.X, rect.Y, rect.Width, hH);
            _bodyRect = new UiRect(rect.X, rect.Y + hH, rect.Width, Math.Max(0f, rect.Height - hH));
            var sel = Selected();
            if (sel != null)
            {
                sel.Arrange(_bodyRect, context);
            }
        }

        public override void Render(MySpriteDrawFrame frame, Vector2 origin, UiContext context)
        {
            if (Visibility == UiVisibility.Collapsed)
            {
                return;
            }

            RenderSelf(frame, origin, context);
            var sel = Selected();
            if (sel != null)
            {
                sel.Render(frame, origin, context);
            }
        }

        protected override void RenderSelf(MySpriteDrawFrame frame, Vector2 origin, UiContext context)
        {
            var count = Children.Count;
            if (count == 0)
            {
                return;
            }

            var style = Style ?? context.Theme.ShellButtonStyle;
            var tabW = _headerRect.Width / count;
            var activeIndex = IsHovered ? HeaderIndexAt(context.PointerLocal) : -1;
            for (var i = 0; i < count; i++)
            {
                var item = Children[i] as TabItem;
                var rect = new UiRect(_headerRect.X + (i * tabW), _headerRect.Y, tabW, _headerRect.Height);
                var selected = i == SelectedIndex;
                UiRenderer.DrawPanel(frame, origin, new UiPanel { Bounds = rect, Background = selected || i == activeIndex ? style.HoverBackground : style.Background });
                if (selected)
                {
                    var ah = Math.Max(1f, rect.Height * 0.1f);
                    UiRenderer.DrawPanel(frame, origin, new UiPanel { Bounds = new UiRect(rect.X, rect.Bottom - ah, rect.Width, ah), Background = context.Theme.Accent });
                }

                UiRenderer.DrawLabel(frame, origin, new UiLabel
                {
                    Bounds = rect,
                    Padding = new UiThickness(Math.Max(2f, context.Theme.SpacingSm), 0f),
                    Text = item != null ? (item.Header ?? string.Empty) : string.Empty,
                    Font = "Monospace",
                    Scale = context.Theme.TextSm,
                    Color = selected ? context.Theme.ForegroundPrimary : context.Theme.ForegroundMuted,
                    Alignment = TextAlignment.CENTER,
                    VerticalAlignment = UiVerticalAlignment.Center
                });
            }
        }

        public override UiElement HitTest(Vector2 point)
        {
            if (Visibility == UiVisibility.Collapsed || !IsEnabled)
            {
                return null;
            }

            if (_headerRect.Contains(point))
            {
                return this;
            }

            var sel = Selected();
            if (sel != null)
            {
                var hit = sel.HitTest(point);
                if (hit != null)
                {
                    return hit;
                }
            }

            return ArrangedRect.Contains(point) ? this : null;
        }

        public override UiCommand OnClick(UiContext context)
        {
            if (Command == null)
            {
                return null;
            }

            var index = HeaderIndexAt(context.PointerLocal);
            return index < 0 ? null : UiCommand.Create(Command.IntentType, Command.AppId, index, Command.Data);
        }
    }
}
