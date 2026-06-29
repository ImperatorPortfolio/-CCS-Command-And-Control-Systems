namespace AGS
{
    public sealed class SystemApp : IGridApp
    {
        public const string IdValue = "system";

        public string Id { get { return IdValue; } }
        public string Title { get { return "System"; } }
        public string Purpose { get { return "Runtime, provider, and platform diagnostics."; } }
        public StationRole? StationRole { get { return AGS.StationRole.System; } }
        public RoleId RequiredRole { get { return RoleId.Admin; } }

        public void Activate(AppContext context)
        {
        }

        public void Build(AppContext context, UiFrame frame)
        {
            frame.Lines.Add("Core: " + frame.ControllerName);
            frame.Lines.Add("Controllers online: " + frame.ControllerCount);
            frame.Lines.Add("Shell screens: " + context.Runtime.ScreenCount);
            frame.Lines.Add("Construct: " + frame.ConstructId);
            frame.Lines.Add("Boot phase: " + frame.BootPhase);
            frame.Lines.Add("Station role: " + StationCatalog.GetTitle(frame.AssignedRole));
            frame.Lines.Add("Provider bus: " + frame.Providers.Count + " services");
            if (frame.Alerts.Count > 0)
            {
                frame.Lines.Add("Primary alert: " + frame.Alerts[0].Message);
            }
        }
    }
}
