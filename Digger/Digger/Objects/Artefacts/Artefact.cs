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

namespace Digger.Objects.Artefacts
{
    abstract class Artefact : GameObject
    {
        protected int pointBonus;
        protected bool enemySensitive;

        public Artefact()
        {
        }

        public Artefact(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, Vector2 position, Texture2D texture, int pointBonus, bool enemySensitive)
            : base(graphics, spriteBatch, position, texture)
        {
            this.pointBonus = pointBonus;
            this.enemySensitive = enemySensitive;
        }
    }
}
