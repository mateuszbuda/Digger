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

namespace Digger.Objects.Artefacts
{
    class GoldBag : Artefact
    {
        private const int EPS = 5;
        private Vector2 speed;
        private bool moving;
        private Vector2 historyPosition;
        private int h = 0;

        public GoldBag(GameState gameState, Vector2 position, Texture2D texture, int pointBonus, bool enemySensitive, Vector2 speed)
            : base(gameState, position, texture, pointBonus, enemySensitive)
        {
            this.speed = speed;
            this.moving = false;
            this.historyPosition = position;
        }

        public override void update(TimeSpan gameTime)
        {
            if (texture == null)
                return;

            position += speed;

            if (!moving)
            {
                setXSpeed();
                setYSpeed();
            }
            else
                collisions();

            if (position.X > MaxX)
            {
                speed.X = 0;
                position.X = MaxX;
                moving = false;
            }
            else if (position.X < MinX)
            {
                speed.X = 0;
                position.X = MinX;
                moving = false;
            }

            if (position.Y > MaxY)
            {
                speed.Y = 0;
                position.Y = MaxY;
                moving = false;
            }
            else if (position.Y < MinY)
            {
                speed.Y = 0;
                position.Y = MinY;
                moving = false;
            }

            if (Math.Abs(historyPosition.X - position.X) == Field.SZ || Math.Abs(historyPosition.Y - position.Y) == Field.SZ)
            {
                historyPosition = position;
                speed.X = speed.Y = 0;
                moving = false;
            }
        }

        private void collisions()
        {
            if (speed.Y > 0)
            {
                List<Enemy> targets = new List<Enemy>();
                foreach (Character e in gameState.enemies)
                    if (hitCharacter(e))
                    {
                        if (e.damage(1) < 1)
                            targets.Add((Enemy)e);
                    }
                foreach (Enemy e in targets)
                {
                    e.damage(1);
                    gameState.enemies.Remove(e);
                }
                if (hitCharacter(gameState.guy))
                    gameState.guy.damage(1);
            }
        }

        private bool hitCharacter(Character e)
        {
            float x = e.getPosition().X, y = e.getPosition().Y;
            return x < position.X + Field.SZ && x + Field.SZ > position.X && y < position.Y + Field.SZ && y > position.Y;
        }

        private void setYSpeed()
        {
            int x = (int)(position.X / Field.SZ), y = (int)(position.Y / Field.SZ);
            if (y + 1 < Map.HEIGHT && Map.getInstance()[x, y + 1].digged)
            {
                speed.X = 0;
                speed.Y = 2;
                moving = true;
                h++;
            }
            else
            {
                if (h > 1)
                {
                    texture = null;
                    gameState.addArtefact(new Gold(gameState, position, Textures.getGoldTex(), 50, true));
                }
                h = 0;
            }
        }

        private void setXSpeed()
        {
            Vector2 p;
            int x, y = (int)(position.Y / Field.SZ);
            bool guy = false;
            p = gameState.guy.getPosition();
            if ((p.Y == position.Y && (Math.Abs(p.X - position.X + Field.SZ) < EPS || Math.Abs(p.X - position.X - Field.SZ) < EPS)))
            {
                x = (int)((position.X + gameState.guy.getSpeed().X) / Field.SZ);
                if (gameState.guy.getSpeed().X > 0)
                    x++;
                if (x < Map.WIDTH && x >= 0)
                    if (Map.getInstance()[x, y].digged)
                    {
                        if ((gameState.guy.getSpeed().X > 0 && position.X - p.X > 0) || (gameState.guy.getSpeed().X < 0 && position.X - p.X < 0))
                        {
                            speed = gameState.guy.getSpeed();
                            moving = true;
                            guy = true;
                        }
                    }
            }
            if (!guy)
                foreach (Enemy e in gameState.enemies)
                {
                    p = e.getPosition();
                    if ((p.Y == position.Y && (Math.Abs(p.X - position.X + Field.SZ) < EPS || Math.Abs(p.X - position.X - Field.SZ) < EPS)))
                    {
                        x = (int)((position.X + e.getSpeed().X) / Field.SZ);
                        if (e.getSpeed().X > 0)
                            x++;
                        if (x < Map.WIDTH && x >= 0)
                            if (Map.getInstance()[x, y].digged)
                            {
                                if ((e.getSpeed().X > 0 && position.X - p.X > 0) || (e.getSpeed().X < 0 && position.X - p.X < 0))
                                {
                                    speed = e.getSpeed();
                                    moving = true;
                                    break;
                                }
                            }
                    }
                }
        }
    }
}
