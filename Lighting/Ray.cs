using Microsoft.Xna.Framework;
using DungeonGame.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using BoundingBox = DungeonGame.Entities.BoundingBox;

namespace Dungeon.Lighting
{
    class Ray
    {
        private Vector2 position; // 3
        private Vector2 direction;

        public Ray(Vector2 position, Vector2 direction)
        {
            this.position = position;
            this.direction = direction * 4;

        }

        public Ray(Vector2 position, double angle)
        {
            this.position = position;
            this.direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 4;
        }

        public IntersectionPoint simulate(List<Edge> edges)
        {
            IntersectionPoint closest = null;
            Vector2 nextRayPosition = position + direction;
            float closestDistance = float.PositiveInfinity;

            foreach (Edge edge in edges)
            {
                float denom;
                float t;
                float u;
                denom = ((edge.X1 - edge.X2) * (position.Y - nextRayPosition.Y)) - ((edge.Y1 - edge.Y2) * (position.X - nextRayPosition.X));
                if (denom != 0)
                {
                    t = (((edge.X1 - position.X) * (position.Y - nextRayPosition.Y)) - ((edge.Y1 - position.Y) * (position.X - nextRayPosition.X))) / denom;
                    u = -(((edge.X1 - edge.X2) * (edge.Y1 - position.Y)) - ((edge.Y1 - edge.Y2) * (edge.X1 - position.X))) / denom;
                    if (t > 0 && t < 1 && u > 0)
                    {
                        IntersectionPoint point = new IntersectionPoint(edge.X1 + t * (edge.X2 - edge.X1), edge.Y1 + t * (edge.Y2 - edge.Y1));
                        float dist = point.distance(this.position);
                        if (closest == null || closestDistance > dist)
                        {
                            closest = point;
                            closestDistance = dist;
                        }
                    }
                }
            }

            return closest;
        }
    }
}
