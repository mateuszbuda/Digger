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
    /// Singleton udostepniający aktualną mapę
    /// </summary>
    public class Map
    {
        /// <summary>
        /// Szerokość mapy w polach
        /// </summary>
        public const int WIDTH = 20;
        /// <summary>
        /// wysokość mapy w polach
        /// </summary>
        public const int HEIGHT = 12;
        /// <summary>
        /// Tablica pól składających się na mapę
        /// </summary>
        private static Field[,] fields;

        /// <summary>
        /// Metoda dostępu do aktualnej instancji mapy
        /// </summary>
        /// <returns>Tablicę z polami</returns>
        public static Field[,] getInstance()
        {
            if (fields == null)
                newMap();

            return fields;
        }

        /// <summary>
        /// Generuje nową mapę
        /// </summary>
        /// <returns>Wygenerowaną mapę jako tablicę z polami</returns>
        public static Field[,] newMap()
        {
            fields = new Field[Map.WIDTH, Map.HEIGHT];
            // raw maze
            for (int i = 0; i < Map.WIDTH; i++)
                for (int j = 0; j < Map.HEIGHT; j++)
                    fields[i, j] = new Field(null, new Vector2(i * Field.SZ, j * Field.SZ), Textures.getFieldTex());

            // random maze digging
            fields[0, 0].dig();
            Random r = new Random();
            int t1, t2;
            for (int i = 0; i < Map.HEIGHT; i++)
            {
                if (i % 2 > 0)
                {
                    t2 = r.Next(12, 20);
                    for (t1 = r.Next(1, 6); t1 < t2; t1++)
                        fields[t1, i].dig();
                }
                else
                {
                    if (i > 0)
                    {
                        t1 = r.Next(0, 6);
                        if (fields[t1, i - 1].digged)
                            fields[t1, i].dig();

                        t1 = r.Next(6, 12);
                        if (fields[t1, i - 1].digged)
                            fields[t1, i].dig();

                        t1 = r.Next(12, 20);
                        if (fields[t1, i - 1].digged)
                            fields[t1, i].dig();
                    }
                }
            }
            t1 = r.Next(0, 6);
            if (fields[t1, 1].digged)
                fields[t1, 0].dig();

            t1 = r.Next(6, 12);
            if (fields[t1, 1].digged)
                fields[t1, 0].dig();

            t1 = r.Next(12, 20);
            if (fields[t1, 1].digged)
                fields[t1, 0].dig();

            return fields;
        }
    }
}
