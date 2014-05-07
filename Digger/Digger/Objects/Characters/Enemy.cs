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
    public abstract class Enemy : Character
    {
        protected int bonusPoints;
        protected int directionUpdateFreq;
        protected bool digger;

        public Enemy(GameState gameState, Vector2 position, Texture2D texture, Vector2 speed, int hp, int bonusPoints, int directionUpdateFreq = 0, bool digger = false)
            : base(gameState, position, texture, speed, hp)
        {
            this.bonusPoints = bonusPoints;
            this.directionUpdateFreq = directionUpdateFreq;
            this.digger = digger;
        }

        public int getBonusPoints()
        {
            return bonusPoints;
        }

        protected Direction getDirectionToGuy()
        {
            Vector2 guyPosition = gameState.guy.getPosition();

            if ((Math.Abs(position.X - guyPosition.X) > Math.Abs(position.Y - guyPosition.Y)))
            {
                if (guyPosition.X > position.X)
                    direction = Direction.E;
                else
                    direction = Direction.W;
            }
            else
            {
                if (guyPosition.Y > position.Y)
                    direction = Direction.S;
                else
                    direction = Direction.N;
            }

            return getDirection();
        }

        protected Direction getDirection()
        {
            // Majors and Generals avoid fires
            if (this is Major || this is General)
                foreach (Weapons.Fire f in gameState.guy.fires)
                    if (f.visible)
                    {
                        if (f.getPosition().Y == position.Y)
                        {
                            if ((int)(position.Y / Field.SZ) - 1 >= 0 &&
                        (Map.getInstance()[(int)(position.X / Field.SZ), (int)(position.Y / Field.SZ) - 1].digged || digger))
                                return Direction.N;
                            if ((int)(position.Y / Field.SZ) + 1 < Map.HEIGHT &&
                        (Map.getInstance()[(int)(position.X / Field.SZ), (int)(position.Y / Field.SZ) + 1].digged || digger))
                                return Direction.S;
                        }
                        if (f.getPosition().X == position.X)
                        {
                            if ((int)(position.X / Field.SZ) + 1 < Map.WIDTH &&
                        (Map.getInstance()[(int)(position.X / Field.SZ) + 1, (int)(position.Y / Field.SZ)].digged || digger))
                                return Direction.E;
                            if ((int)(position.X / Field.SZ) - 1 >= 0 &&
                        (Map.getInstance()[(int)(position.X / Field.SZ) - 1, (int)(position.Y / Field.SZ)].digged || digger))
                                return Direction.W;
                        }
                    }

            // if can continue moving in the same direction
            if (direction == Direction.E)
            {
                if ((int)(position.X / Field.SZ) + 1 < Map.WIDTH &&
                    (Map.getInstance()[(int)(position.X / Field.SZ) + 1, (int)(position.Y / Field.SZ)].digged || digger))
                    return direction;
            }
            else if (direction == Direction.W)
            {
                if ((int)(position.X / Field.SZ) - 1 >= 0 &&
                    (Map.getInstance()[(int)(position.X / Field.SZ) - 1, (int)(position.Y / Field.SZ)].digged || digger))
                    return direction;
            }
            else if (direction == Direction.N)
            {
                if ((int)(position.Y / Field.SZ) - 1 >= 0 &&
                    (Map.getInstance()[(int)(position.X / Field.SZ), (int)(position.Y / Field.SZ) - 1].digged || digger))
                    return direction;
            }
            else if (direction == Direction.S)
            {
                if ((int)(position.Y / Field.SZ) + 1 < Map.HEIGHT &&
                    (Map.getInstance()[(int)(position.X / Field.SZ), (int)(position.Y / Field.SZ) + 1].digged || digger))
                    return direction;
            }

            //if can move in opposite direction
            if (direction == Direction.E)
            {
                if ((int)(position.X / Field.SZ) - 1 >= 0 &&
                    (Map.getInstance()[(int)(position.X / Field.SZ) - 1, (int)(position.Y / Field.SZ)].digged || digger))
                    return Direction.W;
            }
            else if (direction == Direction.W)
            {
                if ((int)(position.X / Field.SZ) + 1 < Map.WIDTH &&
                    (Map.getInstance()[(int)(position.X / Field.SZ) + 1, (int)(position.Y / Field.SZ)].digged || digger))
                    return Direction.E;
            }
            else if (direction == Direction.N)
            {
                if ((int)(position.Y / Field.SZ) + 1 < Map.HEIGHT &&
                    (Map.getInstance()[(int)(position.X / Field.SZ), (int)(position.Y / Field.SZ) + 1].digged || digger))
                    return Direction.S;
            }
            else if (direction == Direction.S)
            {
                if ((int)(position.Y / Field.SZ) - 1 >= 0 &&
                    (Map.getInstance()[(int)(position.X / Field.SZ), (int)(position.Y / Field.SZ) - 1].digged || digger))
                    return Direction.N;
            }

            // must change move axis
            if (direction == Direction.E || direction == Direction.W)
            {
                if ((int)(position.Y / Field.SZ) - 1 >= 0 &&
                    (Map.getInstance()[(int)(position.X / Field.SZ), (int)(position.Y / Field.SZ) - 1].digged || digger))
                    return Direction.N;
                else
                    return Direction.S;
            }
            else if (direction == Direction.N || direction == Direction.S)
            {
                if ((int)(position.X / Field.SZ) + 1 < Map.WIDTH &&
                    (Map.getInstance()[(int)(position.X / Field.SZ) + 1, (int)(position.Y / Field.SZ)].digged || digger))
                    return Direction.E;
                else
                    return Direction.W;
            }

            return direction;
        }
    }
}
