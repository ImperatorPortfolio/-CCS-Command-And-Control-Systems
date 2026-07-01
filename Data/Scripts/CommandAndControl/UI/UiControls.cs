using VRage.Game.GUI.TextPanel;
using VRageMath;

namespace AGS
{
    public sealed class UiPanel
    {
        public UiRect Bounds { get; set; }
        public Color Background { get; set; }
    }

    public sealed class UiLabel
    {
        public UiRect Bounds { get; set; }
        public UiThickness Padding { get; set; }
        public string Text { get; set; }
        public string Font { get; set; }
        public float Scale { get; set; }
        public Color Color { get; set; }
        public TextAlignment Alignment { get; set; }
        public UiVerticalAlignment VerticalAlignment { get; set; }
    }

    public sealed class UiButton
    {
        public UiRect Bounds { get; set; }
        public UiThickness Padding { get; set; }
        public string Text { get; set; }
        public string Font { get; set; }
        public float Scale { get; set; }
        public bool IsHovered { get; set; }
        public bool IsPressed { get; set; }
        public bool IsDisabled { get; set; }
        public Color Background { get; set; }
        public Color HoverBackground { get; set; }
        public Color PressedBackground { get; set; }
        public Color Foreground { get; set; }
        public TextAlignment Alignment { get; set; }
    }

    public sealed class UiTextBox
    {
        public UiRect Bounds { get; set; }
        public UiThickness Padding { get; set; }
        public string Text { get; set; }
        public string Placeholder { get; set; }
        public string Font { get; set; }
        public float Scale { get; set; }
        public bool IsFocused { get; set; }
        public Color Background { get; set; }
        public Color Border { get; set; }
        public Color Foreground { get; set; }
        public Color PlaceholderColor { get; set; }
    }

    public sealed class UiChromePanel
    {
        public UiRect Bounds { get; set; }
        public string Title { get; set; }
        public Color Background { get; set; }
        public Color Border { get; set; }
        public Color Header { get; set; }
        public Color TitleColor { get; set; }
    }

    public sealed class UiSectionHeader
    {
        public UiRect Bounds { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public Color Background { get; set; }
        public Color Accent { get; set; }
        public Color TitleColor { get; set; }
        public Color DetailColor { get; set; }
    }

    public sealed class UiStatusBadge
    {
        public UiRect Bounds { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }
        public Color Background { get; set; }
        public Color Border { get; set; }
        public Color LabelColor { get; set; }
        public Color ValueColor { get; set; }
    }

    public sealed class UiBracketFrame
    {
        public UiRect Bounds { get; set; }
        public string Title { get; set; }
        public Color Border { get; set; }
        public Color Accent { get; set; }
        public Color Background { get; set; }
        public Color TitleColor { get; set; }
    }

    public sealed class UiMetricBar
    {
        public UiRect Bounds { get; set; }
        public string Label { get; set; }
        public string ValueText { get; set; }
        public float Ratio { get; set; }
        public Color LabelColor { get; set; }
        public Color TrackColor { get; set; }
        public Color FillColor { get; set; }
        public Color ValueColor { get; set; }
    }

    public sealed class UiValueTile
    {
        public UiRect Bounds { get; set; }
        public string Caption { get; set; }
        public string Value { get; set; }
        public Color Background { get; set; }
        public Color Border { get; set; }
        public Color CaptionColor { get; set; }
        public Color ValueColor { get; set; }
    }

    public sealed class UiIndicatorStrip
    {
        public UiRect Bounds { get; set; }
        public int Count { get; set; }
        public int ActiveCount { get; set; }
        public Color ActiveColor { get; set; }
        public Color InactiveColor { get; set; }
    }
}
