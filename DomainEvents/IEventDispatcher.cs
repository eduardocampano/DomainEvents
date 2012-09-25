namespace DomainEvents
{
    using System;
    using System.Collections.Generic;

    public interface IEventDispatcher
    {
        void Dispatch<T>(IEnumerable<Action<EventContext<T>>> actions, T args);
    }
}