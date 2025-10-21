using Microsoft.EntityFrameworkCore;
using SociedadesAPI.Models;

namespace SociedadesAPI.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
       
        public DbSet<Sociedad> Sociedades { get; set; }


    }
}
