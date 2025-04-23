using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pg.LetsMeet.Identity.Domain.Services;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Pg.LetsMeet.Identity.Api
{
    public class PartnerVerificationFunction
    {
        private readonly IPartnerService _partnerService;
        private readonly IAuthService _authService; 
        private readonly ILogger _logger;

        public PartnerVerificationFunction(IPartnerService partnerService, IAuthService authService, 
            ILoggerFactory loggerFactory)
        {
            _partnerService = partnerService;
            _authService = authService; 
            _logger = loggerFactory.CreateLogger<PartnerVerificationFunction>();
        }

        [FunctionName("VerifyPartner")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "verifypartner")] HttpRequest req,
            ILogger log)
        {
            _logger.LogInformation("VerifyPartner function triggered");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync(); 
            JObject data = JObject.Parse(requestBody);
            var authHeader = (string)req.Headers["Authorization"];    
            
            if (_authService.IsValid(authHeader))
            {
                _logger.LogInformation("API Connector validation successfull");

                var email = data["email"].ToString(); 
                var result = _partnerService.IsValidPartner(email);

                if (result.IsValid)
                {
                    _logger.LogInformation("Partner e-mail Dataverse validation successfull");

                    return GetB2cApiConnectorResponse("Continue", string.Empty, 200);
                }
                else
                {
                    //TODO: ShowBlockPage does not work es expected for sign-in flows :/
                    _logger.LogInformation($"Partner e-mail {email} Dataverse validation failed");

                   // return GetB2cApiConnectorResponse("ShowBlockPage", result.ErrorMessage, 200);
                    return GetB2cApiConnectorResponse("ValidationError", result.ErrorMessage, 400);
                }
            }
            else
            {
                //TODO: ShowBlockPage does not work es expected for sign-in flows :/
                _logger.LogError("API Connector validation failed");

                return GetB2cApiConnectorResponse("ValidationError", "API Connector authentication error, Please contact support", 400);
                //return GetB2cApiConnectorResponse("ShowBlockPage", "API Connector authentication error, Please contact support", 200);

            }
        }

        private IActionResult GetB2cApiConnectorResponse(string action, string userMessage, int statusCode)
        {
            var responseProperties = new Dictionary<string, object>
            {
                { "version", "1.0.0" },
                { "action", action },
                { "userMessage", userMessage }
            };
            if (statusCode != 200)
            {
                responseProperties["status"] = statusCode.ToString();
            }
            return new JsonResult(responseProperties) { StatusCode = statusCode };
        }
    }
}
