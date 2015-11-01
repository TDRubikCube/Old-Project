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
using System.Diagnostics;

namespace RubikCube
{
    class ButtonSetUp
    {
        public Button btnTutorial;
        public Button btnFreePlay;
        public Button btnOptions;
        public Button btnMute;
        public Button btnUnMute;
        public Button btnEnglish;
        public Button btnHebrew;
        public Button btnScramble;
        public Button btnSolve;
        Texture2D scrambleButton;
        Texture2D solveButton;
        Texture2D englishButton;
        Texture2D hebrewButton;
        Texture2D tutorialButton;
        Texture2D optionsButton;
        Texture2D freePlayButton;
        Texture2D muteButton;
        Texture2D unMuteButton;
        Rectangle classicBound;
        Rectangle rockBound;
        public ButtonSetUp(GraphicsDeviceManager graphics, GraphicsDevice graphicsDevice, ContentManager Content)
        {
            scrambleButton = Content.Load<Texture2D>("pics/scramble");
            solveButton = Content.Load<Texture2D>("pics/solved");
            tutorialButton = Content.Load<Texture2D>("pics/Tutorial");
            freePlayButton = Content.Load<Texture2D>("pics/FreePlay");
            optionsButton = Content.Load<Texture2D>("pics/Options");
            muteButton = Content.Load<Texture2D>("pics/Mute");
            unMuteButton = Content.Load<Texture2D>("pics/unMute");
            englishButton = Content.Load<Texture2D>("pics/english");
            hebrewButton = Content.Load<Texture2D>("pics/hebrew");
            //tutorial button
            btnTutorial = new Button(tutorialButton, graphics.GraphicsDevice);
            btnTutorial.SetPosition(new Vector2(graphicsDevice.Viewport.Width / 2.5f, graphicsDevice.Viewport.Height/4.5f));
            //freeplay button
            btnFreePlay = new Button(freePlayButton, graphics.GraphicsDevice);
            btnFreePlay.SetPosition(new Vector2(graphicsDevice.Viewport.Width / 2.5f, graphicsDevice.Viewport.Height / 2.3f));
            //options button
            btnOptions = new Button(optionsButton, graphics.GraphicsDevice);
            btnOptions.SetPosition(new Vector2(graphicsDevice.Viewport.Width / 2.7f, graphicsDevice.Viewport.Height / 1.5f));
            btnOptions.size = new Vector2(graphicsDevice.Viewport.Width / 4, graphicsDevice.Viewport.Height / 4);
            //mute button
            btnMute = new Button(muteButton, graphics.GraphicsDevice);
            btnMute.SetPosition(new Vector2(graphicsDevice.Viewport.Width / 1.09f, graphicsDevice.Viewport.Height / 30f));
            btnMute.size = new Vector2(graphicsDevice.Viewport.Width / 12f, graphicsDevice.Viewport.Height / 11f);
            //unMute button
            btnUnMute = new Button(unMuteButton, graphics.GraphicsDevice);
            btnUnMute.SetPosition(new Vector2(graphicsDevice.Viewport.Width / 1.09f, graphicsDevice.Viewport.Height / 30f));
            btnUnMute.size = new Vector2(graphicsDevice.Viewport.Width / 12.8f, graphicsDevice.Viewport.Height / 11.8f);
            //english button
            btnEnglish = new Button(englishButton, graphics.GraphicsDevice);
            btnEnglish.SetPosition(new Vector2(graphicsDevice.Viewport.Width / 2.5f, graphicsDevice.Viewport.Height / 1.25f));
            btnEnglish.size = new Vector2(graphicsDevice.Viewport.Width / 10, graphicsDevice.Viewport.Height / 10);
            //hebrew button
            btnHebrew = new Button(hebrewButton, graphics.GraphicsDevice);
            btnHebrew.SetPosition(new Vector2(graphicsDevice.Viewport.Width / 1.9f, graphicsDevice.Viewport.Height / 1.25f));
            btnHebrew.size = new Vector2(graphicsDevice.Viewport.Width / 10, graphicsDevice.Viewport.Height / 10);
            //scramble button
            btnScramble = new Button(scrambleButton, graphics.GraphicsDevice);
            btnScramble.SetPosition(new Vector2(graphicsDevice.Viewport.Width / 20f, graphicsDevice.Viewport.Height / 1.25f));
            btnScramble.size = new Vector2(graphicsDevice.Viewport.Width / 6, graphicsDevice.Viewport.Height / 5);
            //solved button
            btnSolve = new Button(solveButton, graphics.GraphicsDevice);
            btnSolve.SetPosition(new Vector2(graphicsDevice.Viewport.Width / 4.5f, graphicsDevice.Viewport.Height / 1.25f));
            btnSolve.size = new Vector2(graphicsDevice.Viewport.Width / 7, graphicsDevice.Viewport.Height / 5);
            
        }
        public Rectangle ClassicBound
        {
            get { return classicBound; }
            set { classicBound = value; }
        }
        public Rectangle RockBound
        {
            get { return rockBound; }
            set { rockBound = value; }
        }
    }
}
