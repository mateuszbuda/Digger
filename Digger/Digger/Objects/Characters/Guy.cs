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
        private string username;
        public bool invicloak = true;
        private int cloakCountdown = 0;
        private int nextBlink = 0;
        public bool bonusTime = false;
        public int bonusCountdown = 0;
        private Keys lastMoveDirection = Keys.Right;
        public int bombCnt = 20;
        private double lastBomb = 0.0;
        public List<Weapons.Bomb> bombs = new List<Weapons.Bomb>();
        public int invicloackCnt = 0;
        private double lastEnemyHit = 0.0;
        private double lastShoot = 0.0;
        public int firesCnt = 20;
        public List<Fire> fires = new List<Fire>();
        public int points = 0;

        public int getHp()
        {
            return hp;
        }

        public Guy(GameState gameState, Vector2 position, Texture2D texture, Vector2 speed, int hp, string username)
            : base(gameState, position, texture, speed, hp)
        {
            this.username = username;
        }

        public override void update(TimeSpan gameTime)
        {
            updateMove();
            updateFires(gameTime);
            updateBombs(gameTime);
            updateInvicloak(gameTime);
            enemyCollisions(gameTime);
        }

        private void updateInvicloak(TimeSpan gameTime)
        {
            if (invicloak)
            {
                if ((int)gameTime.TotalSeconds >= cloakCountdown)
                {
                    invicloak = false;
                    texture = Textures.getGuyTex();
                    return;
                }

                if ((int)gameTime.TotalMilliseconds >= nextBlink)
                {
                    if (texture == null)
                        texture = Textures.getGuyTex();
                    else
                        texture = null;
                    nextBlink += 300;
                }

                return;
            }

            if (Keyboard.GetState().IsKeyDown(Settings.invclk) && invicloackCnt > 0)
            {
                invicloackCnt--;
                invicloak = true;
                cloakCountdown = (int)gameTime.TotalSeconds + Invicloak.TIMEOUT;
                nextBlink = (int)gameTime.TotalMilliseconds + 200;
            }
        }

        private void updateBombs(TimeSpan gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Settings.bomb) && gameTime.TotalMilliseconds - lastBomb > 200)
                if (bombCnt > 0)
                {
                    bombCnt--;
                    lastBomb = gameTime.TotalMilliseconds;
                    bool set = false;
                    foreach (Weapons.Bomb b in bombs)
                        if (!b.visible)
                        {
                            b.set(setBombPosition(), (int)gameTime.TotalSeconds + Weapons.Bomb.COUNTDOWN);
                            set = true;
                            break;
                        }
                    if (!set)
                    {
                        Weapons.Bomb b = new Weapons.Bomb(gameState, Textures.getBombTex());
                        bombs.Add(b);
                        b.set(setBombPosition(), (int)gameTime.TotalSeconds + Weapons.Bomb.COUNTDOWN);
                    }
                }

            foreach (Weapons.Bomb b in bombs)
                b.update(gameTime);
        }

        private Vector2 setBombPosition()
        {
            Vector2 middle = new Vector2(position.X + Field.SZ / 2, position.Y + Field.SZ / 2);
            return new Vector2((int)(middle.X / Field.SZ) * Field.SZ, (int)(middle.Y / Field.SZ) * Field.SZ);
        }

        private void enemyCollisions(TimeSpan gameTime)
        {
            if (gameTime.TotalSeconds > bonusCountdown)
                bonusTime = false;

            if (gameTime.TotalMilliseconds - lastEnemyHit < 1200)
                return;

            foreach (Enemy e in gameState.enemies)
                if (hitEnemy(e))
                {
                    if (e is General)
                    {
                        this.damage(1);
                        lastEnemyHit = gameTime.TotalMilliseconds;
                        if (bonusTime || invicloak)
                            lastEnemyHit -= 700;
                        break;
                    }

                    if (invicloak)
                    {
                        points += 50;
                        lastEnemyHit = gameTime.TotalMilliseconds - 700;
                        break;
                    }

                    if (bonusTime)
                    {
                        if (e.damage(1) < 1)
                            gameState.enemies.Remove(e);
                        lastEnemyHit = gameTime.TotalMilliseconds - 700;
                        break;
                    }

                    this.damage(1);
                    lastEnemyHit = gameTime.TotalMilliseconds;
                }
        }

        private bool hitEnemy(Enemy e)
        {
            Vector2 middle = new Vector2(e.getPosition().X + Field.SZ / 2, e.getPosition().Y + Field.SZ / 2);
            return middle.X >= position.X && middle.X < position.X + Field.SZ && middle.Y >= position.Y && middle.Y < position.Y + Field.SZ;
        }

        private void updateFires(TimeSpan gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Settings.fire) && gameTime.TotalMilliseconds - lastShoot > 200)
                if (firesCnt > 0)
                {
                    firesCnt--;
                    lastShoot = gameTime.TotalMilliseconds;
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
                        Fire f = new Fire(gameState, Textures.getFireTex());
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

            if (Math.Abs(historyPosition.X - position.X) == Field.SZ || Math.Abs(historyPosition.Y - position.Y) == Field.SZ)
            {
                historyPosition = position;
                speed.X = speed.Y = 0;
                moving = false;
                Map.getInstance()[(int)(position.X / Field.SZ), (int)(position.Y / Field.SZ)].dig();
            }
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);
            foreach (Fire f in fires)
                f.draw(spriteBatch);
            foreach (Weapons.Bomb b in bombs)
                b.draw(spriteBatch);
        }
    }
}
