using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda_game
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        //Player player;
        //List<Enemy> enemies;
        //private Matrix translation;
        //PrintText printText;
        //Random random = new Random();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        //public void CalcTranslation()
        //{
        //    var dx = (GraphicsDevice.Viewport.Width / 2) - player.X;
        //    var dy = (GraphicsDevice.Viewport.Height / 2) - player.Y;
        //    translation = Matrix.CreateTranslation(dx, dy, 0);
        //}
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            GameElements.curentState = GameElements.GameState.Menu;
            GameElements.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //player = new Player(Content.Load<Texture2D>("Player/link_sprite"), GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2, 3f, 3f, Content.Load<Texture2D>("Player/sword"));
            //printText = new PrintText(Content.Load<SpriteFont>("myFont"));

            //enemies = new List<Enemy>();


            //for(int i = 0; i < 10; i++)
            //{
            //    Bokoblin bokoblin = new Bokoblin(Content.Load<Texture2D>("Enemies/Bokoblin_sprite"), random.Next((int)player.X+100, (int)player.X+1000), random.Next((int)player.Y + 100, (int)player.Y + 1000), Content.Load<Texture2D>("enemies/spear"), Content);
            //    enemies.Add(bokoblin);
            //}

            GameElements.LoadContent();

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {


            // TODO: Add your update logic here

            switch (GameElements.curentState)
            {
                case GameElements.GameState.Playing:
                    GameElements.curentState = GameElements.PlayingUpdate(gameTime);
                    break;
                case GameElements.GameState.Highscore:
                    GameElements.curentState = GameElements.HighscoreUpdate(gameTime);
                    break;
                case GameElements.GameState.Quit:
                    this.Exit();
                    break;
                default:
                    GameElements.curentState = GameElements.MenuUpdate();
                    break;
            }



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            switch (GameElements.curentState)
            {
                case GameElements.GameState.Playing:
                    GameElements.PlayingDraw(_spriteBatch);
                    break;
                case GameElements.GameState.Highscore:
                    GameElements.HighscoreDraw(_spriteBatch);
                    break;
                case GameElements.GameState.Quit:
                    this.Exit();
                    break;
                default:
                    GameElements.MenuDraw(_spriteBatch);
                    break;
            }
            //player.Draw(_spriteBatch, player);

            //printText.Print($"Health {player.Health}", _spriteBatch, 0,0);

            //foreach(Enemy enemy in enemies)
            //{
            //    enemy.Draw(_spriteBatch, player);
            //}
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}

