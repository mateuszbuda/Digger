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

namespace Digger.Objects
{
    /// <summary>
    /// Klasa bazowa wszystkich przeciwników w grze.
    /// </summary>
    public abstract class Enemy : Character
    {
        /// <summary>
        /// Ilość punktów jakie dostaje gracz za zabicie danego przeciwnika
        /// </summary>
        protected int bonusPoints;
        /// <summary>
        /// Okres aktualizacji kierunku ruchu w kierunku bohatera w sekundach
        /// </summary>
        protected int directionUpdateFreq;
        /// <summary>
        /// Zmienna informująca czy dany przeciwnik może odkopywać pola
        /// </summary>
        protected bool digger;
        /// <summary>
        /// Obiekt nadający czynnik losowy w ruchu postaci. Stosowany w szególnej sytuacji, kiedy bohater uzył peleryny niewidki.
        /// </summary>
        private Random rand;

        /// <summary>
        /// Konstruktor przeciwnika
        /// </summary>
        /// <param name="gameState">Obiekt stanu gry</param>
        /// <param name="position">Początkowa pozycja obiektu</param>
        /// <param name="texture">Tekstura obiektu</param>
        /// <param name="speed">Początkowa prędkość obiektu</param>
        /// <param name="hp">Początkowa ilość żyć przeciwnika</param>
        /// <param name="bonusPoints">Ilość punktów jakie dostaje gracz za zabicie danego przeciwnika</param>
        /// <param name="directionUpdateFreq">Okres aktualizacji kierunku ruchu w kierunku bohatera w sekundach</param>
        /// <param name="digger">Informacja czy dany przeciwnik może odkopywać pola</param>
        public Enemy(GameState gameState, Vector2 position, Texture2D texture, Vector2 speed, int hp, int bonusPoints, int directionUpdateFreq = 0, bool digger = false)
            : base(gameState, position, texture, speed, hp)
        {
            this.bonusPoints = bonusPoints;
            this.directionUpdateFreq = directionUpdateFreq;
            this.digger = digger;
            this.rand = new Random();
        }

        /// <summary>
        /// Metoda udostępniająca ilość punktów za zabicie danego przeciwnika
        /// </summary>
        /// <returns>Ilość punktów za zabicie postaci w normalnym trybie</returns>
        public int getBonusPoints()
        {
            return bonusPoints;
        }

        /// <summary>
        /// Wyznacza kierunek w którym jest bohater
        /// </summary>
        /// <returns>Kierunek ruchu</returns>
        protected Direction getDirectionToGuy()
        {
            Vector2 guyPosition = gameState.guy.getPosition();
            if (gameState.guy.bonusTime)
            {
                guyPosition.X = Map.WIDTH * Field.SZ - guyPosition.X;
                guyPosition.Y = Map.HEIGHT * Field.SZ - guyPosition.Y;
            }
            else if (gameState.guy.invicloak)
            {
                guyPosition.X = rand.Next();
                guyPosition.Y = rand.Next();
            }

            if ((Math.Abs(position.X - guyPosition.X) > Math.Abs(position.Y - guyPosition.Y)))
            {
                if (guyPosition.X > position.X)
                    direction = Direction.E;
                else
                    direction = Direction.W;
            }
            else
            {
                if (guyPosition.Y > position.Y)
                    direction = Direction.S;
                else
                    direction = Direction.N;
            }

            return getDirection();
        }

