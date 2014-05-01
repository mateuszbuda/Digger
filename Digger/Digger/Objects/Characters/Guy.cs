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
using Digger.Objects;
using Digger.Objects.Artefacts;
using Digger.Objects.Weapons;

namespace Digger.Objects
{
    public class Guy : Character
    {
        private Vector2 historyPosition = Vector2.Zero;
        private bool moving = false;
        private string username;
        private bool invicloak = false;
        private int cloakCountdown;
        private int bonusCountdown;
        private Keys lastMoveDirection = Keys.Right;
        public int bombCnt = 0;
        private double lastBomb = 0.0;
        public List<Weapons.Bomb> bombs = new List<Weapons.Bomb>();
        public int invicloackCnt;
        private double lastEnemyHit = 0.0;
        private double lastShoot = 0.0;
        public int firesCnt = 0;
        public List<Fire> fires = new List<Fire>();
        public int points = 0;
        public GameState gameState;

        public int getHp()
        {
            return hp;
        }

        public Guy(GameState gameState, Vector2 position, Texture2D texture, Vector2 speed, int hp, string username)
            : base(position, texture, speed, hp)
        {
            this.gameState = gameState;
            this.username = username;
            this.historyPosition = position;
        }

        public override void update(GameTime gameTime)
        {
            updateMove();
            updateFires(gameTime);
            updateBombs(gameTime);
            enemyCollisions(gameTime);
        }

        private void updateBombs(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.B) && gameTime.TotalGameTime.TotalMilliseconds - lastBomb > 200)
                if (bombCnt > 0)
                {
                    bombCnt--;
                    lastBomb = gameTime.TotalGameTime.TotalMilliseconds;
                    bool set = false;
                    foreach (Weapons.Bomb b in bombs)
                        if (!b.visible)
                        {
                            b.set(position, gameTime.TotalGameTime.Seconds + Weapons.Bomb.COUNTDOWN);
                            set = true;
                            break;
                        }
                    if (!set)
                    {
                        Weapons.Bomb b = new Weapons.Bomb(Textures.getBombTex(), gameState);
                        bombs.Add(b);
                        b.set(position, gameTime.TotalGameTime.Seconds + Weapons.Bomb.COUNTDOWN);
                    }
                }

            foreach (Weapons.Bomb b in bombs)
                b.update(gameTime);
        }

        private void enemyCollisions(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds - lastEnemyHit < 1500)
                return;

            foreach (Enemy e in gameState.enemies)
                if (hitEnemy(e))
                {
                    this.damage(1);
                    lastEnemyHit = gameTime.TotalGameTime.TotalMilliseconds;
                    break;
                }
        }

        private bool hitEnemy(Enemy e)
        {
            Vector2 middle = new Vector2(e.getPosition().X + Field.SZ / 2, e.getPosition().Y + Field.SZ / 2);
            return middle.X >= position.X && middle.X < position.X + Field.SZ && middle.Y >= position.Y && middle.Y < position.Y + Field.SZ;
        }

        private void updateFires(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.C) && gameTime.TotalGameTime.TotalMilliseconds - lastShoot > 200)
                if (firesCnt > 0)
                {
                    firesCnt--;
                    lastShoot = gameTime.TotalGameTime.TotalMilliseconds;
                    bool fired = false;
                    foreach (Fire f in fires)
                        if (!f.visible)
                        {
                            Vector2 fireSpeed = getFireSpeed();
                            Vector2 position = getFirePosition(fireSpeed);
                            f.shoot(position, fireSpeed);
                            fired = true;
                            break;
                        }
                    if (!fired)
                    {
                        Fire f = new Fire(Textures.getFireTex(), gameState);
                        fires.Add(f);
                        f.shoot(position, getFireSpeed());
                    }
                }

            foreach (Fire f in fires)
                f.update(gameTime);
        }

        private Vector2 getFirePosition(Vector2 fireSpeed)
        {
            if (fireSpeed.X > 0)
                return new Vector2(position.X + Field.SZ, position.Y);
            else if (fireSpeed.X < 0)
                return new Vector2(position.X - Field.SZ, position.Y);
            else if (fireSpeed.Y > 0)
                return new Vector2(position.X, position.Y + Field.SZ);
            else if (fireSpeed.Y < 0)
                return new Vector2(position.X, position.Y - Field.SZ);
            else
                return new Vector2(position.X + Field.SZ, position.Y);
        }

        private Vector2 getFireSpeed()
        {
            if (lastMoveDirection == Keys.Right)
                return new Vector2(5, 0);
            else if (lastMoveDirection == Keys.Left)
                return new Vector2(-5, 0);
            else if (lastMoveDirection == Keys.Down)
                return new Vector2(0, 5);
            else if (lastMoveDirection == Keys.Up)
                return new Vector2(0, -5);
            return new Vector2(5, 0);
        }

        private void updateMove()
        {
            position += speed;
            if (!moving)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    speed.X = -2;
                    speed.Y = 0;
                    moving = true;
                    lastMoveDirection = Keys.Left;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    speed.X = 2;
                    speed.Y = 0;
                    moving = true;
                    lastMoveDirection = Keys.Right;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    speed.X = 0;
                    speed.Y = -2;
                    moving = true;
                    lastMoveDirection = Keys.Up;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    speed.X = 0;
                    speed.Y = 2;
                    moving = true;
                    lastMoveDirection = Keys.Down;
                }
            }

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

            if (Math.Abs(historyPosition.X - position.X) == texture.Width || Math.Abs(historyPosition.Y - position.Y) == texture.Height)
            {
                historyPosition = position;
                speed.X = speed.Y = 0;
                moving = false;
                Map.getInstance()[(int)(position.X / 50), (int)(position.Y / 50)].dig();
            }
        }

        public override void draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            base.draw(spriteBatch, gameTime);
            foreach (Fire f in fires)
                f.draw(spriteBatch, gameTime);
            foreach (Weapons.Bomb b in bombs)
                b.draw(spriteBatch, gameTime);
        }
    }
}
