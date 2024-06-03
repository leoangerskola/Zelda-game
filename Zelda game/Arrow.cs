using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Threading;

namespace Zelda_game
{
    internal class Arrow : Weapon
    {
        public Arrow(Texture2D texture, float X, float Y, float speedX, float speedY, float rotation) : base(texture, X, Y, speedX, speedY, rotation)
        {
            this.rotation = rotation;
        }
        public override void Update(GameTime time)
        {
            position.X += speed.X;
            position.Y += speed.Y;
        }

        public override void Draw(SpriteBatch spriteBatch, Player player)
        {
            Vector2 origin = new Vector2(texture.Width / 2, texture.Height / 2);
            spriteBatch.Draw(texture, position, null, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 0f);
        }
    }
}
