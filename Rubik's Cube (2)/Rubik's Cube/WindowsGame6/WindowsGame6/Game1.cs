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
using System.IO;
using System.Diagnostics;
//using System.Of[A].Down;
namespace RubikCube
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Music music;
        Cube cube;
        ButtonSetUp button;
        SwitchGameState gameState;
        public const float CUBIE_SIZE = 1.918f;
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
            this.IsMouseVisible = true;
            Window.AllowUserResizing = true;
            this.IsFixedTimeStep = false;
            cube = new Cube();
            gameState = new SwitchGameState(GraphicsDevice, graphics, Content);
            music = new Music(graphics, GraphicsDevice, Content);
            button = new ButtonSetUp(graphics, GraphicsDevice, Content);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            //    this.Exit();
            MouseState mouseState = Mouse.GetState();
            button.btnMute.Update(mouseState, true);
            button.btnUnMute.Update(mouseState, true);
            MuteEvent();
            gameState.Update(gameTime);
            base.Update(gameTime);
        }

        private void MuteEvent()
        {
            if (button.btnUnMute.isClicked && MediaPlayer.State == MediaState.Playing)
            {
                music.isMuted = true;
                MediaPlayer.Pause();
            }
            else if (button.btnMute.isClicked && MediaPlayer.State == MediaState.Paused)
            {
                music.isMuted = false;
                MediaPlayer.Resume();
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            gameState.Draw(spriteBatch, GraphicsDevice);
            spriteBatch.Begin();
            if (music.isMuted)
                button.btnMute.Draw(spriteBatch);
            else
                button.btnUnMute.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
