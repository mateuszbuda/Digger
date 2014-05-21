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
    /// Artefakt peleryny niewidki. Jej zebrania powoduje dodanie peleryny do zasobów bohatara. Użycie peleryny sprawia, że bohater jest niewidoczny dla przeciwników i przejście bohateraz przez któegoś przeciwnika sprawia, że bohater nie ginie ale zyskuje punkty.
    /// </summary>
    class Invicloak : Artefact
    {
        /// <summary>
        /// Czas działania peleryny w sekundach
        /// </summary>
        public const int TIMEOUT = 10;
        /// <summary>
        /// Czas gry po jakim peleryna zniknie z mapy
        /// </summary>
        private int timeout;

        /// <summary>
        /// Konstruktor Peleryny Niewidki
        /// </summary>
        /// <param name="gameState">Obiekt stanu gry</param>
        /// <param name="position">Początkowa pozycja obiektu</param>
        /// <param name="texture">Tekstura obiektu</param>
        /// <param name="pointBonus">Ilość punktów za zebranie artefaktu</param>
        /// <param name="enemySensitive">Czy przeciwnicy mogą zebrać dany artefakt</param>
        /// <param name="timeout">Czas gry po którym peleryna przestaje działać</param>
        public Invicloak(GameState gameState, Vector2 position, Texture2D texture, int pointBonus, bool enemySensitive, int timeout)
            : base(gameState, position, texture, pointBonus, enemySensitive)
        {
            this.timeout = timeout;
        }

        /// <summary>
        /// Implementacja aktualizacji stanu peleryny niewidki
        /// </summary>
        /// <param name="gameTime">Czas gry</param>
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
