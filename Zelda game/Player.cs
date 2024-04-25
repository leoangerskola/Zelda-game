using Microsoft.Xna.Framework;
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

        public bool Iframes;
        public double IframeDuration = 0;
        enum directon { up, down, left, right };
        directon facing;
        Timer swordTimer;

        Texture2D swordTexture;
        List<Sword> swords;
        double attackCooldown = 0;
        bool Isattacking;

        public Player(Texture2D texture, float X, float Y, float speedX, float speedY, Texture2D swordTexture) : base(texture, X, Y, speedX, speedY)
        {
            swords = new List<Sword>();
            this.swordTexture = swordTexture;
        }

        public void Update(GameTime time)
        {
 
            //================================================
            //                     Controls
            //================================================

            KeyboardState keyboardState = Keyboard.GetState();

            //sword controls
            if (keyboardState.IsKeyDown(Keys.Space) && time.TotalGameTime.TotalMilliseconds > attackCooldown + 300)
            {

                Sword temp;


                switch (facing)
                {
                    case directon.up:
                        temp = new Sword(swordTexture, position.X +5, position.Y - 20, 0, 0);;
                        break;
                    case directon.down:
                        temp = new Sword(swordTexture, position.X + 5, position.Y + 20, 0, 0);
                        break;
                    case directon.left:
                        temp = new Sword(swordTexture, position.X - 10, position.Y, 0, 0);
                        break;
                    case directon.right:
                        temp = new Sword(swordTexture, position.X + 20, position.Y, 0, 0);
                        break;
                    default:
                        temp = new Sword(swordTexture, position.X + 5, position.Y + 20, 0, 0);
                        break;
                }
                attackCooldown = time.TotalGameTime.TotalMilliseconds;
                swords.Add(temp);
            }
            else
            {
                Isattacking = false;
            }
            swords.RemoveAll(sword => time.TotalGameTime.TotalMilliseconds - attackCooldown > 150);




            //Movement controlls


            if (keyboardState.IsKeyDown(Keys.W))
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

        public List<Sword> Swords
        {
            get { return swords; }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
            foreach (Sword sword in swords)
            {
                sword.Draw(spriteBatch);
            }
        }
    }


}
