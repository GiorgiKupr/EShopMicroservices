using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Worker.IntegrationEvents.Events
{
    public record StockReservedEvent(Guid OrderId, Guid CustomerId, decimal TotalPrice, int TotalAmount, Guid ProductId);

}
