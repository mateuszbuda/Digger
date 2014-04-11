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

namespace Digger.Objects
{
    class Guy : Character
    {
        private Vector2 historyPosition;

        private bool moving = false;
        private string username;
        private bool invicloak;
        private int cloakCountdown;
        private int bonusCountdown;

        public int bombCnt;
        public int invicloackCnt;
        public int missileCnt;
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

        public Guy(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, Vector2 position, Texture2D texture, Vector2 speed, int hp, string username)
            : base(graphics, spriteBatch, position, texture, speed, hp)
        {
            this.username = username;
            this.historyPosition = position;
        }

        public override void update(GameTime gameTime)
        {
            position += speed;
            if (!moving)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    speed.X = -4;
                    speed.Y = 0;
                    moving = true;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    speed.X = 4;
                    speed.Y = 0;
                    moving = true;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    speed.X = 0;
                    speed.Y = -4;
                    moving = true;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    speed.X = 0;
                    speed.Y = 4;
                    moving = true;
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
            }
        }

        public override void draw(GameTime gameTime)
        {
            base.draw(gameTime);
        }
    }
}
