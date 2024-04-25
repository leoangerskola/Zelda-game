using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zelda_game
{
    internal class Bokoblin : Enemy
    {
        float chaseSpeed = 3;
        bool isAttacking;
        float AttackRange = 50;
        public Bokoblin(Texture2D texture, float X, float Y) : base(texture, X, Y, 3, 3, 3)
        {

        }

        public override void Update(GameTime time, Player player)
        {
            Vector2 direction = player.position - position;
            direction.Normalize();  // Normalize to get unit vector

            // Check if within attack range
            if (Vector2.Distance(position, player.position) <= AttackRange)
            {
                isAttacking = true;
                chaseSpeed = 0;
                // Implement attack logic here (e.g., play animation, damage player)
            }
            else
            {
                isAttacking = false;
                position += direction * chaseSpeed;
            }
        
        }
    }
}
