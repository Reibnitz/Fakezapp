using HotChocolate.AspNetCore.Authorization;

namespace Fakezapp.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class AuthQuery
    {
        [Authorize]
        public string TestAuthentication()
        {
            return "Sucesso!";
        }

        [Authorize]
        public string OutroTeste() => "Sucesso";
    }
}
