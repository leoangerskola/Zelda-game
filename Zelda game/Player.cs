using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Zelda_game
{
    internal class Player : PhysicalObject
    {
        int health = 5; //antal liv
        int points = 0; // antal poäng

        private bool isAttacking;
        private Rectangle swordHitbox;

        public Player(Texture2D texture, float X, float Y, float speedX, float speedY) : base(texture, X, Y, speedX, speedY)
        {
            swordHitbox = new Rectangle(0,0, texture.Width, texture.Height);
        }

        public void Update( GameTime time)
        {
            //================================================
            //                     Controls
            //================================================

            KeyboardState keyboardState = Keyboard.GetState();

            if(keyboardState.IsKeyDown(Keys.Up)) 
            {
                position.Y -= speed.Y;
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                position.X -= speed.X;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                position.Y += speed.Y;
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                position.X += speed.X;
            }

            if (keyboardState.IsKeyDown(Keys.X))
            {
                isAttacking = true;
                
            }
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public int Points
        {
            get { return points; }
            set { points = value; }
        }
    }


}
