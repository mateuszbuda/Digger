﻿using System;
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
    class Bomb : Artefact
    {

        public Bomb(GameState gameState, Vector2 position, Texture2D texture, int pointBonus, bool enemySensitive)
            : base(gameState, position, texture, pointBonus, enemySensitive)
        {
        }

        public override void update(TimeSpan gameTime)
        {
            if (texture == null)
                return;

            if (gameState.guy.getPosition() == position)
            {
                gameState.guy.bombCnt++;
                texture = null;
            }

            foreach (Enemy e in gameState.enemies)
                if (e.getPosition() == position)
                    texture = null;
        }
    }
}
