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
    class GoldBag : Artefact
    {
        private Vector2 speed;

        public GoldBag(GameState gameState, Vector2 position, Texture2D texture, int pointBonus, bool enemySensitive, Vector2 speed)
            : base(gameState, position, texture, pointBonus, enemySensitive)
        {
            this.speed = speed;
        }

        public override void update(TimeSpan gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
