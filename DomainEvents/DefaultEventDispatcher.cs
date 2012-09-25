namespace DomainEvents
{
    using System;
    using System.Collections.Generic;

    public class DefaultEventDispatcher : IEventDispatcher
    {
        public virtual void Dispatch<T>(IEnumerable<Action<EventContext<T>>> actions, T args)
        {
            foreach (var action in actions)
                action(new EventContext<T>(args));
        }
    }
}
