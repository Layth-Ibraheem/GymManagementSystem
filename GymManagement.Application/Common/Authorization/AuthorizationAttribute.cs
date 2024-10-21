using GymManagement.Domain.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Common.Authorization
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class AuthorizationAttribute : Attribute
    {
        public AdminRole Role { get; set; }
        public AuthorizationAttribute(AdminRole role)
        {
            Role = role;
        }

    }
}
