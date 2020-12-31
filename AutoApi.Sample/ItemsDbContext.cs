using Microsoft.EntityFrameworkCore;

namespace AutoApi.Sample
{
    public class ItemsDbContext : DbContext
    {
        public ItemsDbContext(DbContextOptions<ItemsDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Item> Items { get; set; }
    }
}