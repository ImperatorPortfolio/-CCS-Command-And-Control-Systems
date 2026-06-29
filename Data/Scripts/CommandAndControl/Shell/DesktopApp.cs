namespace AGS
{
    public sealed class DesktopApp : IGridApp
    {
        public const string IdValue = "desktop";

        public string Id { get { return IdValue; } }
        public string Title { get { return "Dashboard"; } }
        public string Purpose { get { return "Neutral shell dashboard and launch surface."; } }
        public StationRole? StationRole { get { return null; } }
        public RoleId RequiredRole { get { return RoleId.Unknown; } }

        public void Activate(AppContext context)
        {
        }

        public void Build(AppContext context, UiFrame frame)
        {
            frame.Lines.Add("Assigned station: " + StationCatalog.GetTitle(frame.AssignedRole));
            frame.Lines.Add(frame.StationPurpose);
            frame.Lines.Add("Providers online: " + frame.Providers.Count);
            frame.Lines.Add("Alerts: " + frame.Alerts.Count);
            frame.Lines.Add("Use the shell menu to switch station pages.");
        }
    }
}
