// Decompiled with JetBrains decompiler
// Type: DungeonGame.Program
// Assembly: DungeonGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E78E8B53-5180-47B9-9458-06A9AF653F10
// Assembly location: C:\Users\Colton's PC\Documents\Games\Dungeon\Dungeon\bin\Debug\netcoreapp3.1\DungeonGame.dll

using System;

namespace DungeonGame
{
  public static class Program
  {
    [STAThread]
    private static void Main()
    {
      using (Game1 game1 = new Game1())
        game1.Run();
    }
  }
}
