using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Digger
{
    /// <summary>
    /// Singleton udostępniający aktualne ustaiwenia sterowania
    /// </summary>
    class Settings
    {
        /// <summary>
        /// Klawisz ruchu do góry
        /// </summary>
        public static Keys up = Keys.Up;
        /// <summary>
        /// Klawisz ruchu do dołu
        /// </summary>
        public static Keys down = Keys.Down;
        /// <summary>
        /// Klawisz ruchu w lewo
        /// </summary>
        public static Keys left = Keys.Left;
        /// <summary>
        /// Klawisz ruchu w prawo
        /// </summary>
        public static Keys right = Keys.Right;
        /// <summary>
        /// Klawisz zastawiania bomby
        /// </summary>
        public static Keys bomb = Keys.B;
        /// <summary>
        /// Klawisz strzału
        /// </summary>
        public static Keys fire = Keys.C;
        /// <summary>
        /// Klawisz peleryny niewidki
        /// </summary>
        public static Keys invclk = Keys.V;
        /// <summary>
        /// Klawisz pauzy
        /// </summary>
        public static Keys pause = Keys.P;
        /// <summary>
        /// Klawisz ustawień
        /// </summary>
        public static Keys options = Keys.O;
        /// <summary>
        /// Klawisz pomocy
        /// </summary>
        public static Keys help = Keys.H;
        /// <summary>
        /// Klawisz zapisu stanu gry
        /// </summary>
        public static Keys save = Keys.L;
    }
}
