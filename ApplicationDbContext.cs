using Microsoft.EntityFrameworkCore;

namespace AutoMapperEFIssue;

public sealed class ApplicationDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Post> Posts => Set<Post>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        :base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var postBuilder = modelBuilder.Entity<Post>();

        postBuilder.HasOne(x => x.User).WithMany(x => x.Posts).HasForeignKey(x => x.UserId);
    }
}
