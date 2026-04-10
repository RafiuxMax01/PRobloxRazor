using Microsoft.EntityFrameworkCore;

namespace Usuarios.Modelos
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public DbSet<Desarrollador> Desarrolladores { get; set; }
    }
}