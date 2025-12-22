using Code.Core;
using KimMin.Core;

namespace KimMin.Events
{
    public static class PlayerEvents
    {
        public static ChangeInkEvent ChangeInkEvent = new();
    }

    public class ChangeInkEvent : GameEvent
    {
        public InkType inkType;

        public ChangeInkEvent Init(InkType inkType)
        {
            this.inkType = inkType;
            return this;
        }
    }
}