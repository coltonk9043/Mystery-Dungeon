// Colton K
// A class that is used to prepare Textures for rendering.
using DungeonGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon.Utilities
{
    internal static class TextureUtils
    {
        /// <summary>
        /// Creates an array of Textures based off of a tileset given the size.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="sizeX"></param>
        /// <param name="sizeY"></param>
        /// <returns></returns>
        public static Texture2D[] createTextureArrayFromFile(ContentManager contentManager, string fileName, int sizeX, int sizeY)
        {
            Texture2D texture2D1 = contentManager.Load<Texture2D>(fileName);
            int num1 = texture2D1.Width / sizeX;
            int num2 = texture2D1.Height / sizeY;
            Texture2D[] texture2DArray = new Texture2D[num1 * num2];
            Color[] data1 = new Color[texture2D1.Width * texture2D1.Height];
            texture2D1.GetData(data1);
            int num3 = 0;
            for (int y = 0; y < num2; ++y)
            {
                for (int x = 0; x < num1; ++x)
                {
                    int num4 = x * sizeX;
                    int num5 = y * sizeY;
                    Texture2D texture2D2 = new Texture2D(texture2D1.GraphicsDevice, sizeX, sizeY);
                    Color[] data2 = new Color[sizeX * sizeY];
                    for (int index3 = 0; index3 < sizeX; ++index3)
                    {
                        for (int index4 = 0; index4 < sizeY; ++index4)
                            data2[index4 + index3 * sizeY] = data1[num4 + index4 + (num5 + index3) * texture2D1.Width];
                    }
                    texture2D2.SetData(data2);
                    texture2DArray[num3++] = texture2D2;
                }
            }
            return texture2DArray;
        }

        /// <summary>
        /// Creates a 2 Dimensional array of Textures based off of a tileset given the size.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="sizeX"></param>
        /// <param name="sizeY"></param>
        /// <returns></returns>
        public static Texture2D[,] create2DTextureArrayFromFile(ContentManager contentManager, string fileName, int sizeX, int sizeY)
        {
            Texture2D texture2D1 = Game1.getInstance().GetContentManager().Load<Texture2D>(fileName);
            int length1 = texture2D1.Width / sizeX;
            int length2 = texture2D1.Height / sizeY;
            Texture2D[,] texture2DArray = new Texture2D[length1, length2];
            Color[] data1 = new Color[texture2D1.Width * texture2D1.Height];
            texture2D1.GetData(data1);
            for (int index1 = 0; index1 < length2; ++index1)
            {
                for (int index2 = 0; index2 < length1; ++index2)
                {
                    int num1 = index2 * sizeX;
                    int num2 = index1 * sizeY;
                    Texture2D texture2D2 = new Texture2D(texture2D1.GraphicsDevice, sizeX, sizeY);
                    Color[] data2 = new Color[sizeX * sizeY];
                    for (int index3 = 0; index3 < sizeX * sizeY; ++index3)
                    {
                        int num3 = index3 % sizeX;
                        int num4 = index3 / sizeX;
                        data2[index3] = data1[num1 + num3 + texture2D1.Width * num2 + texture2D1.Width * num4];
                    }
                    texture2D2.SetData(data2);
                    texture2DArray[index2, index1] = texture2D2;
                }
            }
            return texture2DArray;
        }
    }
}
