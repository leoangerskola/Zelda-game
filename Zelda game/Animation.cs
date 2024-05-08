using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Threading;

namespace Zelda_game
{
    //modifierad Animation kod från https://www.youtube.com/watch?v=hm4PkqS2bqY
    public class Animation
    {
        private Texture2D texture;
        private List<Rectangle> sourceRectangles = new();
        private int frameCount; 
        private int frameIndex;
        private float timePerFrame;
        private float frameTimeLeft;
        private bool active;

        public Animation(Texture2D texture, int framesX, float timePerFrame)
        {
            this.texture = texture;
            this.timePerFrame = timePerFrame;
            this.frameTimeLeft= timePerFrame;
            frameCount = framesX;
            int frameWidth = texture.Width / framesX;
            for (int i = 0; i < framesX; i++)
            {
                sourceRectangles.Add(new Rectangle(i * frameWidth, 0, frameWidth, texture.Height));
            }
        }
        
        public void Start()
        {
            active = true;
        }
        public void Stop()
        {
            active = false;
        }
        public void Update(GameTime gameTime)
        {
            if (!active)
            {
                return;
            }
            frameTimeLeft -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(frameTimeLeft <= 0)
            {
                frameTimeLeft += timePerFrame;
                frameIndex = (frameIndex + 1) % frameCount;
            }
        
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(texture, position, sourceRectangles[frameIndex], Color.White);
        }
    }
}
