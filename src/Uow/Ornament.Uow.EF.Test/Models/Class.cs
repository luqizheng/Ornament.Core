using Microsoft.EntityFrameworkCore;

namespace Ornament.Uow.EF.Test.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions opts)
            :base(opts)
        {
            
        }
        public DbSet<Blog> Blogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Blog>();
            entity.Property(b => b.Url)
                .HasColumnType("varchar(200)");
            entity.Property(b => b.BlogId);
        }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
    }

    public class MyEfUow : EfUow<MyContext>
    {
        public MyEfUow(MyContext context) : base(context)
        {
        }
    }
}