using Microsoft.Xrm.Sdk;
using Moq;

namespace Pg.LetsMeet.Dataverse.Tests.Shared
{
    public class DataverseTestBase
    {
        public static ITracingService CreateTracingService()
        {
            var tracingService = new Mock<ITracingService>();
            tracingService.Setup(s => s.Trace(It.IsAny<string>())); 

            return tracingService.Object;   
        }
    }
}
