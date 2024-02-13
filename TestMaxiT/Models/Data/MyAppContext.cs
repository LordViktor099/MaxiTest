using Microsoft.EntityFrameworkCore;
using TestMaxiT.Models.Entities;

namespace TestMaxiT.Models.Data
{
    public class MyAppContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public MyAppContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<Beneficiary> Beneficiary { get; set; }

    }
}
