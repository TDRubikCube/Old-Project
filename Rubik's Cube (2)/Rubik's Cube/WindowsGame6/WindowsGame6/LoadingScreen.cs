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

namespace RubikCube
{
    class loadingScreen
    {
        Cube cube;
        SwitchGameState gameState;
        Music music;
        ButtonSetUp button;
        SpriteFont font;
        int totalDots = 1;
        public bool shouldStart;
        float timer = 500;

        public loadingScreen(GraphicsDevice GraphicsDevice, GraphicsDeviceManager graphics, ContentManager Content)
        {
            font = Content.Load<SpriteFont>("font");
            shouldStart = false;
        }

        public virtual void Draw(SpriteBatch spriteBatch, GraphicsDevice GraphicsDevice)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "loading.", new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 14), Color.Black);
            shouldStart = true;
            for (int i = 1; i < totalDots; i++)
            {
                int j = 3;
                if (i == 2)
                    j -= 4;
                spriteBatch.DrawString(font, ".", new Vector2(GraphicsDevice.Viewport.Width / 1.68f - j, GraphicsDevice.Viewport.Height / 14), Color.Black);
            }
            spriteBatch.End();
        }

        public virtual void Draw(SpriteBatch spriteBatch, GraphicsDevice GraphicsDevice, int diffrence)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, ".", new Vector2(GraphicsDevice.Viewport.Width / 2 + diffrence, GraphicsDevice.Viewport.Height / 14), Color.Black);
            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            float elpased = (float)(gameTime.ElapsedGameTime.Milliseconds);
            timer -= elpased;
            if (timer < 0)
            {
                if (totalDots < 3)
                {
                    totalDots++;
                }
                else
                    totalDots = 1;
                timer = 500;
            }
        }

        private void afterLoadingStart(GraphicsDevice GraphicsDevice, GraphicsDeviceManager graphics, ContentManager Content)
        {
            cube = new Cube();
            gameState = new SwitchGameState(GraphicsDevice, graphics, Content);
            music = new Music(graphics, GraphicsDevice, Content);
            button = new ButtonSetUp(graphics, GraphicsDevice, Content);
        }

    }
}
