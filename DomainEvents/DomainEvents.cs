namespace DomainEvents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class DomainEvents
    {
        //[ThreadStatic]
        private static IList<Delegate> actions;

        static DomainEvents()
        {
            actions = new List<Delegate>();

            // Use the default event dispatcher
            Dispatcher = new DefaultEventDispatcher();
        }

        public static void Register<T>(Action<EventContext<T>> callback) where T : IDomainEvent
        {
            actions.Add(callback);
        }

        public static void Register<T>(IDomainEventHandler<T> handler) where T : IDomainEvent
        {
            actions.Add(new Action<EventContext<T>>(handler.Handle));
        }

        public static void ClearCallbacks()
        {
            actions.Clear();
        }

        public static IEventDispatcher Dispatcher { get; set; }

        public static void Raise<T>(T arg) where T : IDomainEvent
        {
            var handlers = GetEventHandlers<T>();
            Dispatcher.Dispatch<T>(handlers, arg);
        }

        public static void Raise<T>() where T : IDomainEvent
        {
            var handlers = GetEventHandlers<T>();
            var arg = Activator.CreateInstance<T>();
            Dispatcher.Dispatch<T>(handlers, arg);
        }

        private static IEnumerable<Action<EventContext<T>>> GetEventHandlers<T>()
        {
            return actions.Where(a => a is Action<EventContext<T>>).Cast<Action<EventContext<T>>>();
        }
    }
}
