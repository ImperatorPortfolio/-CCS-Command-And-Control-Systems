namespace AGS
{
    public sealed class StationState
    {
        public StationState(string key)
        {
            Key = key;
            AssignedRole = StationRole.Command;
            PreferredAppId = StationCatalog.GetDefaultAppId(AssignedRole);
            AlwaysOn = true;
            TimeoutMinutes = 5;
            SecuritySubpage = AGS.SecuritySubpage.Overview;
            SecuritySelectedId = "feature:overview";
            SecurityScrollOffset = 0;
            SecurityShipView = ShipView.Top;
            SecurityShipZoomStep = 0;
            SecurityShipLayer = int.MaxValue;
            EngineeringSubpage = AGS.EngineeringSubpage.Power;
            EngineeringSelectedId = string.Empty;
            EngineeringScrollOffset = 0;
            Layout = new ScreenLayoutState();
        }

        public string Key { get; private set; }
        public StationRole AssignedRole { get; set; }
        public string PreferredAppId { get; set; }
        public bool AlwaysOn { get; set; }
        public int TimeoutMinutes { get; set; }
        public SecuritySubpage SecuritySubpage { get; set; }
        public string SecuritySelectedId { get; set; }
        public int SecurityScrollOffset { get; set; }
        public ShipView SecurityShipView { get; set; }
        public int SecurityShipZoomStep { get; set; }
        public int SecurityShipLayer { get; set; }
        public EngineeringSubpage EngineeringSubpage { get; set; }
        public string EngineeringSelectedId { get; set; }
        public int EngineeringScrollOffset { get; set; }
        public ScreenLayoutState Layout { get; private set; }
    }
}
