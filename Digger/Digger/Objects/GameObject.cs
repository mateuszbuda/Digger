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

namespace Digger
{
    public abstract class GameObject
    {
        protected Vector2 position;
        protected Texture2D texture;

        protected int MaxX;
        protected int MinX;
        protected int MaxY;
        protected int MinY;

        public abstract void update(GameTime gameTime);

        public virtual void draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (texture != null)
                spriteBatch.Draw(texture, position, Color.White);
        }

        public virtual void draw(SpriteBatch spriteBatch, GameTime gameTime, float rotation)
        {
            if (texture != null)
            {
                SpriteEffects flip = SpriteEffects.None;
                Vector2 origin = Vector2.Zero;
                if (rotation == MathHelper.Pi)
                {
                    flip = SpriteEffects.FlipHorizontally;
                    rotation = 0;
                }
                else if (rotation == MathHelper.PiOver2)
                {
                    flip = SpriteEffects.FlipHorizontally;
                    rotation *= -1;
                }
                spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, Field.SZ, Field.SZ), null, Color.White, rotation, origin, flip, 0);
            }
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public GameObject(Vector2 position, Texture2D texture)
        {
            this.position = position;
            this.texture = texture;

            MaxX = DiggerGame.GRAPHICS_WIDTH - texture.Width;
            MinX = 0;
            MaxY = DiggerGame.GRAPHICS_HEIGHT - texture.Height;
            MinY = 0;
        }
    }
}
