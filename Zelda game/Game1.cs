﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

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
            player = new Player(Content.Load<Texture2D>("Player/linktemp"), GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2, 10f, 10f);
           
            enemies = new List<Enemy>();
            Bokoblin bokoblin = new Bokoblin(Content.Load<Texture2D>("Enemies/bokoblintemp"), 0, 200);
            enemies.Add(bokoblin);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            CalcTranslation();
            foreach(Enemy en in enemies)
            {
                if (en.CheckCollision(player))
                {
                    player.Health -= 1;
                    
                }
            }

            if(player.Health == 0)
            {
                player.IsAlive = false;
                this.Exit();
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
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
