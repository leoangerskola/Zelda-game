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

        Texture2D spearTexture;
        List<Spear> spears;
        double attackCooldown = 0;

        public Bokoblin(Texture2D texture, float X, float Y, Texture2D spearTexture) : base(texture, X, Y, 3, 3)
        {
            spears = new List<Spear>();
            this.spearTexture = spearTexture;
        }

        public override void Update(GameTime time, Player player)
        {
            Vector2 direction = player.position - position;
            direction.Normalize();  // Normalize to get unit vector

            // Check if within attack range
            if (Vector2.Distance(position, player.position) <= AttackRange)
            {
                isAttacking = true;
            }
            else
            {
                isAttacking = false;
                position += direction * chaseSpeed;
            }

            if(isAttacking && time.TotalGameTime.TotalMilliseconds > attackCooldown + 1500)
            {
                Spear temp = new Spear(spearTexture, position.X, position.Y, direction.X * 3, direction.Y * 3);
                attackCooldown = time.TotalGameTime.TotalMilliseconds;
                spears.Add(temp);
            }
            spears.RemoveAll(sword => time.TotalGameTime.TotalMilliseconds - attackCooldown > 150);


        }
        public List<Spear> Spears
        {
            get { return spears; }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
            foreach (Spear spear in spears)
            {
                spear.Draw(spriteBatch);
            }
        }
    }
}
