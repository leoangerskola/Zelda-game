using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Threading;

namespace Zelda_game
{
    internal class Player : PhysicalObject
    {
        int health = 6; 
        int points = 0;
        enum directon { up, down, left, right };
        directon facing;
        Timer swordTimer;

        Texture2D swordTexture;
        List <Sword> s;
        double attackCooldown = 0;

        public Player(Texture2D texture, float X, float Y, float speedX, float speedY, Texture2D swordTexture) : base(texture, X, Y, speedX, speedY)
        {
            s = new List<Sword>();
            this.swordTexture = swordTexture;
        }

            public void Update( GameTime time)
        {
            //================================================
            //                     Controls
            //================================================

            KeyboardState keyboardState = Keyboard.GetState();

            //sword controls
            if(keyboardState.IsKeyDown(Keys.Space))
            {
                if(time.TotalGameTime.TotalMilliseconds > attackCooldown + 100)
                {
                    Sword temp = new Sword(swordTexture, position.X, position.Y, 0, 0);
                    s.Add(temp);
                    attackCooldown = time.TotalGameTime.TotalMilliseconds;

                }

                foreach (Sword sword in s)
                {
                    sword.Update();
                    if (attackCooldown + 100 < time.TotalGameTime.TotalMilliseconds)
                    {
                        s.Remove(sword);
                    }
                }
            }
            //Movement controlls
            if(keyboardState.IsKeyDown(Keys.W)) 
            {
                position.Y -= speed.Y;
                facing = directon.up;
            
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                position.X -= speed.X;
                facing = directon.left;
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                position.Y += speed.Y;
                facing = directon.down;
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                position.X += speed.X;
                facing = directon.right;
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

        public List<Sword> S
        {
            get { return s; }
        }
    
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
            foreach (Sword sword in s)
            {
                sword.Draw(spriteBatch);
            }
        }  
    }


}
