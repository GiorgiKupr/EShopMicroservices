using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Worker.Entities
{
    public class CustomerBalance
    {
        public int CustomerBalanceId { get; set; }
        public decimal Balance { get; set; }
        public Guid CustomerId { get; set; }
    }
}
