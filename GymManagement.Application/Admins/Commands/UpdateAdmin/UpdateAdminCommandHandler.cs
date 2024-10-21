using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admin;
using GymManagement.Domain.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Admins.Commands.UpdateAdmin
{
    public class UpdateAdminCommandHandler : IRequestHandler<UpdateAdminCommand, ErrorOr<Admin>>
    {
        private readonly IAdminsRepository _adminsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPassowrdHasher _passowrdHasher;
        public UpdateAdminCommandHandler(IAdminsRepository adminsRepository, IUnitOfWork unitOfWork, IPassowrdHasher passowrdHasher)
        {
            _adminsRepository = adminsRepository;
            _unitOfWork = unitOfWork;
            _passowrdHasher = passowrdHasher;
        }

        public async Task<ErrorOr<Admin>> Handle(UpdateAdminCommand request, CancellationToken cancellationToken)
        {
            var admin = await _adminsRepository.GetByIdAsync(request.adminId);
            if(admin is null)
            {
                return Error.NotFound(code: "Admin.NotFound", description: "There is no admin with such id");
            }
            string hashedPass = _passowrdHasher.HashPassword(request.password);
            admin.UpdateAdminInfo(request.name, admin.UserName, hashedPass, admin.IsActive);

            await _adminsRepository.UpdateAdminAsync(admin);
            await _unitOfWork.CommitChnagesAsync();

            return admin;
        }
    }
}
