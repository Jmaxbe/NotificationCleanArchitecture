using System.Diagnostics;
using System.Security.Claims;
using Application.Common.Interfaces;
using Jaeger.Senders;
using Jaeger.Senders.Thrift;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using NotificationAPI.Filters;
using NotificationAPI.Services;
using OpenTracing;
using OpenTracing.Util;
using Serilog;

namespace NotificationAPI;

public static class ServiceInitializer
{
    public static IServiceCollection AddNotificationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddHttpContextAccessor();

        services.AddControllers();

        AddSwagger(services);
        
        IdentityModelEventSource.ShowPII = true;
        
        AddJwt(services, configuration);
        
        services.AddAuthorization();
        
        AddLogging(services, configuration);

        AddJaeger(services);

        return services;
    }

    private static void AddSwagger(IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "NotificationAPI",
                Description = "Demo API - Clean Architecture Solution in .NET 7",
            });
            options.AddSecurityDefinition("Token", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Description = "Please enter a valid token",
                Name = HeaderNames.Authorization,
                In = ParameterLocation.Header,
                Scheme = "Bearer",
                BearerFormat = "JWT"
            });
            options.OperationFilter<SecureEndpointAuthRequirementFilter>();
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "NotificationAPI.xml"));
        });
    }

    private static void AddJwt(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.
                AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.
                AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.Authority = configuration["Jwt:Authority"];
            o.Audience = configuration["Jwt:Audience"];
            o.RequireHttpsMetadata = false;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                RequireAudience = true,
                RequireExpirationTime = true,
                RoleClaimType = ClaimTypes.
                    Role,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidAudience = configuration["Jwt:Audience"],
                ValidIssuer = configuration["Jwt:Authority"],
                ValidateIssuerSigningKey = true
            };
        });
    }

    private static void AddLogging(IServiceCollection services, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        services.AddLogging(builder =>
        {
            builder.AddSerilog();
        });
    }

    private static void AddJaeger(IServiceCollection services)
    {
        services.AddSingleton<ITracer>(serviceProvider =>
        {
            ILoggerFactory loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
     
            Jaeger.Configuration.SenderConfiguration.DefaultSenderResolver = new SenderResolver(loggerFactory).RegisterSenderFactory<ThriftSenderFactory>();
     
            var config = Jaeger.Configuration.FromEnv(loggerFactory);
     
            ITracer tracer = config.GetTracer();
     
            GlobalTracer.Register(tracer);
     
            return tracer;
        });
     
        services.AddOpenTracing();
    }
}