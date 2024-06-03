using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading;
using static Zelda_game.Bokoblin;

namespace Zelda_game
{
    internal class Player : PhysicalObject
    {
        int health = 6;
        int points = 0;
        int arrowamount = 30;

        public bool Iframes;
        public double IframeDuration = 0;
        enum direction { up, down, left, right };
        direction facing;

        Texture2D swordTexture;
        List<Weapon> weapons;
        Texture2D arrowTexture;
        double attackCooldown = 0;

        public bool unlockedBow = false;

        public Player(Texture2D texture, float X, float Y, float speedX, float speedY, Texture2D swordTexture, Texture2D arrowTexture) : base(texture, X, Y, speedX, speedY)
        {
            weapons = new List<Weapon>();
            this.swordTexture = swordTexture;
            this.arrowTexture = arrowTexture;
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
                    case direction.up:
                        temp = new Sword(swordTexture, position.X + 7, position.Y - 15, 0, 0, MathHelper.ToRadians(0));
                        break;
                    case direction.down:
                        temp = new Sword(swordTexture, position.X + 7, position.Y + 35, 0, 0, MathHelper.ToRadians(180));
                        break;
                    case direction.left:
                        temp = new Sword(swordTexture, position.X - 10, position.Y + 10, 0, 0, MathHelper.ToRadians(270));
                        break;
                    case direction.right:
                        temp = new Sword(swordTexture, position.X + 20, position.Y + 10, 0, 0, MathHelper.ToRadians(90));
                        break;
                    default:
                        temp = new Sword(swordTexture, position.X + 5, position.Y + 20, 0, 0, MathHelper.ToRadians(180));
                        break;
                }
                attackCooldown = time.TotalGameTime.TotalMilliseconds;
                weapons.Add(temp);
            }

            weapons.RemoveAll(sword => time.TotalGameTime.TotalMilliseconds - attackCooldown > 150);


            //arrow controls

            if(unlockedBow && arrowamount > 0 && keyboardState.IsKeyDown(Keys.E) && time.TotalGameTime.TotalMilliseconds > attackCooldown + 300)
            {
                Arrow temp;
                float arrowSpeedX = 0;
                float arrowSpeedY = 0;

                switch (facing)
                {
                    case direction.up:
                        arrowSpeedY = -1f;
                        temp = new Arrow(arrowTexture, position.X + 5, position.Y - 20, arrowSpeedX, arrowSpeedY, MathHelper.ToRadians(0));
                        break;
                    case direction.down:
                        arrowSpeedY = 1f;
                        temp = new Arrow(arrowTexture, position.X + 5, position.Y + 20, arrowSpeedX, arrowSpeedY, MathHelper.ToRadians(180));
                        break;
                    case direction.left:
                        arrowSpeedX = -1f;
                        temp = new Arrow(arrowTexture, position.X - 10, position.Y, arrowSpeedX, arrowSpeedY, MathHelper.ToRadians(270));
                        break;
                    case direction.right:
                        arrowSpeedX = 1f;
                        temp = new Arrow(arrowTexture, position.X + 20, position.Y, arrowSpeedX, arrowSpeedY, MathHelper.ToRadians(90));
                        break;
                    default:
                        temp = new Arrow(arrowTexture, position.X + 5, position.Y + 20, arrowSpeedX, arrowSpeedY, MathHelper.ToRadians(180));
                        break;
                }
                attackCooldown = time.TotalGameTime.TotalMilliseconds;
                weapons.Add(temp);
                arrowamount--;
            }

            if(arrowamount > 30) arrowamount = 30; 

            //Movement controlls


            if (keyboardState.IsKeyDown(Keys.W))
            {
                position.Y -= speed.Y;
                facing = direction.up;

            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                position.X -= speed.X;
                facing = direction.left;
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                position.Y += speed.Y;
                facing = direction.down;
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                position.X += speed.X;
                facing = direction.right;
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

        public List<Weapon> Weapons
        {
            get { return weapons; }
        }

        public int arrowAmount
        {
            get { return arrowamount; }
            set { arrowamount = value; }
        }

        public override void Draw(SpriteBatch spriteBatch, Player player)
        {
            spriteBatch.Draw(texture, position, Color.White);
            foreach (Weapon weapon in weapons)
            {
                weapon.Draw(spriteBatch, player);
            }

        }
    }


}
