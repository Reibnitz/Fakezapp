using Fakezapp.Models;
using Fakezapp.Mutations;
using Fakezapp.Queries;
using Fakezapp.Repositories;
using Fakezapp.Subscriptions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSingleton<IConversasRepository, ConversasRepository>();

builder.Services
    .AddInMemorySubscriptions();

builder.Services.AddGraphQLServer()
    .AddType<Mensagem>()
    .AddType<Conversa>()

    .AddQueryType()
        .AddTypeExtension<ConversaQuery>()

    .AddMutationType()
        .AddTypeExtension<MensagemMutation>()

    .AddSubscriptionType()
        .AddTypeExtension<ConversaSubscription>()
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
