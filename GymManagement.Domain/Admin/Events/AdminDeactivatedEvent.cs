﻿using GymManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Domain.Admin.Events
{
    public record AdminDeactivatedEvent(int adminId) : IDomainEvent;
}
