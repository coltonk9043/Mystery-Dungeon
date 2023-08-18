using Dungeon.Item;
using System.Collections.Generic;

namespace DungeonGame.Item
{
    public class Items
    {
        private Dictionary<string, AbstractItem> items = new Dictionary<string, AbstractItem>();

        public Items()
        {
            this.register("torch", new PlaceableItem("torch", true, 1, 64));
            this.register("copper_ingot", new AbstractItem("copper_ingot", true, 1, 64));
            this.register("copper_hatchet", new AbstractItem("copper_hatchet", true, 1, 1));
            this.register("copper_scythe", new AbstractItem("copper_scythe", true, 1, 1));
            this.register("copper_pickaxe", new AbstractItem("copper_pickaxe", true, 1, 1));
            this.register("copper_sword", new ItemSword("copper_sword", true, 1, 1, new SwordProperties(10, 0.2f, 0.2f)));
            this.register("copper_knife", new ItemSword("copper_knife", true, 1, 1, new SwordProperties(10, 0.2f, 0.2f)));
            this.register("iron_ingot", new AbstractItem("iron_ingot", true, 1, 64));
            this.register("iron_hatchet", new AbstractItem("iron_hatchet", true, 1, 1));
            this.register("iron_scythe", new AbstractItem("iron_scythe", true, 1, 1));
            this.register("iron_pickaxe", new AbstractItem("iron_pickaxe", true, 1, 1));
            this.register("iron_sword", new ItemSword("iron_sword", true, 1, 1, new SwordProperties(10, 0.2f, 0.2f)));
            this.register("iron_knife", new ItemSword("iron_knife", true, 1, 1, new SwordProperties(10, 0.2f, 0.2f)));
            this.register("tungsten_ingot", new AbstractItem("tungsten_ingot", true, 1, 64));
            this.register("tungsten_hatchet", new AbstractItem("tungsten_hatchet", true, 1, 1));
            this.register("tungsten_scythe", new AbstractItem("tungsten_scythe", true, 1, 1));
            this.register("tungsten_pickaxe", new AbstractItem("tungsten_pickaxe", true, 1, 1));
            this.register("tungsten_sword", new ItemSword("tungsten_sword", true, 1, 1, new SwordProperties(10, 0.2f, 0.2f)));
            this.register("tungsten_knife", new ItemSword("tungsten_knife", true, 1, 1, new SwordProperties(10, 0.2f, 0.2f)));
            this.register("elesis_claymore", new ItemSword("sword", true, 4, 1, new SwordProperties(10, 0.2f, 0.2f)));
            this.register("diamond", new AbstractItem("diamond", true, 4, 64));
            this.register("diamond_staff", new ItemStaff("diamond_staff", true, 4, 1));
            this.register("sapphire", new AbstractItem("sapphire", true, 4, 64));
            this.register("sapphire_staff", new ItemStaff("sapphire_staff", true, 4, 1));
            this.register("ruby", new AbstractItem("ruby", true, 4, 64));
            this.register("ruby_staff", new ItemStaff("ruby_staff", true, 4, 1));
            this.register("emerald", new AbstractItem("emerald", true, 4, 64));
            this.register("emerald_staff", new ItemStaff("emerald_staff", true, 4, 1));
        }

        private AbstractItem register(string key, AbstractItem itemIn)
        {
            this.items.Add(key, itemIn);
            return itemIn;
        }

        public AbstractItem GetItem(string name) => this.items[name];
    }
}
