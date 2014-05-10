using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Text;

namespace Digger.Objects
{
    class Major : Enemy
    {
        private Random rand;
        private int updateTime = 0;
        private int diggerTime;

        public Major(GameState gameState, Vector2 position, Texture2D texture, Vector2 speed, int hp, int bonusPoints, int directionUpdateFreq = 0, bool digger = false)
            : base(gameState, position, texture, speed, hp, bonusPoints, directionUpdateFreq, digger)
        {
            rand = new Random();
            diggerTime = rand.Next(6, 15);
        }

        public override void update(TimeSpan totalGameTime)
        {
            if (texture == null)
                return;

            position += speed;

            if (diggerTime <= totalGameTime.TotalSeconds)
            {
                digger = !digger;
                diggerTime = (int)totalGameTime.TotalSeconds + rand.Next(6, 15);
            }

            if (!moving)
            {
                if (updateTime <= totalGameTime.TotalSeconds)
                {
                    updateTime = (int)totalGameTime.TotalSeconds + directionUpdateFreq;
                    direction = getDirectionToGuy();
                }
                else
                    direction = getDirection();

                if (direction == Direction.W)
                {
                    speed.X = -2;
                    speed.Y = 0;
                    moving = true;
                }
                else if (direction == Direction.E)
                {
                    speed.X = 2;
                    speed.Y = 0;
                    moving = true;
                }
                else if (direction == Direction.N)
                {
                    speed.X = 0;
                    speed.Y = -2;
                    moving = true;
                }
                else if (direction == Direction.S)
                {
                    speed.X = 0;
                    speed.Y = 2;
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

            if (Math.Abs(historyPosition.X - position.X) == Field.SZ || Math.Abs(historyPosition.Y - position.Y) == Field.SZ)
            {
                historyPosition = position;
                speed.X = speed.Y = 0;
                moving = false;
                if (digger)
                    Map.getInstance()[(int)(position.X / Field.SZ), (int)(position.Y / Field.SZ)].dig();
            }
        }
    }
}
