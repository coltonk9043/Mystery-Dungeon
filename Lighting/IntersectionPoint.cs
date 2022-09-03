using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dungeon.Lighting
{
    class IntersectionPoint
    {
        float x;
        float y;
        public float X { get { return x; } set { x = value; } }
        public float Y { get { return y; } set { y = value; } }

        public IntersectionPoint(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public IntersectionPoint(Vector2 vec)
        {
            this.x = vec.X;
            this.y = vec.Y;
        }

        public float distance(IntersectionPoint point)
        {
            if (point == null) return float.MaxValue;
            return (float)(Math.Pow(this.x - point.x, 2) + Math.Pow(this.y - point.y, 2));
        }

        public float distance(Vector2 point)
        {
            if (point == null) return float.MaxValue;
            return (float)(Math.Pow(this.x - point.X, 2) + Math.Pow(this.y - point.Y, 2));
        }

        public Vector2 toVector()
        {
            return new Vector2(this.x, this.y);
        }
    }
}
