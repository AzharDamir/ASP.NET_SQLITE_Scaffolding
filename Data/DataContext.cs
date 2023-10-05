using Microsoft.EntityFrameworkCore;
using Tp1AspNet_Sqlite.Models;

namespace Tp1AspNet_Sqlite.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Livre> Livre { get; set; }
    }
}
