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
        public int level = 1;
        public int diamonds;
        public Guy guy;
        public List<Artefact> artefacts = new List<Artefact>();
        private List<Artefact> tmpArtefacts = new List<Artefact>();
        public List<Enemy> enemies = new List<Enemy>();
        private Random rand = new Random();
        private int nextBombTime; // in seconds
        private int nextMissileTime;
        private int invicloakTime;
        private int bonusTime;
        private int nextGoldbagTime;

        public GameState()
        {
            //hero
            guy = new Guy(this, Vector2.Zero, Textures.getGuyTex(), Vector2.Zero, 300);
        }

        public void update(TimeSpan totalGameTime)
        {
            guy.update(totalGameTime);

            foreach (Enemy e in enemies)
                e.update(totalGameTime);

            if (tmpArtefacts.Count > 0)
            {
                artefacts.AddRange(tmpArtefacts);
                tmpArtefacts.Clear();
            }
            foreach (Artefact a in artefacts)
                a.update(totalGameTime);

            if ((int)totalGameTime.TotalSeconds == nextBombTime)
            {
                artefacts.Add(new Bomb(this, getArtefactPosition(true, true), Textures.getBombArtefactTex(), 0, false));
                nextBombTime = (int)totalGameTime.TotalSeconds + rand.Next(2, 10);
            }
            if ((int)totalGameTime.TotalSeconds == nextMissileTime)
            {
                artefacts.Add(new Missile(this, getArtefactPosition(true, true), Textures.getMissileTex(), 0, false));
                nextMissileTime = (int)totalGameTime.TotalSeconds + rand.Next(2, 10);
            }
            if ((int)totalGameTime.TotalSeconds == nextGoldbagTime)
            {
                artefacts.Add(new GoldBag(this, getArtefactPosition(true, false), Textures.getGoldbagTex(), 0, true, Vector2.Zero));
                nextGoldbagTime = (int)totalGameTime.TotalSeconds + rand.Next(2, 10);
            }
            if ((int)totalGameTime.TotalSeconds == invicloakTime)
            {
                artefacts.Add(new Invicloak(this, getArtefactPosition(true, true), Textures.getInvicloakTex(), 0, false, invicloakTime
+ 10));
                invicloakTime = 0;
            }
            if ((int)totalGameTime.TotalSeconds == bonusTime)
            {
                artefacts.Add(new BonusTime(this, getArtefactPosition(true, true), Textures.getBonusTimeTex(), 0, false, 0));
                bonusTime = 0;
            }

            if (diamonds == 0)
            {
                artefacts.Clear();
                enemies.Clear();
                tmpArtefacts.Clear();
                Map.newMap();
                newLevel(totalGameTime);
                guy.nextLevel();
                ++level;
            }
        }

        private void newLevel(TimeSpan gameTime)
        {
            // TODO: extract constants
            nextBombTime = (int)gameTime.TotalSeconds + rand.Next(2, 10);
            nextMissileTime = (int)gameTime.TotalSeconds + rand.Next(2, 10);
            invicloakTime = (int)gameTime.TotalSeconds + rand.Next(2, 10);
            bonusTime = (int)gameTime.TotalSeconds + rand.Next(2, 10);
            nextGoldbagTime = (int)gameTime.TotalSeconds + rand.Next(2, 10);

            // Diamonds distribution
            int d = 0;
            while (d < 5)
            {
                artefacts.Add(new Diamond(this, getArtefactPosition(true, false), Textures.getDiamondTex(), 10, false));
                d++;
            }
            diamonds = d;

            // Sergeants
            int s = 0;
            while (s < 5)
            {
                enemies.Add(new Sergeant(this, getEnemyPosition(), Textures.getSergeantTex(), new Vector2(2, 0), 1, 20));
                s++;
            }

            // Captains
            int c = 0;
            while (c < 2)
            {
                enemies.Add(new Captain(this, getEnemyPosition(), Textures.getCaptainTex(), new Vector2(2, 0), 1, 40, 5));
                c++;
            }

            // Majors
            int m = 0;
            while (m < 2)
            {
                enemies.Add(new Major(this, getEnemyPosition(), Textures.getMajorTex(), new Vector2(2, 0), 1, 60, 5));
                m++;
            }

            // Colonel
            enemies.Add(new Colonel(this, getEnemyPosition(), Textures.getColonelTex(), new Vector2(2, 0), 1, 80, 2, true));

            // General
            enemies.Add(new General(this, getEnemyPosition(), Textures.getGeneralTex(), new Vector2(2.5f, 0), 1, 100, 2, true));
        }

        public void saveGame(string filename)
        {
            return;
        }
        public void loadGame(string filename)
        {
            return;
        }

        public void addArtefact(Artefact artefact)
        {
            tmpArtefacts.Add(artefact);
        }

        private Vector2 getArtefactPosition(bool digged, bool undigged)
        {
            int x, y;
            bool basic, duplicates;
            while (true)
            {
                x = rand.Next(Map.WIDTH);
                y = rand.Next(Map.HEIGHT);

                if (digged && undigged)
                    basic = x >= 0 && y >= 0 && x < Map.WIDTH && y < Map.HEIGHT;
                else if (digged)
                    basic = x >= 0 && y >= 0 && x < Map.WIDTH && y < Map.HEIGHT && Map.getInstance()[x, y].digged;
                else
                    basic = x >= 0 && y >= 0 && x < Map.WIDTH && y < Map.HEIGHT && !Map.getInstance()[x, y].digged;

                if (!basic)
                    continue;

                duplicates = true;
                foreach (Artefact a in artefacts)
                    if (a.getPosition().X == x * Field.SZ && a.getPosition().Y == y * Field.SZ)
                    {
                        duplicates = false;
                        break;
                    }
                if (guy.getPosition().X == x * Field.SZ && guy.getPosition().Y == y * Field.SZ)
                    duplicates = false;

                if (basic && duplicates)
                    break;
            }
            return new Vector2(x * Field.SZ, y * Field.SZ);
        }

        private Vector2 getEnemyPosition()
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
