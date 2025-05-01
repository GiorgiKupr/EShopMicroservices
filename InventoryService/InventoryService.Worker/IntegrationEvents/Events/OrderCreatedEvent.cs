using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Worker.IntegrationEvents.Events
{
    public record OrderCreatedEvent(Guid OrderId ,Guid CustomerId, Guid ProductId, int TotalAmount);

}

