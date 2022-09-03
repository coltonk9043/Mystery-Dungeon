using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dungeon.Entities
{
    interface ILightSource
    {
        Color Color
        {
            get;
            set;
        }

        float Radius
        {
            get;
            set;
        }

        float Intensity
        {
            get;
            set;
        }
    }
}
