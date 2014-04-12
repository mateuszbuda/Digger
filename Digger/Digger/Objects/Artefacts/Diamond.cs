﻿using System;
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
    class Diamond : Artefact
    {
        public Diamond(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, Vector2 position, Texture2D texture, int pointBonus, bool enemySensitive)
            : base(graphics, spriteBatch, position, texture, pointBonus, enemySensitive)
        {
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (DiggerGame.guy.getPosition() == position)
                texture = null;
        }
    }
}
