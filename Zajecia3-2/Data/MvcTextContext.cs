using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Zajecia3_2.Models;

namespace Zajecia3_2.Data
{
    public class MvcTextContext : IdentityDbContext
    {
        public MvcTextContext (DbContextOptions<MvcTextContext> options)
            : base(options)
        {
        }

        public DbSet<Zajecia3_2.Models.Movie> Movie { get; set; }
    }
}
