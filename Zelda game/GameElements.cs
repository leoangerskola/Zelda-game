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
        static List<PhysicalObject> chests;
        enum HsState { PrintHighScore, EnterHighScore };
        static HsState hsState;
        static UI ui;
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

        public static void Initialize(GameWindow gameWindow, ContentManager content)
        {
            window = gameWindow;
            Content = content;
            highscore = new HighScore(10);
        }

        public static void LoadContent()
        {
            highscore.LoadFromFile("highscore.txt");


            menuSprite = Content.Load<Texture2D>("menu");
            menuPosition = new Vector2(window.ClientBounds.Width / 2 - menuSprite.Width / 2, window.ClientBounds.Height / 2 - menuSprite.Height / 2);

            player = new Player(Content.Load<Texture2D>("Player/link_sprite"), window.ClientBounds.Width / 2, window.ClientBounds.Height / 2, 3f, 3f, Content.Load<Texture2D>("Player/sword"), Content.Load<Texture2D>("Player/arrow"));
            printText = new PrintText(Content.Load<SpriteFont>("myFont"));
            enemies = new List<Enemy>();
            chests = new List<PhysicalObject>();
            ui = new UI(printText, player);

            for (int i = 0; i < 10; i++)
            {
                Bokoblin bokoblin = new Bokoblin(Content.Load<Texture2D>("Enemies/Bokoblin_sprite"), random.Next((int)player.X + 100, (int)player.X + 1000), random.Next((int)player.Y + 100, (int)player.Y + 1000), Content.Load<Texture2D>("enemies/spear"), Content);
                enemies.Add(bokoblin);
            }

            for (int i = 0; i < 10; i++)
            {
                PhysicalObject chest = new PhysicalObject(Content.Load<Texture2D>("Player/Chest"), random.Next((int)player.X + 100, (int)player.X + 1000), random.Next((int)player.Y + 100, (int)player.Y + 1000), 4, 4);
                chests.Add(chest);

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
            spriteBatch.Begin();
            spriteBatch.Draw(menuSprite, menuPosition, Color.White);
            spriteBatch.End();
        }
        public static void ResolveEnemyCollisions(List<Enemy> enemies)
        {
            float minDistance = 30f; // Minimum distance between enemies to prevent stacking
            for (int i = 0; i < enemies.Count; i++)
            {
                for (int j = i + 1; j < enemies.Count; j++)
                {
                    float dx = enemies[j].X - enemies[i].X;
                    float dy = enemies[j].Y - enemies[i].Y;
                    float distance = (float)Math.Sqrt(dx * dx + dy * dy);

                    if (distance < minDistance)
                    {
                        float overlap = minDistance - distance;
                        float adjustmentX = (dx / distance) * (overlap / 2);
                        float adjustmentY = (dy / distance) * (overlap / 2);

                        enemies[i].position.X -= adjustmentX;
                        enemies[i].position.Y -= adjustmentY;
                        enemies[j].position.X += adjustmentX;
                        enemies[j].position.Y += adjustmentY;
                    }
                }
            }
        }

        public static GameState PlayingUpdate(GameTime gameTime)
        {

            player.Update(gameTime);

            CalcTranslation();
            ResolveEnemyCollisions(enemies);
            ui.Update(gameTime);
            foreach (Bokoblin en in enemies.ToList())
            {
                en.Update(gameTime, player);

                if (en.CheckCollision(player) && !player.Iframes)
                {
                    player.Health -= 1;
                    player.Iframes = true;
                }

                foreach (Weapon weapon in player.Weapons.ToList())
                {
                    if (en.CheckCollision(weapon))
                    {
                        if(weapon is Sword) { 
                        en.Health -= 3;
                        player.Weapons.Remove(weapon);

                        }
                        else if(weapon is Arrow)
                        {
                            en.Health -= 1;
                            player.Weapons.Remove(weapon);
                        }
                    }
                }


                if (en.Health == 0)
                {
                    enemies.Remove(en);
                    player.Points+=100;
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

            foreach (PhysicalObject chest in chests.ToList())
            {
                if (chest.CheckCollision(player))
                {
                    if (!player.unlockedBow)
                    {
                        player.unlockedBow = true;
                    }
                    else
                    {
                        int loot = random.Next(1, 4);

                        if (loot == 1)
                        {
                            player.Health++;
                        }
                        else if (loot == 2)
                        {
                            player.Points += 500;
                        }
                        else
                        {
                            player.arrowAmount +=5;
                        }
                    }
                    chests.Remove(chest);
                }
            }

            if (player.Iframes)
            {
                player.IframeDuration++;
                if (player.IframeDuration >= 50)
                {
                    player.Iframes = false;
                    player.IframeDuration = 0;
                }

                if (player.Health == 0)
                {
                    player.IsAlive = false;
                    return GameState.Highscore;
                }
            }

            return GameState.Playing;
        }

        public static void PlayingDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: translation);
            player.Draw(spriteBatch, player);
            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(spriteBatch, player);
            }
            foreach (PhysicalObject chest in chests)
            {
                chest.Draw(spriteBatch, player);
            }
            spriteBatch.End();
            spriteBatch.Begin();
            ui.Draw(spriteBatch);
            spriteBatch.End();
        }

        public static GameState HighscoreUpdate(GameTime gameTime)
        {
            switch (hsState)
            {
                case HsState.EnterHighScore:
                    if (highscore.EnterUpdate(gameTime, player.Points)) { 
                        hsState = HsState.PrintHighScore;
                        highscore.SaveToFile("highscore.txt");

                    }


                    break;
                default:
                    KeyboardState keyboardState = Keyboard.GetState();
                    if (keyboardState.IsKeyDown(Keys.E))
                        hsState = HsState.EnterHighScore;

                    break;
            }

            return GameState.Highscore;
        }

        public static void HighscoreDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            SpriteFont myFont = Content.Load<SpriteFont>("myFont");

            switch (hsState)
            {
                case HsState.EnterHighScore:
                    highscore.EnterDraw(spriteBatch, myFont);
                    break;
                default:
                    highscore.PrintDraw(spriteBatch, myFont);
                    break;
            }

            spriteBatch.End();
        }
    }
}