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
    /// <summary>
    /// Artefakt worka ze złotem. Spada pod wpływem grawitacji, jeśli nie stoi na zakopanym polu. Jeśli spadnie na jakąs postać, zadaje jej obrażenia. Jesli spadnie z wysokości większej niż jedno pole, zamienia się w złoto. Spadający worek jest jedynym sposobem na zabicie Generala.
    /// </summary>
    class GoldBag : Artefact
    {
        /// <summary>
        /// Odległość na jaką postać musi się zbliżyć do worka, żeby go przesunąć
        /// </summary>
        private const int EPS = 5;
        /// <summary>
        /// Aktualna prędkość worka
        /// </summary>
        private Vector2 speed;
        /// <summary>
        /// Zmienna informująca czy worek akturlnie się porusza
        /// </summary>
        private bool moving;
        /// <summary>
        /// Pspółrzędna ostatniego pola na jakim znajdował się worek
        /// </summary>
        private Vector2 historyPosition;
        /// <summary>
        /// Wysokość jaką pokonał worek podczas ostatniego spadku
        /// </summary>
        private int h = 0;

        /// <summary>
        /// Kostruktor worka ze złotem
        /// </summary>
        /// <param name="gameState">Obiekt stanu gry</param>
        /// <param name="position">Początkowa pozycja obiektu</param>
        /// <param name="texture">Tekstura obiektu</param>
        /// <param name="pointBonus">Ilość punktów za zebranie artefaktu</param>
        /// <param name="enemySensitive">Czy przeciwnicy mogą zebrać dany artefakt</param>
        /// <param name="speed">Początkowa prędkość worka</param>
        public GoldBag(GameState gameState, Vector2 position, Texture2D texture, int pointBonus, bool enemySensitive, Vector2 speed)
            : base(gameState, position, texture, pointBonus, enemySensitive)
        {
            this.speed = speed;
            this.moving = false;
            this.historyPosition = position;
        }

        /// <summary>
        /// Implementacja aktualizacji stanu worka ze złotem
        /// </summary>
        /// <param name="gameTime">Czas gry</param>
        public override void update(TimeSpan gameTime)
        {
            if (texture == null)
                return;

            position += speed;

            if (!moving)
            {
                setXSpeed();
                setYSpeed();
            }
            else
                collisions();

            if (position.X > MaxX)
            {
                speed.X = 0;
                position.X = MaxX;
                moving = false;
            }
            else if (position.X < MinX)
            {
                speed.X = 0;
                position.X = MinX;
                moving = false;
            }

            if (position.Y > MaxY)
            {
                speed.Y = 0;
                position.Y = MaxY;
                moving = false;
            }
            else if (position.Y < MinY)
            {
                speed.Y = 0;
                position.Y = MinY;
                moving = false;
            }

            if (Math.Abs(historyPosition.X - position.X) == Field.SZ || Math.Abs(historyPosition.Y - position.Y) == Field.SZ)
            {
                int x = (int)(position.X / Field.SZ), y = (int)(position.Y / Field.SZ);
                historyPosition = position;
                speed.X = speed.Y = 0;
                moving = false;
            }
        }

        /// <summary>
        /// Sprawdza kolizje z postaciami na mapie
        /// </summary>
        private void collisions()
        {
            if (speed.Y > 0)
            {
                List<Enemy> targets = new List<Enemy>();
                foreach (Character e in gameState.enemies)
                    if (hitCharacter(e))
                    {
                        if (e.damage(1) < 1)
                            targets.Add((Enemy)e);
                    }
                foreach (Enemy e in targets)
                {
                    e.damage(1);
                    gameState.enemies.Remove(e);
                }
                if (hitCharacter(gameState.guy))
                    gameState.guy.damage(1);
            }
        }

        /// <summary>
        /// Sprzawdza czy dana postać została trafiona przez spadający worek
        /// </summary>
        /// <param name="e">Postać, z którą sprawdzana jest kolizja</param>
        /// <returns>Informacja czy postać przycina drogą spadającego worka</returns>
        private bool hitCharacter(Character e)
        {
            float x = e.getPosition().X, y = e.getPosition().Y;
            return x < position.X + Field.SZ && x + Field.SZ > position.X && y < position.Y + Field.SZ / 2 && y > position.Y;
        }

        /// <summary>
        /// Ustawia aktualną prędkość w kierunku osi Y dla worka 
        /// </summary>
        private void setYSpeed()
        {
            int x = (int)(position.X / Field.SZ), y = (int)(position.Y / Field.SZ);
            if (y + 1 < Map.HEIGHT && Map.getInstance()[x, y + 1].digged)
            {
                speed.X = 0;
                speed.Y = 1;
                moving = true;
                h++;
            }
            else
            {
                if (h > 1)
                {
                    texture = null;
                    gameState.addArtefact(new Gold(gameState, position, Textures.getGoldTex(), 50, true));
                }
                h = 0;
            }
        }

        /// <summary>
        /// Ustawia aktualną prędkość w kierunku osi X dla worka
        /// </summary>
        private void setXSpeed()
        {
            Vector2 p;
            int x, y = (int)(position.Y / Field.SZ);
            bool guy = false;
            p = gameState.guy.getPosition();
            if ((p.Y == position.Y && (Math.Abs(p.X - position.X + Field.SZ) < EPS || Math.Abs(p.X - position.X - Field.SZ) < EPS)))
            {
                x = (int)((position.X + gameState.guy.getSpeed().X) / Field.SZ);
                if (gameState.guy.getSpeed().X > 0)
                    x++;
                if (x < Map.WIDTH && x >= 0)
                    if (Map.getInstance()[x, y].digged)
                    {
                        if ((gameState.guy.getSpeed().X > 0 && position.X - p.X > 0) || (gameState.guy.getSpeed().X < 0 && position.X - p.X < 0))
                        {
                            speed = gameState.guy.getSpeed();
                            moving = true;
                            guy = true;
                        }
                    }
            }
            if (!guy)
                foreach (Enemy e in gameState.enemies)
                {
                    p = e.getPosition();
                    if ((p.Y == position.Y && (Math.Abs(p.X - position.X + Field.SZ) < EPS || Math.Abs(p.X - position.X - Field.SZ) < EPS)))
                    {
                        x = (int)((position.X + e.getSpeed().X) / Field.SZ);
                        if (e.getSpeed().X > 0)
                            x++;
                        if (x < Map.WIDTH && x >= 0)
                            if (Map.getInstance()[x, y].digged)
                            {
                                if ((e.getSpeed().X > 0 && position.X - p.X > 0) || (e.getSpeed().X < 0 && position.X - p.X < 0))
                                {
                                    speed = e.getSpeed();
                                    moving = true;
                                    break;
                                }
                            }
                    }
                }
        }
    }
}
