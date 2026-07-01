namespace AGS
{
    // Session-local interaction state for the demo program, persisted on the shell session
    // like GalleryState. In the server-authoritative model (later steps) this becomes a
    // server-owned, per-construct ProgramStateBlob.
    public sealed class DemoProgramState
    {
        public int Counter;
        public string LastAction = "Ready.";
    }

    // The reference program that proves the program-API pipe end to end:
    // manifest -> serialized ProgramFrame (inline markup + typed bindings) -> ProgramHost
    // renders via UiXmlLoader -> button emits a ProgramCommand (carried as UiIntentType
    // .ProgramAction) -> GridRuntime routes it back here -> state changes -> next frame
    // reflects it. Everything that crosses into the shell is plain serializable data; no
    // UiElement is shared, exactly as an external modpack would have to operate.
    public static class DemoProgram
    {
        public const string IdValue = "demo";

        // Attribute values must be double-quoted (the markup parser only strips '"').
        private const string Markup =
@"<View>
  <Border background=""{bg1}"" border=""{border}"" borderThickness=""1"" padding=""10"">
    <Grid rows=""40,*,46"" columns=""*"">
      <SectionHeader row=""0"" title=""{title}"" detail=""{detail}"" />
      <Stack row=""1"" orientation=""vertical"" spacing=""8"" margin=""2,10,2,10"">
        <Text text=""COUNTER"" scale=""0.4"" color=""{muted}"" />
        <Text text=""{counter}"" scale=""1.1"" color=""{accent}"" />
        <Text text=""{message}"" scale=""0.44"" color=""{muted}"" />
      </Stack>
      <Grid row=""2"" rows=""*"" columns=""*,*,*"">
        <Button column=""0"" text=""- DEC"" command=""ProgramAction"" appId=""demo"" commandData=""dec"" margin=""0,0,4,0"" />
        <Button column=""1"" text=""RESET"" command=""ProgramAction"" appId=""demo"" commandData=""reset"" margin=""2,0,2,0"" />
        <Button column=""2"" text=""+ INC"" command=""ProgramAction"" appId=""demo"" commandData=""inc"" margin=""4,0,0,0"" />
      </Grid>
    </Grid>
  </Border>
</View>";

        public static readonly ProgramManifest Manifest = new ProgramManifest
        {
            Id = IdValue,
            Title = "Demo Program",
            Purpose = "Program-API test harness",
            Version = 1,
            ApiVersion = ProgramApi.Version,
            RequiredRole = (int)RoleId.Unknown,
            Capabilities = (int)(ProgramCapability.PersistState | ProgramCapability.IssueCommands)
        };

        public static ProgramFrame BuildFrame(UiContext context)
        {
            var state = context.Frame != null ? context.Frame.DemoProgram : null;
            if (state == null)
            {
                state = new DemoProgramState();
            }

            var theme = context.Theme;
            var frame = new ProgramFrame { ViewId = IdValue, InlineMarkup = Markup, Title = "Demo Program" };
            frame.Color("bg1", theme.Background1);
            frame.Color("border", theme.Border);
            frame.Color("accent", theme.Accent);
            frame.Color("muted", theme.ForegroundMuted);
            frame.Text("title", "DEMO PROGRAM");
            frame.Text("detail", "Serialized program-API slice");
            frame.Text("counter", state.Counter.ToString());
            frame.Text("message", state.LastAction);
            return frame;
        }

        // Server-side command handling. Mirrors today's ApplyGalleryAction, but addressed
        // through the generic ProgramCommand action string rather than a hardcoded enum.
        public static void Apply(DemoProgramState state, string action, int value)
        {
            if (state == null || string.IsNullOrEmpty(action))
            {
                return;
            }

            switch (action)
            {
                case "inc":
                    state.Counter++;
                    state.LastAction = "Incremented to " + state.Counter;
                    break;
                case "dec":
                    state.Counter--;
                    state.LastAction = "Decremented to " + state.Counter;
                    break;
                case "reset":
                    state.Counter = 0;
                    state.LastAction = "Reset";
                    break;
            }
        }
    }

    // Launcher entry. Like GalleryApp it claims no station role, so opening it never
    // reassigns the station.
    public sealed class DemoProgramApp : IGridApp
    {
        public string Id { get { return DemoProgram.IdValue; } }
        public string Title { get { return "Demo Program"; } }
        public string Purpose { get { return "Program-API test harness"; } }
        public StationRole? StationRole { get { return null; } }
        public RoleId RequiredRole { get { return RoleId.Unknown; } }

        public void Activate(AppContext context)
        {
        }

        public void Build(AppContext context, UiFrame frame)
        {
            frame.StationTitle = Title;
            frame.StationPurpose = Purpose;
        }
    }
}
