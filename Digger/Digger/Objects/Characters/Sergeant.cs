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
    /// <summary>
    /// Przeciwnik Sergeant. Podstawowy przeciwnik porusząjący się bez korekcji ruchu w kierunku bohatera. Po prostu odpija się od ścian. Nie posiada żadnych zdolności kopania korytarzy. Można go zabić dowolną bronią.
    /// </summary>
    class Sergeant : Enemy
    {
        /// <summary>
        /// Konstruktor Sergeanta
        /// </summary>
        /// <param name="gameState">Obiekt stanu gry</param>
        /// <param name="position">Początkowa pozycja obiektu</param>
        /// <param name="texture">Tekstura obiektu</param>
        /// <param name="speed">Początkowa prędkość obiektu</param>
        /// <param name="hp">Początkowa ilość żyć przeciwnika</param>
        /// <param name="bonusPoints">Ilość punktów jakie dostaje gracz za zabicie danego przeciwnika</param>
        /// <param name="directionUpdateFreq">Okres aktualizacji kierunku ruchu w kierunku bohatera w sekundach</param>
        /// <param name="digger">Informacja czy dany przeciwnik może odkopywać pola</param>
        public Sergeant(GameState gameState, Vector2 position, Texture2D texture, Vector2 speed, int hp, int bonusPoints, int directionUpdateFreq = 0, bool digger = false)
            : base(gameState, position, texture, speed, hp, bonusPoints, directionUpdateFreq, digger)
        {
        }

        /// <summary>
        /// Implementacja aktualizacji stanu przez Generala
        /// </summary>
        /// <param name="totalGameTime">Czas gry</param>
        public override void update(TimeSpan totalGameTime)
        {
            if (texture == null)
                return;

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
    }
}
