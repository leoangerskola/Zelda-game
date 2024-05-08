using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zelda_game
{
    internal class GameObject
    {
        protected Texture2D texture;
        public Vector2 position;
        
        public GameObject(Texture2D texture, float X, float Y)
        {
            this.texture = texture;
            position.X = X;
            position.Y = Y;
        }

        public virtual void Draw(SpriteBatch spriteBatch, Player player) 
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
        public float X
        {
            get { return position.X; }
        }
        public float Y
        {
            get { return position.Y; }
        }

        public float Width
        {
            get { return texture.Width; }

        }

        public float Height
        {
            get { return texture.Height; }
        }

    }
}
