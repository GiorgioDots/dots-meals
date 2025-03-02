using Dots.Meals.Api.Services.OpenAI;
using Dots.Meals.DAL;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace Dots.Meals.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var settings = new AppSettings();

            var dbInitializer = new DbInitializer(settings.DbUrl);
            dbInitializer.Initialize();

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSingleton(settings);

            builder.Services.AddDbContext<DotsMealsDbContext>(opt => opt.UseSqlite(settings.DbUrl));

#if !DEBUG
            builder.Services.AddSpaStaticFiles(opt =>
            {
                opt.RootPath = "client";
            });
#endif

            builder.Services.AddAuthenticationJwtBearer(
                    s => s.SigningKey = settings.JwtSecret,
                    b =>
                    {
                        b.TokenValidationParameters.ValidIssuer = Envs.JwtIssuer;
                        b.TokenValidationParameters.ValidAudience = Envs.JwtAudience;
                        b.TokenValidationParameters.ValidateLifetime = true;
                        b.TokenValidationParameters.ClockSkew = TimeSpan.Zero;
                    }
                );
            builder.Services.AddAuthorization();
            builder.Services.AddFastEndpoints();
            builder.Services.SwaggerDocument();
            builder.Services.AddCors(o =>
                o.AddPolicy("cors", p =>
                    p.WithOrigins(Envs.AllowedOrigins.Split(',')).AllowAnyHeader().AllowAnyMethod())
            );

            builder.Services.AddSingleton<OpenAiService>();


            var app = builder.Build();

            app.UseCors("cors");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseFastEndpoints(c =>
            {
                c.Serializer.Options.PropertyNamingPolicy = null;
                c.Serializer.Options.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

#if DEBUG
            app.UseDeveloperExceptionPage();
            app.UseSwaggerGen();
#endif

#if !DEBUG
            app.UseSpaStaticFiles();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "client";
            });
#endif

            app.UseHttpsRedirection();

            app.Run();
        }
    }
}
