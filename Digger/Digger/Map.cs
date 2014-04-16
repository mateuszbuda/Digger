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
    public class Map
    {
        public const int WIDTH = 20;
        public const int HEIGHT = 12;
        private static Field[,] fields;


        public static Field[,] getInstance()
        {
            if (fields == null)
                newMap();

            return fields;
        }

        public static Field[,] newMap()
        {
            fields = new Field[Map.WIDTH, Map.HEIGHT];
            // raw maze
            for (int i = 0; i < Map.WIDTH; i++)
                for (int j = 0; j < Map.HEIGHT; j++)
                    fields[i, j] = new Field(new Vector2(i * Field.SZ, j * Field.SZ), Textures.getFieldTex());

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
