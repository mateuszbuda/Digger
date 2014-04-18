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
using Digger.Objects;
using Digger.Objects.Artefacts;

namespace Digger
{
    public class GameState
    {
        public Map map;
        public static Guy guy;
        public List<Artefact> artefacts = new List<Artefact>();
        public List<Enemy> enemies = new List<Enemy>();
        private bool running = true;

        public GameState()
        {
            Random r = new Random();
            // TODO: extract constants

            //hero
            guy = new Guy(Vector2.Zero, Textures.getGuyTex(), Vector2.Zero, 3, "test");

            // Diamonds distribution
            int x, y, d = 0;
            while (d < 12)
            {
                x = r.Next(Map.WIDTH);
                y = r.Next(d, d + 1);
                if (areCorrectDiamondCoords(x, y))
                {
                    artefacts.Add(new Diamond(new Vector2(x * Field.SZ, y * Field.SZ), Textures.getDiamondTex(), 10, false));
                    d++;
                }
            }

            // Sergeants
            int s = 0;
            while (s < 10)
            {
                x = r.Next(Map.WIDTH);
                y = (r.Next(Map.HEIGHT / 2) * 2 + 1) % Map.HEIGHT;
                if (areCorrectSergeantCoords(x, y))
                {
                    enemies.Add(new Sergeant(new Vector2(x * Field.SZ, y * Field.SZ), Textures.getSergeantTex(), new Vector2(2, 0), 1, 20));
                    s++;
                }
            }
        }

        public void pause()
        {
            running = false;
        }
        public void resume()
        {
            running = true;
        }
        public void update(GameTime gameTime)
        {
            if (!running)
                return;

            guy.update(gameTime);
            foreach (Enemy e in enemies)
                e.update(gameTime);
            foreach (Artefact a in artefacts)
                a.update(gameTime);

        }
        public void saveGame(string filename)
        {
            return;
        }
        public void loadGame(string filename)
        {
            return;
        }

        private bool areCorrectDiamondCoords(int x, int y)
        {
            bool basic = x > 0 && y >= 0 && x < Map.WIDTH && y < Map.HEIGHT && !Map.getInstance()[x, y].digged;
            bool duplicates = true;
            foreach (Artefact a in artefacts)
                if (a.getPosition().X == x * Field.SZ && a.getPosition().Y == y * Field.SZ)
                    duplicates = false;

            return basic && duplicates;
        }

        private bool areCorrectSergeantCoords(int x, int y)
        {
            bool basic = x > 0 && y > 0 && x < Map.WIDTH && y < Map.HEIGHT && Map.getInstance()[x, y].digged;
            bool duplicates = true;
            foreach (Enemy e in enemies)
                if (e.getPosition().X == x * Field.SZ && e.getPosition().Y == y * Field.SZ)
                    duplicates = false;

            return basic && duplicates;
        }
    }
}
