using System.Collections.Generic;

namespace AGS
{
    public abstract class StationAppBase : IGridApp
    {
        protected StationAppBase(string id, string title, string purpose, StationRole stationRole, RoleId requiredRole)
        {
            Id = id;
            Title = title;
            Purpose = purpose;
            StationRole = stationRole;
            RequiredRole = requiredRole;
        }

        public string Id { get; private set; }
        public string Title { get; private set; }
        public string Purpose { get; private set; }
        public StationRole? StationRole { get; private set; }
        public RoleId RequiredRole { get; private set; }

        public virtual void Activate(AppContext context)
        {
        }

        public void Build(AppContext context, UiFrame frame)
        {
            frame.StationTitle = Title;
            frame.StationPurpose = Purpose;
            frame.Lines.Add(Purpose);
            BuildCore(context, frame);
            AppendProviders(frame, GetProviderIds());
            AppendTodos(frame, GetTodos());
        }

        protected abstract void BuildCore(AppContext context, UiFrame frame);
        protected abstract string[] GetProviderIds();
        protected abstract string[] GetTodos();

        protected void AddAlerts(UiFrame frame)
        {
            if (frame.Alerts.Count == 0)
            {
                frame.Lines.Add("Alerts: nominal");
                return;
            }

            for (var i = 0; i < frame.Alerts.Count; i++)
            {
                frame.Lines.Add("Alert: " + frame.Alerts[i].Message);
            }
        }

        private static void AppendProviders(UiFrame frame, string[] ids)
        {
            if (ids == null || ids.Length == 0)
            {
                return;
            }

            frame.Lines.Add("Providers:");
            for (var i = 0; i < ids.Length; i++)
            {
                var provider = FindProvider(frame.Providers, ids[i]);
                if (provider == null)
                {
                    frame.Lines.Add("- " + ids[i] + ": offline");
                    continue;
                }

                frame.Lines.Add("- " + provider.Title + ": " + provider.Summary);
            }
        }

        private static void AppendTodos(UiFrame frame, string[] todos)
        {
            if (todos == null || todos.Length == 0)
            {
                return;
            }

            frame.Lines.Add("TODO:");
            for (var i = 0; i < todos.Length; i++)
            {
                frame.Todos.Add(todos[i]);
                frame.Lines.Add("- " + todos[i]);
            }
        }

        private static ProviderSnapshot FindProvider(List<ProviderSnapshot> providers, string id)
        {
            for (var i = 0; i < providers.Count; i++)
            {
                if (providers[i].Id == id)
                {
                    return providers[i];
                }
            }

            return null;
        }
    }

    public sealed class CommandApp : StationAppBase
    {
        public const string IdValue = "command";

        public CommandApp() : base(IdValue, "Command", StationCatalog.GetPurpose(AGS.StationRole.Command), AGS.StationRole.Command, RoleId.Command)
        {
        }

        protected override void BuildCore(AppContext context, UiFrame frame)
        {
            frame.Lines.Add("Construct " + frame.ConstructId + " command summary");
            frame.Lines.Add("Controllers: " + frame.ControllerCount + " online");
            AddAlerts(frame);
            frame.Lines.Add("Station count: " + context.Runtime.ScreenCount);
        }

        protected override string[] GetProviderIds() { return new[] { "core", "alerts", "power", "integrity", "security" }; }
        protected override string[] GetTodos() { return new[] { "Command authorization actions", "Cross-station task routing", "Alert acknowledgement workflow" }; }
    }

    public sealed class OperationsApp : StationAppBase
    {
        public const string IdValue = "operations";

        public OperationsApp() : base(IdValue, "Operations", StationCatalog.GetPurpose(AGS.StationRole.Operations), AGS.StationRole.Operations, RoleId.Operations)
        {
        }

        protected override void BuildCore(AppContext context, UiFrame frame)
        {
            frame.Lines.Add("Operational readiness board");
            frame.Lines.Add("Shared providers: " + frame.Providers.Count);
            AddAlerts(frame);
        }

        protected override string[] GetProviderIds() { return new[] { "power", "logistics", "sensors", "alerts" }; }
        protected override string[] GetTodos() { return new[] { "Crew notices", "Production routing", "Cargo transfer workflow" }; }
    }

    public sealed class EngineeringApp : StationAppBase
    {
        public const string IdValue = "engineering";

        public EngineeringApp() : base(IdValue, "Engineering", StationCatalog.GetPurpose(AGS.StationRole.Engineering), AGS.StationRole.Engineering, RoleId.Engineering)
        {
        }

        protected override void BuildCore(AppContext context, UiFrame frame)
        {
            frame.Lines.Add("Engineering bus online");
            AddAlerts(frame);
        }

        protected override string[] GetProviderIds() { return new[] { "power", "integrity", "damage", "logistics" }; }
        protected override string[] GetTodos() { return new[] { "Power routing commands", "Hydrogen and tank graphing", "Repair priority editor" }; }
    }

    public sealed class HelmApp : StationAppBase
    {
        public const string IdValue = "helm";

        public HelmApp() : base(IdValue, "Helm", StationCatalog.GetPurpose(AGS.StationRole.Helm), AGS.StationRole.Helm, RoleId.Helm)
        {
        }

        protected override void BuildCore(AppContext context, UiFrame frame)
        {
            frame.Lines.Add("Flight station overview");
            frame.Lines.Add("Pointer routed through construct runtime");
        }

