﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Contracts.Players
{
    public record GetPlayersRelatedToRoomResponse(int roomId, List<PlayerResponse> players);
}
