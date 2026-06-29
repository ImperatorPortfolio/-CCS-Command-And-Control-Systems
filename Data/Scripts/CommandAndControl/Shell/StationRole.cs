namespace AGS
{
    public enum StationRole
    {
        Command,
        Operations,
        Engineering,
        Helm,
        Navigation,
        Tactical,
        Security,
        Weapons,
        Sensors,
        DamageControl,
        CommsStatus,
        System
    }

    public static class StationCatalog
    {
        public static StationRole[] All =
        {
            StationRole.Command,
            StationRole.Operations,
            StationRole.Engineering,
            StationRole.Helm,
            StationRole.Navigation,
            StationRole.Tactical,
            StationRole.Security,
            StationRole.Weapons,
            StationRole.Sensors,
            StationRole.DamageControl,
            StationRole.CommsStatus,
            StationRole.System
        };

        public static string GetTitle(StationRole role)
        {
            switch (role)
            {
                case StationRole.Command: return "Command";
                case StationRole.Operations: return "Operations";
                case StationRole.Engineering: return "Engineering";
                case StationRole.Helm: return "Helm";
                case StationRole.Navigation: return "Navigation";
                case StationRole.Tactical: return "Tactical";
                case StationRole.Security: return "Security";
                case StationRole.Weapons: return "Weapons";
                case StationRole.Sensors: return "Sensors";
                case StationRole.DamageControl: return "Damage Control";
                case StationRole.CommsStatus: return "Comms / Status";
                case StationRole.System: return "System";
                default: return "Station";
            }
        }

        public static string GetPurpose(StationRole role)
        {
            switch (role)
            {
                case StationRole.Command: return "Construct command overview, alerts, and coordination.";
                case StationRole.Operations: return "Operational readiness, logistics, and systems summary.";
                case StationRole.Engineering: return "Power, fuel, routing, and subsystem health.";
                case StationRole.Helm: return "Flight posture, control readiness, and piloting systems.";
                case StationRole.Navigation: return "Waypoints, jumps, routes, and travel state.";
                case StationRole.Tactical: return "Threat picture, targeting posture, and combat readiness.";
                case StationRole.Security: return "Access posture, lockdown state, and security systems.";
                case StationRole.Weapons: return "Weapon groups, ammunition state, and fire control readiness.";
                case StationRole.Sensors: return "Sensors, cameras, contact quality, and radar feeds.";
                case StationRole.DamageControl: return "Hull integrity, damage response, and repair priorities.";
                case StationRole.CommsStatus: return "Shipwide messaging, link state, and external status.";
                case StationRole.System: return "Runtime, providers, and platform diagnostics.";
                default: return string.Empty;
            }
        }

        public static string GetDefaultAppId(StationRole role)
        {
            switch (role)
            {
                case StationRole.Command: return CommandApp.IdValue;
                case StationRole.Operations: return OperationsApp.IdValue;
                case StationRole.Engineering: return EngineeringApp.IdValue;
                case StationRole.Helm: return HelmApp.IdValue;
                case StationRole.Navigation: return NavigationApp.IdValue;
                case StationRole.Tactical: return TacticalApp.IdValue;
                case StationRole.Security: return SecurityApp.IdValue;
                case StationRole.Weapons: return WeaponsApp.IdValue;
                case StationRole.Sensors: return SensorsApp.IdValue;
                case StationRole.DamageControl: return DamageControlApp.IdValue;
                case StationRole.CommsStatus: return CommsApp.IdValue;
                case StationRole.System: return SystemApp.IdValue;
                default: return CommandApp.IdValue;
            }
        }

        public static RoleId GetRoleId(StationRole role)
        {
            switch (role)
            {
                case StationRole.Command: return RoleId.Command;
                case StationRole.Operations: return RoleId.Operations;
                case StationRole.Engineering: return RoleId.Engineering;
                case StationRole.Helm: return RoleId.Helm;
                case StationRole.Navigation: return RoleId.Navigation;
                case StationRole.Tactical: return RoleId.Tactical;
                case StationRole.Security: return RoleId.Security;
                case StationRole.Weapons: return RoleId.Tactical;
                case StationRole.Sensors: return RoleId.Operations;
                case StationRole.DamageControl: return RoleId.Engineering;
                case StationRole.CommsStatus: return RoleId.Operations;
                case StationRole.System: return RoleId.Admin;
                default: return RoleId.Unknown;
            }
        }

        public static StationRole GetRoleForApp(string appId)
        {
            for (var i = 0; i < All.Length; i++)
            {
                if (GetDefaultAppId(All[i]) == appId)
                {
                    return All[i];
                }
            }

            return StationRole.Command;
        }
    }
}
