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

namespace ATaleOfTwoHorns
{



    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Level tiles;

        Background background1;

        Camera camera;

        BackgroundManager backgroundManager;

        Player player;

        ScreenManager screenManager;

        Hud hud;

        PickUpManager pickupManager;
        GateManager gateManager;

        EnemyManager m_EnemyManager;

        Boss boss;

        int screenWidth = 1074, screenHeight = 768;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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

            //stuff to change screen size / fullscreen
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            Sounds.loadContent(Content);
            BackgroundMusic.loadContent(Content);
            IsMouseVisible = true;

            hud = new Hud();
            hud.LoadContent(Content);

            screenManager = new ScreenManager();
            screenManager.LoadContent(Content, graphics);

            gateManager = new GateManager();
            m_EnemyManager = new EnemyManager();
            pickupManager = new PickUpManager();
            tiles = new Level(new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32));
            tiles.loadContent(Content, "EnchantedForestTileSetUpdated", "Content/Level0.txt");

            EnemyManager.loadContent(Content);
            PickUpManager.loadContent(Content);
            GateManager.loadContent(Content);
            player = new Player( new Vector2(200, 1000),new Rectangle(0, 0, 128, 64), new Rectangle(0, 0, 128,64 ));
            player.loadContent(Content);

            

            camera = new Camera(GraphicsDevice.Viewport);
            camera.Limits = new Rectangle(0, 0, Level.getWidthOfArray() * tiles.Destination.Width, Level.getHeightOfArray() * tiles.Destination.Height);

            background1 = new Background(camera);
            Background.loadContent(Content, Level.levelCount, 1);
            background1.setParallaxes();

            backgroundManager = new BackgroundManager();
            backgroundManager.addBackground(background1);

            boss = new Boss();
            boss.loadContent(Content);
            // TODO: use this.Content to load your game content here
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
            screenManager.Update(gameTime, this);

            if (screenManager.CurrentGameState == GameState.Game)
            {

                // TODO: Add your update logic here

                KeyboardState keyBoard = Keyboard.GetState();



               // if (keyBoard.IsKeyDown(Keys.L))
               //     tiles.levelIncrement();

                player.update(gameTime);

                camera.lookAt(player.Position);
                camera.Update(gameTime);

                hud.Update(gameTime);

                pickupManager.update(gameTime);

                gateManager.update(gameTime);

                backgroundManager.update(gameTime, player);

                m_EnemyManager.update(gameTime);

                boss.update(gameTime);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (screenManager.CurrentGameState == GameState.Game)
            {
                // TODO: Add your drawing code here
                backgroundManager.draw(spriteBatch, camera);
                spriteBatch.Begin(SpriteSortMode.Immediate, null, SamplerState.PointClamp, null, null, null, Camera.ViewMatrix);
                
                
                tiles.Draw(spriteBatch);

                gateManager.draw(spriteBatch);
                pickupManager.draw(spriteBatch);

                player.draw(spriteBatch);

                boss.draw(spriteBatch);

                m_EnemyManager.draw(spriteBatch);

                spriteBatch.End();

                spriteBatch.Begin();

                hud.Draw(spriteBatch, gameTime);

                spriteBatch.End();

                player.drawParticles(spriteBatch);
                boss.drawParticles(spriteBatch);
            }
            else
            {
            spriteBatch.Begin();
            screenManager.Draw(spriteBatch);
            spriteBatch.End();
                }
            base.Draw(gameTime);
        }

        public void quit()
        {
            this.Exit();
        }
    }
}
