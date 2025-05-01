using MediatR;

namespace OrderService.Application.Features.Command.CancelOrderStatus
{
    public record CancelOrderStatus(Guid OrderId): IRequest<Unit>; 

}
