namespace Fakezapp.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class AuthQuery
    {
        public string TestAuthentication()
        {
            return "Sucesso!";
        }
    }
}
