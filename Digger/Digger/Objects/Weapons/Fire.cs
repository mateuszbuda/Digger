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

namespace Digger.Objects.Weapons
{
    public class Fire : Weapon
    {
        public bool visible = false;

        public Fire(GameState gameState, Texture2D texture)
            : base(gameState, Vector2.Zero, texture, Vector2.Zero)
        {
        }

        public override void update(TimeSpan totalGameTime)
        {
            if (!visible)
                return;

            position += speed;

            if (position.X > MaxX || position.X < MinX || position.Y > MaxY || position.Y < MinY)
            {
                visible = false;
                position.X = MaxX;
                position.Y = MaxY;
            }

            if (isOutsideDiggedArea())
                visible = false;

            Enemy target = null;
            foreach (Enemy e in gameState.enemies)
                if (hitTarget(e))
                {
                    target = e;
                    if (!(target is Colonel))
                        this.visible = false;
                    break;
                }

            if (target != null && !(target is Colonel) && !(target is General))
                if (target.damage(1) < 1)
                {
                    gameState.guy.points += target.getBonusPoints();
                    gameState.enemies.Remove(target);
                    return;
                }
            if (target is Colonel || target is General)
                speed = -speed;
        }

        private bool hitTarget(Character e)
        {
            Vector2 middle = new Vector2(e.getPosition().X + Field.SZ / 2, e.getPosition().Y + Field.SZ / 2);
            return middle.X >= position.X && middle.X < position.X + Field.SZ && middle.Y >= position.Y && middle.Y < position.Y + Field.SZ;
        }

        private bool isOutsideDiggedArea()
        {
            return (int)(position.X / Field.SZ) + 1 < Map.WIDTH &&
                (speed.X > 0 &&
                !Map.getInstance()[(int)(position.X / Field.SZ) + 1, (int)(position.Y / Field.SZ)].digged) ||
                (speed.X <= 0 &&
                !Map.getInstance()[(int)(position.X / Field.SZ), (int)(position.Y / Field.SZ)].digged) ||
                (int)(position.Y / Field.SZ) + 1 < Map.HEIGHT &&
                (speed.Y > 0 &&
                !Map.getInstance()[(int)(position.X / Field.SZ), (int)(position.Y / Field.SZ) + 1].digged);
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            if (visible)
            {
                float angle = 0;
                if (speed.X < 0)
                    angle = MathHelper.Pi;
                else if (speed.Y > 0)
                    angle = MathHelper.PiOver2;
                else if (speed.Y < 0)
                    angle = -MathHelper.PiOver2;
                base.draw(spriteBatch, angle);
            }
        }

        public void shoot(Vector2 position, Vector2 speed)
        {
            this.position = position;
            this.speed = speed;
            visible = true;
        }
    }
}
