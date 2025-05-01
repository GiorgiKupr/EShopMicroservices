using EventBus;
using MediatR;
using OrderService.Application.Abstraction;
using OrderService.Application.IntegrationEvents;
using OrderService.Application.IntegrationEvents.Events;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Features.Command.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Unit>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEventDispatcher _eventDispatcher;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IEventDispatcher eventDispatcher)
        {
            _orderRepository = orderRepository;
            _eventDispatcher = eventDispatcher;
        }

        public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            await using var tx = await _eventDispatcher.BeginTransactionAsync(cancellationToken);

            var order = new Order
            {
                OrderId = new Guid(),
                CustomerId = request.CustomerId,
                ProductId = request.ProductId,
                Total = request.Total,
                Status = "pending",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null
            };
            await _orderRepository.AddAsync(order);
            await _eventDispatcher.PublishAsync(TopicNames.OrderCreated, new OrderCreatedEvent(order.OrderId, request.CustomerId, request.ProductId, request.Total));
            await tx.CommitAsync();

            return Unit.Value;

        }
    }
}
