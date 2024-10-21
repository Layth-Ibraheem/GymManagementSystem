using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Domain.Admin
{
    public static class AdminErrors
    {
        public static readonly Error InvalidCredentials = Error.Validation(
            code: "Admin.InvalidCredentials",
            description:"Invalid user name or password!"
            );

        public static readonly Error InActiveAdmin = Error.Validation(
            code: "Admin.InActiveAdmin",
            description: "This admin account is deactivated"
            );

        public static readonly Error NotUniqueUserName = Error.Validation(
            code: "Admin.NonUniqueUserName",
            description: "This user name is already exists"
            );

        public static readonly Error AdminHasActiveSubscription = Error.Conflict(
            code: "Admin.AdminHasActiveSubscription",
            description: "This Admin has an active subscription"
            );
    }
}
