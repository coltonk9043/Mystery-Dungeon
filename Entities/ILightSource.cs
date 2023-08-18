using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dungeon.Entities
{
    interface ILightSource
    {
        Color LightColor
        {
            get;
            set;
        }

        float LightRadius
        {
            get;
            set;
        }

        float LightIntensity
        {
            get;
            set;
        }

        bool LightOn
        {
            get;
            set;
        }
    }
}
