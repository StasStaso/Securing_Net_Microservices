using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Microsoft.IdentityModel.Tokens;

namespace ApiGateway
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile("ocelot.json");

            var authenticationProvideKey = "IdentityApiKey";

            builder.Services.AddAuthentication()
                .AddJwtBearer(authenticationProvideKey, x => 
                {
                    x.Authority = "https://localhost:5005";

                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });

            builder.Services.AddOcelot();

            var app = builder.Build();

            app.MapControllers();

            await app.UseOcelot();

            app.Run();
        }
    }
}
