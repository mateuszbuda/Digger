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

namespace Digger.Objects
{
    /// <summary>
    /// Klasa bazowa wszystkich postaci wystaępujących w grze.
    /// </summary>
    public abstract class Character : GameObject
    {
        /// <summary>
        /// Enumerator kierunku
        /// </summary>
        public enum Direction
        {
            /// <summary>
            /// Kierunek na północ (do góry)
            /// </summary>
            N,
            /// <summary>
            /// Kierunek na południe (do dołu)
            /// </summary>
            S,
            /// <summary>
            /// Kierunek na wschód (w prawo)
            /// </summary>
            E,
            /// <summary>
            /// Kierunek na zachód (w lewo)
            /// </summary>
            W
        };

        /// <summary>
        /// Ilość życia postaci
        /// </summary>
        protected int hp;
        /// <summary>
        /// Prędkość postaci
        /// </summary>
        protected Vector2 speed;
        /// <summary>
        /// Kierunek ruchu postaci
        /// </summary>
        protected Direction direction;
        /// <summary>
        /// Amienna informująca czy obiekt się porusza
        /// </summary>
        protected bool moving = false;
        /// <summary>
        /// Pozycja ostatniego pola na którym znajdowała się postać
        /// </summary>
        protected Vector2 historyPosition;

        /// <summary>
        /// Konstruktor postaci
        /// </summary>
        /// <param name="gameState">Obiekt stanu gry</param>
        /// <param name="position">Początkowa pozycja obiektu</param>
        /// <param name="texture">Tekstura obiektu</param>
        /// <param name="speed">Początkowa prędkość postaci</param>
        /// <param name="hp">Początkowa ilość życia postaci</param>
        public Character(GameState gameState, Vector2 position, Texture2D texture, Vector2 speed, int hp)
            : base(gameState, position, texture)
        {
            this.speed = speed;
            this.hp = hp;
            this.historyPosition = position;
            this.direction = Direction.E;
        }

        /// <summary>
        /// Zadaje postaci obrażenia, odbierając jej tyle życia ile zostało zadanych obrażeń
        /// </summary>
        /// <param name="damage">Ilość obraż, które będą zadane postaci</param>
        /// <returns>Stan życia postaci, po zadaniu obrażeń</returns>
        public int damage(int damage)
        {
            hp -= damage;
            if (hp <= 0)
                texture = null;
            return hp;
        }

        /// <summary>
        /// MEtoda udostępniająca aktualną prędkość postaci
        /// </summary>
        /// <returns>Prędkość postaci</returns>
        public Vector2 getSpeed()
        {
            return speed;
        }
    }
}
