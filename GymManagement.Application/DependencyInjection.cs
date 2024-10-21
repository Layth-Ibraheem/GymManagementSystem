using ErrorOr;
using FluentValidation;
using GymManagement.Application.Common.Behaviors;
using GymManagement.Application.Gyms.Commands.CreateGym;
using GymManagement.Domain.Gym;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));

                //options.AddBehavior
                //<IPipelineBehavior<CreateGymCommand, ErrorOr<Gym>>,
                //CreateGymCommandBehavior>();

                options.AddOpenBehavior(typeof(ValidationBehavior<,>));
                options.AddOpenBehavior(typeof(RolesCheckerBehavior<,>));

            });
            services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));
            return services;
        }
    }
}
