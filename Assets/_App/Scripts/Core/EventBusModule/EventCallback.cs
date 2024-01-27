namespace VgGames.Core.EventBusModule
{
    public class EventCallback
    {
        public readonly int Order;
        public readonly object Callback;

        public EventCallback(object callback, int order)
        {
            Callback = callback;
            Order = order;
        }
    }
}