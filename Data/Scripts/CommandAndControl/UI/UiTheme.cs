using VRageMath;

namespace AGS
{
    public enum UiTemplateKind
    {
        Default,
        ShellChrome,
        StationChrome,
        Minimal
    }

    public sealed class UiStyle
    {
        public UiTemplateKind Template { get; set; }
        public Color Background { get; set; }
        public Color HoverBackground { get; set; }
        public Color PressedBackground { get; set; }
        public Color Border { get; set; }
        public Color Foreground { get; set; }
        public Color SecondaryForeground { get; set; }
        public Color Accent { get; set; }
    }

    public sealed class UiBinding
    {
        public string Key { get; set; }
        public string StringValue { get; set; }
        public int IntValue { get; set; }
        public float FloatValue { get; set; }
        public bool BoolValue { get; set; }
    }

    public sealed class UiTheme
    {
        public float LayoutScale { get; private set; }
        public Color Background0 { get; private set; }
        public Color Background1 { get; private set; }
        public Color Background2 { get; private set; }
        public Color ForegroundPrimary { get; private set; }
        public Color ForegroundMuted { get; private set; }
        public Color ForegroundDim { get; private set; }
        public Color Accent { get; private set; }
        public Color AccentSoft { get; private set; }
        public Color Success { get; private set; }
        public Color Warning { get; private set; }
        public Color Danger { get; private set; }
        public Color Border { get; private set; }
        public float SpacingXs { get; private set; }
        public float SpacingSm { get; private set; }
        public float SpacingMd { get; private set; }
        public float SpacingLg { get; private set; }
        public float TextXs { get; private set; }
        public float TextSm { get; private set; }
        public float TextMd { get; private set; }
        public float TextLg { get; private set; }
        public float StrokeThin { get; private set; }
        public float StrokeNormal { get; private set; }
        public float StrokeHeavy { get; private set; }

        public UiStyle ShellButtonStyle { get; private set; }
        public UiStyle PanelStyle { get; private set; }
        public UiStyle SectionStyle { get; private set; }

        public static UiTheme Create(UiFrame frame, float layoutScale)
        {
            var accent = frame != null ? frame.AccentColor : new Color(120, 136, 164);
            var border = ScaleColor(accent, 0.82f, 96);
            var accentSoft = ScaleColor(accent, 0.48f, 32);

            var theme = new UiTheme();
            theme.LayoutScale = layoutScale;
            theme.Background0 = new Color(16, 16, 16);
            theme.Background1 = new Color(12, 12, 12);
            theme.Background2 = new Color(22, 22, 22);
            theme.ForegroundPrimary = new Color(234, 226, 214);
            theme.ForegroundMuted = new Color(164, 146, 128);
            theme.ForegroundDim = new Color(112, 102, 92);
            theme.Accent = accent;
            theme.AccentSoft = accentSoft;
            theme.Success = new Color(118, 214, 132);
            theme.Warning = new Color(244, 176, 86);
            theme.Danger = new Color(236, 86, 92);
            theme.Border = border;
            theme.SpacingXs = 3f * layoutScale;
            theme.SpacingSm = 6f * layoutScale;
            theme.SpacingMd = 10f * layoutScale;
            theme.SpacingLg = 16f * layoutScale;
            theme.TextXs = 0.32f + (layoutScale * 0.08f);
            theme.TextSm = 0.4f + (layoutScale * 0.1f);
            theme.TextMd = 0.5f + (layoutScale * 0.12f);
            theme.TextLg = 0.64f + (layoutScale * 0.15f);
            theme.StrokeThin = 1f;
            theme.StrokeNormal = 1f + (layoutScale * 0.15f);
            theme.StrokeHeavy = 2f + (layoutScale * 0.2f);

            theme.ShellButtonStyle = new UiStyle
            {
                Template = UiTemplateKind.ShellChrome,
                Background = new Color(accent.R / 3, accent.G / 3, accent.B / 3),
                HoverBackground = new Color(accent.R / 2, accent.G / 2, accent.B / 2),
                PressedBackground = new Color((byte)System.Math.Min(255, accent.R + 28), (byte)System.Math.Min(255, accent.G + 28), (byte)System.Math.Min(255, accent.B + 28)),
                Border = border,
                Foreground = theme.ForegroundPrimary,
                SecondaryForeground = theme.ForegroundMuted,
                Accent = accent
            };
            theme.PanelStyle = new UiStyle
            {
                Template = UiTemplateKind.StationChrome,
                Background = theme.Background1,
                HoverBackground = theme.Background1,
                PressedBackground = theme.Background2,
                Border = border,
                Foreground = theme.ForegroundPrimary,
                SecondaryForeground = theme.ForegroundMuted,
                Accent = accent
            };
            theme.SectionStyle = new UiStyle
            {
                Template = UiTemplateKind.StationChrome,
                Background = theme.Background0,
                HoverBackground = theme.Background0,
                PressedBackground = theme.Background0,
                Border = border,
                Foreground = theme.ForegroundPrimary,
                SecondaryForeground = theme.ForegroundDim,
                Accent = accentSoft
            };
            return theme;
        }

        private static Color ScaleColor(Color color, float factor, byte minimum)
        {
            return new Color(Scale(color.R, factor, minimum), Scale(color.G, factor, minimum), Scale(color.B, factor, minimum));
        }

        private static byte Scale(byte channel, float factor, byte minimum)
        {
            var value = (int)(channel * factor);
            if (value < minimum)
            {
                value = minimum;
            }
            if (value > 255)
            {
                value = 255;
            }
            return (byte)value;
        }
    }
}


