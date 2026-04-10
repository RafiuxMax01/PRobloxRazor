using Microsoft.EntityFrameworkCore;

namespace Usuarios.Modelos
{
    // Clase que maneja la conexión principal con la base de datos SQL Server
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public DbSet<Desarrollador> Desarrolladores { get; set; }
    }
}