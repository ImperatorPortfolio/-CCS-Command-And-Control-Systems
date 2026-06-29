using VRage.Game.ModAPI;

namespace AGS
{
    public sealed class ShellSession
    {
        private const int TicksPerMinute = 360;

        public ShellSession(string key, IMyCubeBlock block, Sandbox.ModAPI.IMyTextSurface surface)
        {
            Key = key;
            State = new ShellState();
            Bind(block, surface);
        }

        public string Key { get; private set; }
        public IMyCubeBlock Block { get; private set; }
        public Sandbox.ModAPI.IMyTextSurface Surface { get; private set; }
        public ShellState State { get; private set; }
        public bool IsValid { get { return Block != null && Surface != null && !Block.MarkedForClose && !Block.Closed; } }

        public void Bind(IMyCubeBlock block, Sandbox.ModAPI.IMyTextSurface surface)
        {
            Block = block;
            Surface = surface;
        }

        public void SyncStation(StationState station)
        {
            if (station == null)
            {
                return;
            }

            State.AlwaysOn = station.AlwaysOn;
            State.PendingAlwaysOn = station.AlwaysOn;
            State.TimeoutMinutes = station.TimeoutMinutes;
            State.PendingTimeoutMinutes = station.TimeoutMinutes;
            if (string.IsNullOrEmpty(State.ActiveAppId))
            {
                State.ActiveAppId = station.PreferredAppId;
            }
        }

        public void UpdateBoot(bool hasController)
        {
            State.Boot.Update(hasController);
            if (!hasController)
            {
                State.StartMenuOpen = false;
                State.SettingsOpen = false;
                State.IsStandby = false;
                State.IdleTicks = 0;
            }
        }

        public void UpdatePower(bool activeOnScreen, bool wakeInteraction)
        {
            if (State.Boot.Phase != BootPhase.Desktop)
            {
                State.IsStandby = false;
                State.IdleTicks = 0;
                return;
            }

            if (State.IsStandby)
            {
                if (wakeInteraction)
                {
                    Wake();
                }
                return;
            }

            if (State.AlwaysOn)
            {
                State.IdleTicks = 0;
                return;
            }

            if (activeOnScreen)
            {
                State.IdleTicks = 0;
                return;
            }

            State.IdleTicks++;
            if (State.IdleTicks >= State.TimeoutMinutes * TicksPerMinute)
            {
                EnterStandby();
            }
        }

        public void ResetDraftSettings()
        {
            State.PendingAlwaysOn = State.AlwaysOn;
            State.PendingTimeoutMinutes = State.TimeoutMinutes;
        }

        public void ApplyDraftSettings()
        {
            State.AlwaysOn = State.PendingAlwaysOn;
            State.TimeoutMinutes = State.PendingTimeoutMinutes;
            State.IdleTicks = 0;
            if (State.AlwaysOn)
            {
                State.IsStandby = false;
            }
        }

        public void Wake()
        {
            State.IsStandby = false;
            State.IdleTicks = 0;
        }

        public void EnterStandby()
        {
            State.IsStandby = true;
            State.StartMenuOpen = false;
            State.SettingsOpen = false;
            State.IdleTicks = 0;
        }

        public void ResetShell(StationState station)
        {
            State.ActiveAppId = station != null ? station.PreferredAppId : CommandApp.IdValue;
            State.StartMenuOpen = false;
            State.StartMenuPage = 0;
            State.SettingsOpen = false;
            State.IsStandby = false;
            State.IdleTicks = 0;
            SyncStation(station);
            ResetDraftSettings();
        }
    }
}
