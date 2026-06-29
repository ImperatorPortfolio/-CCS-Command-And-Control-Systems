namespace AGS
{
    public interface ISystemProvider
    {
        string Id { get; }
        string Title { get; }
        void Update(ShipContext context, ShipState state);
    }
}
