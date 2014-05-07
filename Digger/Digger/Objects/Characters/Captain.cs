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
    class Captain : Enemy
    {
        private int updateTime = 0;

        public Captain(GameState gameState, Vector2 position, Texture2D texture, Vector2 speed, int hp, int bonusPoints, int directionUpdateFreq = 0, bool digger = false)
            : base(gameState, position, texture, speed, hp, bonusPoints, directionUpdateFreq, digger)
        {
        }

        public override void update(GameTime gameTime)
        {
            if (texture == null)
                return;

            position += speed;

            if (!moving)
            {
                if (updateTime <= gameTime.TotalGameTime.TotalSeconds)
                {
                    updateTime = (int)gameTime.TotalGameTime.TotalSeconds + directionUpdateFreq;
                    direction = getDirectionToGuy();
                }
                else
                    direction = getDirection(direction);

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
            }
        }

        private Direction getDirectionToGuy()
        {
            Vector2 guyPosition = gameState.guy.getPosition();
            Direction toGuy;

            if ((Math.Abs(position.X - guyPosition.X) > Math.Abs(position.Y - guyPosition.Y)))
            {
                if (guyPosition.X > position.X)
                    toGuy = Direction.E;
                else
                    toGuy = Direction.W;
            }
            else
            {
                if (guyPosition.Y > position.Y)
                    toGuy = Direction.S;
                else
                    toGuy = Direction.N;
            }

            return getDirection(toGuy);
        }

        private Direction getDirection(Direction direction)
        {
            // if can continue moving in the same direction
            if (direction == Direction.E)
            {
                if ((int)(position.X / Field.SZ) + 1 < Map.WIDTH &&
                    Map.getInstance()[(int)(position.X / Field.SZ) + 1, (int)(position.Y / Field.SZ)].digged)
                    return direction;
            }
            else if (direction == Direction.W)
            {
                if ((int)(position.X / Field.SZ) - 1 >= 0 &&
                    Map.getInstance()[(int)(position.X / Field.SZ) - 1, (int)(position.Y / Field.SZ)].digged)
                    return direction;
            }
            else if (direction == Direction.N)
            {
                if ((int)(position.Y / Field.SZ) - 1 >= 0 &&
                    Map.getInstance()[(int)(position.X / Field.SZ), (int)(position.Y / Field.SZ) - 1].digged)
                    return direction;
            }
            else if (direction == Direction.S)
            {
                if ((int)(position.Y / Field.SZ) + 1 < Map.HEIGHT &&
                    Map.getInstance()[(int)(position.X / Field.SZ), (int)(position.Y / Field.SZ) + 1].digged)
                    return direction;
            }

            //if can move in opposite direction
            if (direction == Direction.E)
            {
                if ((int)(position.X / Field.SZ) - 1 >= 0 &&
                    Map.getInstance()[(int)(position.X / Field.SZ) - 1, (int)(position.Y / Field.SZ)].digged)
                    return Direction.W;
            }
            else if (direction == Direction.W)
            {
                if ((int)(position.X / Field.SZ) + 1 < Map.WIDTH &&
                    Map.getInstance()[(int)(position.X / Field.SZ) + 1, (int)(position.Y / Field.SZ)].digged)
                    return Direction.E;
            }
            else if (direction == Direction.N)
            {
                if ((int)(position.Y / Field.SZ) + 1 < Map.HEIGHT &&
                    Map.getInstance()[(int)(position.X / Field.SZ), (int)(position.Y / Field.SZ) + 1].digged)
                    return Direction.S;
            }
            else if (direction == Direction.S)
            {
                if ((int)(position.Y / Field.SZ) - 1 >= 0 &&
                    Map.getInstance()[(int)(position.X / Field.SZ), (int)(position.Y / Field.SZ) - 1].digged)
                    return Direction.N;
            }

            // must change move axis
            if (direction == Direction.E || direction == Direction.W)
            {
                if ((int)(position.Y / Field.SZ) - 1 >= 0 &&
                    Map.getInstance()[(int)(position.X / Field.SZ), (int)(position.Y / Field.SZ) - 1].digged)
                    return Direction.N;
                else
                    return Direction.S;
            }
            else if (direction == Direction.N || direction == Direction.S)
            {
                if ((int)(position.X / Field.SZ) + 1 < Map.WIDTH &&
                    Map.getInstance()[(int)(position.X / Field.SZ) + 1, (int)(position.Y / Field.SZ)].digged)
                    return Direction.E;
                else
                    return Direction.W;
            }

            return direction;
        }
    }
}
