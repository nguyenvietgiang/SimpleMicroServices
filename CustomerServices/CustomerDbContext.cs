using CustomerServices.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace CustomerServices
{
    public class CustomerDbContext : DbContext
    {
        // viết như này có thể tự gen migration ko cần chạy migration nữa
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {
            try 
            {
                var databaseCeator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databaseCeator != null)
                {
                    if (!databaseCeator.CanConnect()) databaseCeator.Create();
                    if (!databaseCeator.HasTables()) databaseCeator.CreateTables();
                }    
            } 
            catch (Exception ex)
            {
             Console.WriteLine(ex.Message);
            }
        }
        public DbSet<Customer> Customers { get; set; }
    }
}
