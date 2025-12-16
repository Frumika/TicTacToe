using Backend.Domain.Models.App;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Backend.DataAccess.Postgres.Context;

public class AppDbContext : DbContext
{
    private const int LoginLength = 60;
    private const int PasswordLength = 128;

    public DbSet<User> Users { get; set; }
    public DbSet<GameMove> GameMoves { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
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
            entity.HasKey(user => user.Id);

            entity.Property(user => user.Id).ValueGeneratedOnAdd();

            entity.Property(user => user.Login)
                .IsRequired()
                .HasMaxLength(LoginLength)
                .HasColumnType($"VARCHAR({LoginLength})");

            entity.HasIndex(user => user.Login).IsUnique();

            entity.Property(user => user.Matches).HasDefaultValue(0);
            entity.Property(user => user.Wins).HasDefaultValue(0);
            entity.Property(user => user.Losses).HasDefaultValue(0);
            entity.Property(user => user.Draws).HasDefaultValue(0);

            entity.Property(user => user.HashPassword).IsRequired()
                .HasMaxLength(PasswordLength)
                .HasColumnType($"VARCHAR({PasswordLength})");
        });

        modelBuilder.Entity<GameMove>(entity =>
        {
            entity.HasKey(m => m.Id);
            entity.Property(m => m.Id).ValueGeneratedOnAdd();

            entity.Property(m => m.UserId).IsRequired();

            entity.HasOne<User>()
                .WithMany()
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Property(m => m.GameSessionId)
                .IsRequired()
                .HasMaxLength(64)
                .HasColumnType("VARCHAR(64)");

            entity.Property(m => m.PlayerItem)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(8)
                .HasColumnType("VARCHAR(8)");
            
            entity.Property(m => m.ResetCount).IsRequired();
            entity.Property(m => m.Row).IsRequired();
            entity.Property(m => m.Column).IsRequired();
            entity.Property(m => m.CreatedAt).IsRequired();
        });
    }
}