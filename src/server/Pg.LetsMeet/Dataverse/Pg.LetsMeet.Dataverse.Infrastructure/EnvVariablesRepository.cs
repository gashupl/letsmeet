using Microsoft.Xrm.Sdk;
using Pg.LetsMeet.Dataverse.Context;
using Pg.LetsMeet.Dataverse.Domain.DataAccess;
using System;
using System.Linq;

namespace Pg.LetsMeet.Dataverse.Infrastructure
{
    public class EnvVariablesRepository : RepositoryBase, IEnvVariablesRepository
    {

        public string GetDefaultValue(string name)
        {

            using (var context = CreateContext<DataverseContext>())
            {
                var query = context.EnvironmentVariableDefinitionSet
                    .Where(a => a.SchemaName == name)
                    .Select(a => a.DefaultValue); 
                   
                return query.FirstOrDefault();
            }
        }
    }
}
