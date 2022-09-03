// Decompiled with JetBrains decompiler
// Type: DungeonGame.Entities.Player.Inventory
// Assembly: DungeonGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E78E8B53-5180-47B9-9458-06A9AF653F10
// Assembly location: C:\Users\Colton's PC\Documents\Games\Dungeon\Dungeon\bin\Debug\netcoreapp3.1\DungeonGame.dll

using DungeonGame.Item;

namespace DungeonGame.Entities.Player
{
  public class Inventory
  {
    private ItemStack[,] inventory;

    public Inventory() => this.inventory = new ItemStack[10, 4];

    public bool addItem(DungeonGame.Item.AbstractItem item)
    {
      if (this.isFull())
        return false;
      int[] positionItemStack = this.getPositionItemStack(item);
      if (positionItemStack[0] == -1 && positionItemStack[1] == -1)
      {
        for (int index1 = 0; index1 < this.inventory.GetLength(1); ++index1)
        {
          for (int index2 = 0; index2 < this.inventory.GetLength(0); ++index2)
          {
            if (this.inventory[index2, index1] == null)
            {
              this.inventory[index2, index1] = new ItemStack(item);
              return true;
            }
          }
        }
      }
      else
        this.inventory[positionItemStack[0], positionItemStack[1]].grow(1);
      return false;
    }

    public int[] getPositionItemStack(AbstractItem item)
    {
      for (int index1 = 0; index1 < this.inventory.GetLength(1); ++index1)
      {
        for (int index2 = 0; index2 < this.inventory.GetLength(0); ++index2)
        {
          ItemStack itemStack = this.inventory[index2, index1];
          if (itemStack != null && itemStack.getItem().Equals((object) item) && !this.inventory[index2, index1].isFull())
            return new int[2]{ index2, index1 };
        }
      }
      return new int[2]{ -1, -1 };
    }

    public bool isFull()
    {
      for (int index1 = 0; index1 < this.inventory.GetLength(1); ++index1)
      {
        int index2 = 0;
        while (index1 < this.inventory.GetLength(0))
        {
          if (this.inventory[index1, index2] == null)
            return false;
          ++index2;
        }
      }
      return true;
    }

    public ItemStack removeItemStack(int x, int y)
    {
      ItemStack itemStack = this.inventory[x, y];
      this.inventory[x, y] = (ItemStack) null;
      return itemStack;
    }

    public ItemStack[] getHotbar()
    {
      ItemStack[] itemStackArray = new ItemStack[10];
      for (int index = 0; index < 10; ++index)
        itemStackArray[index] = this.inventory[index, 0];
      return itemStackArray;
    }
  }
}
