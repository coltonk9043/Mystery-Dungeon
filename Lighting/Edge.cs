using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dungeon.Lighting
{
    class Edge
    {
        private float x1;
        private float y1;
        private float x2;
        private float y2;

        public float X1 { get { return x1; } set { x1 = value; } }

        public float Y1 { get { return y1; } set { y1 = value; } }

        public float X2 { get { return x2; } set { x2 = value; } }

        public float Y2 { get { return y2; } set { y2 = value; } }

        public Edge(Point a, Point b)
        {
            this.x1 = a.X;
            this.y1 = a.Y;
            this.x2 = b.X;
            this.y2 = b.Y;
        }

        public Edge(float x1, float y1, float x2, float y2)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
        }
    }
}
