using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pg.LetsMeet.Identity.Domain.Services
{
    public interface IAuthService
    {
        bool IsValid(string authHeader); 
    }
}
