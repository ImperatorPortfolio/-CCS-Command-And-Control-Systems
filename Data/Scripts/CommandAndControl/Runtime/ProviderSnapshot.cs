namespace AGS
{
    public enum SubsystemStatus
    {
        Offline,
        Degraded,
        Online
    }

    public sealed class ProviderSnapshot
    {
        public ProviderSnapshot(string id, string title, SubsystemStatus status, string summary)
        {
            Id = id;
            Title = title;
            Status = status;
            Summary = summary;
        }

        public string Id { get; private set; }
        public string Title { get; private set; }
        public SubsystemStatus Status { get; set; }
        public string Summary { get; set; }
    }
}
