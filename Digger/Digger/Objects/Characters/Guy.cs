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
using Digger.Objects;
using Digger.Objects.Artefacts;
using Digger.Objects.Weapons;

namespace Digger.Objects
{
    /// <summary>
    /// Klasa bohatera Guya. Zawira ona informacje o dostępnych broniach i stanach w jakich się znajduje (czas bonsuwy, peleryna niewidka). Przechowuje też informacje o ilości punktów, jakie dotychcasz zebrał.
    /// </summary>
    public class Guy : Character
    {
        /// <summary>
        /// Zmienna informująca czy bohater ma na sobie pelerynę
        /// </summary>
        public bool invicloak = false;
        /// <summary>
        /// Czas gry po jakim uzyta peleryna przestanie działać
        /// </summary>
        private int cloakCountdown = 0;
        /// <summary>
        /// Czas gry po jakim bohater, który użył peleryny niewidki pojawi się lub zniknie.
        /// </summary>
        private int nextBlink = 0;
        /// <summary>
        /// Zmienna informująca czy bohater jest w trybie czasu bonusowego
        /// </summary>
        public bool bonusTime = false;
        /// <summary>
        /// Ilość sekund pozostała do końca czasu bonusowego
        /// </summary>
        public int bonusTimeLeft = 0;
        /// <summary>
        /// Czas po którym czas bonusowy przestanie działać
        /// </summary>
        public int bonusCountdown = 0;
        /// <summary>
        /// Ostatnio użyty klawisz ruchu bohatera. Na jego podstawie wyznaczany jest kierunek wystrzału pocisku
        /// </summary>
        private Keys lastMoveDirection = Settings.right;
        /// <summary>
        /// Ilość bomb do dyspozycji
        /// </summary>
        public int bombCnt = 0;
        /// <summary>
        /// Czas gry kiedy ostatnio została zastawiona bomba. Zapobiega zastawianiu kilku bomb na raz
        /// </summary>
        private double lastBomb = 0.0;
        /// <summary>
        /// Lista bomb, które może zastawiać bohater
        /// </summary>
        public List<Weapons.Bomb> bombs = new List<Weapons.Bomb>();
        /// <summary>
        /// Ilość dostępnych peleryn
        /// </summary>
        public int invicloackCnt = 0;
        /// <summary>
        /// Czas gry, kiedy bohater ostatnio zetknął się z przeciwnikiem. Zapobiega utracie wszystkich żyć, przy jednym zetknięciu się z przeciwnikiem i daje czas na ucieczkę
        /// </summary>
        private double lastEnemyHit = 0.0;
        /// <summary>
        /// Czas gry kiedy bohater ostatnio wystrzelił pocisk. Zapobieka wystrzeleniu wielu pocisków na raz
        /// </summary>
        private double lastShoot = 0.0;
        /// <summary>
        /// Ilość dostępnych pocisków
        /// </summary>
        public int firesCnt = 0;
        /// <summary>
        /// Lista pocisków, które wystrzela bohater
        /// </summary>
        public List<Fire> fires = new List<Fire>();
        /// <summary>
        /// Ilość punktów, które dotychczas zebrał bohater
        /// </summary>
        public int points = 0;

        /// <summary>
        /// Metoda udostępniająca ilość zyć bohatera
        /// </summary>
        /// <returns>Ilość żyć pozostałych bohaterowi</returns>
        public int getHp()
        {
            return hp;
        }

        /// <summary>
        /// Metoda zwiększająca ilość żyć bohatera
        /// </summary>
        /// <param name="hp">ilość zyć do dodania</param>
        public void addHp(int hp)
        {
            this.hp += hp;
        }

        /// <summary>
        /// Konstruktor bohatera
        /// </summary>
        /// <param name="gameState">Obiekt stanu gry</param>
        /// <param name="position">Początkowa pozycja bohatera</param>
        /// <param name="texture">Tekstura bohatera</param>
        /// <param name="speed">Początkowa prędkość bohatera</param>
        /// <param name="hp">Początkowa ilość życia bohatera</param>
        public Guy(GameState gameState, Vector2 position, Texture2D texture, Vector2 speed, int hp)
            : base(gameState, position, texture, speed, hp)
        {
        }

        /// <summary>
        /// Implementacja aktualizacji stanu przez bohatera
        /// </summary>
        /// <param name="gameTime">Czas gry</param>
        public override void update(TimeSpan gameTime)
        {
            updateMove();
            updateFires(gameTime);
            updateBombs(gameTime);
            updateInvicloak(gameTime);
            enemyCollisions(gameTime);
            bonusTimeLeft = (bonusCountdown - (int)gameTime.TotalSeconds) < 0 ? 0 : bonusCountdown - (int)gameTime.TotalSeconds;
        }

        /// <summary>
        /// Metoda przenosząca bohatera do kolejnego poziomu
        /// </summary>
        public void nextLevel()
        {
            historyPosition = Vector2.Zero;
            position = Vector2.Zero;
            lastMoveDirection = Settings.right;
            speed = Vector2.Zero;
        }

