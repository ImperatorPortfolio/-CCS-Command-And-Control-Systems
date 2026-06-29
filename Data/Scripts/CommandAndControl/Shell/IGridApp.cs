namespace AGS
{
    public interface IGridApp
    {
        string Id { get; }
        string Title { get; }
        string Purpose { get; }
        StationRole? StationRole { get; }
        RoleId RequiredRole { get; }
        void Activate(AppContext context);
        void Build(AppContext context, UiFrame frame);
    }
}
