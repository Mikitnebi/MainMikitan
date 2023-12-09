using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MainMikitan.Database.DbContext;

public class MikDbContextFactory : IDesignTimeDbContextFactory<MikDbContext>
{
    public MikDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MikDbContext>();
        optionsBuilder.UseSqlServer(args[0]);
        return new MikDbContext(optionsBuilder.Options);
    }
    
}