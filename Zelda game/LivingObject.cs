using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Zelda_game
{
    // Living Object är en klass för levande saker i spelet så som spelaren, fiender med mera.
    internal class LivingObject : GameObject
    {
        protected Vector2 speed;

        public LivingObject(Texture2D texture, float X, float Y, float speedX, float SpeedY) : base(texture, X, Y)
        {
            speed.X = speedX;
            speed.Y= SpeedY;
        }
    }
}
