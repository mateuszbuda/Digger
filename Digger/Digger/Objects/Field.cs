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
    public class Field : GameObject
    {
        public const int SZ = 50;
        public bool digged;
        public void dig()
        {
            digged = true;
            texture = null;
        }

        public Field(GameState gameState, Vector2 position, Texture2D texture, bool digged = false)
            : base(gameState, position, texture)
        {
            this.digged = digged;
        }

        public override void update(GameTime gameTime)
        {
            return;
        }
    }
}
