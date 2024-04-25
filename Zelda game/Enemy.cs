using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
namespace Zelda_game
{
    abstract class Enemy : PhysicalObject
    {
        int health;
        public Enemy(Texture2D texture, float X, float Y, float speedX, float speedY, int health) : base(texture, X, Y, speedX, speedY)
        {
        }
        public abstract void Update(GameTime time, Player player);

        public int Health
        {
            get { return health; }
            set { health = value; } 
        }
    }
}