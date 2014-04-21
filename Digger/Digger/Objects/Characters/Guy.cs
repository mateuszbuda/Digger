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
        private bool invicloak;
        private int cloakCountdown;
        private int bonusCountdown;
        private Keys lastMoveDirection = Keys.Right;
        public int bombCnt;
        public int invicloackCnt;
        private double lastShoot = 0.0;
        private int firesCnt = 1000;
        private List<Fire> fires = new List<Fire>();
        public int points;


        public void shotMissile()
        {
            return;
        }
        public void setBomb()
        {
            return;
        }
        public void putOnInvicloak()
        {
            return;
        }

        public void addMissile()
        {
            firesCnt++;
        }

        public Guy(Vector2 position, Texture2D texture, Vector2 speed, int hp, string username)
            : base(position, texture, speed, hp)
        {
            this.username = username;
            this.historyPosition = position;
        }

        public override void update(GameTime gameTime)
        {
            updateMove();
            updateFires(gameTime);

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
                            f.Shoot(position, fireSpeed);
                            fired = true;
                            break;
                        }
                    if (!fired)
                    {
                        Fire f = new Fire(position, Textures.getFireTex(), Vector2.Zero);
                        fires.Add(f);
                        f.Shoot(position, getFireSpeed());
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
        }
    }
}
