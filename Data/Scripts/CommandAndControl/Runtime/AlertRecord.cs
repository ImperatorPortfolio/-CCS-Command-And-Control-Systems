namespace AGS
{
    public enum AlertSeverity
    {
        Info,
        Warning,
        Critical
    }

    public sealed class AlertRecord
    {
        public AlertRecord(string id, string message, AlertSeverity severity)
        {
            Id = id;
            Message = message;
            Severity = severity;
        }

        public string Id { get; private set; }
        public string Message { get; private set; }
        public AlertSeverity Severity { get; private set; }
    }

    public sealed class ContactRecord
    {
        public ContactRecord(string label, string detail)
        {
            Label = label;
            Detail = detail;
        }

        public string Label { get; private set; }
        public string Detail { get; private set; }
    }
}
