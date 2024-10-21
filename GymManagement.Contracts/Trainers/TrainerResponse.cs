using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Contracts.Trainers
{
    public record TrainerResponse(int trainerId, string name, short weight, short height, bool isActive, int roomId);
}
