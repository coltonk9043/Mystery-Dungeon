// Decompiled with JetBrains decompiler
// Type: DungeonGame.Entities.NPCs.Dialogue
// Assembly: DungeonGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E78E8B53-5180-47B9-9458-06A9AF653F10
// Assembly location: C:\Users\Colton's PC\Documents\Games\Dungeon\Dungeon\bin\Debug\netcoreapp3.1\DungeonGame.dll

namespace DungeonGame.Entities.NPCs
{
  public class Dialogue
  {
    public Dialogue nextDialogue;
    public string text;
    public bool hasResponses;
    public DialogueResponse response1;
    public DialogueResponse response2;

    public Dialogue(string text)
    {
      this.text = text;
      this.hasResponses = false;
    }

    public Dialogue(string text, DialogueResponse response1, DialogueResponse response2)
    {
      this.text = text;
      this.hasResponses = true;
      this.response1 = response1;
      this.response2 = response2;
    }

    public void setNextDialogue(Dialogue next) => this.nextDialogue = next;
  }
}
