using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTareas.Models
{
    public class WebTareasDbContext : DbContext
    {
        public WebTareasDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Tarea> Tareas { get; set; }
    }
}
