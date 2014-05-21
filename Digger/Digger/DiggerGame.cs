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
using Digger.Objects;
using Digger.Objects.Artefacts;

namespace Digger
{
    /// <summary>
    /// Głowny obiekt gry
    /// </summary>
    public class DiggerGame : Microsoft.Xna.Framework.Game
    {
        /// <summary>
        /// Referencja na From w którym wyświetlana jest gra
        /// </summary>
        private GameForm gameForm;
        /// <summary>
        /// Referencja na obiekt, na którym rysowana jest gra
        /// </summary>
        private IntPtr drawSurface;

        /// <summary>
        /// Czas w milisekundach przez jaki gra była zatrzymana
        /// </summary>
        private TimeSpan pauseMilis = new TimeSpan(0);
        /// <summary>
        /// Czas ostatniej pausy w grze
        /// </summary>
        private TimeSpan lastPause = new TimeSpan(0);
        /// <summary>
        /// Minimalny czas zatrzymania i wznowienia gry
        /// </summary>
        private TimeSpan delay = new TimeSpan(0, 0, 0, 0, 200);
        /// <summary>
        /// Zmienna informująca czy gra jest zatrzymana czy też toczy się normalnie
        /// </summary>
        private bool pause = true;
        /// <summary>
        /// Obiekt aktualnego stanu gry
        /// </summary>
        GameState gameState;
        /// <summary>
        /// Obiekt obsługujący urządzenie graficzne
        /// </summary>
        GraphicsDeviceManager graphics;
        /// <summary>
        /// Obiekt umożliwiający rysowanie grupy obiektów z tymi samymi ustawieniami
        /// </summary>
        SpriteBatch spriteBatch;

        /// <summary>
        /// Szerokość mapy
        /// </summary>
        public const int GRAPHICS_WIDTH = Field.SZ * Map.WIDTH;
        /// <summary>
        /// Wysokość mapy
        /// </summary>
        public const int GRAPHICS_HEIGHT = Field.SZ * Map.HEIGHT;

        /// <summary>
        /// Obiekt udostępniający tekstury
        /// </summary>
        public static Textures textures;

        /// <summary>
        /// Konstruktor przekazujący referencje na froma w którym wyświetlana jest gra
        /// </summary>
        /// <param name="gameForm">Referencja na form gry</param>
        public DiggerGame(GameForm gameForm)
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = GRAPHICS_WIDTH;
            graphics.PreferredBackBufferHeight = GRAPHICS_HEIGHT;
            Content.RootDirectory = "Content";

            this.gameForm = gameForm;
            this.drawSurface = gameForm.getDrawSurface();
            graphics.PreparingDeviceSettings +=
            new EventHandler<PreparingDeviceSettingsEventArgs>(graphics_PreparingDeviceSettings);
            System.Windows.Forms.Control.FromHandle((this.Window.Handle)).VisibleChanged +=
            new EventHandler(Game1_VisibleChanged);

            gameForm.setGame(this);
        }

        /// <summary>
        /// Zdarzenie informujące o tym, że panel do rysowania rozgrywki został utworzony.
        /// </summary>
        ///<param name="sender">Nadawca zdarzenia</param>
        ///<param name="e">Argumenty zdarzenia</param>
        void graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            e.GraphicsDeviceInformation.PresentationParameters.DeviceWindowHandle =
            drawSurface;
        }

        /// <summary>
        /// Zdarzenie informujące o pojawieniu się wtyginalnego okna z grą
        /// </summary>
        ///<param name="sender">Nadawca zdarzenia</param>
        ///<param name="e">Argumenty zdarzenia</param>
        private void Game1_VisibleChanged(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Control.FromHandle((this.Window.Handle)).Visible == true)
                System.Windows.Forms.Control.FromHandle((this.Window.Handle)).Visible = false;
        }

        /// <summary>
        /// Ładowanie zasobów i grafiki.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            textures = new Textures(Content);
            gameState = new GameState();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// Główna metoda logiki gry, aktualizująca jej stan i sprawdzająca różne sytuacje wyjątkowe jak śmierć bohatera czy przejśc do następnego poziomu
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            updateOptions(gameTime);
            updateHelp(gameTime);
            if (updatePause(gameTime))
                return;

            gameState.update(gameTime.TotalGameTime.Subtract(pauseMilis));

            if (gameState.guy.getHp() < 1)
            {
                pause = true;
            }

            updateForm();
        }

        /// <summary>
        /// Obsługuje wyświetlanie pomocy
        /// </summary>
        /// <param name="gameTime">Czas gry</param>
        private void updateHelp(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Settings.help))
            {
                if (!pause)
                {
                    if (gameTime.TotalGameTime - lastPause > delay)
                    {
                        pause = true;
                        lastPause = gameTime.TotalGameTime;
                    }
                }

                gameForm.openHelp();
            }
        }

        /// <summary>
        /// Obsługuje wyświetlanie ustawień
        /// </summary>
        /// <param name="gameTime">Czas gry</param>
        private void updateOptions(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Settings.options))
            {
                if (!pause)
                {
                    if (gameTime.TotalGameTime - lastPause > delay)
                    {
                        pause = true;
                        lastPause = gameTime.TotalGameTime;
                    }
                }

                gameForm.openSettings();
            }
        }

        /// <summary>
        /// Obsługuje zatrzymywanie i startowanie rozgrywki
        /// </summary>
        /// <param name="gameTime">Czas gry</param>
        /// <returns>Informację czy gra została zatrzymana</returns>
        private bool updatePause(GameTime gameTime)
        {
            if (!pause)
            {
                if (Keyboard.GetState().IsKeyDown(Settings.pause))
                {
                    if (gameTime.TotalGameTime - lastPause > delay)
                    {
                        pause = true;
                        lastPause = gameTime.TotalGameTime;
                    }
                }
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Settings.pause))
                {
                    if (gameTime.TotalGameTime - lastPause > delay)
                    {
                        pause = false;
                        pauseMilis = pauseMilis.Add(gameTime.TotalGameTime.Subtract(lastPause));
                        lastPause = gameTime.TotalGameTime;
                    }
                }
                return true;
            }

            return false;
        }

        /// <summary>
        /// Metoda aktualizująca stan gry wyświetlnay na formie gry
        /// </summary>
        private void updateForm()
        {
            gameForm.updateLevel(gameState.level);
            gameForm.updateGuy(gameState.guy.getHp());
            gameForm.updatePoints(gameState.guy.points);
            gameForm.updateMissiles(gameState.guy.firesCnt);
            gameForm.updateBombs(gameState.guy.bombCnt);
            gameForm.updateInvicloacks(gameState.guy.invicloackCnt);
            gameForm.updateBonusTime(gameState.guy.bonusTimeLeft);
        }

        /// <summary>
        /// Zatrzymuje grę
        /// </summary>
        public void stop()
        {
            pause = true;
        }

        /// <summary>
        /// Wznawia zatrzymaną grę
        /// </summary>
        public void start()
        {
            pause = false;
        }

        /// <summary>
        /// Główna metoda rysujaza aktualny stan gry
        /// </summary>
        /// <param name="gameTime">Czas gry</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            foreach (Field f in Map.getInstance())
                f.draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            foreach (Artefact a in gameState.artefacts)
                a.draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            foreach (Enemy e in gameState.enemies)
                e.draw(spriteBatch);

            gameState.guy.draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
