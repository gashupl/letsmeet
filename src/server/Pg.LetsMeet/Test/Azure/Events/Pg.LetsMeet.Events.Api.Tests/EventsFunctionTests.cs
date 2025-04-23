using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Moq;
using Newtonsoft.Json;
using Pg.LetsMeet.Events.Domain.Data;
using Pg.LetsMeet.Events.Domain.Model;
using Pg.LetsMeet.Events.Domain.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Http;
using Xunit;

namespace Pg.LetsMeet.Events.Api.Tests
{
    public class EventsFunctionTests : FakeLogger
    {
        private readonly ILogger _logger = new FakeLogger(); 

        [Fact]
        public async void FindEventsByPartnerCode_ValidId_ReturnOk()
        {

            var service = new Mock<IEventService>();
            service.Setup(r => r.FindByAccountCode(It.IsAny<string>()))
                .Returns(new List<EventDto>());

            var function = new EventsFunction(service.Object); 

            var request = CreateRequest("AC-123456");

            var result = await function.FindEventsByPartnerCode(request, _logger);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void CreateEvent_Successfull_ReturnOk()
        {
            var service = new Mock<IEventService>();
            service.Setup(r => r.CreateEvent(It.IsAny<EventCreateDto>())); 

            var function = new EventsFunction(service.Object);

            var body = JsonConvert.SerializeObject(new EventCreateDto()); 
            var request = CreateRequestWithBody(body);

            var result = await function.CreateEvent(request, _logger);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async void CreateEvent_Failed_ReturnBadRequest()
        {
            var service = new Mock<IEventService>();
            service.Setup(r => r.CreateEvent(It.IsAny<EventCreateDto>()))
                .Throws(new Exception());

            var function = new EventsFunction(service.Object);

            var body = JsonConvert.SerializeObject(new EventCreateDto());
            var request = CreateRequestWithBody(body);

            var result = await function.CreateEvent(request, _logger);

            Assert.IsType<ExceptionResult>(result);
        }

        private HttpRequest? CreateRequest(string partnerCodeQueryValue)
        {

            var query = new QueryCollection(new Dictionary<string, StringValues>()
            {
                { "partnercode", partnerCodeQueryValue }
            });

            var context = new DefaultHttpContext();
            var request = context.Request;
            request.Query = query;

            return request; 
        }

        private HttpRequest? CreateRequestWithBody(string body)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(body);

            var context = new DefaultHttpContext();
            var request = context.Request;
            request.Body = new MemoryStream(byteArray);

            return request;
        }
    }
}