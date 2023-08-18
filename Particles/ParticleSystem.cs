using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Dungeon.Particles
{
    public class ParticleSystem
    {
        private List<Vector3> particles;

        // General Settings
        public int MaxParticles { get; set; }   

        public float InitialSpeed { get; set; }
        public float EndSpeed { get; set; }

        // Looping 
        public bool LoopingEnabled { get; set; }
   

        public ParticleSystem()
        {

        }
    }
}
