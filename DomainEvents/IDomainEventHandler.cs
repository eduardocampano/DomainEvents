namespace DomainEvents
{
    public interface IDomainEventHandler<T> where T : IDomainEvent
    {
         void Handle(EventContext<T> eventContext);  
    }
}
