using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gym;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Gyms.Queries.GetGym
{
    public class GetGymQueryHandler : IRequestHandler<GetGymQuery, ErrorOr<Gym>>
    {
        private readonly IGymsRepository _gymsRepository;
        public GetGymQueryHandler(IGymsRepository gymsRepository)
        {
            _gymsRepository = gymsRepository;
        }
        public async Task<ErrorOr<Gym>> Handle(GetGymQuery request, CancellationToken cancellationToken)
        {
            var gym = await _gymsRepository.GetByIdAsync(request.gymId);
            if (gym is null)
            {
                return Error.NotFound(code: "Gym.NotFound", description: "There is no gym with such id");
            }
            return gym;
        }
    }
}
