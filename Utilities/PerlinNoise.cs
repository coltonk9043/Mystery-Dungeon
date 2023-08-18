using Microsoft.Xna.Framework;
using System;


namespace Dungeon.Utilities
{
    public class NoiseGenerator
    {
        private int _seed;

        public int Seed
        {
            get { return _seed; }
            set
            {
                _seed = value;
                _random = new Random(_seed);
            }
        }

        private static Random _random;

        public NoiseGenerator()
        {
            Random seedGenerator = new Random();

            _seed = Convert.ToInt32(Math.Floor(((seedGenerator.NextDouble() * 2.0) - 1) * int.MaxValue));
            _random = new Random(_seed);
        }

        public NoiseGenerator(int seed)
        {
            _seed = seed;
            _random = new Random(_seed);
        }

        public double GenerateNoise(float x, float y)
        {
            Vector2 cell = new Vector2((float)Math.Floor(x), (float)Math.Floor(y));
            Vector2 weights = new Vector2(x - cell.X, y - cell.Y); 

            // Get Values for corners.
            Vector2 grad1 = CalculateGradient(cell.X, cell.Y);
            Vector2 grad2 = CalculateGradient(cell.X + 1, cell.Y);
            Vector2 grad3 = CalculateGradient(cell.X, cell.Y + 1);
            Vector2 grad4 = CalculateGradient(cell.X + 1, cell.Y + 1);

            double v1 = grad1.X + grad1.Y;
            double v2 = grad2.X + grad2.Y;
            double v3 = grad3.X + grad3.Y;
            double v4 = grad4.X + grad4.Y;

            double i1 = Lerp(v1, v2, weights.X);
            double i2 = Lerp(v3, v4, weights.X);

            return Math.Clamp(Lerp(i1, i2, weights.Y), -1.0, 1.0);
        }

        private Vector2 CalculateGradient(float x, float y)
        {
            Vector2 gradient;

            int new_X = Convert.ToInt32((x * _seed) % int.MaxValue);
            int new_Y = Convert.ToInt32((y * _seed) % int.MaxValue);

            gradient = new Vector2((float)(1.0 - ((new_X * (new_X * new_X * 13254 + 512561) + 61261361) & 0x7fffffff) / 1073741823.0), (float)(1.0 - ((new_Y * (new_Y * new_Y * 13254 + 512561) + 1126136151) & 0x7fffffff) / 1073741824.0));
            gradient.Normalize();

            return gradient;
        }

        private double Lerp(double a, double b, double t)
        {
            return a + t * (b - a);
        }
    }
}
