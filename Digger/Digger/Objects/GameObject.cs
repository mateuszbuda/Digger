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
    /// Klasa bazowa dla wszelkich obiektów gry, które są rysowane na planszy.
    /// </summary>
    public abstract class GameObject
    {
        /// <summary>
        /// Kontekst gry
        /// </summary>
        protected GameState gameState;
        /// <summary>
        /// Współrzędna pozycji obiektu gry
        /// </summary>
        protected Vector2 position;
        /// <summary>
        /// Tekstura obiektu gry
        /// </summary>
        protected Texture2D texture;

        /// <summary>
        /// Maksymalna pozycja obiektu na osi X
        /// </summary>
        protected int MaxX;
        /// <summary>
        /// Minimalna pozycja obiektu na osi X
        /// </summary>
        protected int MinX;
        /// <summary>
        /// Maksymalna pozycja obiektu na osi Y
        /// </summary>
        protected int MaxY;
        /// <summary>
        /// Minimalna pozycja obiektu na osi Y
        /// </summary>
        protected int MinY;

        /// <summary>
        /// Metoda aktualizująca stan obiektu, implementowana przez obiekty według ich przeznaczenia
        /// </summary>
        /// <param name="totalGameTime">Czas gry</param>
        public abstract void update(TimeSpan totalGameTime);

        /// <summary>
        /// Metoda rysująca obiekty. Najprostrza wersja sprawdzająca czy tekstura obiektu nie jest nullem i rysująca ten obiekt w jego aktualnej pozycji
        /// </summary>
        /// <param name="spriteBatch">Kontekst rysowanego obiektu</param>
        public virtual void draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
                spriteBatch.Draw(texture, position, Color.White);
        }

        /// <summary>
        /// Metoda rysująca obiekt jeśli jego tekstura nie jest nullem. Dodatkowo pozwala narysować teksturę obiektu obróconą o podany kąt, wokół środka tekstury.
        /// </summary>
        /// <param name="spriteBatch">Kontekst rysowanego obiektu</param>
        /// <param name="rotation">Kąt w radianach o jaki ma być obrócona oryginalna tekstura obiektu przy rysowaniu</param>
        public virtual void draw(SpriteBatch spriteBatch, float rotation)
        {
            if (texture != null)
            {
                Vector2 origin = new Vector2(Field.SZ / 2, Field.SZ / 2);
                spriteBatch.Draw(texture, new Vector2(position.X + Field.SZ / 2, position.Y + Field.SZ / 2), null, Color.White, rotation, origin, Vector2.One, SpriteEffects.None, 0f);
            }
        }

        /// <summary>
        /// Metoda udostępniająca aktualną pozycją obiektu.
        /// </summary>
        /// <returns>Pozycja obiektu</returns>
        public virtual Vector2 getPosition()
        {
            return position;
        }

        /// <summary>
        /// Konstrutor obiektu gry
        /// </summary>
        /// <param name="gameState">Obiekt stanu gry</param>
        /// <param name="position">Początkowa pozycja obiektu</param>
        /// <param name="texture">Tekstura obiektu</param>
        public GameObject(GameState gameState, Vector2 position, Texture2D texture)
        {
            this.gameState = gameState;
            this.position = position;
            this.texture = texture;

            MaxX = DiggerGame.GRAPHICS_WIDTH - texture.Width;
            MinX = 0;
            MaxY = DiggerGame.GRAPHICS_HEIGHT - texture.Height;
            MinY = 0;
        }
    }
}
