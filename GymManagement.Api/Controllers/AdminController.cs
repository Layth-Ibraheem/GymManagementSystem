using ErrorOr;
using GymManagement.Application.Admins.Commands.CreateAdmin;
using GymManagement.Application.Admins.Commands.DeactivateAdmin;
using GymManagement.Application.Admins.Commands.UpdateAdmin;
using GymManagement.Application.Admins.Common;
using GymManagement.Application.Admins.Queries.GetAdmin;
using GymManagement.Application.Subscriptions.Queries.GetSubscriptionsRelatedToAdmin;
using GymManagement.Contracts.Admins;
using GymManagement.Contracts.Authentication;
using GymManagement.Contracts.Subscriptions;
using GymManagement.Domain.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers
{
    [Route("[controller]")]
    public class AdminController : APIController
    {
        private readonly ISender _medaiteR;
        public AdminController(ISender medaiteR)
        {
            _medaiteR = medaiteR;
        }


        [HttpGet("{adminId:int}")]
        public async Task<IActionResult> GetAdmin(int adminId)
        {
            var query = new GetAdminQuery(adminId);
            var result = await _medaiteR.Send(query);
            return result.Match(
                admin => Ok(new AdminResponse(admin.Id, admin.Name, admin.UserName, admin.IsActive, admin.Roles)),
                Problem);
        }

        [HttpPut("updateAdmin")]
        public async Task<IActionResult> UpdateAdmin(UpdateAdminRequest request)
        {
            var command = new UpdateAdminCommand(request.adminId, request.name, request.password);
            var result = await _medaiteR.Send(command);

            return result.Match(
                admin => Ok(new AdminResponse(admin.Id, admin.Name, admin.UserName, admin.IsActive, admin.Roles)),
                Problem);
        }
        [HttpGet("{adminId:int}/subscriptions")]
        public async Task<IActionResult> GetAllSubscriptions(int adminId)
        {
            var query = new GetSubscriptionsRelatedToAdminQuery(adminId);

            var subscriptionsResult = await _medaiteR.Send(query);
            return subscriptionsResult.Match(
                subscriptions =>
                {
                    var orderedSubscriptions = subscriptions.OrderByDescending(s => s.IsActive).ToList();
                    return Ok(new GetSubscriptionsRelatedToAdminResponse(
                    adminId,
                    Utils.ConvertFromDomainSubscriptionsListToApiSubscriptionsList(orderedSubscriptions)));
                },
                Problem
                );
        }

        [HttpPut("{adminId}/deactivateAdmin")]
        public async Task<IActionResult> DeactivateAdmin(int adminId)
        {
            var command = new DeactivateAdminCommand(adminId);

            var deactivationResult = await _medaiteR.Send(command);

            return deactivationResult.Match(
                    admin =>
                    {
                        return Ok(new AdminResponse(admin.Id, admin.Name, admin.UserName, admin.IsActive, admin.Roles));
                    },
                    Problem
                );
        }
        
    }
}
