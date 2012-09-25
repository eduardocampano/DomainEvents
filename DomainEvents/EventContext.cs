namespace DomainEvents
{
    using System.Diagnostics;

    public class EventContext<T>
    {
        private string source;

        public EventContext(T eventArgument)
        {
            this.EventArgument = eventArgument;

            var stackTrace = new StackTrace();
            var stackFrames = stackTrace.GetFrames();

            foreach (var stackFrame in stackFrames)
            {
                var method = stackFrame.GetMethod();
                if (method.DeclaringType.Namespace != "DomainEvents")
                {
                    this.source = method.DeclaringType.Namespace + "." + method.Name;
                    break;
                }
            }
        }

        public T EventArgument { get; private set; }

        public string Source
        {
            get
            {
                return this.source;
            }
        }

        public string Name
        {
            get
            {
                return this.GetType().Name;
            }
        }
    }
}
