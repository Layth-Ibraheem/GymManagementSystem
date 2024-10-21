using ErrorOr;
using GymManagement.Domain.Gym;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Gyms.Queries.GetGym
{
    public record GetGymQuery(int gymId) : IRequest<ErrorOr<Gym>>;
    
}
