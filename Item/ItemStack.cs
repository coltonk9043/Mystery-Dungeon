using System;

namespace DungeonGame.Item
{
    public class ItemStack
    {
        private DungeonGame.Item.AbstractItem item;
        private int count;

        public ItemStack(DungeonGame.Item.AbstractItem item)
          : this(item, 1)
        {
        }

        public ItemStack(DungeonGame.Item.AbstractItem item, int count)
        {
            this.item = item;
            this.count = count;
        }

        public int getCount() => this.count;

        public bool isFull() => this.count == this.item.getMaxStackSize();

        public DungeonGame.Item.AbstractItem getItem() => this.item;

        public ItemStack split(int amount)
        {
            int num = Math.Min(amount, this.count);
            ItemStack itemStack = new ItemStack(this.item, num);
            this.shrink(num);
            return itemStack;
        }

        public void shrink(int amount) => this.count -= amount;

        public void grow(int amount)
        {
            int count = this.count;
            this.count += amount;
            Console.WriteLine("Increased item stack from " + count.ToString() + " to " + this.count.ToString());
        }
    }
}
