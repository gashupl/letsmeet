using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pg.LetsMeet.Events.Domain.Model;
using Pg.LetsMeet.Events.Domain.Services;
using System.Web.Http;

namespace Pg.LetsMeet.Events.Api
{
    public class EventsFunction
    {
        private readonly IEventService _eventService; 

        public EventsFunction(IEventService eventService)
        {
            _eventService = eventService;
        }

        /// <summary>
        /// (get) http://localhost:7034/api/events?partnercode=AC-000001
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("FindEventsByPartnerCode")]
        public async Task<IActionResult> FindEventsByPartnerCode(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "events")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string partnerCode = req.Query["partnercode"];
            var events = _eventService.FindByAccountCode(partnerCode);
            log.LogInformation("FindEventsByPartnerCode completed successfully");
            return new OkObjectResult(events);
        }

        /// <summary>
        ///(post) http://localhost:7034/api/event 
        ///sample body:
        //{
        ///    "name": "Event from API",
        ///    "details": "...",
        ///    "partnername": "API Partner",
        ///    "partnerid": "AC-000001",
        ///    "partneremail": "partner@api.com",
        ///    "plannedDate": "2022-08-31T06:00:00Z",
        ///    "allowedParticipants": 10
        ///}
        /// </summary>
        [FunctionName("CreateEvent")]
        public async Task<IActionResult> CreateEvent(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "event")] HttpRequest req, ILogger log)
        {
            log.LogInformation("Creating a new event application");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<EventCreateDto>(requestBody);

            try
            {
                _eventService.CreateEvent(input);
                log.LogInformation("CreateEvent completed successfully");
                return new OkResult(); 
            }
            catch(ArgumentException ex)
            {
                log.LogError($"CreateEvent failed: {ex.Message}");
                return new BadRequestObjectResult(ex.Message);       
            }
            catch (Exception ex)
            {
                log.LogError($"CreateEvent failed: {ex.Message}");
                return new ExceptionResult(ex, true); 

            }
        }
    }
}
