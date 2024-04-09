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
            Random random = new Random();

            while (true)
            {
                int direction = random.Next(1, 4);
                
                if(direction == 1)
                {
                    position.X += speed.X;
                }
                if (direction == 2)
                {
                    position.Y += speed.Y;
                }
                if (direction == 3)
                {
                    position.X -= speed.X;
                }
                if (direction == 4)
                {
                    position.Y -= speed.Y;
                }
            }
        }
    }
}
