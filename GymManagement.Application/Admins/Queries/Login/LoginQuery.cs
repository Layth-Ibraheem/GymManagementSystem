using ErrorOr;
using GymManagement.Application.Admins.Common;
using GymManagement.Domain.Admin;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Admins.Queries.Login
{
    public record LoginQuery(string userName, string password) : IRequest<ErrorOr<AuthenticationResult>>;
}
