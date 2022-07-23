namespace Media.DataAccess;

public class DatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<PostInteraction> PostInteractions { get; set; }
    public DbSet<CommentInteraction> CommentInteractions { get; set; }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(user =>
        {
            user.HasKey(u => u.Id);
            user.HasIndex(u => u.Email).IsUnique();
            user.HasIndex(u => u.Email).IsUnique();
        });

        modelBuilder.Entity<Post>();
        modelBuilder.Entity<Comment>();
        modelBuilder.Entity<PostInteraction>();
        modelBuilder.Entity<CommentInteraction>();
    }
}
