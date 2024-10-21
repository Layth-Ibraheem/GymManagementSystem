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

namespace GymManagement.Application.Admins.Commands.CreateAdmin
{
    public class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IAdminsRepository _adminRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPassowrdHasher _passowrdHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public CreateAdminCommandHandler(IAdminsRepository adminRepository, IUnitOfWork unitOfWork, IPassowrdHasher passowrdHasher, IJwtTokenGenerator jwtTokenGenerator)
        {
            _adminRepository = adminRepository;
            _unitOfWork = unitOfWork;
            _passowrdHasher = passowrdHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<ErrorOr<AuthenticationResult>> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            if (await _adminRepository.ExistsBuUserNameAsync(request.userName))
            {
                return AdminErrors.NotUniqueUserName;
            }

            var hashedPass = _passowrdHasher.HashPassword(request.password);

            var admin = new Admin(request.name, hashedPass, request.userName, request.isActive, request.rolls);

            await _adminRepository.AddAdminAsync(admin);
            await _unitOfWork.CommitChnagesAsync();
            var token = _jwtTokenGenerator.GenerateToken(admin);
            return new AuthenticationResult(admin, token);
        }
    }
}
