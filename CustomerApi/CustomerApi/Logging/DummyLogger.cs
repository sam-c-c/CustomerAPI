namespace CustomerApi.Logging
{
    /// <summary>
    /// Dummy class to represent a logger
    /// </summary>
    public class DummyLogger : ILogger
    {
        public void LogError(string data)
        {
            // Log error data. This could be a writing out to some sort of event service, app insights, file system etc.
        }

        public void LogInformation(string data)
        {
            // Log information data. This could be a writing out to some sort of event service, app insights, file system etc.
        }
    }
}
