using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using INMN3_PA1_2_MVC.Models;

namespace INMN3_PA1_2_MVC.Data
{
    public class INMN3_PA1_2_MVCContext : DbContext
    {
        public INMN3_PA1_2_MVCContext (DbContextOptions<INMN3_PA1_2_MVCContext> options)
            : base(options)
        {
        }

        public DbSet<Miasto> Miasta { get; set; } = default!;
    }
}
