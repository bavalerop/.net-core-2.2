using API_ENTERPRISE.Models;
using API_ENTERPRISE.Models.ResponsModels;
using Microsoft.EntityFrameworkCore;

namespace API_ENTERPRISE.Data
{
    public class TodoContext : DbContext
    {

        //Crear nuestro dbSet por objeto que use la base de datos
        public DbSet<ResponsConfig> Configuracion { get; set; }
        public DbSet<AuthUser> AuthUser { get; set; }
        public DbSet<Rol> rol { get; set; }
        public DbSet<UserXrol> userXrol { get; set; }
        public DbSet<UsuarioDemograficoItem> uDIt { get; set; }
        public DbSet<ResponsSection> section { get; set; }
        public DbSet<UserXSection> UsSection { get; set; }

        //Llave compuesta para UsuarioDemograficoItem no lleva anotacion [Key] en el modelo
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioDemograficoItem>()
                .HasKey(o => new { o.idDemograficoItem, o.idUser });

            modelBuilder.Entity<UserXSection>()
                .HasKey(u => new { u.idUser, u.idSection });
        }

        public DbSet<Demografico> Demogra { get; set; }
        public DbSet<DemograficoItem> DemoIt { get; set; }
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {

        }
    }
}
