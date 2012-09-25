namespace DomainEvents.Tests
{
    public class SampleDomainEventHandler : IDomainEventHandler<SampleDomainEvent>
    {
        public SampleDomainEventHandler()
        {
            this.HandlerExecuted = false;
        }

        public void Handle(EventContext<SampleDomainEvent> eventContext)
        {
            this.HandlerExecuted = true;
        }

        public bool HandlerExecuted { get; set; }
    }
}
