// Colton K
// A class representing a basic NPC.
using DungeonGame.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonGame.Entities.NPCs
{
    public class NPC : LivingEntity
    {
        public Dialogue currentDialogues;
        public Texture2D portrait;

        public NPC(Vector3 position)
          : base(position)
        {
        }

        /// <summary>
        /// When the NPC is clicked, perform an action such as gifting or NPC dialogue.
        /// </summary>
        public override void onMouseClicked()
        {
            Game1.getInstance().setCurrentScreen((Gui)new DialogueBoxGui((Gui)null, Game1.getInstance().font, this));
        }
    }
}
