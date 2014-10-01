using Microsoft.Practices.Prism.PubSubEvents;

namespace Game.Lastoneout.Events
{
    /// <summary>
    /// Defines an event to communicate that a user finished his turn.
    /// The event payload is the player object hashcode.
    /// </summary>
    public class EndTurnEvent : PubSubEvent<int>
    {
    }
}
