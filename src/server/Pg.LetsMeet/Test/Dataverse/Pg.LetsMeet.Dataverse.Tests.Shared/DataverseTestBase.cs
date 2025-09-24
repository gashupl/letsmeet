using Microsoft.Xrm.Sdk;
using Moq;

namespace Pg.LetsMeet.Dataverse.Tests.Shared
{
    public class DataverseTestBase
    {
        public Mock<ITracingService> TracingServiceMock { get; set; }

        public ITracingService CreateTracingService()
        {
            TracingServiceMock = new Mock<ITracingService>();
            TracingServiceMock.Setup(s => s.Trace(It.IsAny<string>())); 

            return TracingServiceMock.Object;   
        }
    }
}
