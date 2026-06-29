using System.Collections.Generic;
using VRageMath;

namespace AGS
{
    public sealed class UiFrame
    {
        public UiFrame()
        {
            StartItems = new List<AppEntry>();
            DesktopItems = new List<AppEntry>();
            Lines = new List<string>();
            Providers = new List<ProviderSnapshot>();
            Alerts = new List<AlertRecord>();
            Todos = new List<string>();
            Security = new SecurityScreenModel();
            Engineering = new EngineeringScreenModel();
        }

        public long ConstructId { get; set; }
        public BootPhase BootPhase { get; set; }
        public bool HasController { get; set; }
        public int ControllerCount { get; set; }
        public string ControllerName { get; set; }
        public string ActiveAppId { get; set; }
        public string ActiveAppTitle { get; set; }
        public StationRole AssignedRole { get; set; }
        public string StationTitle { get; set; }
        public string StationPurpose { get; set; }
        public RoleId AccessRole { get; set; }
        public bool StartMenuOpen { get; set; }
        public int StartMenuPage { get; set; }
        public bool SettingsOpen { get; set; }
        public bool IsStandby { get; set; }
        public bool AlwaysOn { get; set; }
        public bool PendingAlwaysOn { get; set; }
        public int TimeoutMinutes { get; set; }
        public int PendingTimeoutMinutes { get; set; }
        public string Message { get; set; }
        public Color AccentColor { get; set; }
        public Color AccentGlow { get; set; }
        public PointerState Pointer { get; set; }
        public SecurityScreenModel Security { get; set; }
        public EngineeringScreenModel Engineering { get; set; }
        public List<AppEntry> StartItems { get; private set; }
        public List<AppEntry> DesktopItems { get; private set; }
        public List<string> Lines { get; private set; }
        public List<ProviderSnapshot> Providers { get; private set; }
        public List<AlertRecord> Alerts { get; private set; }
        public List<string> Todos { get; private set; }

        public static UiFrame CreateOffline()
        {
            return new UiFrame
            {
                BootPhase = BootPhase.MissingController,
                Message = "Runtime offline",
                AssignedRole = StationRole.Command,
                StationTitle = StationCatalog.GetTitle(StationRole.Command),
                StationPurpose = StationCatalog.GetPurpose(StationRole.Command),
                AccessRole = RoleId.Unknown,
                AccentColor = new Color(120, 136, 164),
                AccentGlow = new Color(120, 136, 164, 44),
                Security = new SecurityScreenModel(),
                Engineering = new EngineeringScreenModel()
            };
        }
    }

    public sealed class AppEntry
    {
        public AppEntry(string id, string title)
        {
            Id = id;
            Title = title;
        }

        public string Id { get; private set; }
        public string Title { get; private set; }
    }
}
