using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Threading;

namespace Zelda_game
{
    internal class Sword : Weapon
    {
        public Sword(Texture2D texture, float X, float Y, float speedX, float speedY) : base(texture, X, Y, 4, 4)
        {
            
        }
        public void Update(GameTime time)
        {

        }
    }
}
