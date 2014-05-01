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
using Digger.Objects.Weapons;

namespace Digger.Objects
{
    class Sergeant : Enemy
    {
        public Sergeant(Vector2 position, Texture2D texture, Vector2 speed, int hp, int bonusPoints, int directionUpdateFreq = 0, bool digger = false)
            : base(position, texture, speed, hp, bonusPoints, directionUpdateFreq, digger)
        {
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (texture == null)
                return;

            foreach (Fire f in GameState.guy.fires)
                if (f.visible)
                    if (hitTarget(f))
                    {
                        texture = null;
                        f.visible = false;
                    }

            position += speed;

            if (position.X > MaxX)
            {
                speed.X *= -1;
                position.X = MaxX;
            }
            else if (position.X < MinX)
            {
                speed.X *= -1;
                position.X = MinX;
            }

            if ((int)(position.X / Field.SZ) + 1 < Map.WIDTH &&
                (speed.X > 0 &&
                !Map.getInstance()[(int)(position.X / Field.SZ) + 1, (int)(position.Y / Field.SZ)].digged) ||
                (speed.X <= 0 &&
                !Map.getInstance()[(int)(position.X / Field.SZ), (int)(position.Y / Field.SZ)].digged))
            {
                speed.X *= -1;
            }
        }

        private bool hitTarget(Fire f)
        {
            Vector2 middle = new Vector2(f.getPosition().X + Field.SZ / 2, f.getPosition().Y + Field.SZ / 2);
            return middle.X >= position.X && middle.X < position.X + Field.SZ && middle.Y >= position.Y && middle.Y < position.Y + Field.SZ;
        }
    }
}
