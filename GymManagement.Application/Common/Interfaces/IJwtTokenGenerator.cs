using GymManagement.Domain.Admin;

namespace GymManagement.Application.Common.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Admin admin);
    }
}