        /// <summary>
        /// Metoda wyznaczająca ogólny keirunek ruchu przeciwników. Dla niektórych z nich uwzględnia ruchy pocisków i ucieka od nich.
        /// Dla pozostałych przeciwników stara się utrzymać zwrot ruchu, następnie kierunek.
        /// </summary>
        /// <returns>Wyznaczony kierunek ruchu dla postaci</returns>
        protected Direction getDirection()
        {
            // Majors and Generals avoid fires
            if (this is Major || this is General)
                foreach (Weapons.Fire f in gameState.guy.fires)
                    if (f.visible)
                    {
                        if (f.getPosition().Y == position.Y)
                        {
                            if ((int)(position.Y / Field.SZ) - 1 >= 0 &&
                        (Map.getInstance()[(int)(position.X / Field.SZ), (int)(position.Y / Field.SZ) - 1].digged || digger))
                                return Direction.N;
                            if ((int)(position.Y / Field.SZ) + 1 < Map.HEIGHT &&
                        (Map.getInstance()[(int)(position.X / Field.SZ), (int)(position.Y / Field.SZ) + 1].digged || digger))
                                return Direction.S;
                        }
                        if (f.getPosition().X == position.X)
                        {
                            if ((int)(position.X / Field.SZ) + 1 < Map.WIDTH &&
                        (Map.getInstance()[(int)(position.X / Field.SZ) + 1, (int)(position.Y / Field.SZ)].digged || digger))
                                return Direction.E;
                            if ((int)(position.X / Field.SZ) - 1 >= 0 &&
                        (Map.getInstance()[(int)(position.X / Field.SZ) - 1, (int)(position.Y / Field.SZ)].digged || digger))
                                return Direction.W;
                        }
                    }

            // if can continue moving in the same direction
            if (direction == Direction.E)
            {
                if ((int)(position.X / Field.SZ) + 1 < Map.WIDTH &&
                    (Map.getInstance()[(int)(position.X / Field.SZ) + 1, (int)(position.Y / Field.SZ)].digged || digger))
                    return direction;
            }
            else if (direction == Direction.W)
            {
                if ((int)(position.X / Field.SZ) - 1 >= 0 &&
                    (Map.getInstance()[(int)(position.X / Field.SZ) - 1, (int)(position.Y / Field.SZ)].digged || digger))
                    return direction;
            }
            else if (direction == Direction.N)
            {
                if ((int)(position.Y / Field.SZ) - 1 >= 0 &&
                    (Map.getInstance()[(int)(position.X / Field.SZ), (int)(position.Y / Field.SZ) - 1].digged || digger))
                    return direction;
            }
            else if (direction == Direction.S)
            {
                if ((int)(position.Y / Field.SZ) + 1 < Map.HEIGHT &&
                    (Map.getInstance()[(int)(position.X / Field.SZ), (int)(position.Y / Field.SZ) + 1].digged || digger))
                    return direction;
            }

            //if can move in opposite direction
            if (direction == Direction.E)
            {
                if ((int)(position.X / Field.SZ) - 1 >= 0 &&
                    (Map.getInstance()[(int)(position.X / Field.SZ) - 1, (int)(position.Y / Field.SZ)].digged || digger))
                    return Direction.W;
            }
            else if (direction == Direction.W)
            {
                if ((int)(position.X / Field.SZ) + 1 < Map.WIDTH &&
                    (Map.getInstance()[(int)(position.X / Field.SZ) + 1, (int)(position.Y / Field.SZ)].digged || digger))
                    return Direction.E;
            }
            else if (direction == Direction.N)
            {
                if ((int)(position.Y / Field.SZ) + 1 < Map.HEIGHT &&
                    (Map.getInstance()[(int)(position.X / Field.SZ), (int)(position.Y / Field.SZ) + 1].digged || digger))
                    return Direction.S;
            }
            else if (direction == Direction.S)
            {
                if ((int)(position.Y / Field.SZ) - 1 >= 0 &&
                    (Map.getInstance()[(int)(position.X / Field.SZ), (int)(position.Y / Field.SZ) - 1].digged || digger))
                    return Direction.N;
            }

            // must change move axis
            if (direction == Direction.E || direction == Direction.W)
            {
                if ((int)(position.Y / Field.SZ) - 1 >= 0 &&
                    (Map.getInstance()[(int)(position.X / Field.SZ), (int)(position.Y / Field.SZ) - 1].digged || digger))
                    return Direction.N;
                else
                    return Direction.S;
            }
            else if (direction == Direction.N || direction == Direction.S)
            {
                if ((int)(position.X / Field.SZ) + 1 < Map.WIDTH &&
                    (Map.getInstance()[(int)(position.X / Field.SZ) + 1, (int)(position.Y / Field.SZ)].digged || digger))
                    return Direction.E;
                else
                    return Direction.W;
            }

            return direction;
        }
    }
}
