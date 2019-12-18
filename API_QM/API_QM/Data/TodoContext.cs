using API_QM.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace API_QM.Data
{
    public class TodoContext : DbContext
    {
        //Crear nuestro dbSet
        public DbSet<TrabajoSede> Configuracion { get; set; }
        public DbSet<User> User { get; set; }
        public TodoContext(DbContextOptions<TodoContext> options):base(options) {

        }

        
    }
}
