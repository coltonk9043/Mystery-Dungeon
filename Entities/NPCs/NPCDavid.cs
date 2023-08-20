// Colton K
// NPC representing David.
using Dungeon.Utilities;
using DungeonGame.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace DungeonGame.Entities.NPCs
{
    internal class NPCDavid : NPC
    {
        public NPCDavid(World world, Vector3 position) : base(world, position)
        {
            this.texture = TextureUtils.create2DTextureArrayFromFile(this.world.GetCurrentGame().Content, "Textures/NPCs/david", 16, 24)[0, 0];
            this.animated = false;
            this.boundingBox = new DungeonGame.Entities.BoundingBox(this.position.X, this.position.Y, 16f, 24f);
            this.portrait = Game1.getInstance().GetContentManager().Load<Texture2D>("Textures/NPCs/david_portrait");
            this.currentDialogues = new Dialogue("Hello team. My NPC name is David.",
                new List<DialogueResponse> {
                    new DialogueResponse("Grettings David. Nice to meet you.", new Dialogue("Nice to meet you too.")),
                    new DialogueResponse("That was mean... :(", new Dialogue("Sorry I should apologize for that... That was very mean. :("))
                }
            );
        }
    }
}
