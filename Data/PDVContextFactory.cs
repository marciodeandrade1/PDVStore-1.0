using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PDVStore.Helpers;

namespace PDVStore.Data
{
    public class PDVContextFactory : IDesignTimeDbContextFactory<PDVContext>
    {
        public PDVContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<PDVContext>();
            builder.UseSqlServer(ConnectionHelper.GetConnectionString());
            return new PDVContext(builder.Options);
        }
    }
}
