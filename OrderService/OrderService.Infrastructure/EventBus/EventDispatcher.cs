using DotNetCore.CAP;
using EventBus;
using OrderService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.EventBus
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly ICapPublisher _publisher;
        private readonly OrderServiceDbContext _db;

        public EventDispatcher(ICapPublisher publisher, OrderServiceDbContext orderServiceDbContext)
        {
            _publisher = publisher;
            _db = orderServiceDbContext;
        }
        public async Task PublishAsync<TEvent>(string topicName, TEvent eventData)
        {
            await _publisher.PublishAsync(topicName, eventData);
        }
        public async ValueTask<IEventTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            var transaction = await _db.Database.BeginTransactionAsync(_publisher, autoCommit: false, cancellationToken: cancellationToken);
            return new CapEventTransaction(transaction, _publisher);
        }
    }
}
