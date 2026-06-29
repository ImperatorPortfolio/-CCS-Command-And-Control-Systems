namespace AGS
{
    public sealed class AppContext
    {
        public AppContext(GridRuntime runtime, ShellSession session, StationState station, ShipState shipState)
        {
            Runtime = runtime;
            Session = session;
            Station = station;
            ShipState = shipState;
        }

        public GridRuntime Runtime { get; private set; }
        public ShellSession Session { get; private set; }
        public StationState Station { get; private set; }
        public ShipState ShipState { get; private set; }
    }
}
