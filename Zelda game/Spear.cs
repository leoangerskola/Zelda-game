using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zelda_game
{
    internal class Spear : Weapon
    {
        double timeAlive;
        public Spear(Texture2D texture, float X, float Y, float speedX, float speedY) : base(texture, X, Y, 4, 4)
        {

        }
        public void Update(GameTime time)
        {

        }
    }
}
