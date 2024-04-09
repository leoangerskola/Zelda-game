using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Zelda_game
{
    abstract class Weapon : PhysicalObject
    {
        protected int damage;
        public Weapon(Texture2D texture, float X, float Y, float speedX, float speedY, int damage) : base(texture, X, Y, speedX, speedY)
        {

        }
        public abstract void Update();
    }
}
