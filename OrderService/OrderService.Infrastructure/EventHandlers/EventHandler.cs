using DotNetCore.CAP;
using EventBus;
using MediatR;
using OrderService.Application.Features.Command.AcceptOrderStatus;
using OrderService.Application.Features.Command.CancelOrderStatus;
using OrderService.Application.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.EventHandlers
{
    public class EventHandler : ICapSubscribe
    {
        private readonly IMediator _mediator;

        public EventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        [CapSubscribe(TopicNames.StockNotReserved)]
        public async Task HandleStockNotReserved(StockNotReservedEvent @event)
        {
            var command = new CancelOrderStatus(@event.OrderId);
            await _mediator.Publish(command);
        }

        [CapSubscribe(TopicNames.PaymentSuccesful)]
        public async Task HandlePaymentSuccesful(PaymentSuccesfulEvent @event)
        {
            var command = new AcceptOrderStatus(@event.OrderId);
            await _mediator.Publish(command);
        }

        [CapSubscribe(TopicNames.PaymentFailed)]
        public async Task HandlePaymentSuccesful(PaymentFailedEvent @event)
        {
            var command = new CancelOrderStatus(@event.OrderId);
            await _mediator.Publish(command);
        }
    }
}
