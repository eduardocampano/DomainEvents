namespace DomainEvents.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DomainEventsTests
    {
        [TestMethod]
        public void ActionEventHanlderShouldBeExecuted()
        {
            var handlerExecuted = false;

            DomainEvents.ClearCallbacks();

            DomainEvents.Register<SampleDomainEvent>(e => handlerExecuted = true);

            DomainEvents.Raise<SampleDomainEvent>();

            Assert.IsTrue(handlerExecuted);
        }

        [TestMethod]
        public void DomainEventHandlerShouldBeExecuted()
        {
            DomainEvents.ClearCallbacks();

            var sampleDomainEventHandler = new SampleDomainEventHandler();

            DomainEvents.Register<SampleDomainEvent>(sampleDomainEventHandler);

            DomainEvents.Raise<SampleDomainEvent>();

            Assert.IsTrue(sampleDomainEventHandler.HandlerExecuted);
        }
    }
}
