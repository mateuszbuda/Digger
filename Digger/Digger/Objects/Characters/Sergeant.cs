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
    class Sergeant : Enemy
    {
        public Sergeant(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, Vector2 position, Texture2D texture, Vector2 speed, int hp)
            : base(graphics, spriteBatch, position, texture, speed, hp)
        {
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            position += speed;

            if (position.X > MaxX)
            {
                speed.X *= -1;
                position.X = MaxX;
            }
            else if (position.X < MinX)
            {
                speed.X *= -1;
                position.X = MinX;
            }

            if ((int)(position.X / Field.SZ) + 1 < Map.WIDTH && (speed.X > 0 && !DiggerGame.fields[(int)(position.X / Field.SZ) + 1, (int)(position.Y / Field.SZ)].digged) || (speed.X <= 0 && !DiggerGame.fields[(int)(position.X / Field.SZ), (int)(position.Y / Field.SZ)].digged))
                speed.X *= -1;
        }
    }
}
