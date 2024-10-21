using ErrorOr;
using GymManagement.Application.Common.Authorization;
using GymManagement.Domain.Gym;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Gyms.Commands.CreateGym
{
    [Authorization(Domain.Admin.AdminRole.CreateGym)]
    public record CreateGymCommand(string name, int subscriptionId, bool isActive) : IRequest<ErrorOr<Gym>>;
}
