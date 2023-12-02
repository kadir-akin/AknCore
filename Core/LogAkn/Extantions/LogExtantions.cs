using Core.LogAkn.Abstract;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.LogAkn.Extantions
{
    public static class LogExtantions
    {
        
        public static Task LogDebug(this ILogService logger, EventId eventId, System.Exception exception, string message, params object[] args)
        {
            logger.LogAsync(LogLevel.Debug, eventId, exception, message, args);
            return Task.CompletedTask;
        }
        public static Task LogDebug(this ILogService logger, EventId eventId, string message, params object[] args)
        {
            logger.LogAsync(LogLevel.Debug, eventId, message, args);
            return Task.CompletedTask;
        }


        public static Task LogDebug(this ILogService logger, System.Exception exception, string message, params object[] args)
        {
            logger.LogAsync(LogLevel.Debug, exception, message, args);
            return Task.CompletedTask;
        }

        public static Task LogDebug(this ILogService logger, string message, params object[] args)
        {
            logger.LogAsync(LogLevel.Debug, message, args);
            return Task.CompletedTask;
        }

        public static Task LogTraceAsync(this ILogService logger, EventId eventId, System.Exception exception, string message, params object[] args)
        {
            logger.LogAsync(LogLevel.Trace, eventId, exception, message, args);
            return Task.CompletedTask;
        }

        public static Task LogTraceAsync(this ILogService logger, EventId eventId, string message, params object[] args)
        {
            logger.LogAsync(LogLevel.Trace, eventId, message, args);
            return Task.CompletedTask;
        }


        public static Task LogTraceAsync(this ILogService logger, System.Exception exception, string message, params object[] args)
        {
            logger.LogAsync(LogLevel.Trace, exception, message, args);
            return Task.CompletedTask;
        }

        public static Task LogTraceAsync(this ILogService logger, string message, params object[] args)
        {
            logger.LogAsync(LogLevel.Trace, message, args);
            return Task.CompletedTask;
        }

        public static Task LogInformationAsync(this ILogService logger, EventId eventId, System.Exception exception, string message, params object[] args)
        {
            logger.LogAsync(LogLevel.Information, eventId, exception, message, args);
            return Task.CompletedTask;
        }
        public static Task LogInformationAsync(this ILogService logger, EventId eventId, string message, params object[] args)
        {
            logger.LogAsync(LogLevel.Information, eventId, message, args);
            return Task.CompletedTask;
        }

        public static Task LogInformationAsync(this ILogService logger, System.Exception exception, string message, params object[] args)
        {
            logger.LogAsync(LogLevel.Information, exception, message, args);
            return Task.CompletedTask;
        }

        public static Task LogInformationAsync(this ILogService logger, string message, params object[] args)
        {
            logger.LogAsync(LogLevel.Information, message, args);
            return Task.CompletedTask;
        }

        public static Task LogWarningAsync(this ILogService logger, EventId eventId, System.Exception exception, string message, params object[] args)
        {
            logger.LogAsync(LogLevel.Warning, eventId, exception, message, args);
            return Task.CompletedTask;
        }

        public static Task LogWarningAsync(this ILogService logger, EventId eventId, string message, params object[] args)
        {
            logger.LogAsync(LogLevel.Warning, eventId, message, args);
            return Task.CompletedTask;
        }

        public static Task LogWarningAsync(this ILogService logger, System.Exception exception, string message, params object[] args)
        {
            logger.LogAsync(LogLevel.Warning, exception, message, args);
            return Task.CompletedTask;
        }

        public static Task LogWarningAsync(this ILogService logger, string message, params object[] args)
        {
            logger.LogAsync(LogLevel.Warning, message, args);
            return Task.CompletedTask;
        }

        public static Task LogErrorAsync(this ILogService logger, EventId eventId, System.Exception exception, string message, params object[] args)
        {
            logger.LogAsync(LogLevel.Error, eventId, exception, message, args);
            return Task.CompletedTask;
        }


        public static Task LogErrorAsync(this ILogService logger, EventId eventId, string message, params object[] args)
        {
            logger.LogAsync(LogLevel.Error, eventId, message, args);
            return Task.CompletedTask;
        }


        public static Task LogErrorAsync(this ILogService logger, System.Exception exception, string message, params object[] args)
        {
            logger.LogAsync(LogLevel.Error, exception, message, args);
            return Task.CompletedTask;
        }


        public static Task LogErrorAsync(this ILogService logger, string message, params object[] args)
        {
            logger.LogAsync(LogLevel.Error, message, args);
            return Task.CompletedTask;
        }


        public static Task LogCriticalAsync(this ILogService logger, EventId eventId, System.Exception exception, string message, params object[] args)
        {
            logger.LogAsync(LogLevel.Critical, eventId, exception, message, args);
            return Task.CompletedTask;
        }


        public static Task LogCriticalAsync(this ILogService logger, EventId eventId, string message, params object[] args)
        {
            logger.LogAsync(LogLevel.Critical, eventId, message, args);
            return Task.CompletedTask;
        }


        public static Task LogCriticalAsync(this ILogService logger, System.Exception exception, string message, params object[] args)
        {
            logger.LogAsync(LogLevel.Critical, exception, message, args);
            return Task.CompletedTask;
        }


        public static Task LogCriticalAsync(this ILogService logger, string message, params object[] args)
        {
            logger.LogAsync(LogLevel.Critical, message, args);
            return Task.CompletedTask;
        }


        public static Task LogAsync(this ILogService logger, LogLevel logLevel, string message, params object[] args)
        {
            logger.LogAsync(logLevel, 0, null, message, args);
            return Task.CompletedTask;
        }


        public static Task LogAsync(this ILogService logger, LogLevel logLevel, EventId eventId, string message, params object[] args)
        {
            logger.LogAsync(logLevel, eventId, null, message, args);
            return Task.CompletedTask;
        }


        public static Task LogAsync(this ILogService logger, LogLevel logLevel, System.Exception exception, string message, params object[] args)
        {
            logger.LogAsync(logLevel, 0, exception, message, args);
            return Task.CompletedTask;
        }


        public static Task LogAsync(this ILogService logger, LogLevel logLevel, EventId eventId, System.Exception exception, string message, params object[] args)
        {
            logger.LogAsync(logLevel, eventId, exception, message, args);
            return Task.CompletedTask;
        }

        

    }
}
