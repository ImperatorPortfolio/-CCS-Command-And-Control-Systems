using System;
using VRage.Utils;

namespace AGS
{
    // Renders a program's serialized ProgramFrame into a live element tree (Step 2) and
    // isolates faults (Step 3): a program that produces a bad frame or throws while building
    // yields an on-screen error panel instead of taking down the whole shell render. The
    // frame's markup + bindings are fed through the existing UiXmlLoader, so programs reach
    // the same widget library as first-party views — but only through serializable data.
    public static class ProgramHost
    {
        public static UiElement Render(UiContext context, ProgramManifest manifest, ProgramFrame frame)
        {
            try
            {
                if (frame == null)
                {
                    return BuildError(context, "Program produced no frame.");
                }

                var bindings = frame.ToUiBindings();

                if (!string.IsNullOrEmpty(frame.InlineMarkup))
                {
                    // Cache the parsed markup per program id (markup is stable across frames).
                    var cacheKey = manifest != null ? manifest.Id : frame.ViewId;
                    return UiXmlLoader.BuildFromMarkup(cacheKey, frame.InlineMarkup, context, bindings);
                }

                if (!string.IsNullOrEmpty(frame.ViewId))
                {
                    // A named view: resolves to a markup file the OS ships/knows. (A view
                    // registry replaces this raw path use once more programs migrate.)
                    return UiXmlLoader.Build(frame.ViewId, context, bindings, null);
                }

                return BuildError(context, "Program frame supplied no view.");
            }
            catch (Exception exception)
            {
                var id = manifest != null ? manifest.Id : "<unknown>";
                MyLog.Default.WriteLineAndConsole($"[CCS] Program '{id}' render failed: {exception.Message}\n{exception.StackTrace}");
                return BuildError(context, "Program error: " + exception.Message);
            }
        }

        private static UiElement BuildError(UiContext context, string message)
        {
            return new Border
            {
                Background = context.Theme.Background1,
                BorderBrush = context.Theme.Danger,
                BorderThickness = 1f,
                Padding = new UiThickness(8f),
                Content = new TextBlock
                {
                    Text = message,
                    Font = "Monospace",
                    Scale = Math.Max(context.Theme.TextSm, 0.36f),
                    Color = context.Theme.Danger,
                    Wrap = true
                }
            };
        }
    }
}
