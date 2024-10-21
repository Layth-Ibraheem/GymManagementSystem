using ErrorOr;
using GymManagement.Application.Admins.Common;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admin;
using GymManagement.Domain.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Admins.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IAdminsRepository _adminsRepository;
        private readonly IPassowrdHasher _passowrdHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public LoginQueryHandler(IAdminsRepository adminsRepository, IPassowrdHasher passowrdHasher, IJwtTokenGenerator jwtTokenGenerator)
        {
            _adminsRepository = adminsRepository;
            _passowrdHasher = passowrdHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var admin = await _adminsRepository.GetByUserNameAsync(request.userName);
            if (admin is null)
            {
                return AdminErrors.InvalidCredentials;
            }
            var result = admin.Login(request.userName, request.password, _passowrdHasher);
            if (result.IsError)
            {
                return result.FirstError;
            }
            return new AuthenticationResult(admin, _jwtTokenGenerator.GenerateToken(admin));
        }
    }
}
