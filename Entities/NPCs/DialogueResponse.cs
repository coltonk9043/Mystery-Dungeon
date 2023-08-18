using System;

namespace DungeonGame.Entities.NPCs
{
    public class DialogueResponse
    {
        public string text;
        public Dialogue NPCResponse;

        public Action onPressed;

        private int friendshipChange;

        public DialogueResponse(string text, Dialogue NPCResponse)
        {
            this.text = text;
            this.NPCResponse = NPCResponse;
        }

        public DialogueResponse(string text, Dialogue nPCResponse, Action onPressed) : this(text, nPCResponse)
        {
            this.onPressed = onPressed;
        }
    }
}