        /// <summary>
        /// Aktualizacja stanu bohatera w momencie kiedy ma założoną pelerynę niewidkę. Wtedy bohater mruga i odliczany jest czas działania peleryny
        /// </summary>
        /// <param name="gameTime">Czas gry</param>
        private void updateInvicloak(TimeSpan gameTime)
        {
            if (invicloak)
            {
                if ((int)gameTime.TotalSeconds >= cloakCountdown)
                {
                    invicloak = false;
                    texture = Textures.getGuyTex();
                    return;
                }

                if ((int)gameTime.TotalMilliseconds >= nextBlink)
                {
                    if (texture == null)
                        texture = Textures.getGuyTex();
                    else
                        texture = null;
                    nextBlink += 300;
                }

                return;
            }

            if (Keyboard.GetState().IsKeyDown(Settings.invclk) && invicloackCnt > 0)
            {
                invicloackCnt--;
                invicloak = true;
                cloakCountdown = (int)gameTime.TotalSeconds + Invicloak.TIMEOUT;
                nextBlink = (int)gameTime.TotalMilliseconds + 200;
            }
        }

        /// <summary>
        /// Obsługa bomb - zarówno ich zastawienia jak i wybuchu w odpowiednim momencie.
        /// </summary>
        /// <param name="gameTime">Czas gry</param>
        private void updateBombs(TimeSpan gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Settings.bomb) && gameTime.TotalMilliseconds - lastBomb > 200)
                if (bombCnt > 0)
                {
                    bombCnt--;
                    lastBomb = gameTime.TotalMilliseconds;
                    bool set = false;
                    foreach (Weapons.Bomb b in bombs)
                        if (!b.visible)
                        {
                            b.set(getBombPosition(), (int)gameTime.TotalSeconds + Weapons.Bomb.COUNTDOWN);
                            set = true;
                            break;
                        }
                    if (!set)
                    {
                        Weapons.Bomb b = new Weapons.Bomb(gameState, Textures.getBombTex());
                        bombs.Add(b);
                        b.set(getBombPosition(), (int)gameTime.TotalSeconds + Weapons.Bomb.COUNTDOWN);
                    }
                }

