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

namespace Digger
{
    /// <summary>
    /// Klasa pola na mapie. Pole może być zakopane albo odkopane.
    /// </summary>
    public class Field : GameObject
    {
        /// <summary>
        /// Rozmiar pola na mapie
        /// </summary>
        public const int SZ = 50;
        /// <summary>
        /// Informacja czy pole jest odkopane
        /// </summary>
        public bool digged;

        /// <summary>
        /// Metoda odkopująca pole jesli pole jest zakopane. Jeśli polae było wcześniej odkopane, to pozostaje odkopane.
        /// </summary>
        public void dig()
        {
            digged = true;
            texture = null;
        }

        /// <summary>
        /// Konstruktor pola mapy
        /// </summary>
        /// <param name="gameState">Obiekt stanu gry</param>
        /// <param name="position">Początkowa pozycja obiekt</param>
        /// <param name="texture">Tekstura obiektu</param>
        /// <param name="digged">Informacja o tym czy początkowo pole jest odkopane. Domyślnie pole nie jest odkopane</param>
        public Field(GameState gameState, Vector2 position, Texture2D texture, bool digged = false)
            : base(gameState, position, texture)
        {
            this.digged = digged;
        }

        /// <summary>
        /// Metoda aktualizująca stan pola. Nie robi nic, gdyż pole nie podejmuje żadnych akcji ani nie oddziałuje z obiektami na mapie
        /// </summary>
        /// <param name="titalGameTime">Aktualny czas gry</param>
        public override void update(TimeSpan titalGameTime)
        {
            return;
        }
    }
}
