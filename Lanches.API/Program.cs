using FluentValidation.AspNetCore;
using Lanches.API.Extensions;
using Lanches.API.Filters;
using Lanches.Application;
using Lanches.Application.Validators;
using Lanches.Core.Entities;
using Lanches.Core.Security;
using Lanches.Infraestructure.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using YourNamespace;

var builder = WebApplication.CreateBuilder(args);
// teste
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ValidationFilter));
    options.Filters.Add<UniqueFilter>();

}).AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateOrderCommandValidator>());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "LanchesAPI", Version = "V1" });

    var xmlFile = "Lanches.API.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    opt.IncludeXmlComments(xmlPath);

    var xmlFileInfra = "Lanches.Infra.xml";
    var xmlPathInfra = Path.Combine(AppContext.BaseDirectory, xmlFileInfra);
    opt.IncludeXmlComments(xmlPathInfra);
});
builder.Services.AddDbContext<LanchesDbContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<LanchesDbContext>(options =>
        options.UseInMemoryDatabase("TestDb"),
        ServiceLifetime.Scoped);
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<LanchesDbContext>().AddDefaultTokenProviders();

builder.Services.AddAuthorization(AuthorizationPolicies.ConfigurePolicies);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
    logging.AddDebug();
});

builder.Services
    .AddMessageBus()
    .AddApplication()
    .AddInfrastructure()
    .AddGenericsRepositories()
    .AddRedisCache()
    .AddServices()
    .AddSubscribers();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
      .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
      .RequireAuthenticatedUser()
      .Build();

    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("Customer", policy => policy.RequireRole("Customer"));

});
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtBearerTokenSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtBearerTokenSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JwtBearerTokenSettings:SecretKey"]))
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];

            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken))
            {
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});
builder.Services.AddHostedService<RoleInitializer>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
