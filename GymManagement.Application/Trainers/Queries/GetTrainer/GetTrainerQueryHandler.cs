using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Trainer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Trainers.Queries.GetTrainer
{
    public class GetTrainerQueryHandler : IRequestHandler<GetTrainerQuery, ErrorOr<Trainer>>
    {
        private readonly ITrainersRepository _trainersRepository;

        public GetTrainerQueryHandler(ITrainersRepository trainersRepository)
        {
            _trainersRepository = trainersRepository;
        }

        public async Task<ErrorOr<Trainer>> Handle(GetTrainerQuery request, CancellationToken cancellationToken)
        {
            var trainer = await _trainersRepository.GetTrainerByIdAsync(request.trainerId);
            if (trainer is null)
            {
                return Error.NotFound(code: "Trainer.NotFound", description: "There is no trainer with such id");   
            }
            return trainer;
        }
    }
}
