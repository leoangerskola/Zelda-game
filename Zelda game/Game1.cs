using Microsoft.Xna.Framework;
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

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            player = new Player(Content.Load<Texture2D>("Player/linktemp"), 300, 200, 10f, 10f);

            enemies = new List<Enemy>();
            Bokoblin bokoblin = new Bokoblin(Content.Load<Texture2D>("Enemies/bokoblintemp"), 0, 200);
            enemies.Add(bokoblin);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            foreach(Enemy en in enemies)
            {
                if (en.CheckCollision(player))
                {
                    player.Health -= 1;
                    
                }
                if (player.isAttacking)
                {
                    if (en.hitbox.Intersect(player.swordHitbox))
                    {

                    }
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
            _spriteBatch.Begin();
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
