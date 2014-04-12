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

namespace Digger.Objects
{
    public abstract class Character : GameObject
    {
        protected int hp;
        protected Vector2 speed;

        public Character(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, Vector2 position, Texture2D texture, Vector2 speed, int hp)
            : base(graphics, spriteBatch, position, texture)
        {
            this.speed = speed;
            this.hp = hp;
        }
    }
}
