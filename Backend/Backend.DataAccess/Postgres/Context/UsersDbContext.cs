using Backend.Domain.Models.App;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Backend.DataAccess.Postgres.Context;

public class UsersDbContext : DbContext
{
    private const int LoginLength = 60;
    private const int PasswordLength = 128;

    public DbSet<User> Users { get; set; }

    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.LogTo(_ => { }, LogLevel.Warning);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(user => user.Login);
            entity.Property(user => user.Login)
                .IsRequired()
                .HasMaxLength(LoginLength)
                .HasColumnType($"VARCHAR({LoginLength})");

            entity.Property(user => user.Matches).HasDefaultValue(0);
            entity.Property(user => user.Wins).HasDefaultValue(0);
            entity.Property(user => user.Losses).HasDefaultValue(0);
            entity.Property(user => user.Draws).HasDefaultValue(0);

            entity.Property(user => user.HashPassword).IsRequired()
                .HasMaxLength(PasswordLength)
                .HasColumnType($"VARCHAR({PasswordLength})");
        });
    }
}