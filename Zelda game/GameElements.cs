using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Zelda_game
{
    static class GameElements
    {
        static Texture2D menuSprite;
        static Vector2 menuPosition;
        static Player player;
        static List<Enemy> enemies;
        static PrintText printText;
        static ContentManager Content;
        static Random random = new Random();
        static GameWindow window;
        private static Matrix translation;
        static HighScore highscore;
        enum HsState { PrintHighScore, EnterHighScore };
        static HsState hsState;
        public enum GameState
        {
            Menu,
            Playing,
            Highscore,
            Quit
        }
        public static GameState curentState;

        public static void CalcTranslation()
        {
            var dx = (window.ClientBounds.Width / 2) - player.X;
            var dy = (window.ClientBounds.Height / 2) - player.Y;
            translation = Matrix.CreateTranslation(dx, dy, 0);
        }

        public static void Initialize()
        {
            highscore = new HighScore(10);
        }
        public static void LoadContent()
        {

            menuSprite = Content.Load<Texture2D>("menu");
            menuPosition = new Vector2(window.ClientBounds.Width / 2 - menuSprite.Width / 2, window.ClientBounds.Height / 2 - menuSprite.Height / 2);

            player = new Player(Content.Load<Texture2D>("Player/link_sprite"), window.ClientBounds.Width / 2, window.ClientBounds.Height / 2, 3f, 3f, Content.Load<Texture2D>("Player/sword"));
            printText = new PrintText(Content.Load<SpriteFont>("myFont"));
            enemies = new List<Enemy>();


            for (int i = 0; i < 10; i++)
            {
                Bokoblin bokoblin = new Bokoblin(Content.Load<Texture2D>("Enemies/Bokoblin_sprite"), random.Next((int)player.X + 100, (int)player.X + 1000), random.Next((int)player.Y + 100, (int)player.Y + 1000), Content.Load<Texture2D>("enemies/spear"), Content);
                enemies.Add(bokoblin);
            }
        }
        public static GameState MenuUpdate()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.S))
            {
                return GameState.Playing;
            }
            if (keyboardState.IsKeyDown(Keys.H))
            {
                return GameState.Highscore;
            }
            if (keyboardState.IsKeyDown(Keys.Q))
            {
                return GameState.Quit;
            }

            return GameState.Menu;
        }
        public static void MenuDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(menuSprite, menuPosition, Color.White);
        }

        public static GameState PlayingUpdate(GameTime gameTime)
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

                if (en.Health == 0)
                {
                    enemies.Remove(en);
                    player.Points++;

                }


            }

            if (enemies.Count < 5)
            {
                for (int i = 0; i < 10; i++)
                {
                    Bokoblin bokoblin = new Bokoblin(Content.Load<Texture2D>("Enemies/Bokoblin_sprite"), random.Next((int)player.X + 100, (int)player.X + 1000), random.Next((int)player.Y + 100, (int)player.Y + 1000), Content.Load<Texture2D>("enemies/spear"), Content);
                    enemies.Add(bokoblin);
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
                    return GameState.Highscore;
                }


                // TODO: Add your update logic here

            }

            return GameState.Playing;
        }
        public static void PlayingDraw(SpriteBatch spriteBatch)
            {
                spriteBatch.Begin(transformMatrix: translation);
                player.Draw(spriteBatch, player);

                printText.Print($"Health {player.Health}", spriteBatch, 0, 0);

                foreach (Enemy enemy in enemies)
                {
                    enemy.Draw(spriteBatch, player);
                }
                spriteBatch.End();
            }
            public static GameState HighscoreUpdate(GameTime gameTime)
            {

            switch (hsState)
            {
                case HsState.EnterHighScore: // Skriv in oss i listan
                                           // Fortsätt så länge HighScore.EnterUpdate() returnerar true:
                    if (highscore.EnterUpdate(gameTime, player.Points))
                        hsState = HsState.PrintHighScore;
                    break;
                default: // Highscore-listan (tar emot en tangent)
                    KeyboardState keyboardState = Keyboard.GetState();
                    if (keyboardState.IsKeyDown(Keys.E))
                        hsState = HsState.EnterHighScore;
                    break;
            }
            return GameState.Highscore;
        }
            public static void HighscoreDraw(SpriteBatch spriteBatch)
            {
            SpriteFont myFont;

            myFont = Content.Load<SpriteFont>("myFont");
            switch (hsState)
            {
                case HsState.EnterHighScore: // Skriv in oss i listan
                    highscore.EnterDraw(spriteBatch, myFont);
                    break;
                default: // Rita ut highscore-listan
                    highscore.PrintDraw(spriteBatch, myFont);
                    break;
            }
            spriteBatch.End();
        }
        }
    }

