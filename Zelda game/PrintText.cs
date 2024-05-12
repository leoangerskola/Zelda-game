using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Threading;

namespace Zelda_game
{
     class PrintText
    {
        SpriteFont font;

        public PrintText(SpriteFont font)
        {
            this.font = font;
        }
        public void Print(string text, SpriteBatch spriteBatch, int X, int Y)
        {
            spriteBatch.DrawString(font, text, new Vector2(X, Y), Color.White);
        }
        

        
    }
}
