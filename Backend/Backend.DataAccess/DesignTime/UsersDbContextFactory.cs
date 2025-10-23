using Backend.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Backend.DataAccess.DesignTime;

public class UsersDbContextFactory : IDesignTimeDbContextFactory<UsersDbContext>
{
    public UsersDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<UsersDbContext>();
        
        optionsBuilder.UseNpgsql("Host=localhost;Port=5444;Database=users_info;Username=postgres;Password=1234");
        
        return new UsersDbContext(optionsBuilder.Options);
    }
}