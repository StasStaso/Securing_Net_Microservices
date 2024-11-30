using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Movies.API.Data;

namespace Movies.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<MoviesAPIContext>(options =>
                options.UseInMemoryDatabase("Movies"));

            // Add services to the container

            builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "https://localhost:5005";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("ClientIdPolicy", policy => policy.RequireClaim("client_id", "movieClient", "movies_mvc_client"));
            });

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            SeedDatabase(app);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        private static void SeedDatabase(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var moviesContext = services.GetRequiredService<MoviesAPIContext>();

                MoviesContextSeed.SeedAsync(moviesContext);
            }
        }
    }
}
