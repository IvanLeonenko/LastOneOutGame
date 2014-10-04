using Microsoft.Practices.Prism.PubSubEvents;

namespace Game.Lastoneout.Events
{
    /// <summary>
    /// Defines an event to communicate that user exits game view.
    /// </summary>
    public class ReturnEvent : PubSubEvent<object>
    {
    }
}
