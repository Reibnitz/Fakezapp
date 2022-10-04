using Fakezapp.Models;
using Fakezapp.Mutations;
using Fakezapp.Queries;
using Fakezapp.Repositories;
using Fakezapp.Subscriptions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSingleton<IMensagensRepository, MensagensRepository>();

builder.Services
    .AddInMemorySubscriptions();

builder.Services.AddGraphQLServer()
    .AddType<Mensagem>()
    //.AddType<>()

    .AddQueryType()
        .AddTypeExtension<ConversaQuery>()
    //    .AddTypeExtension<>()
    //    .AddTypeExtension<>()

    .AddMutationType()
        .AddTypeExtension<MensagemMutation>()
    //    .AddTypeExtension<>()
    //    .AddTypeExtension<>()

    .AddSubscriptionType()
        .AddTypeExtension<ConversaSubscription>()
    //    .AddTypeExtension<>()
    //    .AddTypeExtension<>()
    ;

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
})
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("CorsPolicy");
}

//app.MapGraphQL();

app.UseWebSockets() // Subscription
    .UseRouting()
    .UseEndpoints(endpoint => endpoint.MapGraphQL());

app.Run();
