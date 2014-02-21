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

namespace InShadows
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Renderer2D renderer;
        Editor editor;
        Level level;
        FPSCounter fps;
        float numFrames = 0;
        
        //long timeElapsed = 0;
        public Game1()
        {
            this.Window.AllowUserResizing = true;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            this.IsFixedTimeStep = false;
            
            graphics.SynchronizeWithVerticalRetrace = false;
            graphics.ApplyChanges();
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
            renderer = new Renderer2D(GraphicsDevice);
            renderer.TextFont = Content.Load<SpriteFont>("default_font");

            Texture2D texture = new Texture2D(GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.White });
            Entity.SetDefaultTexture(texture);


            level = TestLevelBuilder.buildTestLevel(texture);
            Camera camera = new Camera(GraphicsDevice.Viewport);
            camera.Pos = new Vector2(400, 250);
            level.Camera = camera;
            editor = new Editor();
            editor.CurrentLevel = level;

            fps = new FPSCounter();
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            
            editor.Update();
            level.Update(gameTime);
            fps.tickUpdate(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            fps.tickDraw();
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            level.Draw(renderer);
            editor.Draw(renderer);
            fps.Draw(renderer);
            numFrames++;
            base.Draw(gameTime);
        }
    }
}
