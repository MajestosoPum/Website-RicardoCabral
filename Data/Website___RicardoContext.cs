using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Website___Ricardo.Models;

namespace Website___Ricardo.Data
{
    public class Website___RicardoContext : DbContext
    {
        public Website___RicardoContext (DbContextOptions<Website___RicardoContext> options)
            : base(options)
        {
        }

        public DbSet<Website___Ricardo.Models.Cargo> Cargo { get; set; }

        public DbSet<Website___Ricardo.Models.Empresa> Empresa { get; set; }
    }
}
