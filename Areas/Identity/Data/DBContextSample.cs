using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SPJ_ProyectoMVC.Areas.Identity.Data;

namespace SPJ_ProyectoMVC.Areas.Identity.Data
{
    public class DBContextSample : IdentityDbContext<SampleUser>
    {
        public DBContextSample(DbContextOptions<DBContextSample> options)
            : base(options)
        {

        }
    }
}