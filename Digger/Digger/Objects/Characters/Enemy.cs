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
    public abstract class Enemy : Character
    {
        protected int bonusPoints;
        protected int directionUpdateFreq;
        protected bool digger;

        public Enemy(GameState gameState, Vector2 position, Texture2D texture, Vector2 speed, int hp, int bonusPoints, int directionUpdateFreq = 0, bool digger = false)
            : base(gameState, position, texture, speed, hp)
        {
            this.bonusPoints = bonusPoints;
            this.directionUpdateFreq = directionUpdateFreq;
            this.digger = digger;
        }

        public int getBonusPoints()
        {
            return bonusPoints;
        }
    }
}
