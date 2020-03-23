using API_ENTERPRISE_SMC.Models;
using API_ENTERPRISE_SMC.Models.ResponsModels;
using Microsoft.EntityFrameworkCore;

namespace API_ENTERPRISE_SMC.Data
{
    public class TodoContext : DbContext
    {

        //Crear nuestro dbSet por objeto que use la base de datos
        public DbSet<ResponsConfig> Configuracion { get; set; }
        public DbSet<AuthUser> AuthUser { get; set; }
        public DbSet<Rol> rol { get; set; }
        public DbSet<UserXrol> userXrol { get; set; }
        public DbSet<Lab21> Lab21 { get; set; }
        public DbSet<Lab22> Lab22 { get; set; }
        public DbSet<Lab20> Lab20 { get; set; }
        public DbSet<UsuarioDemograficoItem> uDIt { get; set; }
        //Llave compuesta para UsuarioDemograficoItem no lleva anotacion [Key] en el modelo
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioDemograficoItem>()
                .HasKey(o => new { o.idDemograficoItem, o.idUser });

            modelBuilder.Entity<Lab20>()
               .HasKey(u => new { u.idOrder, u.idDemographic});
        }
        public DbSet<Demografico> Demogra { get; set; }
        public DbSet<DemograficoItem> DemoIt { get; set; }
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {

        }
    }
}
