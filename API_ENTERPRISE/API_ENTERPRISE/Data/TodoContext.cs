using API_ENTERPRISE.Models;
using Microsoft.EntityFrameworkCore;

namespace API_ENTERPRISE.Data
{
    public class TodoContext : DbContext
    {

        //Crear nuestro dbSet por objeto que use la base de datos
        public DbSet<Config> Configuracion { get; set; }
        public DbSet<AuthUser> AuthUser { get; set; }
        public DbSet<Rol> rol { get; set; }
        public DbSet<UserXrol> userXrol { get; set; }
        public DbSet<UsuarioDemograficoItem> uDIt { get; set; }
        public DbSet<Demografico> Demogra { get; set; }
        public DbSet<DemograficoItem> DemoIt { get; set; }
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {

        }
    }
}
