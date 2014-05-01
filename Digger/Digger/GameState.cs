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
        private Random rand = new Random();
        private int nextBombTime; // in seconds
        private int nextMissileTime;

        public GameState()
        {
            // TODO: extract constants
            nextBombTime = rand.Next(12, 15);
            nextMissileTime = rand.Next(8, 12);

            //hero
            guy = new Guy(this, Vector2.Zero, Textures.getGuyTex(), Vector2.Zero, 3, "test");

            // Diamonds distribution
            int d = 0;
            while (d < 12)
            {
                artefacts.Add(new Diamond(getDiamondPosition(), Textures.getDiamondTex(), 10, false));
                d++;
            }

            // Sergeants
            int s = 0;
            while (s < 10)
            {
                enemies.Add(new Sergeant(getSergeantPosition(), Textures.getSergeantTex(), new Vector2(2, 0), 1, 20));
                s++;
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

            if ((int)gameTime.TotalGameTime.TotalSeconds == nextBombTime)
            {
                artefacts.Add(new Bomb(getBombPosition(), Textures.getBombTex(), 0, false));
                nextBombTime = (int)gameTime.TotalGameTime.TotalSeconds + rand.Next(12, 15);
            }
            if ((int)gameTime.TotalGameTime.TotalSeconds == nextMissileTime)
            {
                artefacts.Add(new Missile(getMissilePosition(), Textures.getMissileTex(), 0, false));
                nextMissileTime = (int)gameTime.TotalGameTime.TotalSeconds + rand.Next(8, 12);
            }
        }

        public void saveGame(string filename)
        {
            return;
        }
        public void loadGame(string filename)
        {
            return;
        }

        private Vector2 getBombPosition()
        {
            int x, y;
            bool basic, duplicates;
            while (true)
            {
                x = rand.Next(Map.WIDTH);
                y = rand.Next(Map.HEIGHT);

                basic = x >= 0 && y >= 0 && x < Map.WIDTH && y < Map.HEIGHT;
                duplicates = true;
                foreach (Artefact a in artefacts)
                    if (a.getPosition().X == x * Field.SZ && a.getPosition().Y == y * Field.SZ)
                        duplicates = false;
                if (guy.getPosition().X == x * Field.SZ && guy.getPosition().Y == y * Field.SZ)
                    duplicates = false;

                if (basic && duplicates)
                    break;
            }
            return new Vector2(x * Field.SZ, y * Field.SZ);
        }

        private Vector2 getMissilePosition()
        {
            int x, y;
            bool basic, duplicates;
            while (true)
            {
                x = rand.Next(Map.WIDTH);
                y = rand.Next(Map.HEIGHT);

                basic = x >= 0 && y >= 0 && x < Map.WIDTH && y < Map.HEIGHT;
                duplicates = true;
                foreach (Artefact a in artefacts)
                    if (a.getPosition().X == x * Field.SZ && a.getPosition().Y == y * Field.SZ)
                        duplicates = false;
                if (guy.getPosition().X == x * Field.SZ && guy.getPosition().Y == y * Field.SZ)
                    duplicates = false;

                if (basic && duplicates)
                    break;
            }
            return new Vector2(x * Field.SZ, y * Field.SZ);
        }

        private Vector2 getDiamondPosition()
        {
            int x, y;
            bool basic, duplicates;
            while (true)
            {
                x = rand.Next(Map.WIDTH);
                y = rand.Next(Map.HEIGHT);

                basic = x >= 0 && y >= 0 && x < Map.WIDTH && y < Map.HEIGHT && !Map.getInstance()[x, y].digged;
                duplicates = true;
                foreach (Artefact a in artefacts)
                    if (a.getPosition().X == x * Field.SZ && a.getPosition().Y == y * Field.SZ)
                        duplicates = false;
                if (guy.getPosition().X == x * Field.SZ && guy.getPosition().Y == y * Field.SZ)
                    duplicates = false;

                if (basic && duplicates)
                    break;
            }
            return new Vector2(x * Field.SZ, y * Field.SZ);
        }

        private Vector2 getSergeantPosition()
        {
            int x, y;
            bool basic, duplicates;
            while (true)
            {
                x = rand.Next(Map.WIDTH);
                y = rand.Next(Map.HEIGHT);

                basic = x >= 0 && y >= 0 && x < Map.WIDTH && y < Map.HEIGHT && Map.getInstance()[x, y].digged && y % 2 == 1;
                duplicates = true;
                foreach (Enemy e in enemies)
                    if (e.getPosition().X == x * Field.SZ && e.getPosition().Y == y * Field.SZ)
                        duplicates = false;

                if (basic && duplicates)
                    break;
            }
            return new Vector2(x * Field.SZ, y * Field.SZ);
        }
    }
}
