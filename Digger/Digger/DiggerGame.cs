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
    /// This is the main type for your game
    /// </summary>
    public class DiggerGame : Microsoft.Xna.Framework.Game
    {
        private GameForm gameForm;
        private IntPtr drawSurface;

        private TimeSpan pauseMilis = new TimeSpan(0);
        private TimeSpan lastPause = new TimeSpan(0);
        private TimeSpan delay = new TimeSpan(0, 0, 0, 0, 200);
        private bool pause = true;
        GameState gameState;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public const int GRAPHICS_WIDTH = Field.SZ * Map.WIDTH;
        public const int GRAPHICS_HEIGHT = Field.SZ * Map.HEIGHT;

        public static Textures textures;

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
        /// Event capturing the construction of a draw surface and makes sure this gets redirected to
        /// a predesignated drawsurface marked by pointer drawSurface
        /// </summary>
        ///
        ///<param name="sender"></param>
        ///
        ///<param name="e"></param>
        void graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            e.GraphicsDeviceInformation.PresentationParameters.DeviceWindowHandle =
            drawSurface;
        }

        /// <summary>
        /// Occurs when the original gamewindows' visibility changes and makes sure it stays invisible
        /// </summary>
        ///
        ///<param name="sender"></param>
        ///
        ///<param name="e"></param>
        private void Game1_VisibleChanged(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Control.FromHandle((this.Window.Handle)).Visible == true)
                System.Windows.Forms.Control.FromHandle((this.Window.Handle)).Visible = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            textures = new Textures(Content);
            gameState = new GameState();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
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
                return;
            }

            gameState.update(gameTime.TotalGameTime.Subtract(pauseMilis));
            
            if (gameState.guy.getHp() < 1)
            {
                pause = true;
                gameOver();
            }

            updateForm();
        }

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

        public void stop()
        {
            pause = true;
        }

        public void start()
        {
            pause = false;
        }

        private void gameOver()
        {
            return;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
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
