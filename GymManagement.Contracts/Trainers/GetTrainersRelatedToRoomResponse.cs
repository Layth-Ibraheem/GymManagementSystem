using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Contracts.Trainers
{
    public record GetTrainersRelatedToRoomResponse(int roomId, List<TrainerResponse> trainers);
}
