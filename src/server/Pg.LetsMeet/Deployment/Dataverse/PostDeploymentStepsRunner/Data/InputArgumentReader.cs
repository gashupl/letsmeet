using Pg.LetsMeet.Dataverse.Context;
using PostDeploymentStepsRunner.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostDeploymentStepsRunner.Data
{
    internal class InputArgumentReader
    {
        public const string UrlPrefix = "-url:";
        public const string AppIdPrefix = "-appid:";
        public const string ClientSecretPrefix = "-clientsecret:";
        public const string WelcomeScreenPrefix = "-welcomescreen:";

        public InputDto GetInput(string[] args)
        {
            var input = new InputDto();
            foreach (var arg in args.Where(a => a != null))
            {
                var formattedArg = arg.ToLower();
                if (formattedArg.StartsWith(UrlPrefix))
                {
                    input.DataverseUrl = arg.Remove(0, UrlPrefix.Length);
                }
                else if (formattedArg.StartsWith(AppIdPrefix))
                {
                    input.ApplicationId = arg.Remove(0, AppIdPrefix.Length);
                }
                else if (formattedArg.StartsWith(ClientSecretPrefix))
                {
                    input.ClientSecret = arg.Remove(0, ClientSecretPrefix.Length);
                }
                else if (formattedArg.StartsWith(WelcomeScreenPrefix))
                {
                    var result = bool.TryParse(formattedArg.Remove(0, WelcomeScreenPrefix.Length), out bool welcomeScreen);
                    if (result)
                    {
                        input.WelcomeScreenSettingValue = welcomeScreen;
                    }
                    else
                    {
                        throw new ArgumentException($"Invalid WelcomeScreen parameter value : {formattedArg}");
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException($"Invalid parameter: {arg}");
                }
            }

            if (string.IsNullOrEmpty(input.DataverseUrl))
            {
                ThrowMissingParameterException(UrlPrefix);
            }
            else if (string.IsNullOrEmpty(input.ApplicationId))
            {
                ThrowMissingParameterException(AppIdPrefix);
            }
            else if (string.IsNullOrEmpty(input.ClientSecret))
            {
                ThrowMissingParameterException(ClientSecretPrefix);
            }
            return input;
        }

        private void ThrowMissingParameterException(string parameterName)
        {
            throw new ArgumentException($"Missing parameter: {parameterName}");
        }
    }
}   
