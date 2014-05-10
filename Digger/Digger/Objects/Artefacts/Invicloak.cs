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
    class Invicloak : Artefact
    {
        public const int TIMEOUT = 10;
        private int timeout;

        public Invicloak(GameState gameState, Vector2 position, Texture2D texture, int pointBonus, bool enemySensitive, int timeout)
            : base(gameState, position, texture, pointBonus, enemySensitive)
        {
            this.timeout = timeout;
        }

        public override void update(TimeSpan gameTime)
        {
            if (texture != null && gameState.guy.getPosition() == position)
            {
                gameState.guy.invicloackCnt++;
                texture = null;
            }

            if (timeout < gameTime.TotalSeconds)
                texture = null;
        }
    }
}
