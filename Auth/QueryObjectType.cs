using Fakezapp.Queries;

namespace Fakezapp.Auth
{
    public class QueryObjectType : ObjectType<AuthQuery>
    {
        protected override void Configure(IObjectTypeDescriptor<AuthQuery> descriptor)
        {
            base.Configure(descriptor);
            descriptor.Field(exemploAuth => exemploAuth.TestAuthentication()).Authorize();
        }
    }
}
