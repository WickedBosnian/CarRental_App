using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CarRental_UI.Models;

namespace CarRental_UI.Data
{
    public class CarRental_UIContext : DbContext
    {
        public CarRental_UIContext (DbContextOptions<CarRental_UIContext> options)
            : base(options)
        {
        }

        public DbSet<CarRental_UI.Models.ClientVM> ClientVM { get; set; } = default!;
    }
}
