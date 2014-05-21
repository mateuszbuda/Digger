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

namespace Digger.Objects.Weapons
{
    /// <summary>
    /// Klasa bazowa dla broni uzywanych przez bohatera podczas rozgrywki.
    /// </summary>
    public abstract class Weapon : GameObject
    {
        /// <summary>
        /// Prekość poruszania się broni
        /// </summary>
        protected Vector2 speed;

        /// <summary>
        /// Konstruktor obiektu broni
        /// </summary>
        /// <param name="gameState">Obiekt stanu gry</param>
        /// <param name="position">Początkowa pozycja broni</param>
        /// <param name="texture">Tekstura broni</param>
        /// <param name="speed">Początkowa prędkość broni</param>
        public Weapon(GameState gameState, Vector2 position, Texture2D texture, Vector2 speed)
            : base(gameState, position, texture)
        {
            this.gameState = gameState;
            this.speed = speed;
        }
    }
}
