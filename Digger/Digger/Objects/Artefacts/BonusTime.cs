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
    /// Artefakt czasu bonusowego. Po jego zebraniu bohater przez 25 sekund działa w bonusowym trybie. Po zetknięciu z przeciwnikiem, który nie jest Generalem, bohater zabija go a wszyscy przeciwnicy którzy normalnie poprawiają swoją pozycję w kierunku bohatera, uciekają od niego.
    /// </summary>
    class BonusTime : Artefact
    {
        /// <summary>
        /// Czas w sekundach przez jaki bohater działa w czasie bonusowym po zebraniu artefaktu
        /// </summary>
        private const int TIMEOUT = 25;

        /// <summary>
        /// Konstruktor Czasu Bonusowego
        /// </summary>
        /// <param name="gameState">Obiekt stanu gry</param>
        /// <param name="position">Początkowa pozycja obiektu</param>
        /// <param name="texture">Tekstura obiektu</param>
        /// <param name="pointBonus">Ilość punktów za zebranie artefaktu</param>
        /// <param name="enemySensitive">Czy przeciwnicy mogą zebrać dany artefakt</param>
        public BonusTime(GameState gameState, Vector2 position, Texture2D texture, int pointBonus, bool enemySensitive)
            : base(gameState, position, texture, pointBonus, enemySensitive)
        {
        }

        /// <summary>
        /// implementacja aktualizacji stanu czasu bonusowego
        /// </summary>
        /// <param name="gameTime">Czas gry</param>
        public override void update(TimeSpan gameTime)
        {
            if (texture != null && gameState.guy.getPosition() == position)
            {
                gameState.guy.bonusTime = true;
                gameState.guy.bonusCountdown = (int)gameTime.TotalSeconds + TIMEOUT;
                texture = null;
            }
        }
    }
}
