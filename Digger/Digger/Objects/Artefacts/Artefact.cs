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
    /// Klasa bazowa dla obiektów gry, które są artefaktami pojawiającymi się w czasie rozgrywki na planszy. Artefakty może zbierać bohater a niektóre z nich również przeciwnicy. Za zebranie artefaktu bohater dostaje punkty.
    /// </summary>
    public abstract class Artefact : GameObject
    {
        /// <summary>
        /// Ilość punktów za zebranie artefaktu
        /// </summary>
        protected int pointBonus;
        /// <summary>
        /// Czy przeciwnicy mogą zebrać dany artefakt
        /// </summary>
        protected bool enemySensitive;

        /// <summary>
        /// Konstruktor Artefaktu
        /// </summary>
        /// <param name="gameState">Obiekt stanu gry</param>
        /// <param name="position">Początkowa pozycja obiektu</param>
        /// <param name="texture">Tekstura obiektu</param>
        /// <param name="pointBonus">Ilość punktów za zebranie artefaktu</param>
        /// <param name="enemySensitive">Czy przeciwnicy mogą zebrać dany artefakt</param>
        public Artefact(GameState gameState, Vector2 position, Texture2D texture, int pointBonus, bool enemySensitive = false)
            : base(gameState, position, texture)
        {
            this.pointBonus = pointBonus;
            this.enemySensitive = enemySensitive;
        }
    }
}
