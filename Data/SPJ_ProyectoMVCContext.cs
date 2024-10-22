using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPJ_ProyectoMVC.Models;

namespace SPJ_ProyectoMVC.Data
{
    public class SPJ_ProyectoMVCContext : DbContext
    {
        public SPJ_ProyectoMVCContext (DbContextOptions<SPJ_ProyectoMVCContext> options)
            : base(options)
        {
        }

        public DbSet<SPJ_ProyectoMVC.Models.Catalogo> Catalogo { get; set; } = default!;
    }
}
