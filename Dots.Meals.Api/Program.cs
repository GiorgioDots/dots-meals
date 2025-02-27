using FastEndpoints.Security;
using FastEndpoints.Swagger;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace Dots.Meals.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var settings = new AppSettings();

            var builder = WebApplication.CreateBuilder(args);

#if !DEBUG
            builder.Services.AddSpaStaticFiles(opt =>
            {
                opt.RootPath = "client";
            });
#endif

            builder.Services.AddSingleton(settings);
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
