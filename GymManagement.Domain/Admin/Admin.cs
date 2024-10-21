using ErrorOr;
using GymManagement.Domain.Admin.Events;
using GymManagement.Domain.Common;
using GymManagement.Domain.Common.Interfaces;
using GymManagement.Domain.Subscriptions;
#nullable disable
namespace GymManagement.Domain.Admin
{
    public class Admin : Entity
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string UserName { get; private set; }
        private string _hashedPassowrd;
        public bool IsActive { get; private set; }
        private readonly List<int> _subscriptionIds = new();
        public int Roles { get; set; }
        public Admin(string name, string password, string userName, bool isActive, int rools, int? id = null) : base()
        {
            Id = id ?? Id;
            Name = name;
            UserName = userName;
            _hashedPassowrd = password;
            IsActive = isActive;
            Roles = rools;
        }

        //public ErrorOr<Success> Login(string userName, string password)
        //{
        //    if (IsActive)
        //    {
        //        if (UserName == userName && _hashedPassowrd == password)
        //        {
        //            return Result.Success;
        //        }

        //        return AdminErrors.InvalidCredentials;

        //    }
        //    return AdminErrors.InActiveAdmin;
        //}

        public void UpdateAdminInfo(string name, string userName, string password, bool isActive)
        {
            // this method is to apply any business rules if exists
            Name = name;
            UserName = userName;
            _hashedPassowrd = password;
            IsActive = isActive;
        }
        public void UpdateRole(int newRoles)
        {
            Roles = newRoles;
        }
        public ErrorOr<Success> CanHasASubscription(List<Subscription> subscriptions)
        {
            if (!IsActive)
            {
                return AdminErrors.InActiveAdmin;
            }
            if (subscriptions.Exists(s => s.IsActive == true))
            {
                return AdminErrors.AdminHasActiveSubscription;
            }
            return Result.Success;
        }
        public void LoadSubscriptionIds(IEnumerable<int> subscriptionIds)
        {
            _subscriptionIds.Clear();
            _subscriptionIds.AddRange(subscriptionIds);
        }
        public void Deactivate()
        {
            IsActive = false;

            _domainEvents.Add(new AdminDeactivatedEvent(Id));
        }
        private bool _isCorrectPassord(string password, IPassowrdHasher passowrdHasher)
        {
            return passowrdHasher.IsCorrectPassword(password, _hashedPassowrd);
        }
        public ErrorOr<Success> Login(string userName, string password, IPassowrdHasher passowrdHasher)
        {
            
            if (!IsActive)
            {
                return AdminErrors.InActiveAdmin;
            }
            if (UserName != userName || !_isCorrectPassord(password, passowrdHasher))
            {
                return AdminErrors.InvalidCredentials;
            }
            return Result.Success;
        }
        public ErrorOr<Success> HasAccessTo(AdminRole Role)
        {
            if (Roles == (int)AdminRole.AllRolls || ((int)Role & this.Roles) == (int)Role)
            {
                return Result.Success;
            }
            else
            {
                return Error.Forbidden(
                    code: "Admin.Errors.AccessDenied",
                    description: $"Access Denied You Don`t Have The Role {Role}, Please Contact An Admin That Has A Role Of {AdminRole.UpdateRoles} To Others.");
            }
        }
        private Admin()
        {

        }
    }
}
