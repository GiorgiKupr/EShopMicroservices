using InventoryService.Worker.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Worker.Data
{
    public class InventoryServiceDbContext : DbContext
    {
        public InventoryServiceDbContext(DbContextOptions<InventoryServiceDbContext> options)
            : base(options)
        {
        }

        public DbSet<Inventory> inventories { get; set; }
    }
}
