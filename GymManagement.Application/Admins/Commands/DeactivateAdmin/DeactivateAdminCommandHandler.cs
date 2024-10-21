using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admin;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Admins.Commands.DeactivateAdmin
{
    public class DeactivateAdminCommandHandler : IRequestHandler<DeactivateAdminCommand, ErrorOr<Admin>>
    {
        private readonly IAdminsRepository _adminsRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeactivateAdminCommandHandler(IAdminsRepository adminsRepository, IUnitOfWork unitOfWork)
        {
            _adminsRepository = adminsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Admin>> Handle(DeactivateAdminCommand request, CancellationToken cancellationToken)
        {
            var admin = await _adminsRepository.GetByIdAsync(request.adminId);
            if (admin is null)
            {
                return Error.Conflict(code: "Admin.NotFound",
                    description: "There is no admin with such id");
            }

            admin.Deactivate();

            await _adminsRepository.UpdateAdminAsync(admin);
            await _unitOfWork.CommitChnagesAsync();
            return admin;

        }
    }
}
