using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        float attackRange = 50;

        double attackCooldown = 0;

        // Dictionary to store attack textures for each direction
        Dictionary<Direction, Texture2D> attackTextures;

        public Bokoblin(Texture2D texture, float X, float Y, Texture2D spearTexture, ContentManager content)
            : base(texture, X, Y, 3, 3)
        {
            // Initialize attack textures (replace with your actual textures)
            attackTextures = new Dictionary<Direction, Texture2D>()
            {
                { Direction.Up, content.Load<Texture2D>("enemies/Boko_atk_up") },
                { Direction.Down, content.Load<Texture2D>("enemies/Boko_Atk_down") },
                { Direction.Left, content.Load<Texture2D>("enemies/Boko_Atk_left") },
                { Direction.Right, content.Load<Texture2D>("enemies/Boko_Atk_right") },
            };
        }

        public override void Update(GameTime time, Player player)
        {
            Vector2 direction = player.position - position;
            direction.Normalize();  // Normalize to get unit vector

            // Check if within attack range
            if (Vector2.Distance(position, player.position) <= attackRange)
            {
                // Determine attack direction based on largest absolute component
                Direction attackDirection = GetAttackDirection(direction);

                if (time.TotalGameTime.TotalMilliseconds > attackCooldown + 1500)
                {
                    // Perform attack logic
                    attackCooldown = time.TotalGameTime.TotalMilliseconds;
                    isAttacking = true;

                }
            }
            else 
            {
                isAttacking = false;
                position += direction * chaseSpeed;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Player player)
        {
            Texture2D textureToUse;
            if (isAttacking)
            {
                // Use attack texture based on determined direction
                textureToUse = attackTextures[GetAttackDirection(player.position - position)];
            }
            else
            {
                textureToUse = base.texture; // Use default texture when not attacking
            }

            // Draw the appropriate texture based on attack state
            spriteBatch.Draw(textureToUse, position, Color.White);
        }

        private Direction GetAttackDirection(Vector2 direction)
        {
            float absX = Math.Abs(direction.X);
            float absY = Math.Abs(direction.Y);

            if (absY > absX)
            {
                return direction.Y > 0 ? Direction.Down : Direction.Up;
            }
            else
            {
                return direction.X > 0 ? Direction.Right : Direction.Left;
            }
        }

        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }
    }
}