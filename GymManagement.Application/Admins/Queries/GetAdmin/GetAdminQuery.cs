using ErrorOr;
using GymManagement.Domain.Admin;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Admins.Queries.GetAdmin
{
    public record GetAdminQuery(int adminId) : IRequest<ErrorOr<Admin>>;
}
