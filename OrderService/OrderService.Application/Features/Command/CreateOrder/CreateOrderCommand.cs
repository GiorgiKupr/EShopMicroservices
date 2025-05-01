using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Features.Command.CreateOrder
{
    public record CreateOrderCommand(Guid CustomerId, Guid ProductId, int Total) : IRequest<Unit>;
}
