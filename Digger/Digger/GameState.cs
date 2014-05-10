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
        public Guy guy;
        public List<Artefact> artefacts = new List<Artefact>();
        public List<Enemy> enemies = new List<Enemy>();
        private Random rand = new Random();
        private int nextBombTime; // in seconds
        private int nextMissileTime;
        private int invicloakTime;
        private int bonusTime;

        public GameState()
        {
            // TODO: extract constants
            nextBombTime = rand.Next(12, 15);
            nextMissileTime = rand.Next(8, 12);
            invicloakTime = rand.Next(2, 10);
            bonusTime = rand.Next(2, 10);

            //hero
            guy = new Guy(this, Vector2.Zero, Textures.getGuyTex(), Vector2.Zero, 300, "test");

            // Diamonds distribution
            int d = 0;
            while (d < 12)
            {
                artefacts.Add(new Diamond(this, getDiamondPosition(), Textures.getDiamondTex(), 10, false));
                d++;
            }

            // Sergeants
            int s = 0;
            while (s < 2)
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

        public void update(TimeSpan totalGameTime)
        {
            guy.update(totalGameTime);
            foreach (Enemy e in enemies)
                e.update(totalGameTime);
            foreach (Artefact a in artefacts)
                a.update(totalGameTime);

            if ((int)totalGameTime.TotalSeconds == nextBombTime)
            {
                artefacts.Add(new Bomb(this, getArtefactPosition(), Textures.getBombArtefactTex(), 0, false));
                nextBombTime = (int)totalGameTime.TotalSeconds + rand.Next(12, 15);
            }
            if ((int)totalGameTime.TotalSeconds == nextMissileTime)
            {
                artefacts.Add(new Missile(this, getArtefactPosition(), Textures.getMissileTex(), 0, false));
                nextMissileTime = (int)totalGameTime.TotalSeconds + rand.Next(8, 12);
            }
            if ((int)totalGameTime.TotalSeconds == invicloakTime)
            {
                artefacts.Add(new Invicloak(this, getArtefactPosition(), Textures.getInvicloakTex(), 0, false, invicloakTime
+ 10));
                invicloakTime = 0;
            }
            if ((int)totalGameTime.TotalSeconds == bonusTime)
            {
                artefacts.Add(new BonusTime(this, getArtefactPosition(), Textures.getBonusTimeTex(), 0, false, 0));
                bonusTime = 0;
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

        // TODO: refactor get*Position()
        private Vector2 getArtefactPosition()
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
