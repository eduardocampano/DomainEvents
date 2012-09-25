namespace DomainEvents.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ParallelEventDispatcherTests
    {
        [TestMethod]
        public void HandlerShouldBeExecuted()
        {
            var handlerExecuted = false;
            var dispatcher = new ParallelEventDispatcher();
            var actions = new List<Action<EventContext<SampleDomainEvent>>>();
            actions.Add(new Action<EventContext<SampleDomainEvent>>(e => handlerExecuted = true));

            dispatcher.Dispatch<SampleDomainEvent>(actions, new SampleDomainEvent());

            Assert.IsTrue(handlerExecuted);
        }

        [TestMethod]
        public void MultipleHandlersShouldBeExecutedInParallel()
        {
            var dispatcher = new ParallelEventDispatcher();
            var actions = new List<Action<EventContext<SampleDomainEvent>>>();
            var log = new List<string>();

            // 1st handler
            actions.Add(new Action<EventContext<SampleDomainEvent>>(e => 
                {
                    log.Add("1st handler");
                    Thread.Sleep(20);
                    log.Add("1st handler finished");
                }));
            
            // 2nd handler
            actions.Add(new Action<EventContext<SampleDomainEvent>>(e => 
                {
                    log.Add("2nd handler");
                    Thread.Sleep(20);
                    log.Add("2nd handler finished");
                }));

            dispatcher.Dispatch<SampleDomainEvent>(actions, new SampleDomainEvent());

            Thread.Sleep(40);

            Assert.AreEqual<int>(4, log.Count);
            Assert.IsTrue(log[0] == "1st handler" || log[0] == "2nd handler");
            Assert.IsTrue(log[2] == "1st handler finished" || log[2] == "2nd handler finished");
        }
    }
}
