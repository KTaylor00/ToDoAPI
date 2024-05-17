using Carter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ToDoAPI;

public static class Configuration
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddCors(options => options.AddPolicy(name: "NgOrigins",
            policy =>
            {
                policy.WithOrigins("").AllowAnyMethod().AllowAnyHeader();
            }));
        builder.Services.AddCarter();
    }

    public static void RegisterCustomServices(this WebApplicationBuilder builder)
    {

    }

    public static void RegisterAuthServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorizationBuilder()
            .SetFallbackPolicy(new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build());

        builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer(opts =>
            {
                opts.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration.GetValue<string>("Auth:Issuer"),
                    ValidAudience = builder.Configuration.GetValue<string>("Auth:Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("Auth:SecretKey")))
                };
            });
    }

    public static void RegisterMiddlewares(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("NgOrigins");
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
    }
}
