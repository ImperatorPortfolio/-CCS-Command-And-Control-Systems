namespace AGS
{
    public sealed class StationAssignment
    {
        public StationAssignment(string screenKey, StationRole role, string appId)
        {
            ScreenKey = screenKey;
            Role = role;
            AppId = appId;
        }

        public string ScreenKey { get; private set; }
        public StationRole Role { get; private set; }
        public string AppId { get; private set; }
    }
}
