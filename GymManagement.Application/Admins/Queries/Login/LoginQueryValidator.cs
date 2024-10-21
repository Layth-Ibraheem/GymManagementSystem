using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Admins.Queries.Login
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(a => a.userName)
                .NotEmpty();

            RuleFor(a => a.password)
                .NotEmpty();
        }
    }
}
