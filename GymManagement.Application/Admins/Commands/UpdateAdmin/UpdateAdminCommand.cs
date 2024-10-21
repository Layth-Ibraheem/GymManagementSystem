using ErrorOr;
using GymManagement.Domain.Admin;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Admins.Commands.UpdateAdmin
{
    public record UpdateAdminCommand(int adminId, string name, string password) : IRequest<ErrorOr<Admin>>;
}
