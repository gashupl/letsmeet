using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.PluginTelemetry;
using Moq;
using Pg.LetsMeet.Dataverse.Shared.Services;

namespace Pg.LetsMeet.Dataverse.Tests.Shared
{
    public class DataverseTestBase
    {
        public Mock<ITracingService> TracingServiceMock { get; set; }
        public Mock<ILogger> LoggerMock { get; set; }

        public IPluginTracingService CreateTracingService()
        {
            TracingServiceMock = new Mock<ITracingService>();
            LoggerMock = new Mock<ILogger>();

            TracingServiceMock.Setup(s => s.Trace(It.IsAny<string>()));
            LoggerMock.Setup(s => s.IsEnabled(It.IsAny<LogLevel>())).Returns(false);
            LoggerMock.Setup(l => l.Log(It.IsAny<LogLevel>(), It.IsAny<string>()));

            return new PluginTracingService(TracingServiceMock.Object, LoggerMock.Object);
        }
    }
}
