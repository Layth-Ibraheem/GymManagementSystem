using ErrorOr;
using GymManagement.Application.Admins.Common;
using GymManagement.Domain.Admin;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Admins.Commands.CreateAdmin
{
    public record CreateAdminCommand(string name, string userName, string password, bool isActive, int rolls) : IRequest<ErrorOr<AuthenticationResult>>;

}
