﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Zelda_game
{
    internal class Weapon : PhysicalObject
    {
        protected float rotation;
        public Weapon(Texture2D texture, float X, float Y, float speedX, float speedY, float rotation) : base(texture, X, Y, speedX, speedY)
        {
        }
        public virtual void Update(GameTime gameTime)
        {

        }

    }
}
