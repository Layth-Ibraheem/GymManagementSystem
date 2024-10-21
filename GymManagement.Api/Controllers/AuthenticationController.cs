using GymManagement.Application.Admins.Commands.CreateAdmin;
using GymManagement.Application.Admins.Common;
using GymManagement.Application.Admins.Queries.Login;
using GymManagement.Contracts.Admins;
using GymManagement.Contracts.Authentication;
using GymManagement.Domain.Admin;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers
{
    [Route("[controller]")]
    [AllowAnonymous]
    public class AuthenticationController : APIController
    {
        private readonly ISender _mediatR;
        public AuthenticationController(ISender mediatR)
        {
            _mediatR = mediatR;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateAdminRequest request)
        {
            var command = new CreateAdminCommand(request.name, request.userName, request.password, request.isActive, request.rolls);
            var result = await _mediatR.Send(command);

            return result.Match(
                authResult =>
                {
                    return Ok(MapToAuthResponse(authResult));
                },
                Problem);
        }
        [HttpGet("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = new LoginQuery(request.UserName, request.Password);
            var result = await _mediatR.Send(query);

            if (result.IsError && result.FirstError == AdminErrors.InvalidCredentials)
            {
                return Problem(
                    detail: result.FirstError.Description,
                    statusCode: StatusCodes.Status401Unauthorized);
            }

            return result.Match(
                authResult => Ok(MapToAuthResponse(authResult)),
                Problem);
        }
        private static AuthenticationResponse MapToAuthResponse(AuthenticationResult authResult)
        {
            return new AuthenticationResponse(
                authResult.admin.Id,
                authResult.admin.Name,
                authResult.admin.UserName,
                authResult.admin.Roles,
                authResult.token);
        }
    }
}
