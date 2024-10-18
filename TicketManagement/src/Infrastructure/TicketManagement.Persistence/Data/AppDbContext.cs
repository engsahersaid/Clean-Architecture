using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;
using TicketManagement.Domain.Common;
using TicketManagement.Domain.Entities;

namespace TicketManagement.Persistence.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach (var item in ChangeTracker.Entries<BaseAuditableEntity>().AsEnumerable())
            {
                //Auto Timestamp
                if (item.State == EntityState.Added)
                    item.Entity.CreatedDate = DateTime.Now;

                if (item.State == EntityState.Modified)
                    item.Entity.LastModifiedDate = DateTime.Now;
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<BaseAuditableEntity>();
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //Seed some dummy data
            ModelBuilderSeed(modelBuilder);
        }

        private void ModelBuilderSeed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Football Match", UniqueName = "FootballMatch" },
                new Category { Id = 2, Name = "Basketball Match", UniqueName = "BasketballMatch" });
        }
    }
}
