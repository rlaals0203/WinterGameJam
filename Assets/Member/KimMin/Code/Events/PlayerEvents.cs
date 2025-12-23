using Code.Core;
using KimMin.Core;

namespace KimMin.Events
{
    public static class PlayerEvents
    {
        public static readonly PlayerDead PlayerDead = new PlayerDead();
        public static readonly PlayerHealthEvent PlayerHealthEvent = new PlayerHealthEvent();
        public static ChangeInkEvent ChangeInkEvent = new();
        public static ChangeExtractorEvent ChangeExtractorEvent = new();
    }
    
    public class PlayerHealthEvent : GameEvent
    {
        public float health;
        public float maxHealth;

        public PlayerHealthEvent Initialize(float health, float maxHealth)
        {
            this.health = health;
            this.maxHealth = maxHealth;
            return this;
        }
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

    public class ChangeExtractorEvent : GameEvent
    {
        public int count;

        public ChangeExtractorEvent Initialize(int count)
        {
            this.count = count;
            return this;
        }
    }
    
    public class PlayerDead : GameEvent {}
}