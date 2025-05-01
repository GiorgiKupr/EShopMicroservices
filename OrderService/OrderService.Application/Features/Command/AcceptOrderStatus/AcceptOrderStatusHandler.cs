using MediatR;
using OrderService.Application.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Features.Command.AcceptOrderStatus
{
    public class AcceptOrderStatusHandler : IRequestHandler<AcceptOrderStatus, Unit>
    {
        private readonly IOrderRepository _orderRepository;

        public AcceptOrderStatusHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Unit> Handle(AcceptOrderStatus request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);
            order.Status = "Accepted";
            await _orderRepository.SaveAsync();

            return Unit.Value;
        }
    }
}
