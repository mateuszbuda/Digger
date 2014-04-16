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
    public class Fire : Weapon
    {
        public bool visible = false;

        public Fire(Vector2 position, Texture2D texture, Vector2 speed)
            : base(position, texture, speed)
        {
        }

        public override void update(GameTime gameTime)
        {
            if (!visible)
                return;

            position += speed;

            if (position.X > MaxX || position.X < MinX || position.Y > MaxY || position.Y < MinY)
            {
                visible = false;
                position.X = MaxX;
                position.Y = MaxY;
            }

            if (isOutsideDiggedArea())
            {
                visible = false;
            }
        }

        private bool isOutsideDiggedArea()
        {
            return (int)(position.X / Field.SZ) + 1 < Map.WIDTH &&
                (speed.X > 0 &&
                !Map.getInstance()[(int)(position.X / Field.SZ) + 1, (int)(position.Y / Field.SZ)].digged) ||
                (speed.X <= 0 &&
                !Map.getInstance()[(int)(position.X / Field.SZ), (int)(position.Y / Field.SZ)].digged) ||
                (int)(position.Y / Field.SZ) + 1 < Map.HEIGHT &&
                (speed.Y > 0 &&
                !Map.getInstance()[(int)(position.X / Field.SZ), (int)(position.Y / Field.SZ) + 1].digged);
        }

        public override void draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (visible)
            {
                float angle = 0;
                if (speed.X < 0)
                    angle = MathHelper.Pi;
                else if (speed.Y > 0)
                    angle = MathHelper.PiOver2;
                else if (speed.Y < 0)
                    angle = -MathHelper.PiOver2;
                base.draw(spriteBatch, gameTime, angle);
            }
        }

        public void Shoot(Vector2 position, Vector2 speed)
        {
            this.position = position;
            this.speed = speed;
            visible = true;
        }
    }
}
