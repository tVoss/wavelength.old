using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wavelength.Api.Models;

namespace Wavelength.Api
{
    public class WavelengthDbContext : DbContext
    {

        public WavelengthDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Bar> Bars { get; set; }
        public DbSet<BarReport> BarReports { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Shift> Shifts { get; set; }
    }
}
