namespace AGS
{
    public sealed class CommandIntent
    {
        public CommandIntent(string id, string value)
        {
            Id = id ?? string.Empty;
            Value = value ?? string.Empty;
        }

        public string Id { get; private set; }
        public string Value { get; private set; }
    }

    public sealed class CommandResult
    {
        public CommandResult(bool success, string message)
        {
            Success = success;
            Message = message ?? string.Empty;
        }

        public bool Success { get; private set; }
        public string Message { get; private set; }
    }

    public sealed class CommandContext
    {
        public CommandContext(GridRuntime runtime, ShellSession session)
        {
            Runtime = runtime;
            Session = session;
        }

        public GridRuntime Runtime { get; private set; }
        public ShellSession Session { get; private set; }
    }
}
