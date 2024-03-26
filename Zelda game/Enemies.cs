using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zelda_game
{
    abstract class Enemies : PhysicalObject
    {
        public Enemies(Texture2D texture, float X, float Y, float speedX, float speedY, int health) : base(texture, X, Y, speedX, speedY)
        {

        }
        public abstract void Update();
    }
}