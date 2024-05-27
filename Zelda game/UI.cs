using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda_game
{
    class UI
    {
        PrintText printText;
        Player player;

        public UI(PrintText printText, Player player)
        {
            this.printText = printText;
            this.player = player;
        }

        public void Update(GameTime gameTime)
        {
            // Any necessary updates to the UI can be handled here.
            // For now, we don't need to update anything dynamically.
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (printText != null && player != null)
            {
                printText.Print($"Health: {player.Health}", spriteBatch, 10, 10);
                printText.Print($"Points: {player.Points}", spriteBatch, 10, 30);
                printText.Print($"Arrows: {player.arrowAmount}", spriteBatch, 10, 50);
            }
        }


    }
}