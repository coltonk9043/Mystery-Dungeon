using System.Collections.Generic;

namespace DungeonGame.Entities.NPCs
{
    public class Dialogue
    {
        public string text;
        public List<DialogueResponse> responses;

        public Dialogue(string text)
        {
            this.text = text;
            this.responses = new List<DialogueResponse>();
        }

        public Dialogue(string text, List<DialogueResponse> responses)
        {
            this.text = text;
            this.responses = responses;
        }

        public void addResponse(DialogueResponse response)
        {
            this.responses.Add(response);
        }
    }
}
