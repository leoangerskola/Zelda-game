﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace Zelda_game
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Player player;
        List<Enemy> enemies;
        private Matrix translation;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        public void CalcTranslation()
        {
            var dx = (GraphicsDevice.Viewport.Width / 2) - player.X;
            var dy = (GraphicsDevice.Viewport.Height / 2) - player.Y;
            translation = Matrix.CreateTranslation(dx, dy, 0);
        }
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            player = new Player(Content.Load<Texture2D>("Player/link_sprite"), GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2, 3f, 3f, Content.Load<Texture2D>("Player/sword"));
            
           
            enemies = new List<Enemy>();
            Bokoblin bokoblin = new Bokoblin(Content.Load<Texture2D>("Enemies/Bokoblin_sprite"), 0, 200, Content.Load<Texture2D>("enemies/spear"));
            enemies.Add(bokoblin);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            CalcTranslation();
            foreach (Bokoblin en in enemies.ToList())
            {
                en.Update(gameTime, player);
                if (en.CheckCollision(player) && !player.Iframes)
                {
                    player.Health -= 1;
                    player.Iframes = true;

                }

                foreach (Sword sword in player.Swords)
                {
                    if (en.CheckCollision(sword))
                    {
                        en.Health -= 1;
                    }
                }

                if(en.Health == 0)
                {
                    enemies.Remove(en);
                }


            }
            if (player.Iframes)
            {
                player.IframeDuration++;
                if (player.IframeDuration >= 50)  // Replace with timer logic
                {
                    player.Iframes = false;
                    player.IframeDuration = 0;
                }

                if (player.Health == 0)
                {
                    player.IsAlive = false;
                    this.Exit();
                }

                // TODO: Add your update logic here

                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(transformMatrix: translation);
            player.Draw(_spriteBatch);
            
            foreach(Enemy enemy in enemies)
            {
                enemy.Draw(_spriteBatch);
            }

            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
