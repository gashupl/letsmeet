using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.PluginTelemetry;

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
            _tracingService.Trace(format, args);
            if(_logger.IsEnabled(logLevel))
            {
                _logger.Log(logLevel, format, args);
            }
        }
    }
}
