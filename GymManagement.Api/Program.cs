using GymManagement.Api.CurrentUserProviderInterface;
using GymManagement.Application;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Infrastructure;
namespace GymManagement.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services
                .AddApplicationDependencies()
                .AddInfrastructureDependencies(builder.Configuration);

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();

            builder.Services.AddProblemDetails();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.AddInfrastructureMiddleware();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
