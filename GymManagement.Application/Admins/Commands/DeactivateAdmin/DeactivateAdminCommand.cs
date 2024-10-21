using ErrorOr;
using GymManagement.Domain.Admin;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Admins.Commands.DeactivateAdmin
{
    public record DeactivateAdminCommand(int adminId) : IRequest<ErrorOr<Admin>>;
}
