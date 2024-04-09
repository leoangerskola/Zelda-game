using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Zelda_game
{
    internal class Sword : Weapon
    {
        public Rectangle swordHitbox; // Hitbox för svärd
        public Sword(Texture2D texture, float X, float Y) : base(texture, X, Y, 3, 3, 1)
        {
            swordHitbox = new Rectangle(0, 0, texture.Width / 4, texture.Height / 4);
        }
        public override void Update()
        {

        }
    }
}
