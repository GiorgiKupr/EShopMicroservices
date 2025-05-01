using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.IntegrationEvents.Events
{
    public record OrderCreatedEvent(Guid OrderId, Guid CustomerId, Guid ProductId, int TotalAmount);
}

