using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zelda_game
{
    internal class Bokoblin : Enemy
    {
        
        public Bokoblin(Texture2D texture, float X, float Y) : base(texture, X, Y, 5, 3, 3)
        {

        }

        public override void Update()
        {
            if (health <= 0)
            {
                isAlive = false;
            }

            
        }
    }
}
