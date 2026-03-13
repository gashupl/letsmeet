using Microsoft.Xrm.Sdk.PluginTelemetry;

namespace Pg.LetsMeet.Dataverse.Shared.Services
{
    public interface IPluginTracingService
    {
        void Trace(LogLevel logLevel, string format, params object[] args);
    }
}