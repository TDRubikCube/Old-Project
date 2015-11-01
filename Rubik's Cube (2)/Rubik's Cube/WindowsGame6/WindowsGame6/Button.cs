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
using System.Threading;
using System.Diagnostics;

namespace RubikCube
{
    class Button
    {
        Texture2D texture;
        Vector2 position;
        Rectangle rectangle;
        public Vector2 size;
        public bool isClicked;

        public Button(Texture2D newTexture, GraphicsDevice graphics)
        {
            texture = newTexture;
            size = new Vector2(graphics.Viewport.Width / 5, graphics.Viewport.Height / 7);
        }

        public void Update(MouseState mouse, bool isMuteButton)
        {
            rectangle = new Rectangle((int)(position.X), (int)(position.Y), (int)(size.X), (int)(size.Y));
            Point mousePos = new Point(mouse.X, mouse.Y);
            if (rectangle.Contains(mousePos) && mouse.LeftButton == ButtonState.Pressed)
            {
                if (!isMuteButton)
                    Thread.Sleep(500);
                else 
                    Thread.Sleep(70);
                isClicked = true;
            }
            else isClicked = false;
        }

        public void SetPosition(Vector2 newPositon)
        {
            position = newPositon;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
                spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}
