using MediatR;

namespace OrderService.Application.Features.Command.AcceptOrderStatus
{
    public record AcceptOrderStatus(Guid OrderId) : IRequest<Unit>;

}
