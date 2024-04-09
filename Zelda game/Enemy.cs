using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
namespace Zelda_game
{
    abstract class Enemy : PhysicalObject
    {
        protected int health;
        public Rectangle hitbox;
        public Enemy(Texture2D texture, float X, float Y, float speedX, float speedY, int health) : base(texture, X, Y, speedX, speedY)
        {
            hitbox = new Rectangle(0, 0, texture.Width, texture.Height);
        }
        public abstract void Update();
    }
}