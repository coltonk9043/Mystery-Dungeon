// Colton K
// A class representing a basic NPC.
using DungeonGame.Levels;
using DungeonGame.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonGame.Entities.NPCs
{
    public class NPC : LivingEntity
    {
        public Dialogue currentDialogues;
        public Texture2D portrait;

        public NPC(World world, Vector3 position)
          : base(world, position)
        {
        }

        /// <summary>
        /// When the NPC is clicked, perform an action such as gifting or NPC dialogue.
        /// </summary>
        public override void OnMouseClicked()
        {
            Game1.getInstance().setCurrentScreen(new DialogueBoxGui(Game1.getInstance(), null, Game1.getInstance().GetFont(), this));
        }
    }
}
