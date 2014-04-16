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

namespace Digger.Objects.Artefacts
{
    class BonusTime : Artefact
    {
        private int timeout;

        public BonusTime(Vector2 position, Texture2D texture, int pointBonus, bool enemySensitive, int timeout)
            : base(position, texture, pointBonus, enemySensitive)
        {
            this.timeout = timeout;
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
