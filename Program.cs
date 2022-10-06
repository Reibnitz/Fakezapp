using Fakezapp.Models;
using Fakezapp.Mutations;
using Fakezapp.Queries;
using Fakezapp.Repositories;
using Fakezapp.Subscriptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSingleton<IConversasRepository, ConversasRepository>();

builder.Services
    .AddInMemorySubscriptions();

builder.Services.AddGraphQLServer()
    .AddAuthorization()
    .AddType<Mensagem>()
    .AddType<Conversa>()

    .AddQueryType()
        .AddTypeExtension<ConversaQuery>()
        .AddTypeExtension<AuthQuery>()

    .AddMutationType()
        .AddTypeExtension<MensagemMutation>()
        .AddTypeExtension<AuthMutation>()

    .AddSubscriptionType()
        .AddTypeExtension<ConversaSubscription>()
    ;

builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

builder.Services
    .AddAuthorization()
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration.GetSection("TokenSettings")
                .GetValue<string>("Issuer"),
            ValidAudience = builder.Configuration.GetSection("TokenSettings")
                .GetValue<string>("Audience"),
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration.
                GetSection("TokenSettings").GetValue<string>("Key")))
        };
    });

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

app.UseAuthentication();
app.UseAuthorization();

app.Run();
