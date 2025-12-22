using System;

namespace KimMin.Items
{
    public abstract class ItemBase : IEquatable<ItemBase>
    {
        public ItemDataSO ItemData { get; private set;}
        public int Stack { get; protected set; }

        public ItemBase(ItemDataSO itemData, int stack)
        {
            ItemData = itemData;
            Stack = stack;
        }

        public virtual bool Equals(ItemBase other)
            => other != null && other.ItemData == this.ItemData;
    }
}