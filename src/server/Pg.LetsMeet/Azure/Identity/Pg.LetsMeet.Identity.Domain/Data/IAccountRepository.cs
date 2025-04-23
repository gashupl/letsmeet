using Pg.LetsMeet.Dataverse.Context;
using System;

namespace Pg.LetsMeet.Identity.Domain.Data
{
    public interface IAccountRepository
    {
        public Account? FindByEmail(string email);
    }
}