        protected override string[] GetProviderIds() { return new[] { "propulsion", "navigation", "integrity" }; }
        protected override string[] GetTodos() { return new[] { "Maneuver commands", "Docking assist", "Flight mode presets" }; }
    }

    public sealed class NavigationApp : StationAppBase
    {
        public const string IdValue = "navigation";

        public NavigationApp() : base(IdValue, "Navigation", StationCatalog.GetPurpose(AGS.StationRole.Navigation), AGS.StationRole.Navigation, RoleId.Navigation)
        {
        }

        protected override void BuildCore(AppContext context, UiFrame frame)
        {
            frame.Lines.Add("Route planning skeleton online");
            if (context.ShipState.Contacts.Count > 0)
            {
                frame.Lines.Add("Contact feed: " + context.ShipState.Contacts[0].Detail);
            }
        }

        protected override string[] GetProviderIds() { return new[] { "navigation", "sensors", "propulsion" }; }
        protected override string[] GetTodos() { return new[] { "Waypoint persistence", "Map overlay canvas", "Jump planning and commit" }; }
    }

    public sealed class TacticalApp : StationAppBase
    {
        public const string IdValue = "tactical";

        public TacticalApp() : base(IdValue, "Tactical", StationCatalog.GetPurpose(AGS.StationRole.Tactical), AGS.StationRole.Tactical, RoleId.Tactical)
        {
        }

        protected override void BuildCore(AppContext context, UiFrame frame)
        {
            frame.Lines.Add("Threat board skeleton");
            AddAlerts(frame);
        }

        protected override string[] GetProviderIds() { return new[] { "weapons", "sensors", "defense", "alerts" }; }
        protected override string[] GetTodos() { return new[] { "Target queue", "Weapon release workflow", "Threat scoring" }; }
    }

    public sealed class SecurityApp : StationAppBase
    {
        public const string IdValue = "security";

        public SecurityApp() : base(IdValue, "Security", StationCatalog.GetPurpose(AGS.StationRole.Security), AGS.StationRole.Security, RoleId.Security)
        {
        }

        protected override void BuildCore(AppContext context, UiFrame frame)
        {
            frame.Lines.Add("Access and lockdown posture");
            AddAlerts(frame);
        }

        protected override string[] GetProviderIds() { return new[] { "security", "defense", "alerts" }; }
        protected override string[] GetTodos() { return new[] { "Door zoning", "Lockdown presets", "Armed action confirmation" }; }
    }

    public sealed class WeaponsApp : StationAppBase
    {
        public const string IdValue = "weapons";

        public WeaponsApp() : base(IdValue, "Weapons", StationCatalog.GetPurpose(AGS.StationRole.Weapons), AGS.StationRole.Weapons, RoleId.Tactical)
        {
        }

        protected override void BuildCore(AppContext context, UiFrame frame)
        {
            frame.Lines.Add("Fire control skeleton");
            AddAlerts(frame);
        }

        protected override string[] GetProviderIds() { return new[] { "weapons", "power", "sensors" }; }
        protected override string[] GetTodos() { return new[] { "Weapon group editing", "Ammo dependency panels", "Manual fire authorization" }; }
    }

    public sealed class SensorsApp : StationAppBase
    {
        public const string IdValue = "sensors";

        public SensorsApp() : base(IdValue, "Sensors", StationCatalog.GetPurpose(AGS.StationRole.Sensors), AGS.StationRole.Sensors, RoleId.Operations)
        {
        }

        protected override void BuildCore(AppContext context, UiFrame frame)
        {
            frame.Lines.Add("Sensor registry skeleton");
            frame.Lines.Add("Contacts tracked: " + context.ShipState.Contacts.Count);
        }

        protected override string[] GetProviderIds() { return new[] { "sensors", "navigation", "alerts" }; }
        protected override string[] GetTodos() { return new[] { "Track filters", "Camera handoff", "Detection quality scoring" }; }
    }

    public sealed class DamageControlApp : StationAppBase
    {
        public const string IdValue = "damage";

        public DamageControlApp() : base(IdValue, "Damage Control", StationCatalog.GetPurpose(AGS.StationRole.DamageControl), AGS.StationRole.DamageControl, RoleId.Engineering)
        {
        }

        protected override void BuildCore(AppContext context, UiFrame frame)
        {
            frame.Lines.Add("Repair triage skeleton");
            AddAlerts(frame);
        }

        protected override string[] GetProviderIds() { return new[] { "damage", "integrity", "logistics" }; }
        protected override string[] GetTodos() { return new[] { "Breach queue", "Repair sequencing", "Critical block watchlist" }; }
    }

    public sealed class CommsApp : StationAppBase
    {
        public const string IdValue = "comms";

        public CommsApp() : base(IdValue, "Comms / Status", StationCatalog.GetPurpose(AGS.StationRole.CommsStatus), AGS.StationRole.CommsStatus, RoleId.Operations)
        {
        }

        protected override void BuildCore(AppContext context, UiFrame frame)
        {
            frame.Lines.Add("Construct status link panel");
            frame.Lines.Add("Shared alert count: " + frame.Alerts.Count);
        }

        protected override string[] GetProviderIds() { return new[] { "alerts", "sensors", "core" }; }
        protected override string[] GetTodos() { return new[] { "External link registry", "Message queue", "Broadcast presets" }; }
    }
}
