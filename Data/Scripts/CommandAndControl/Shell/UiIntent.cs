namespace AGS
{
    public enum UiIntentType
    {
        ToggleStart,
        LaunchApp,
        OpenDesktop,
        ToggleSettings,
        CloseSettings,
        EnterStandby,
        WakeScreen,
        PrevStartPage,
        NextStartPage,
        ToggleAlwaysOn,
        AdjustTimeout,
        ApplySettings,
        InputKey,
        GalleryAction,
        // Generic, app-scoped action carried by appId + commandData(action) + commandValue.
        // Replaces per-feature enum entries for program-API apps (Step 3).
        ProgramAction,
        SelectSubpage,
        SelectEngineeringSubpage,
        SelectEngineeringNode,
        RunEngineeringCommand,
        SelectNode,
        RunSecurityCommand,
        SetSecurityScroll,
        AdjustSidebarWidth,
        AdjustDetailsWidth,
        CycleShipView,
        AdjustShipZoom,
        AdjustShipLayer
    }

    public sealed class UiIntent
    {
        public UiIntent(UiIntentType type, string appId = null, int value = 0, string data = null)
        {
            Type = type;
            AppId = appId ?? string.Empty;
            Value = value;
            Data = data ?? string.Empty;
        }

        public UiIntentType Type { get; private set; }
        public string AppId { get; private set; }
        public int Value { get; private set; }
        public string Data { get; private set; }
    }
}
