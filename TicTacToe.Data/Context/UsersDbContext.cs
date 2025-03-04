using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using System.ComponentModel.DataAnnotations;


namespace TicTacToe.Data.Context;

[PrimaryKey(nameof(Id))]
public class User
{
    public int Id { get; set; }
    [MaxLength(45)] public string? Login { get; set; } = string.Empty;
    public string? HashPassword { get; set; } = string.Empty;
}

public class UsersDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
    {
    }
}