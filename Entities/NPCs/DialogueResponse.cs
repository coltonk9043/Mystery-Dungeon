// Decompiled with JetBrains decompiler
// Type: DungeonGame.Entities.NPCs.DialogueResponse
// Assembly: DungeonGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E78E8B53-5180-47B9-9458-06A9AF653F10
// Assembly location: C:\Users\Colton's PC\Documents\Games\Dungeon\Dungeon\bin\Debug\netcoreapp3.1\DungeonGame.dll

namespace DungeonGame.Entities.NPCs
{
  public class DialogueResponse
  {
    public string text;
    public Dialogue NPCResponse;
    private int friendshipChange;

    public DialogueResponse(string text, Dialogue NPCResponse)
    {
      this.text = text;
      this.NPCResponse = NPCResponse;
    }
  }
}
