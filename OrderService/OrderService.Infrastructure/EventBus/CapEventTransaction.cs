using DotNetCore.CAP;
using EventBus;
using Microsoft.EntityFrameworkCore.Storage;

namespace OrderService.Infrastructure.EventBus
{
    public class CapEventTransaction : IEventTransaction
    {
        private readonly IDbContextTransaction _DbContextTransaction;
        private readonly ICapPublisher _cap;

        public CapEventTransaction(IDbContextTransaction efTx, ICapPublisher cap)
        {
            _DbContextTransaction = efTx;
            _cap = cap;
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _DbContextTransaction.CommitAsync(cancellationToken);
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            await _DbContextTransaction.RollbackAsync(cancellationToken);
        }

        public async ValueTask DisposeAsync()
        {
            await _DbContextTransaction.DisposeAsync();
        }
    }
}
