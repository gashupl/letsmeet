using FakeXrmEasy.Abstractions;
using FakeXrmEasy.Abstractions.Enums;
using FakeXrmEasy.FakeMessageExecutors;
using FakeXrmEasy.Middleware;
using FakeXrmEasy.Middleware.Crud;
using FakeXrmEasy.Middleware.Messages;
using Microsoft.Xrm.Sdk;
using Moq;
using System.Diagnostics;
using System.Reflection;

namespace Pg.LetsMeet.Azure.Core
{
    public class TestBase
    {
        private ITracingService _tracingService;
        public ITracingService FakeTracingService
        {
            get
            {
                if (_tracingService == null)
                {
                    InitTracingService();
                }
                return _tracingService;
            }
        }

        public IXrmFakedContext FakeContext
        {
            get
            {
                return MiddlewareBuilder
                    .New()
                    .AddCrud()
                    .AddFakeMessageExecutors(Assembly.GetAssembly(typeof(AddListMembersListRequestExecutor)))
                    .UseCrud()
                    .UseMessages()
                    .SetLicense(FakeXrmEasyLicense.RPL_1_5)
                    .Build();
            }

        }

        public IOrganizationService FakeOrganizationService
        {
            get
            {
                return FakeContext.GetOrganizationService();
            } 
        }

        private void InitTracingService()
        {
            var tracingSeviceMock = new Mock<ITracingService>();
            tracingSeviceMock.Setup(t => t.Trace(It.IsAny<string>()))
                .Callback<string>(s => Debug.WriteLine(s));
            _tracingService = tracingSeviceMock.Object;
        }
    }
}