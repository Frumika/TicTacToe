using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;

namespace TicTacToe.Data.Context;

public class User
{
    public string Login { get; set; } = string.Empty;
    public string HashPassword { get; set; } = string.Empty;
    
    public int Matches { get; set; } 
    public int Wins { get; set; }
}

public class UsersDbContext : DbContext
{
    private const int LoginLength = 60;

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
            
            entity.Property(user => user.HashPassword).IsRequired();
        });
    }
}