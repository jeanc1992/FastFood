using FastFood.Domain.Entities;
using FastFood.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FastFood.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Product> products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }


        public override int SaveChanges()
        {
            AddMissingValues();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddMissingValues();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AddMissingValues()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

            var now = DateTime.Now;

            foreach (var entry in modifiedEntries)
            {
               
                if (!(entry.Entity is IEntityBase))
                    continue;

                var entity = entry.Entity as IEntityBase;
              
                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = now;
                }
                else
                {
                    entity.UpdatedAt = now;
                }
            }
        }

    }
}
