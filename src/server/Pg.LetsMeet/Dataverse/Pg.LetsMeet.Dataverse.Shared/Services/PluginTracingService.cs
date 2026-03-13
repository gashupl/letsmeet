using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.PluginTelemetry;
using System.Text.RegularExpressions;

namespace Pg.LetsMeet.Dataverse.Shared.Services
{
    public class PluginTracingService : IPluginTracingService
    {
        private readonly ILogger _logger;
        private readonly ITracingService _tracingService;

        public PluginTracingService(ITracingService tracingService, ILogger logger) 
        { 
            _tracingService = tracingService;
            _logger = logger;
        }

        public void Trace(LogLevel logLevel, string format, params object[] args)
        {
            // Convert named placeholders to positional for ITracingService (uses String.Format)
            var positionalFormat = ConvertToPositionalFormat(format, args?.Length ?? 0);
            _tracingService.Trace(positionalFormat, args);

            // Use original format with named placeholders for structured logging (Application Insights)
            if(_logger.IsEnabled(logLevel))
            {
                _logger.Log(logLevel, format, args);
            }
        }

        /// <summary>
        /// Converts named placeholders like {text1}, {text2} to positional placeholders like {0}, {1}
        /// for compatibility with String.Format used by ITracingService.
        /// </summary>
        private string ConvertToPositionalFormat(string format, int argCount)
        {
            if (string.IsNullOrEmpty(format) || argCount == 0)
            {
                return format;
            }

            int index = 0;
            return Regex.Replace(format, @"\{([a-zA-Z_][a-zA-Z0-9_]*)\}", match =>
            {
                if (index < argCount)
                {
                    return "{" + index++ + "}";
                }
                return match.Value;
            });
        }
    }
}
