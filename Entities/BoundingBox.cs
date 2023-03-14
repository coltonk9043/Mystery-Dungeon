// Colton K
// A class representing a bounding box.
using Microsoft.Xna.Framework;
using System;

namespace DungeonGame.Entities
{
    public class BoundingBox
    {
        // Variables
        private float x;
        private float y;
        private float width;
        private float height;

        public float X { get { return x; } set { x = value; } }

        public float Y { get { return y; } set { y = value; } }

        public float Width { get { return width; } set { width = value; } }

        public float Height { get { return height; } set { height = value; } }

        /// <summary>
        /// A generic bounding box.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public BoundingBox(float x, float y, float width, float height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        /// <summary>
        /// Gets the intersection between it and another bounding boxes.
        /// </summary>
        /// <param name="bb">The other bounding box.</param>
        /// <returns>The intersection between the other bounding box.</returns>
        public BoundingBox Intersection(BoundingBox bb)
        {
            if (bb == null) return null;

            float x1 = this.X + this.Width;
            float x2 = bb.X + bb.Width;
            float y1 = this.Y + this.Height;
            float y2 = bb.Y + bb.Height;
            float x = Math.Max(this.X, bb.X);
            float num1 = Math.Min(x1, x2);
            if (num1 <=x)
                return null;
            float y = Math.Max(this.Y, bb.Y);
            float num2 = Math.Min(y1, y2);
            return num2 <= y ? null : new BoundingBox(x, y, num1 - x, num2 - y);
        }
    }
}
