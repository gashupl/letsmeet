using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostDeploymentStepsRunner.Model
{
    public class InputDto
    {
        public string DataverseUrl { get; set; }
        public string ApplicationId { get; set; }
        public string ClientSecret { get; set; }
        public Guid? AppModuleId { get; set; }
        public bool WelcomeScreenSettingValue { get; set; }
    }
}
