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
    /// <summary>
    /// Klasa aktualnego stanu gry zawierająca informacje o wszystkich mapie, wszystkich postaciach i artefaktach.
    /// </summary>
    public class GameState
    {
        /// <summary>
        /// Aktualna mapa
        /// </summary>
        public Map map;
        /// <summary>
        /// Aktualny poziom
        /// </summary>
        public int level = 0;
        /// <summary>
        /// Liczba diamentów na mapie
        /// </summary>
        public int diamonds;
        /// <summary>
        /// Główny bohater
        /// </summary>
        public Guy guy;
        /// <summary>
        /// Lista artefaktów na mapie
        /// </summary>
        public List<Artefact> artefacts = new List<Artefact>();
        /// <summary>
        /// Tymczasowa lista artefaktów dodana przez inne artefakty lub obiekty w grze
        /// </summary>
        private List<Artefact> tmpArtefacts = new List<Artefact>();
        /// <summary>
        /// Lista przeciwników na mapie
        /// </summary>
        public List<Enemy> enemies = new List<Enemy>();
        /// <summary>
        /// Aktualny dla poziomu limit przeciwników
        /// </summary>
        private int enemiesLimit;
        /// <summary>
        /// Obiekt wpływający na losowość zdarzeń w grze
        /// </summary>
        private Random rand = new Random();
        /// <summary>
        /// Czas gry w sekundach po którym pojawi się kolejna bomba do zebrania
        /// </summary>
        private int nextBombTime; // in seconds
        /// <summary>
        /// Czas gry w sekundach po którym pojawi się kolejny pocisk do zebrania
        /// </summary>
        private int nextMissileTime;
        /// <summary>
        /// Czas gry w sekundach po którym pojawi się peleryna do zebrania
        /// </summary>
        private int invicloakTime;
        /// <summary>
        /// Czas gry w sekundach po którym pojawi się czas bonusowy do zebrania
        /// </summary>
        private int bonusTime;
        /// <summary>
        /// Czas gry w sekundach po którym pojawi się kolejny worek ze złotem
        /// </summary>
        private int nextGoldbagTime;

        /// <summary>
        /// Domyślny konstruktor tworzący obiekt głównego bohatera
        /// </summary>
        public GameState()
        {
            //hero
            guy = new Guy(this, Vector2.Zero, Textures.getGuyTex(), Vector2.Zero, 3);

        }

        /// <summary>
        /// Metoda aktualizująca stan gry
        /// </summary>
        /// <param name="totalGameTime">Czas gry</param>
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

            updateArtefacts(totalGameTime);
            updateEnemies(totalGameTime);

            if (diamonds == 0)
            {
                artefacts.Clear();
                enemies.Clear();
                tmpArtefacts.Clear();
                Map.newMap();
                newLevel(totalGameTime);
                guy.nextLevel();
                guy.points += 300 * level;
                if (((double)(10 * (1 + level / 11)) / (double)(enemiesLimit + enemies.Count)) <= 0.1d)
                    guy.addHp(1);
                ++level;
            }
        }

        /// <summary>
        /// Metoda dodająca przeciwników, jeśli nie został przekroczny limit dla poziomu
        /// </summary>
        /// <param name="totalGameTime">Czas gry</param>
        private void updateEnemies(TimeSpan totalGameTime)
        {
            if (enemies.Count < 3 * (1 + level / 11) && enemiesLimit > 0)
            {
                if (level < 6)
                {
                    if (totalGameTime.TotalSeconds % 2 == 0)
                        enemies.Add(new Sergeant(this, getEnemyPosition(), Textures.getSergeantTex(), new Vector2(2, 0), 1, 20));
                    else
                        enemies.Add(new Captain(this, getEnemyPosition(), Textures.getCaptainTex(), new Vector2(2, 0), 1, 40, 5));
                }
                else if (level >= 6 && level < 11)
                {
                    enemies.Add(new Captain(this, getEnemyPosition(), Textures.getCaptainTex(), new Vector2(2, 0), 1, 40, 5));
                }
                else if (level >= 11 && level < 16)
                {
                    if (totalGameTime.TotalSeconds % 2 == 0)
                        enemies.Add(new Major(this, getEnemyPosition(), Textures.getMajorTex(), new Vector2(2, 0), 1, 60, 5));
                    else
                        enemies.Add(new Colonel(this, getEnemyPosition(), Textures.getColonelTex(), new Vector2(2, 0), 1, 80, 2, true));
                }
                else if (level >= 16)
                {
                    if (totalGameTime.TotalSeconds % 2 == 0)
                        enemies.Add(new Colonel(this, getEnemyPosition(), Textures.getColonelTex(), new Vector2(2, 0), 1, 80, 2, true));
                    else
                        enemies.Add(new General(this, getEnemyPosition(), Textures.getGeneralTex(), new Vector2(2.5f, 0), 1, 100, 2, true));
                }

                enemiesLimit--;
            }
        }

        /// <summary>
        /// Metoda dodająza artefakty
        /// </summary>
        /// <param name="totalGameTime">Czas gry</param>
        private void updateArtefacts(TimeSpan totalGameTime)
        {
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
                artefacts.Add(new BonusTime(this, getArtefactPosition(true, true), Textures.getBonusTimeTex(), 0, false));
                bonusTime = 0;
            }
        }

        /// <summary>
        /// Matoda odpowiedzialna za przejście do następnego poziomu
        /// </summary>
        /// <param name="gameTime">Czas gry</param>
        private void newLevel(TimeSpan gameTime)
        {
            enemiesLimit = (10 * (1 + level / 11)) - (3 * (1 + level / 11));

            nextBombTime = (int)gameTime.TotalSeconds + rand.Next(4, 10);
            nextMissileTime = (int)gameTime.TotalSeconds + rand.Next(4, 10);
            invicloakTime = (int)gameTime.TotalSeconds + rand.Next(6, 15);
            bonusTime = (int)gameTime.TotalSeconds + rand.Next(6, 15);
            nextGoldbagTime = (int)gameTime.TotalSeconds + rand.Next(6, 12);

            // Diamonds distribution
            int d = diamonds = 10 + level / 5;
            while (d > 0)
            {
                artefacts.Add(new Diamond(this, getArtefactPosition(true, false), Textures.getDiamondTex(), 10, false));
                d--;
            }

            // Sergeants
            int ser = level < 6 ? 2 : 1;
            while (ser > 0)
            {
                enemies.Add(new Sergeant(this, getEnemyPosition(), Textures.getSergeantTex(), new Vector2(2, 0), 1, 20));
                ser--;
            }

            // Captains
            int cap = level < 6 ? 1 : 2;
            while (cap > 0)
            {
                enemies.Add(new Captain(this, getEnemyPosition(), Textures.getCaptainTex(), new Vector2(2, 0), 1, 40, 5));
                cap--;
            }

            // Majors
            if (level > 10)
            {
                int maj = level < 16 ? 2 : 1;
                while (maj > 0)
                {
                    enemies.Add(new Major(this, getEnemyPosition(), Textures.getMajorTex(), new Vector2(2, 0), 1, 60, 5));
                    maj--;
                }
            }

            // Colonel
            if (level > 10)
                enemies.Add(new Colonel(this, getEnemyPosition(), Textures.getColonelTex(), new Vector2(2, 0), 1, 80, 2, true));

            // General
            if (level > 15)
                enemies.Add(new General(this, getEnemyPosition(), Textures.getGeneralTex(), new Vector2(2.5f, 0), 1, 100, 2, true));
        }

        /// <summary>
        /// Metoda zapisujące grę
        /// </summary>
        /// <param name="filename">Nazwa pliku do zapisu</param>
        public void saveGame(string filename)
        {
            return;
        }
        /// <summary>
        /// Ładuje sapisany stan gry
        /// </summary>
        /// <param name="filename">Nazwa pliku do odczytu stanu gry</param>
        public void loadGame(string filename)
        {
            return;
        }

        /// <summary>
        /// Dodaje artefakt na mapie
        /// </summary>
        /// <param name="artefact">Artefakt do dodania</param>
        public void addArtefact(Artefact artefact)
        {
            tmpArtefacts.Add(artefact);
        }

        /// <summary>
        /// Metoda zwracająca poprawną pozycję dla nowego artefaktu
        /// </summary>
        /// <param name="digged">Informacja czy artefakt może się znajdować na odkopanym polu</param>
        /// <param name="undigged">Informacja czy artefakt może się znajdować na zakopanym polu</param>
        /// <returns>Pozycja dla nowego artefaktu</returns>
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

        /// <summary>
        /// Metoda podająca prawidłową pozycję nowego przeciwnika na mapie
        /// </summary>
        /// <returns>Pozycja dla nowego przeciwnika</returns>
        private Vector2 getEnemyPosition()
        {
            int x, y;
            bool basic, duplicates, guy;
            while (true)
            {
                x = rand.Next(Map.WIDTH);
                y = rand.Next(Map.HEIGHT);

                basic = x >= 0 && y >= 0 && x < Map.WIDTH && y < Map.HEIGHT && Map.getInstance()[x, y].digged && y % 2 == 1;
                duplicates = true;
                foreach (Enemy e in enemies)
                    if (e.getPosition().X == x * Field.SZ && e.getPosition().Y == y * Field.SZ)
                    {
                        duplicates = false;
                        break;
                    }

                guy = Math.Abs(x - this.guy.getPosition().X / Field.SZ) > 3 && Math.Abs(y - this.guy.getPosition().Y / Field.SZ) > 3;

                if (basic && duplicates && guy)
                    break;
            }
            return new Vector2(x * Field.SZ, y * Field.SZ);
        }
    }
}
