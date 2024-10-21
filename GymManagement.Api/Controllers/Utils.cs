using ApiSubscriptionType = GymManagement.Contracts.Subscriptions.SubscriptionType;
using GymManagement.Domain.Subscriptions;
using DomainSubscriptionType = GymManagement.Domain.Subscriptions.SubscriptionType;
using GymManagement.Contracts.Subscriptions;
using ApiRoomType = GymManagement.Contracts.Rooms.RoomType;
using DomainRoomType = GymManagement.Domain.Room.RoomType;
namespace GymManagement.Api.Controllers
{
    public static class Utils
    {
        public static ApiSubscriptionType FromDomainSubscriptionTypeToPresintationSubscriptionType(DomainSubscriptionType subscriptionType)
        {
            return subscriptionType.Name switch
            {
                nameof(DomainSubscriptionType.Free) => ApiSubscriptionType.Free,
                nameof(DomainSubscriptionType.Starter) => ApiSubscriptionType.Starter,
                nameof(DomainSubscriptionType.Pro) => ApiSubscriptionType.Pro,
                _ => throw new InvalidOperationException(),
            };
        }
        public static List<SubscriptionResponse> ConvertFromDomainSubscriptionsListToApiSubscriptionsList(List<Subscription> subscriptions)
        {
            var subscriptionsResponse = new List<SubscriptionResponse>();
            foreach (var subscription in subscriptions)
            {
                subscriptionsResponse.Add(new SubscriptionResponse(subscription.Id,
                    Utils.FromDomainSubscriptionTypeToPresintationSubscriptionType(subscription.SubscriptionType)
                    , subscription.IsActive, subscription.AdminId));
            }
            return subscriptionsResponse;
        }
        public static ApiRoomType FromDomainRoomTypeToPresintaionRoomType(DomainRoomType roomType)
        {
            return roomType.Name switch
            {
                nameof(DomainRoomType.Boxing) => ApiRoomType.Boxing,
                nameof(DomainRoomType.Kickboxing) => ApiRoomType.Kickboxing,
                nameof(DomainRoomType.Dancing) => ApiRoomType.Dancing,
                nameof(DomainRoomType.Zomba) => ApiRoomType.Zomba,
                _ => throw new InvalidOperationException(),
            };
        }
    }
}
