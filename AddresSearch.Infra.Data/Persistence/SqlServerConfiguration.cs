using AddressSearch.Infra.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace SIGAVS.Infra.Data.Contexts
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var cs = Environment.GetEnvironmentVariable("ViaCep__ConnectionStrings__SqlServer")
                     ?? "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ViaCep;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(cs, sql => sql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null))
                .Options;

            return new AppDbContext(options);
        }
    }
}
