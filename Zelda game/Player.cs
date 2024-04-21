using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Zelda_game
{
    internal class Player : PhysicalObject
    {
        int health = 5; 
        int points = 0;


        public Player(Texture2D texture, float X, float Y, float speedX, float speedY) : base(texture, X, Y, speedX, speedY)
        {
        }

        public void Update( GameTime time)
        {
            //================================================
            //                     Controls
            //================================================

            KeyboardState keyboardState = Keyboard.GetState();

            //sword controls
            
            //Movement controlls
            if(keyboardState.IsKeyDown(Keys.W)) 
            {
                position.Y -= speed.Y;
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                position.X -= speed.X;
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                position.Y += speed.Y;
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                position.X += speed.X;
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

        public object Globals { get; }
    }


}
