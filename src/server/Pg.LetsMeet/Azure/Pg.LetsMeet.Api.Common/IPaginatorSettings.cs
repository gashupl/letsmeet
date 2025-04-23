using Microsoft.Xrm.Sdk.Query;

namespace Pg.LetsMeet.Api.Common
{
    public interface IPaginatorSettings
    {
        int PageSize { get; set; }
        QueryExpression Query { get; set; }
    }
}
