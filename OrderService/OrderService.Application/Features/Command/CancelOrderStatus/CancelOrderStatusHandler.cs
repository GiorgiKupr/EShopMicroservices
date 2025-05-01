using MediatR;
using OrderService.Application.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Features.Command.CancelOrderStatus
{
    public class CancelOrderStatusHandler : IRequestHandler<CancelOrderStatus, Unit>
    {
        private readonly IOrderRepository _orderRepository;

        public CancelOrderStatusHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Unit> Handle(CancelOrderStatus request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);
            order.Status = "canceled";
            await _orderRepository.SaveAsync();

            return Unit.Value;
        }
    }
}
