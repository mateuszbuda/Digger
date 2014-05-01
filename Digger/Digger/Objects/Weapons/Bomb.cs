using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Digger.Objects.Weapons
{
    public class Bomb : Weapon
    {
        public const int COUNTDOWN = 3;
        private GameState gameState;
        private int explosionTime;
        public bool visible = false;

        public Bomb(Texture2D texture, GameState gameState)
            : base(Vector2.Zero, texture, Vector2.Zero)
        {
            this.gameState = gameState;
            this.explosionTime = 0;
        }

        public override void update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.Seconds == explosionTime)
                explode();
        }

        private void explode()
        {
            List<Enemy> targets = new List<Enemy>();
            foreach (Enemy e in gameState.enemies)
                if (inRange(e))
                    targets.Add(e);

            foreach (Enemy e in targets)
                if (e.damage(1) < 1)
                {
                    GameState.guy.points += (2 * e.getBonusPoints());
                    gameState.enemies.Remove(e);
                }

            visible = false;
        }

        private bool inRange(Enemy e)
        {
            // X
            if (e.getPosition().Y == position.Y && e.getPosition().X > position.X - 3 * Field.SZ && e.getPosition().X < position.X + 3 * Field.SZ)
                return true;
            // Y
            if (e.getPosition().X == position.X && e.getPosition().Y > position.Y - 3 * Field.SZ && e.getPosition().Y < position.Y + 4 * Field.SZ)
                return true;

            // diag
            if (e.getPosition().X > position.X - Field.SZ && e.getPosition().X < position.X + Field.SZ && e.getPosition().Y > position.Y - Field.SZ && e.getPosition().Y < position.Y + Field.SZ)
                return true;

            return false;
        }

        public void set(Vector2 position, int explosionTime)
        {
            visible = true;
            this.position = position;
            this.explosionTime = explosionTime;
        }

        public override void draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (visible)
                base.draw(spriteBatch, gameTime);
        }
    }
}
