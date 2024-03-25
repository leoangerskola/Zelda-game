using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zelda_game
{
    internal class Player : PhysicalObject
    {
        int health = 3; //antal liv
        int points = 0; // antal poäng

        public Player(Texture2D texture, float X, float Y, float speedX, float speedY) : base(texture, X, Y, speedX, speedY)
        {

        }
    }


}
