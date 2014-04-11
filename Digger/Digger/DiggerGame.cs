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

namespace Digger
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class DiggerGame : Microsoft.Xna.Framework.Game
    {
        GameState gameState;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Guy guy;
        public static Field[,] fields;

        public DiggerGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 50 * 20;
            graphics.PreferredBackBufferHeight = 50 * 12;
            Content.RootDirectory = "Content";
            fields = new Field[20, 15];
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

            guy = new Guy(graphics, spriteBatch, Vector2.Zero, Content.Load<Texture2D>(Textures.GUY), Vector2.Zero, 3, "test");
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 12; j++)
                    fields[i, j] = new Field(graphics, spriteBatch, new Vector2(i * 50, j * 50), Content.Load<Texture2D>(Textures.FIELD), false);
            fields[0, 0].dig();
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
            guy.update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 12; j++)
                    fields[i, j].draw(gameTime);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            guy.draw(gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
