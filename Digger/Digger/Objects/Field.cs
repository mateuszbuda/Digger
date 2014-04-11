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
    class Field : GameObject
    {
        private bool digged;
        public void dig()
        {
            return;
        }

        public Field(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, Vector2 position, Texture2D texture)
            : base(graphics, spriteBatch, position, texture)
        {
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
