using DotNetCore.CAP;
using EventBus;
using Microsoft.EntityFrameworkCore;
using PaymentService.Worker.Data;
using PaymentService.Worker.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Worker.IntegrationEvents.EventHandlers
{
    public class StockReservedEventHandler : ICapSubscribe
    {
        private readonly PaymentServiceDbContext _dbContext;
        private readonly ICapPublisher _capPublisher;
        public StockReservedEventHandler(PaymentServiceDbContext dbContext, ICapPublisher capPublisher) 
        {
            _dbContext = dbContext;
            _capPublisher = capPublisher;
        }

        [CapSubscribe(TopicNames.StockReserved)]
        public async Task CheckCustomersBalance(StockReservedEvent @event)
        {
            var item = await _dbContext.customerBalances.Where(a => a.CustomerId == @event.CustomerId).FirstOrDefaultAsync();
            if(item.Balance >= @event.TotalPrice)
            {
                using(var transaction = _dbContext.Database.BeginTransactionAsync(_capPublisher))
                {
                    item.Balance -= @event.TotalPrice;
                    await _capPublisher.PublishAsync(TopicNames.PaymentSuccesful, new PaymentSuccesfulEvent(@event.OrderId));
                    await _dbContext.SaveChangesAsync();
                }
            }
            else
            {
                await _capPublisher.PublishAsync(TopicNames.PaymentFailed, new PaymentFailedEvent(@event.OrderId, @event.TotalAmount, @event.ProductId));
            }
        }
    }
}
