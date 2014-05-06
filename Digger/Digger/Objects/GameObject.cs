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
        protected GameState gameState;
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
                Vector2 origin = new Vector2(Field.SZ / 2, Field.SZ / 2);
                spriteBatch.Draw(texture, new Vector2(position.X + Field.SZ / 2, position.Y + Field.SZ / 2), null, Color.White, rotation, origin, Vector2.One, SpriteEffects.None, 0f);
            }
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public GameObject(GameState gameState, Vector2 position, Texture2D texture)
        {
            this.gameState = gameState;
            this.position = position;
            this.texture = texture;

            MaxX = DiggerGame.GRAPHICS_WIDTH - texture.Width;
            MinX = 0;
            MaxY = DiggerGame.GRAPHICS_HEIGHT - texture.Height;
            MinY = 0;
        }
    }
}
