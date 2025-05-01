namespace EventBus
{
    public interface IEventDispatcher
    {
        Task PublishAsync<TEvent>(string topicName, TEvent eventData);
        ValueTask<IEventTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    }
}
