using ErrorOr;
using FluentValidation;
using GymManagement.Application.Common.Authorization;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admin;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Common.Behaviors
{
    internal class RolesCheckerBehavior<TRequest, TResponse>(ICurrentUserProvider _currentUserProvider, IHttpContextAccessor _httpContextAccessor)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IErrorOr
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_httpContextAccessor.HttpContext!.User!.Identity is not { IsAuthenticated: true })
            {
                return await next();
            }
            var currentAdminUser = _currentUserProvider.GetCurrentUser();
            
            Admin adminObject = new Admin("", "", currentAdminUser.UserName, true, currentAdminUser.Roles, currentAdminUser.Id);

            AuthorizationAttribute authAttribute = (AuthorizationAttribute)request.GetType()
                .GetCustomAttributes(typeof(AuthorizationAttribute), false)
                .First();

            if (authAttribute is null)
            {
                return await next();
            }

            var permissionResult = adminObject.HasAccessTo(authAttribute.Role);
            if(permissionResult.IsError)
            {

                return (dynamic)permissionResult.FirstError;
            }

            return await next();

        }
    }
}
