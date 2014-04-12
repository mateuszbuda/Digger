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
        GameState gameState;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static Guy guy;
        List<Diamond> diamonds = new List<Diamond>();
        Texture2D diamond;
        public static Field[,] fields;

        public DiggerGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = Field.SZ * Map.WIDTH;
            graphics.PreferredBackBufferHeight = Field.SZ * Map.HEIGHT;
            Content.RootDirectory = "Content";
            fields = new Field[Map.WIDTH, Map.HEIGHT];
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

            //hero
            guy = new Guy(graphics, spriteBatch, Vector2.Zero, Content.Load<Texture2D>(Textures.GUY), Vector2.Zero, 3, "test");

            // raw maze
            for (int i = 0; i < Map.WIDTH; i++)
                for (int j = 0; j < Map.HEIGHT; j++)
                    fields[i, j] = new Field(graphics, spriteBatch, new Vector2(i * Field.SZ, j * Field.SZ), Content.Load<Texture2D>(Textures.FIELD), false);

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
                    else
                    {
                        t1 = r.Next(0, 6);
                        if (fields[t1, i + 1].digged)
                            fields[t1, i].dig();

                        t1 = r.Next(6, 12);
                        if (fields[t1, i + 1].digged)
                            fields[t1, i].dig();

                        t1 = r.Next(12, 20);
                        if (fields[t1, i + 1].digged)
                            fields[t1, i].dig();
                    }
                }
            }

            // diamonds distribution
            diamond = Content.Load<Texture2D>(Textures.DIAMOND);
            int x, y, d = 0;
            while (d < 10)
            {
                x = r.Next(Map.WIDTH);
                y = r.Next(Map.HEIGHT);
                if (!fields[x, y].digged)
                {
                    diamonds.Add(new Diamond(graphics, spriteBatch, new Vector2(x * Field.SZ, y * Field.SZ), diamond, 10, false));
                    d++;
                }
            }
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
            foreach (Diamond d in diamonds)
                d.update(gameTime);
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
            foreach (Diamond d in diamonds)
                d.draw(gameTime);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            guy.draw(gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
