using GymManagement.Application.Common.Interfaces;
using GymManagement.Application.Common.Models;
using System.IdentityModel.Tokens.Jwt;

namespace GymManagement.Api.CurrentUserProviderInterface
{
    public class CurrentUserProvider(IHttpContextAccessor _httpContextAccessor) : ICurrentUserProvider
    {
        public CurrentUser GetCurrentUser()
        {
			try
			{
                
                var claims = _httpContextAccessor.HttpContext!.User.Claims;

                int Id = Convert.ToInt32(claims.First(c => c.Type == "id").Value);

                string UserName = claims.First(c => c.Type == "userName").Value;

                var Roles = Convert.ToInt32(claims.First(c => c.Type == "adminRoles").Value);

                return new CurrentUser(Id, UserName, Roles);
            }
			catch (Exception)
			{
                throw ;
			}
        }
    }
}
