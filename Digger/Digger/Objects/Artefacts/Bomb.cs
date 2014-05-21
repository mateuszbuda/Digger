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
    /// Artefakt bomby. Mogą go zebrać wszystkie postacie. Jeśli zbierze go bohater, to zwiększa mu się ilość dostępnych do zastawwienia bomb.
    /// </summary>
    class Bomb : Artefact
    {
        /// <summary>
        /// Konstruktor Artefaktu Bomby
        /// </summary>
        /// <param name="gameState">Obiekt stanu gry</param>
        /// <param name="position">Początkowa pozycja obiektu</param>
        /// <param name="texture">Tekstura obiektu</param>
        /// <param name="pointBonus">Ilość punktów za zebranie artefaktu</param>
        /// <param name="enemySensitive">Czy przeciwnicy mogą zebrać dany artefakt</param>
        public Bomb(GameState gameState, Vector2 position, Texture2D texture, int pointBonus, bool enemySensitive)
            : base(gameState, position, texture, pointBonus, enemySensitive)
        {
        }

        /// <summary>
        /// Implementacja aktualizacji stanu bomby
        /// </summary>
        /// <param name="gameTime">Czas gry</param>
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
