using Microsoft.EntityFrameworkCore;
using PaymentService.Worker.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Worker.Data
{
    public class PaymentServiceDbContext : DbContext
    {
        public PaymentServiceDbContext(DbContextOptions<PaymentServiceDbContext> options)
            : base(options)
        {
        }

        public DbSet<CustomerBalance> customerBalances { get; set; }
    }
}
