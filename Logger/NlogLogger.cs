using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MunterServer
{
    public static class NlogLogger
    {
        #region Public constants
        public static readonly string FatalErrorMsg = "An unexpected exception has occured";
        #endregion

        public static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public static string InitMethodName([CallerMemberName] string callerName = "")
        {
            return $"[{callerName}]";
        }
    }
    public static class LoggerManager
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        public static void LogDebug(string message) => logger.Debug(message);
        public static void LogError(string message) => logger.Error(message);
        public static void LogInfo(string message) => logger.Info(message);
        public static void LogWarn(string message) => logger.Warn(message);
    }
}
