// Colton K
// A class representing each layer of a world.
using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonGame.Levels
{
    public class Layer
    {
        // Variables
        private int width;
        private int height;
        private int[,] tiles;
        private bool collidable;
        private bool lightCollides;

        /// <summary>
        /// Constructor representing a layer.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="collidable">Sets if a layer is collidable</param>
        /// <param name="lightCollides">Sets if light collides with it (For realtime raytracing)</param>
        public Layer(int width, int height, bool collidable, bool lightCollides)
        {
            this.width = width;
            this.height = height;
            this.tiles = new int[width, height];
            for(int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    tiles[x, y] = -1;
                }
            }
            this.collidable = collidable;
            this.lightCollides = lightCollides;
        }

        /// <summary>
        /// Gets all of the tiles in the layer.
        /// </summary>
        /// <returns></returns>
        public int[,] getTiles()
        {
            return this.tiles;
        }

        /// <summary>
        /// Gets a tile at a specific location.
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <returns></returns>
        public int getTile(int posX, int posY)
        {
            if (posX >= width || posX < 0) return -1;
            if (posY >= height || posY < 0) return -1;
            return this.tiles[posX, posY];
        }

        /// <summary>
        /// Sets a tile at a specific location.
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <param name="tile"></param>
        public void setTile(int posX, int posY, int tile)
        {
            if (posX >= this.width) return;
            if (posY >= this.height) return;
            this.tiles[posX, posY] = tile;
        }

        /// <summary>
        /// Gets if the layer is collidable.
        /// </summary>
        /// <returns></returns>
        public bool getCollidable()
        {
            return this.collidable;
        }

        /// <summary>
        /// Sets if light collides with the layer.
        /// </summary>
        /// <returns></returns>
        public bool getLightCollides()
        {
            return this.lightCollides;
        }
    }
}
