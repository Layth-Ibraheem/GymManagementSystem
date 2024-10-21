﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Contracts.Players
{
    public record CreatePlayerRequest(string name, short weight, short height, bool isActive, int roomId);
}
