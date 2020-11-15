namespace CustomerApi.Logging
{
    public interface ILogger
    {
        /// <summary>
        /// Logs out information data
        /// </summary>
        /// <param name="data">The data to log</param>
        void LogInformation(string data);

        /// <summary>
        /// Logs out error data
        /// </summary>
        /// <param name="data">The data to log</param>
        void LogError(string data);
    }
}
