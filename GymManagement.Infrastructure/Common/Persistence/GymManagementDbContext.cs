using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admin;
using GymManagement.Domain.Common;
using GymManagement.Domain.Gym;
using GymManagement.Domain.Player;
using GymManagement.Domain.Room;
using GymManagement.Domain.Subscriptions;
using GymManagement.Domain.Trainer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GymManagement.Infrastructure.Common.Persistence
{
    public class GymManagementDbContext : DbContext, IUnitOfWork
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Gym> Gyms { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Admin> Admins { get; set; }



        public GymManagementDbContext(DbContextOptions<GymManagementDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task CommitChnagesAsync()
        {
            var domainEvents = ChangeTracker.Entries<Entity>()
                .Select(e => e.Entity.PopDomainEvents())
                .SelectMany(d => d)
                .ToList();

            AddDomainEventsToOfflineProcessingQueu(domainEvents);
            await base.SaveChangesAsync();
        }

        private void AddDomainEventsToOfflineProcessingQueu(List<IDomainEvent> domainEvents)
        {
            //var domainEventsQueu = new Queue<IDomainEvent>();

            //if (_httpContextAccessor.HttpContext!.Items
            //    .TryGetValue("DomainEventsQueu",out var value)
            //    && value is Queue<IDomainEvent> exisitingDomainEvents)
            //{
            //    domainEventsQueu = exisitingDomainEvents;
            //}

            var domainEventsQueu = _httpContextAccessor.HttpContext!.Items
                .TryGetValue("DomainEventsQueue", out var value)
                && value is Queue<IDomainEvent> existingDomainEvents ? existingDomainEvents : new Queue<IDomainEvent>();

            domainEvents.ForEach(domainEventsQueu.Enqueue);

            _httpContextAccessor.HttpContext!.Items["DomainEventsQueue"] = domainEventsQueu;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
