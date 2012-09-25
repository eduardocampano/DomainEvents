namespace DomainEvents.Tests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DefaultEventDispatcherTests
    {
        [TestMethod]
        public void HandlerShouldBeExecuted()
        {
            var handlerExecuted = false;
            var dispatcher = new DefaultEventDispatcher();
            var actions = new List<Action<EventContext<SampleDomainEvent>>>();
            actions.Add(new Action<EventContext<SampleDomainEvent>>(e => handlerExecuted = true));

            dispatcher.Dispatch<SampleDomainEvent>(actions, new SampleDomainEvent());

            Assert.IsTrue(handlerExecuted);
        }

        [TestMethod]
        public void MultipleHandlersShouldBeExecutedSequentially()
        {
            var dispatcher = new DefaultEventDispatcher();
            var actions = new List<Action<EventContext<SampleDomainEvent>>>();
            var log = new List<string>();

            // 1st handler
            actions.Add(new Action<EventContext<SampleDomainEvent>>(e =>
            {
                log.Add("1st handler");
            }));

            // 2nd handler
            actions.Add(new Action<EventContext<SampleDomainEvent>>(e =>
            {
                log.Add("2nd handler");
            }));

            dispatcher.Dispatch<SampleDomainEvent>(actions, new SampleDomainEvent());

            Assert.AreEqual<int>(2, log.Count);
            Assert.AreEqual<string>("1st handler", log[0]);
            Assert.AreEqual<string>("2nd handler", log[1]);
        }
    }
}
