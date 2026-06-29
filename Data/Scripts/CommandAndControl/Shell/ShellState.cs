namespace AGS
{
    public sealed class ShellState
    {
        public ShellState()
        {
            Boot = new BootState();
            ActiveAppId = CommandApp.IdValue;
            AlwaysOn = true;
            PendingAlwaysOn = true;
            TimeoutMinutes = 5;
            PendingTimeoutMinutes = 5;
        }

        public BootState Boot { get; private set; }
        public string ActiveAppId { get; set; }
        public bool StartMenuOpen { get; set; }
        public int StartMenuPage { get; set; }
        public bool SettingsOpen { get; set; }
        public bool IsStandby { get; set; }
        public bool AlwaysOn { get; set; }
        public bool PendingAlwaysOn { get; set; }
        public int TimeoutMinutes { get; set; }
        public int PendingTimeoutMinutes { get; set; }
        public int IdleTicks { get; set; }
    }
}
