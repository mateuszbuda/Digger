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
    /// Bomba to szególna broń, ponieważ zabicie przeciwnika za jej pomocą daje dwa razy więcej punktów niż inna metodą. Zasię bomby to trzy pola w każdym keirunku oraz jedno pole dookoła bomby. Bomba wybucha z pewnym opóźnieniem od jej zastawienia.
    /// </summary>
    public class Bomb : Weapon
    {
        /// <summary>
        /// Opóźnienie wybuchu bomby
        /// </summary>
        public const int COUNTDOWN = 2;
        /// <summary>
        /// Czas wybuchu bomby
        /// </summary>
        private int explosionTime;
        /// <summary>
        /// Zmienna informująca czy bomba jest widoczna na mapię
        /// </summary>
        public bool visible = false;

        /// <summary>
        /// Konstruktor obiektu bomby
        /// </summary>
        /// <param name="gameState">Obiekt stanu gry</param>
        /// <param name="texture">Tekstura bomby</param>
        public Bomb(GameState gameState, Texture2D texture)
            : base(gameState, Vector2.Zero, texture, Vector2.Zero)
        {
            this.explosionTime = 0;
        }

        /// <summary>
        /// Implementacja metody aktualizującej stan bomby
        /// </summary>
        /// <param name="totalGameTime">Czas gry</param>
        public override void update(TimeSpan totalGameTime)
        {
            if (visible)
                if (totalGameTime.TotalSeconds >= explosionTime)
                    explode();
        }

        /// <summary>
        /// Metoda zabierająca życia trafionym przez eksplocję przeciwnikom
        /// </summary>
        private void explode()
        {
            visible = false;

            List<Enemy> targets = new List<Enemy>();
            foreach (Enemy e in gameState.enemies)
                if (inRange(e))
                    targets.Add(e);

            foreach (Enemy e in targets)
                if (!(e is General))
                    if (e.damage(1) < 1)
                    {
                        gameState.guy.points += (2 * e.getBonusPoints());
                        gameState.enemies.Remove(e);
                    }
        }

        /// <summary>
        /// Metoda sprawdzająca czy przeciwnik znajdował się w zasięgu eksplocji bomby
        /// </summary>
        /// <param name="e">Spradzany przeciwnik</param>
        /// <returns>Informacja czy jest w zasięgu</returns>
        private bool inRange(Enemy e)
        {
            Vector2 eMidPoint = e.getPosition();
            eMidPoint.X += Field.SZ / 2;
            eMidPoint.Y += Field.SZ / 2;

            // X
            if (eMidPoint.Y > position.Y && eMidPoint.Y < position.Y + Field.SZ && eMidPoint.X > position.X - 3 * Field.SZ && eMidPoint.X < position.X + 4 * Field.SZ)
                return true;
            // Y
            if (eMidPoint.X > position.X && eMidPoint.X < position.X + Field.SZ && eMidPoint.Y > position.Y - 3 * Field.SZ && eMidPoint.Y < position.Y + 4 * Field.SZ)
                return true;

            // inner square
            if (eMidPoint.X > position.X - Field.SZ && eMidPoint.X < position.X + 2 * Field.SZ && eMidPoint.Y > position.Y - Field.SZ && eMidPoint.Y < position.Y + 2 * Field.SZ)
                return true;

            return false;
        }

        /// <summary>
        /// Metoda zastawiająca bombę w podanej pozycji i ustawiająca czas wybuchu zastawianej bomby
        /// </summary>
        /// <param name="position">Pozycja zastawienia bomby</param>
        /// <param name="explosionTime">Czas wybuchu bomby</param>
        public void set(Vector2 position, int explosionTime)
        {
            visible = true;
            this.position = position;
            this.explosionTime = explosionTime;
        }

        /// <summary>
        /// Metoda rysująca obiekt widocznej bomby na mapie
        /// </summary>
        /// <param name="spriteBatch">Kontekst rysowanego obiektu</param>
        public override void draw(SpriteBatch spriteBatch)
        {
            if (visible)
                base.draw(spriteBatch);
        }
    }
}
