namespace DomainEvents
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ParallelEventDispatcher : IEventDispatcher
    {
        public virtual void Dispatch<T>(IEnumerable<Action<EventContext<T>>> actions, T args)
        {
            Parallel.ForEach(actions, a =>
            {
                a(new EventContext<T>(args));
            });
        }
    }
}
