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
}

public class UsersDbContext : DbContext
{
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
            // Настройка первичного ключа
            entity.HasKey(user => user.Login);

            // Настройка обязательных полей
            entity.Property(user => user.Login).IsRequired();
            entity.Property(user => user.HashPassword).IsRequired();
        });
    }
}