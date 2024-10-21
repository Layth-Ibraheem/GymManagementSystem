using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Common.Interfaces;
using GymManagement.Domain.Player;
using GymManagement.Domain.Trainer;
using GymManagement.Infrastructure.Admins.Persistence;
using GymManagement.Infrastructure.Authentication.TokenGenerator;
using GymManagement.Infrastructure.Common.PassowrdHash;
using GymManagement.Infrastructure.Common.Persistence;
using GymManagement.Infrastructure.Gyms.Persistence;
using GymManagement.Infrastructure.Players.Persistence;
using GymManagement.Infrastructure.Rooms.Persistence;
using GymManagement.Infrastructure.Subscriptions.Persistence;
using GymManagement.Infrastructure.Trainers.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace GymManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddAuthentication(configuration)
                .AddPersistence();
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<GymManagementDbContext>(options =>
            {
                options.UseSqlServer("Server=.;Database=GymManagementDb;Integrated Security=True;TrustServerCertificate=True;");
            });

            services.AddScoped<ISubscriptionsRepository, SubscriptionsRepository>();
            services.AddScoped<IGymsRepository, GymsRepository>();
            services.AddScoped<IAdminsRepository, AdminsRepository>();
            services.AddScoped<IRoomsRepository, RoomsRepository>();
            services.AddScoped<IPlayersRepository, PlayersRepository>();
            services.AddScoped<ITrainersRepository, TrainersRepository>();
            services.AddScoped<IUnitOfWork>(serviceProvide => serviceProvide.GetRequiredService<GymManagementDbContext>());
            return services;
        }

        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings.Section, jwtSettings);

            services.AddSingleton(Options.Create(jwtSettings));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IPassowrdHasher, PassowrdHasher>();

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                });


            return services;
        }
    }
}
