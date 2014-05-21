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

namespace Digger.Objects.Artefacts
{
    /// <summary>
    /// Artefakt diamentu. Podstawowy artefakt, który zbiera bohater. W celu przejścia do następnego poziomu, musi zebrać wszystkie diamenty na aktualnym poziomie.
    /// </summary>
    class Diamond : Artefact
    {
        /// <summary>
        /// Konstruktor Artefaktu Bomby
        /// </summary>
        /// <param name="gameState">Obiekt stanu gry</param>
        /// <param name="position">Początkowa pozycja obiektu</param>
        /// <param name="texture">Tekstura obiektu</param>
        /// <param name="pointBonus">Ilość punktów za zebranie artefaktu</param>
        /// <param name="enemySensitive">Czy przeciwnicy mogą zebrać dany artefakt</param>
        public Diamond(GameState gameState, Vector2 position, Texture2D texture, int pointBonus, bool enemySensitive)
            : base(gameState, position, texture, pointBonus, enemySensitive)
        {
        }

        /// <summary>
        /// Implementacja aktualizacji stanu bomby
        /// </summary>
        /// <param name="gameTime">Czas gry</param>
        public override void update(TimeSpan gameTime)
        {
            if (texture != null && gameState.guy.getPosition() == position)
            {
                gameState.guy.points += 10;
                --gameState.diamonds;
                texture = null;
            }
        }
    }
}
