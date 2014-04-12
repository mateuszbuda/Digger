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
        protected GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;
        protected Vector2 position;
        protected Texture2D texture;

        protected int MaxX;
        protected int MinX;
        protected int MaxY;
        protected int MinY;

        public abstract void update(GameTime gameTime);
        public virtual void draw(GameTime gameTime)
        {
            if (texture != null)
                spriteBatch.Draw(texture, position, Color.White);
        }

        public GameObject()
        {
        }

        public virtual Vector2 getPosition()
        {
            return position;
        }

        public GameObject(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, Vector2 position, Texture2D texture)
        {
            this.graphics = graphics;
            this.spriteBatch = spriteBatch;
            this.position = position;
            this.texture = texture;

            MaxX = graphics.GraphicsDevice.Viewport.Width - texture.Width;
            MinX = 0;
            MaxY = graphics.GraphicsDevice.Viewport.Height - texture.Height;
            MinY = 0;
        }
    }
}
