using UnityEngine;

namespace EntertainmentFramework.InternalLoggers
{
    public static class InternalLogger
    {
        private const bool IsLogging = true; // IMPORTANT. Change to false before deploying.

        private static Logger loggerObject; // The Logger Object for logging.

        /// <summary>
        /// Log a custom message.
        /// </summary>
        /// <param name="logMessage">Meesage of the log.</param>
        public static void Log(string logMessage)
        {
            if (!IsLogging)
                return;

            // If loggerObject is null then initialize it with default log handler.
            if (loggerObject == null)
            {
                loggerObject = new Logger(Debug.unityLogger.logHandler);
            }
            loggerObject.Log(logMessage); // calling the log function with Log Type, Log Tag and Log Message.
        }

        /// <summary>
        /// Log a custom message
        /// </summary>
        /// <param name="logMessage">Meesage of the log.</param>
        public static void Log(object logMessage)
        {
            if (!IsLogging)
            {
                return;
            }
            // If loggerObject is null then initialize it with default log handler.
            if (loggerObject == null)
            {
                loggerObject = new Logger(Debug.unityLogger.logHandler);
            }
            loggerObject.Log(logMessage); // calling the log function with Log Message.
        }

        /// <summary>
        /// Log a custom error message
        /// </summary>
        /// <param name="logMessage">Meesage of the log.</param>
        public static void LogError(object logMessage)
        {
            if (!IsLogging)
            {
                return;
            }
            // If loggerObject is null then initialize it with default log handler.
            if (loggerObject == null)
            {
                loggerObject = new Logger(Debug.unityLogger.logHandler);
            }
            loggerObject.LogError(Constants.EntertainmentInternalLogger, logMessage); // calling the log function with Log error Message.
        }
    }
}