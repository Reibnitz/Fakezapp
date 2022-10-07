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
    .AddGraphQLServer()
    .AddAuthorization()
    //.AddType<Mensagem>()
    //.AddType<Conversa>()

    .AddQueryType()
        .AddTypeExtension<ConversaQuery>()
        .AddTypeExtension<AuthQuery>()

    .AddMutationType()
        .AddTypeExtension<MensagemMutation>()
        .AddTypeExtension<AuthMutation>()

    .AddSubscriptionType()
        .AddTypeExtension<ConversaSubscription>()

    .AddInMemorySubscriptions()
    .AddApolloTracing()
    ;

builder.Services
    .AddInMemorySubscriptions();

builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
builder.Services.AddAuthorization();
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration.GetSection("TokenSettings").GetValue<string>("Issuer"),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = builder.Configuration.GetSection("TokenSettings").GetValue<string>("Audience"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("TokenSettings").GetValue<string>("Key"))),
        };
    });

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseRouting();

app.UseWebSockets(); // Subscription

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoint => endpoint.MapGraphQL());

app.Run();
