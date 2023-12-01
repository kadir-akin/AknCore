using Core.LogAkn.Abstract;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.LogAkn.Extantions
{
    public static class LogExtantions
    {
        
        public static void LogDebug(this ILogService logger, EventId eventId, System.Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Debug, eventId, exception, message, args);
        }
        public static void LogDebug(this ILogService logger, EventId eventId, string message, params object[] args)
        {
            logger.Log(LogLevel.Debug, eventId, message, args);
        }


        public static void LogDebug(this ILogService logger, System.Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Debug, exception, message, args);
        }

        public static void LogDebug(this ILogService logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Debug, message, args);
        }

        public static void LogTrace(this ILogService logger, EventId eventId, System.Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Trace, eventId, exception, message, args);
        }

        public static void LogTrace(this ILogService logger, EventId eventId, string message, params object[] args)
        {
            logger.Log(LogLevel.Trace, eventId, message, args);
        }


        public static void LogTrace(this ILogService logger, System.Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Trace, exception, message, args);
        }

        public static void LogTrace(this ILogService logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Trace, message, args);
        }

        public static void LogInformation(this ILogService logger, EventId eventId, System.Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Information, eventId, exception, message, args);
        }
        public static void LogInformation(this ILogService logger, EventId eventId, string message, params object[] args)
        {
            logger.Log(LogLevel.Information, eventId, message, args);
        }

        public static void LogInformation(this ILogService logger, System.Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Information, exception, message, args);
        }

        public static void LogInformation(this ILogService logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Information, message, args);
        }

        public static void LogWarning(this ILogService logger, EventId eventId, System.Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Warning, eventId, exception, message, args);
        }

        public static void LogWarning(this ILogService logger, EventId eventId, string message, params object[] args)
        {
            logger.Log(LogLevel.Warning, eventId, message, args);
        }

        public static void LogWarning(this ILogService logger, System.Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Warning, exception, message, args);
        }

        public static void LogWarning(this ILogService logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Warning, message, args);
        }

        public static void LogError(this ILogService logger, EventId eventId, System.Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Error, eventId, exception, message, args);
        }


        public static void LogError(this ILogService logger, EventId eventId, string message, params object[] args)
        {
            logger.Log(LogLevel.Error, eventId, message, args);
        }


        public static void LogError(this ILogService logger, System.Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Error, exception, message, args);
        }


        public static void LogError(this ILogService logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Error, message, args);
        }


        public static void LogCritical(this ILogService logger, EventId eventId, System.Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Critical, eventId, exception, message, args);
        }


        public static void LogCritical(this ILogService logger, EventId eventId, string message, params object[] args)
        {
            logger.Log(LogLevel.Critical, eventId, message, args);
        }


        public static void LogCritical(this ILogService logger, System.Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Critical, exception, message, args);
        }


        public static void LogCritical(this ILogService logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Critical, message, args);
        }


        public static void Log(this ILogService logger, LogLevel logLevel, string message, params object[] args)
        {
            logger.Log(logLevel, 0, null, message, args);
        }


        public static void Log(this ILogService logger, LogLevel logLevel, EventId eventId, string message, params object[] args)
        {
            logger.Log(logLevel, eventId, null, message, args);
        }


        public static void Log(this ILogService logger, LogLevel logLevel, System.Exception exception, string message, params object[] args)
        {
            logger.Log(logLevel, 0, exception, message, args);
        }


        public static void Log(this ILogService logger, LogLevel logLevel, EventId eventId, System.Exception exception, string message, params object[] args)
        {
            logger.Log(logLevel, eventId, exception, message, args);
        }

        

    }
}