            foreach (Weapons.Bomb b in bombs)
                b.update(gameTime);
        }

        /// <summary>
        /// Metoda podające pozycję, na której zastawiona ma być bomba. Pozycja ta jest zawsze pozycją któregoś pola, dzięki temu bomba zawsze jest zastawiona w całości na jednym polu
        /// </summary>
        /// <returns>Pozycja na której będzie zastawiona bomba</returns>
        private Vector2 getBombPosition()
        {
            Vector2 middle = new Vector2(position.X + Field.SZ / 2, position.Y + Field.SZ / 2);
            return new Vector2((int)(middle.X / Field.SZ) * Field.SZ, (int)(middle.Y / Field.SZ) * Field.SZ);
        }

        /// <summary>
        /// Metoda sprawdzająca kolizje z przeciwnikami na mapie
        /// </summary>
        /// <param name="gameTime">Czas gry</param>
        private void enemyCollisions(TimeSpan gameTime)
        {
            if (gameTime.TotalSeconds > bonusCountdown)
                bonusTime = false;

            if (gameTime.TotalMilliseconds - lastEnemyHit < 1200)
                return;

            foreach (Enemy e in gameState.enemies)
                if (hitEnemy(e))
                {
                    if (e is General)
                    {
                        this.damage(1);
                        lastEnemyHit = gameTime.TotalMilliseconds;
                        if (bonusTime || invicloak)
                            lastEnemyHit -= 700;
                        break;
                    }

                    if (invicloak)
                    {
                        points += 50;
                        lastEnemyHit = gameTime.TotalMilliseconds - 700;
                        break;
                    }

                    if (bonusTime)
                    {
                        if (e.damage(1) < 1)
                            gameState.enemies.Remove(e);
                        lastEnemyHit = gameTime.TotalMilliseconds - 700;
                        break;
                    }

                    this.damage(1);
                    lastEnemyHit = gameTime.TotalMilliseconds;
                }
        }

        /// <summary>
        /// Test kolizji z przeciwnikiem
        /// </summary>
        /// <param name="e">Przeciwnik, z którym sprawdzana jest kolicja</param>
        /// <returns>Informacja czy kolicja nastąpiła</returns>
        private bool hitEnemy(Enemy e)
        {
            Vector2 middle = new Vector2(e.getPosition().X + Field.SZ / 2, e.getPosition().Y + Field.SZ / 2);
            return middle.X >= position.X && middle.X < position.X + Field.SZ && middle.Y >= position.Y && middle.Y < position.Y + Field.SZ;
        }

        /// <summary>
        /// Metoda obsługująca strzały pocisków i propagująca aktualizację wstrzelonych wcześniej.
        /// </summary>
        /// <param name="gameTime">Czas gry</param>
        private void updateFires(TimeSpan gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Settings.fire) && gameTime.TotalMilliseconds - lastShoot > 200)
                if (firesCnt > 0)
                {
                    firesCnt--;
                    lastShoot = gameTime.TotalMilliseconds;
                    bool fired = false;
                    foreach (Fire f in fires)
                        if (!f.visible)
                        {
                            Vector2 fireSpeed = getFireSpeed();
                            Vector2 position = getFirePosition(fireSpeed);
                            f.shoot(position, fireSpeed);
                            fired = true;
                            break;
                        }
                    if (!fired)
                    {
                        Fire f = new Fire(gameState, Textures.getFireTex());
                        fires.Add(f);
                        f.shoot(position, getFireSpeed());
                    }
                }

            foreach (Fire f in fires)
                f.update(gameTime);
        }

        /// <summary>
        /// Metoda podająca pozycję początkową aktualnie wystrzelanaego, nowego pocisku.
        /// </summary>
        /// <param name="fireSpeed">Prędkość z jaką będzie wystrzelony pocisk. Definiuje ona równierz kierunek na podstawie którego wyzanczana jest pozycja początkowa</param>
        /// <returns>Pozycja początkowa pocisku</returns>
        private Vector2 getFirePosition(Vector2 fireSpeed)
        {
            if (fireSpeed.X > 0)
                return new Vector2(position.X + Field.SZ, position.Y);
            else if (fireSpeed.X < 0)
                return new Vector2(position.X - Field.SZ, position.Y);
            else if (fireSpeed.Y > 0)
                return new Vector2(position.X, position.Y + Field.SZ);
            else if (fireSpeed.Y < 0)
                return new Vector2(position.X, position.Y - Field.SZ);
            else
                return new Vector2(position.X + Field.SZ, position.Y);
        }

        /// <summary>
        /// Metoda podająca początkową prędkość strzału na podstawie ostatniego kierunku ruchu bohatera
        /// </summary>
        /// <returns>Początkową prędkość pocisku</returns>
        private Vector2 getFireSpeed()
        {
            if (lastMoveDirection == Settings.right)
                return new Vector2(5, 0);
            else if (lastMoveDirection == Settings.left)
                return new Vector2(-5, 0);
            else if (lastMoveDirection == Settings.down)
                return new Vector2(0, 5);
            else if (lastMoveDirection == Settings.up)
                return new Vector2(0, -5);
            return new Vector2(5, 0);
        }

        /// <summary>
        /// Metoda odpowiedzialna za aktualizację ruchu bohatera. Aktualizuje prędkość bohatera w zależności od wybranego rpzez gracza kierunku oraz utrzymuje gracza w granicach mapy.
        /// </summary>
        private void updateMove()
        {
            position += speed;
            if (!moving)
            {
                if (Keyboard.GetState().IsKeyDown(Settings.left))
                {
                    speed.X = -2;
                    speed.Y = 0;
                    moving = true;
                    lastMoveDirection = Settings.left;
                }
                else if (Keyboard.GetState().IsKeyDown(Settings.right))
                {
                    speed.X = 2;
                    speed.Y = 0;
                    moving = true;
                    lastMoveDirection = Settings.right;
                }
                else if (Keyboard.GetState().IsKeyDown(Settings.up))
                {
                    speed.X = 0;
                    speed.Y = -2;
                    moving = true;
                    lastMoveDirection = Settings.up;
                }
                else if (Keyboard.GetState().IsKeyDown(Settings.down))
                {
                    speed.X = 0;
                    speed.Y = 2;
                    moving = true;
                    lastMoveDirection = Settings.down;
                }
            }

            if (position.X > MaxX)
            {
                speed.X = 0;
                position.X = MaxX;
                moving = false;
            }
            else if (position.X < MinX)
            {
                speed.X = 0;
                position.X = MinX;
                moving = false;
            }

            if (position.Y > MaxY)
            {
                speed.Y = 0;
                position.Y = MaxY;
                moving = false;
            }
            else if (position.Y < MinY)
            {
                speed.Y = 0;
                position.Y = MinY;
                moving = false;
            }

            if (Math.Abs(historyPosition.X - position.X) == Field.SZ || Math.Abs(historyPosition.Y - position.Y) == Field.SZ)
            {
                historyPosition = position;
                speed.X = speed.Y = 0;
                moving = false;
                Map.getInstance()[(int)(position.X / Field.SZ), (int)(position.Y / Field.SZ)].dig();
            }
        }

        /// <summary>
        /// Implementacja bohatera rysująca najpierw jego samego a następnie pociski przez niego wystrzelone i bomby, które zastawił.
        /// </summary>
        /// <param name="spriteBatch">Kontekst rysowanego obiektu</param>
        public override void draw(SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);
            foreach (Fire f in fires)
                f.draw(spriteBatch);
            foreach (Weapons.Bomb b in bombs)
                b.draw(spriteBatch);
        }
    }
}
