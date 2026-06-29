using System;
using Sandbox.Game.GameSystems.TextSurfaceScripts;
using VRage.Game.GUI.TextPanel;
using VRageMath;

namespace AGS
{
    public sealed class TextBlock : Control
    {
        public string Text { get; set; }
        public string Font { get; set; }
        public float Scale { get; set; }
        public Color Color { get; set; }
        public TextAlignment Alignment { get; set; }
        public UiVerticalAlignment TextVerticalAlignment { get; set; }
        public UiThickness Padding { get; set; }

        public TextBlock()
        {
            Font = "Monospace";
            Scale = 0.42f;
            Color = Color.White;
            Alignment = TextAlignment.LEFT;
            TextVerticalAlignment = UiVerticalAlignment.Center;
        }

        protected override Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            return new Vector2(Width >= 0f ? Width : availableSize.X, Height >= 0f ? Height : Math.Max(24f * context.Theme.LayoutScale, context.Theme.SpacingLg * 2f));
        }

        protected override void RenderSelf(MySpriteDrawFrame frame, Vector2 origin, UiContext context)
        {
            UiRenderer.DrawLabel(frame, origin, new UiLabel
            {
                Bounds = ArrangedRect,
                Padding = Padding,
                Text = Text ?? string.Empty,
                Font = Font,
                Scale = Scale,
                Color = Color,
                Alignment = Alignment,
                VerticalAlignment = TextVerticalAlignment
            });
        }
    }

    public sealed class Border : ContentControl
    {
        public Color Background { get; set; }
        public Color BorderBrush { get; set; }
        public float BorderThickness { get; set; }
        public UiThickness Padding { get; set; }

        protected override Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            var size = availableSize;
            if (Content != null)
            {
                var inner = new Vector2(Math.Max(0f, availableSize.X - Padding.Horizontal), Math.Max(0f, availableSize.Y - Padding.Vertical));
                Content.Measure(inner, context);
                size = new Vector2(Content.DesiredSize.X + Padding.Horizontal, Content.DesiredSize.Y + Padding.Vertical);
            }
            return size;
        }

        protected override void ArrangeCore(UiRect rect, UiContext context)
        {
            if (Content != null)
            {
                Content.Arrange(rect.Deflate(Padding), context);
            }
        }

        protected override void RenderSelf(MySpriteDrawFrame frame, Vector2 origin, UiContext context)
        {
            if (Background.A > 0)
            {
                UiRenderer.DrawPanel(frame, origin, new UiPanel { Bounds = ArrangedRect, Background = Background });
            }
            if (BorderThickness > 0f && BorderBrush.A > 0)
            {
                UiRenderer.DrawOutline(frame, origin, ArrangedRect, BorderBrush, BorderThickness);
            }
        }
    }

    public class Button : Control
    {
        public string Text { get; set; }
        public string Font { get; set; }
        public float Scale { get; set; }
        public UiThickness Padding { get; set; }
        public TextAlignment Alignment { get; set; }
        public UiCommand Command { get; set; }

        public Button()
        {
            Font = "Monospace";
            Scale = 0.42f;
            Padding = new UiThickness(4f, 3f);
            Alignment = TextAlignment.CENTER;
            Focusable = true;
        }

        protected override Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            return new Vector2(Width >= 0f ? Width : availableSize.X, Height >= 0f ? Height : Math.Max(28f * context.Theme.LayoutScale, context.Theme.SpacingLg * 2f));
        }

        protected override void RenderSelf(MySpriteDrawFrame frame, Vector2 origin, UiContext context)
        {
            var style = Style ?? context.Theme.ShellButtonStyle;
            UiRenderer.DrawButton(frame, origin, new UiButton
            {
                Bounds = ArrangedRect,
                Padding = Padding,
                Text = Text ?? string.Empty,
                Font = Font,
                Scale = Scale,
                IsHovered = State == UiControlState.Hover || State == UiControlState.Focused,
                IsPressed = State == UiControlState.Pressed,
                Background = style.Background,
                HoverBackground = style.HoverBackground,
                PressedBackground = style.PressedBackground,
                Foreground = style.Foreground,
                Alignment = Alignment
            });
        }

        public override UiCommand OnClick(UiContext context)
        {
            return IsEnabled ? Command : null;
        }
    }

    public sealed class IconButton : Button
    {
        public IconKind Icon { get; set; }
        public string Caption { get; set; }
        public bool Active { get; set; }

        protected override void RenderSelf(MySpriteDrawFrame frame, Vector2 origin, UiContext context)
        {
            if (Icon == IconKind.Ring)
            {
                var chrome = context.Theme.ForegroundPrimary;
                var glow = new Color(context.Theme.Accent.R, context.Theme.Accent.G, context.Theme.Accent.B, 80);
                UiRenderer.DrawRingButton(frame, origin, ArrangedRect, Active, State == UiControlState.Hover || State == UiControlState.Focused, State == UiControlState.Pressed, chrome, context.Theme.Accent, glow);
                return;
            }

            base.RenderSelf(frame, origin, context);
            var caption = !string.IsNullOrEmpty(Caption) ? Caption : (Icon == IconKind.Power ? "PWR" : "CFG");
            UiRenderer.DrawLabel(frame, origin, new UiLabel
            {
                Bounds = new UiRect(ArrangedRect.X, ArrangedRect.Bottom - (ArrangedRect.Height * 0.28f), ArrangedRect.Width, ArrangedRect.Height * 0.2f),
                Padding = new UiThickness(0f),
                Text = caption,
                Font = "Monospace",
                Scale = Math.Max(context.Theme.TextSm, 0.38f),
                Color = context.Theme.ForegroundPrimary,
                Alignment = TextAlignment.CENTER,
                VerticalAlignment = UiVerticalAlignment.Center
            });
        }
    }

    public sealed class ListItem : Button
    {
        public string BadgeText { get; set; }
        public bool Selected { get; set; }

        protected override void RenderSelf(MySpriteDrawFrame frame, Vector2 origin, UiContext context)
        {
            var style = Style ?? context.Theme.ShellButtonStyle;
            UiRenderer.DrawPanel(frame, origin, new UiPanel
            {
                Bounds = ArrangedRect,
                Background = State == UiControlState.Pressed ? style.PressedBackground : Selected || State == UiControlState.Hover || State == UiControlState.Focused ? style.HoverBackground : style.Background
            });

            var pad = Math.Max(context.Theme.SpacingSm, ArrangedRect.Height * 0.18f);
            var badgeSize = Math.Max(12f * context.Theme.LayoutScale, ArrangedRect.Height * 0.56f);
            var badgeRect = new UiRect(ArrangedRect.X + pad, ArrangedRect.Y + ((ArrangedRect.Height - badgeSize) * 0.5f), badgeSize, badgeSize);
            var labelRect = new UiRect(badgeRect.Right + pad, ArrangedRect.Y, ArrangedRect.Width - ((pad * 3f) + badgeSize), ArrangedRect.Height);

            UiRenderer.DrawPanel(frame, origin, new UiPanel { Bounds = badgeRect, Background = Selected ? context.Theme.ForegroundPrimary : context.Theme.Background2 });
            UiRenderer.DrawLabel(frame, origin, new UiLabel
            {
                Bounds = badgeRect,
                Padding = new UiThickness(0f),
                Text = BadgeText ?? string.Empty,
                Font = "Monospace",
                Scale = Math.Max(context.Theme.TextSm, 0.38f),
                Color = Selected ? context.Theme.Background0 : context.Theme.ForegroundPrimary,
                Alignment = TextAlignment.CENTER,
                VerticalAlignment = UiVerticalAlignment.Center
            });
            UiRenderer.DrawLabel(frame, origin, new UiLabel
            {
                Bounds = labelRect,
                Padding = new UiThickness(0f),
                Text = Text ?? string.Empty,
                Font = Font,
                Scale = Scale,
                Color = context.Theme.ForegroundPrimary,
                Alignment = TextAlignment.LEFT,
                VerticalAlignment = UiVerticalAlignment.Center
            });
        }
    }

    public sealed class ListView : ItemsControl
    {
        protected override Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            var size = Vector2.Zero;
            for (var i = 0; i < Items.Count; i++)
            {
                Items[i].Measure(availableSize, context);
                size.X = Math.Max(size.X, Items[i].DesiredSize.X);
                size.Y += Items[i].DesiredSize.Y;
            }
            return size;
        }

        protected override void ArrangeCore(UiRect rect, UiContext context)
        {
            var y = rect.Y;
            for (var i = 0; i < Items.Count; i++)
            {
                var childHeight = Math.Max(0f, Items[i].DesiredSize.Y - Items[i].Margin.Vertical);
                Items[i].Arrange(new UiRect(rect.X, y, rect.Width, childHeight), context);
                y += Items[i].ArrangedRect.Height;
            }
        }
    }

    public sealed class TextBox : Control
    {
        public string Text { get; set; }
        public string Placeholder { get; set; }
        public UiThickness Padding { get; set; }
        public float Scale { get; set; }

        public TextBox()
        {
            Focusable = true;
            Scale = 0.42f;
            Padding = new UiThickness(6f, 4f);
        }

        protected override Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            return new Vector2(Width >= 0f ? Width : availableSize.X, Height >= 0f ? Height : Math.Max(28f * context.Theme.LayoutScale, context.Theme.SpacingLg * 2f));
        }

        protected override void RenderSelf(MySpriteDrawFrame frame, Vector2 origin, UiContext context)
        {
            UiRenderer.DrawTextBox(frame, origin, new UiTextBox
            {
                Bounds = ArrangedRect,
                Padding = Padding,
                Text = Text,
                Placeholder = Placeholder,
                Font = "Monospace",
                Scale = Scale,
                IsFocused = IsFocused,
                Background = context.Theme.Background2,
                Border = context.Theme.Border,
                Foreground = context.Theme.ForegroundPrimary,
                PlaceholderColor = context.Theme.ForegroundDim
            });
        }
    }

    public sealed class ToggleButton : Button
    {
        public bool IsChecked { get; set; }

        protected override void RenderSelf(MySpriteDrawFrame frame, Vector2 origin, UiContext context)
        {
            var style = Style ?? context.Theme.ShellButtonStyle;
            UiRenderer.DrawButton(frame, origin, new UiButton
            {
                Bounds = ArrangedRect,
                Padding = Padding,
                Text = Text ?? string.Empty,
                Font = Font,
                Scale = Scale,
                IsHovered = State == UiControlState.Hover || State == UiControlState.Focused,
                IsPressed = State == UiControlState.Pressed,
                Background = IsChecked ? style.HoverBackground : style.Background,
                HoverBackground = style.HoverBackground,
                PressedBackground = style.PressedBackground,
                Foreground = style.Foreground,
                Alignment = Alignment
            });
        }
    }

    public sealed class Checkbox : Control
    {
        public string Text { get; set; }
        public bool IsChecked { get; set; }
        public UiCommand Command { get; set; }

        public Checkbox()
        {
            Focusable = true;
        }

        protected override Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            return new Vector2(Width >= 0f ? Width : availableSize.X, Height >= 0f ? Height : 34f * context.Theme.LayoutScale);
        }

        protected override void RenderSelf(MySpriteDrawFrame frame, Vector2 origin, UiContext context)
        {
            var side = Math.Min(ArrangedRect.Height, 16f * context.Theme.LayoutScale);
            var boxRect = new UiRect(ArrangedRect.X, ArrangedRect.Y + ((ArrangedRect.Height - side) * 0.5f), side, side);
            UiRenderer.DrawPanel(frame, origin, new UiPanel { Bounds = boxRect, Background = context.Theme.Background2 });
            UiRenderer.DrawOutline(frame, origin, boxRect, context.Theme.Border, context.Theme.StrokeThin);

            if (IsChecked)
            {
                var inner = boxRect.Deflate(new UiThickness(Math.Max(2f, side * 0.22f)));
                UiRenderer.DrawPanel(frame, origin, new UiPanel { Bounds = inner, Background = context.Theme.Accent });
            }

            UiRenderer.DrawLabel(frame, origin, new UiLabel
            {
                Bounds = new UiRect(boxRect.Right + Math.Max(4f, context.Theme.SpacingSm), ArrangedRect.Y, ArrangedRect.Width - boxRect.Width - Math.Max(4f, context.Theme.SpacingSm), ArrangedRect.Height),
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
            return Command == null ? null : UiCommand.Create(Command.IntentType, Command.AppId, IsChecked ? 0 : 1, Command.Data);
        }
    }

    public sealed class Slider : Control
    {
        public string Label { get; set; }
        public string ValueText { get; set; }
        public float Ratio { get; set; }
        public Color FillColor { get; set; }
        public UiCommand Command { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }

        public Slider()
        {
            Focusable = true;
            MaxValue = 100;
        }

        protected override Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            return new Vector2(Width >= 0f ? Width : availableSize.X, Height >= 0f ? Height : 40f * context.Theme.LayoutScale);
        }

        protected override void RenderSelf(MySpriteDrawFrame frame, Vector2 origin, UiContext context)
        {
            UiRenderer.DrawMetricBar(frame, origin, new UiMetricBar
            {
                Bounds = ArrangedRect,
                Label = Label,
                ValueText = ValueText,
                Ratio = Ratio,
                LabelColor = context.Theme.ForegroundMuted,
                TrackColor = context.Theme.Background2,
                FillColor = FillColor.A > 0 ? FillColor : context.Theme.Accent,
                ValueColor = context.Theme.ForegroundPrimary
            });
        }

        public override UiCommand OnClick(UiContext context)
        {
            if (Command == null)
            {
                return null;
            }

            var ratio = MathHelper.Clamp((context.PointerLocal.X - ArrangedRect.X) / Math.Max(1f, ArrangedRect.Width), 0f, 1f);
            var value = MinValue + (int)Math.Round((MaxValue - MinValue) * ratio);
            return UiCommand.Create(Command.IntentType, Command.AppId, value, Command.Data);
        }
    }

    public sealed class ScrollBar : Control
    {
        public int Value { get; set; }
        public int MaxValue { get; set; }
        public int ViewSize { get; set; }
        public bool Vertical { get; set; }
        public UiCommand Command { get; set; }

        public ScrollBar()
        {
            Focusable = true;
            Vertical = true;
        }

        protected override Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            if (Vertical)
            {
                return new Vector2(Width >= 0f ? Width : Math.Max(12f * context.Theme.LayoutScale, context.Theme.SpacingLg), Height >= 0f ? Height : availableSize.Y);
            }

            return new Vector2(Width >= 0f ? Width : availableSize.X, Height >= 0f ? Height : Math.Max(8f, context.Theme.SpacingLg));
        }

        protected override void RenderSelf(MySpriteDrawFrame frame, Vector2 origin, UiContext context)
        {
            UiRenderer.DrawPanel(frame, origin, new UiPanel { Bounds = ArrangedRect, Background = context.Theme.Background2 });
            UiRenderer.DrawOutline(frame, origin, ArrangedRect, context.Theme.Border, context.Theme.StrokeThin);

            var thumb = GetThumbRect();
            UiRenderer.DrawPanel(frame, origin, new UiPanel
            {
                Bounds = thumb,
                Background = State == UiControlState.Pressed ? context.Theme.Accent : context.Theme.AccentSoft
            });
            UiRenderer.DrawOutline(frame, origin, thumb, context.Theme.Accent, context.Theme.StrokeThin);
        }

        public override UiCommand OnClick(UiContext context)
        {
            if (Command == null || MaxValue <= 0)
            {
                return null;
            }

            float ratio;
            if (Vertical)
            {
                ratio = MathHelper.Clamp((context.PointerLocal.Y - ArrangedRect.Y) / Math.Max(1f, ArrangedRect.Height), 0f, 1f);
            }
            else
            {
                ratio = MathHelper.Clamp((context.PointerLocal.X - ArrangedRect.X) / Math.Max(1f, ArrangedRect.Width), 0f, 1f);
            }

            var value = (int)Math.Round(MaxValue * ratio);
            return UiCommand.Create(Command.IntentType, Command.AppId, value, Command.Data);
        }

        private UiRect GetThumbRect()
        {
            if (MaxValue <= 0)
            {
                return ArrangedRect;
            }

            var page = Math.Max(1, ViewSize);
            var trackLength = Vertical ? ArrangedRect.Height : ArrangedRect.Width;
            var thumbLength = Math.Max(10f, trackLength * Math.Min(1f, page / (float)(MaxValue + page)));
            var travel = Math.Max(0f, trackLength - thumbLength);
            var ratio = MathHelper.Clamp(MaxValue == 0 ? 0f : Value / (float)Math.Max(1, MaxValue), 0f, 1f);
            var offset = travel * ratio;

            if (Vertical)
            {
                return new UiRect(ArrangedRect.X, ArrangedRect.Y + offset, ArrangedRect.Width, thumbLength);
            }

            return new UiRect(ArrangedRect.X + offset, ArrangedRect.Y, thumbLength, ArrangedRect.Height);
        }
    }

    public sealed class SectionHeader : Control
    {
        public string Title { get; set; }
        public string Detail { get; set; }

        protected override Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            return new Vector2(Width >= 0f ? Width : availableSize.X, Height >= 0f ? Height : 34f * context.Theme.LayoutScale);
        }

        protected override void RenderSelf(MySpriteDrawFrame frame, Vector2 origin, UiContext context)
        {
            var style = Style ?? context.Theme.SectionStyle;
            UiRenderer.DrawSectionHeader(frame, origin, new UiSectionHeader
            {
                Bounds = ArrangedRect,
                Title = Title,
                Detail = Detail,
                Background = style.Background,
                Accent = style.Accent,
                TitleColor = style.Foreground,
                DetailColor = style.SecondaryForeground
            });
        }
    }

    public sealed class StatusBadge : Control
    {
        public string Label { get; set; }
        public string Value { get; set; }
        public Color ValueColor { get; set; }

        protected override Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            return new Vector2(Width >= 0f ? Width : availableSize.X, Height >= 0f ? Height : 34f * context.Theme.LayoutScale);
        }

        protected override void RenderSelf(MySpriteDrawFrame frame, Vector2 origin, UiContext context)
        {
            var style = Style ?? context.Theme.PanelStyle;
            UiRenderer.DrawStatusBadge(frame, origin, new UiStatusBadge
            {
                Bounds = ArrangedRect,
                Label = Label,
                Value = Value,
                Background = style.Background,
                Border = style.Border,
                LabelColor = style.SecondaryForeground,
                ValueColor = ValueColor.A > 0 ? ValueColor : style.Foreground
            });
        }
    }

    public sealed class MetricBar : Control
    {
        public string Label { get; set; }
        public string ValueText { get; set; }
        public float Ratio { get; set; }
        public Color FillColor { get; set; }

        protected override Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            return new Vector2(Width >= 0f ? Width : availableSize.X, Height >= 0f ? Height : 38f * context.Theme.LayoutScale);
        }

        protected override void RenderSelf(MySpriteDrawFrame frame, Vector2 origin, UiContext context)
        {
            UiRenderer.DrawMetricBar(frame, origin, new UiMetricBar
            {
                Bounds = ArrangedRect,
                Label = Label,
                ValueText = ValueText,
                Ratio = Ratio,
                LabelColor = context.Theme.ForegroundMuted,
                TrackColor = context.Theme.Background2,
                FillColor = FillColor.A > 0 ? FillColor : context.Theme.Accent,
                ValueColor = context.Theme.ForegroundPrimary
            });
        }
    }

    public sealed class ValueTile : Control
    {
        public string Caption { get; set; }
        public string Value { get; set; }
        public Color ValueColor { get; set; }

        protected override Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            var height = Height >= 0f ? Height : 56f * context.Theme.LayoutScale;
            return new Vector2(Width >= 0f ? Width : availableSize.X, height);
        }

        protected override void RenderSelf(MySpriteDrawFrame frame, Vector2 origin, UiContext context)
        {
            UiRenderer.DrawValueTile(frame, origin, new UiValueTile
            {
                Bounds = ArrangedRect,
                Caption = Caption,
                Value = Value,
                Background = context.Theme.Background1,
                Border = context.Theme.Border,
                CaptionColor = context.Theme.ForegroundMuted,
                ValueColor = ValueColor.A > 0 ? ValueColor : context.Theme.ForegroundPrimary
            });
        }
    }

    public sealed class CustomVisual : Control
    {
        public Action<MySpriteDrawFrame, Vector2, UiRect, UiContext> RenderAction { get; set; }
        public UiCommand ClickCommand { get; set; }

        protected override Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            return new Vector2(Width >= 0f ? Width : availableSize.X, Height >= 0f ? Height : availableSize.Y);
        }

        protected override void RenderSelf(MySpriteDrawFrame frame, Vector2 origin, UiContext context)
        {
            if (RenderAction != null)
            {
                RenderAction(frame, origin, ArrangedRect, context);
            }
        }

        public override UiCommand OnClick(UiContext context)
        {
            return ClickCommand;
        }
    }
}

