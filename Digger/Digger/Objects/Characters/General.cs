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
    class General : Enemy
    {
        public General(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, Vector2 position, Texture2D texture, Vector2 speed, int hp)
            : base(graphics, spriteBatch, position, texture, speed, hp)
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
