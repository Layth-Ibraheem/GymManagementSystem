using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Common.Models
{
    public record CurrentUser(int Id, string UserName, int Roles);
}
