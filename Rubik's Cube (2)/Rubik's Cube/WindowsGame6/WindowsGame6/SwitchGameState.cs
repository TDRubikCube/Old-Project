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
    class SwitchGameState
    {
        Cube cube;
        ButtonSetUp button;
        Music music;
        Text lang;
        Matrix world;
        Matrix view;
        Matrix projection;
        MouseState oldMouseState;
        KeyboardState oldKeyboardState;
        SpriteFont text;
        Point mousePos;
        Vector3 cameraPos;
        Vector3 realLeft;
        Vector3 realRight;
        Vector3 realForward;
        Vector3 realBackward;
        Vector2 previousMousePosition = new Vector2();
        int previousMouseWheel;
        public bool justSwitched = false;
        bool shouldScramble;
        //bool lockScreen = true;
        string whichGenre = "classic";
        float horizontalAngle = 0;
        float verticalAngle = 0;
        float radius = 27.32f;
        public string algOrder = "";
        bool stopAnim = false;
        int rotationsLeft = 0;
        public enum GameState
        {
            MainMenu, Tutorial, FreePlay, Options
        }
        public GameState currentGameState = GameState.MainMenu;

        public SwitchGameState(GraphicsDevice GraphicsDevice, GraphicsDeviceManager graphics, ContentManager Content)
        {

            lang = new Text();
            cube = new Cube();
            button = new ButtonSetUp(graphics, GraphicsDevice, Content);
            music = new Music(graphics, GraphicsDevice, Content);
            button.ClassicBound = new Rectangle((int)(GraphicsDevice.Viewport.Width / 1.32f), GraphicsDevice.Viewport.Height / 3, 60, 40);
            button.RockBound = new Rectangle((int)(GraphicsDevice.Viewport.Width / 1.55f), GraphicsDevice.Viewport.Height / 3, 50, 40);
            cube.Model = Content.Load<Model>("rubik");
            text = Content.Load<SpriteFont>("font");
            world = Matrix.CreateTranslation(new Vector3(0, 0, 0));
            view = Matrix.CreateLookAt(new Vector3(20, 20, 20), new Vector3(0, 0, 0), Vector3.Up);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45f), GraphicsDevice.Viewport.AspectRatio, 10f, 200f);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            if (currentGameState == GameState.FreePlay)
            {
                CameraMovement(mouseState, oldMouseState);
                RotateWhichSide(keyboardState, oldKeyboardState, cameraPos, algOrder, stopAnim);
            }
            //if (keyboardState.IsKeyDown(Keys.O) && oldKeyboardState.IsKeyUp(Keys.O)) lockScreen = !lockScreen;
            mousePos = new Point(mouseState.X, mouseState.Y);
            cameraPos = Matrix.Invert(view).Translation;
            SwitchUpdate(mouseState, keyboardState, gameTime);
            OldState(ref mouseState, ref keyboardState);
        }

        private void realRotate(Vector3 cameraPos)
        {
            if (cameraPos.X >= -radius / 1.61f && cameraPos.X <= radius / 1.61f && cameraPos.Z > radius / 1.438f && cameraPos.Z <= radius * 1.012f)
            {
                realLeft = Vector3.Left;
                realRight = Vector3.Right;
                realForward = Vector3.Backward;
                realBackward = Vector3.Forward;
            }
            if (cameraPos.X >= radius / 1.366f && cameraPos.X <= radius * 1.012f && cameraPos.Z >= -radius / 1.366f && cameraPos.Z <= radius / 1.366f)
            {
                realLeft = Vector3.Backward;
                realRight = Vector3.Forward;
                realForward = Vector3.Right;
                realBackward = Vector3.Left;
            }
            if (cameraPos.X >= -radius / 1.366f && cameraPos.X <= radius / 1.366f && cameraPos.Z >= -radius * 1.012f && cameraPos.Z <= -radius / 1.52f)
            {
                realLeft = Vector3.Right;
                realRight = Vector3.Left;
                realForward = Vector3.Forward;
                realBackward = Vector3.Backward;
            }
            if (cameraPos.X >= -radius * 1.012f && cameraPos.X <= -radius / 1.52f && cameraPos.Z >= -radius / 1.52f && cameraPos.Z <= radius / 1.52f)
            {
                realLeft = Vector3.Forward;
                realRight = Vector3.Backward;
                realForward = Vector3.Left;
                realBackward = Vector3.Right;
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice GraphicsDevice)
        {
            SwitchDraw(spriteBatch, GraphicsDevice);
        }

        private void RotateWhichSide(KeyboardState keyboardState, KeyboardState oldKeyboardState, Vector3 cameraPos, string algOrder, bool stopAnim)
        {
            if (cube.Angle <= -100)
            {

                
                
                Debug.WriteLine("Original=   " + algOrder);
                if (algOrder.Length > 1)
                {
                    if ((algOrder[1] == 'i') || (algOrder[1] == 'I') || (algOrder[1] == '\''))
                    {
                        algOrder = algOrder.Substring(2);
                    }
                    else
                    {
                        algOrder = algOrder.Substring(1);
                    }

                    Debug.WriteLine("After Change" + algOrder);
                }
                else
                {
                    algOrder = "";
                }
                realRotate(cameraPos);
                 rotationsLeft = algOrder.Length;
                if (algOrder.Split('i').Length != -1)
                {
                    rotationsLeft -= algOrder.Split('i').Length - 1;

                }
                if (algOrder.Split('I').Length != -1)
                {
                    rotationsLeft -= algOrder.Split('I').Length - 1;

                }
                if (algOrder.Split('\'').Length != -1)
                {
                    rotationsLeft -= algOrder.Split('\'').Length - 1;

                }

                Debug.WriteLine("Number of rotations left:" + rotationsLeft);

                cube.Angle = 0;
            }
            
            if (keyboardState.IsKeyDown(Keys.L) && oldKeyboardState.IsKeyUp(Keys.L))
            {

                algOrder += "L";
                if (keyboardState.IsKeyDown(Keys.LeftShift) || keyboardState.IsKeyDown(Keys.RightShift))
                    algOrder += "I";

            }
            if (keyboardState.IsKeyDown(Keys.R) && oldKeyboardState.IsKeyUp(Keys.R))
            {

                algOrder += "R";
                if (keyboardState.IsKeyDown(Keys.LeftShift) || keyboardState.IsKeyDown(Keys.RightShift))
                    algOrder += "I";
            }
            if (keyboardState.IsKeyDown(Keys.U) && oldKeyboardState.IsKeyUp(Keys.U))
            {

                algOrder += "U";
                if (keyboardState.IsKeyDown(Keys.LeftShift) || keyboardState.IsKeyDown(Keys.RightShift))
                    algOrder += "I";
            }
            if (keyboardState.IsKeyDown(Keys.D) && oldKeyboardState.IsKeyUp(Keys.D))
            {

                algOrder += "D";
                if (keyboardState.IsKeyDown(Keys.LeftShift) || keyboardState.IsKeyDown(Keys.RightShift))
                    algOrder += "I";
            }
            if (keyboardState.IsKeyDown(Keys.B) && oldKeyboardState.IsKeyUp(Keys.B))
            {

                algOrder += "B";
                if (keyboardState.IsKeyDown(Keys.LeftShift) || keyboardState.IsKeyDown(Keys.RightShift))
                    algOrder += "I";
            }
            if (keyboardState.IsKeyDown(Keys.F) && oldKeyboardState.IsKeyUp(Keys.F))
            {

                algOrder += "F";
                if (keyboardState.IsKeyDown(Keys.LeftShift) || keyboardState.IsKeyDown(Keys.RightShift))
                    algOrder += "I";
            }
            UpdateAlgo(algOrder);

            //here the fun starts
            //Debug.WriteLine("algOrder=    "+algOrder);
            if (cube.ShouldAddScrambleToOrder)
            {
                algOrder += cube.ScramblingVectors;
                cube.ShouldAddScrambleToOrder = false;
            }
            if (algOrder.Length > 0 && !IsCubeCurrentlyRotating)
            {
                if ((algOrder[0] == 'L') || (algOrder[0] == 'l'))
                {
                    if (algOrder.Length > 1)
                    {
                        if ((algOrder[1] == 'i') || (algOrder[1] == 'I') || (algOrder[1] == '\''))
                        {
                            cube.Rotate(realLeft, false, algOrder);
                        }
                        else
                        {
                            cube.Rotate(realLeft, true, algOrder);                            
                        }
                    }
                    else
                        cube.Rotate(realLeft, true, algOrder);
                }
                else if ((algOrder[0] == 'R') || (algOrder[0] == 'r'))
                {
                    if (algOrder.Length > 1)
                    {
                        if ((algOrder[1] == 'i') || (algOrder[1] == 'I') || (algOrder[1] == '\''))
                        {
                            cube.Rotate(realRight, false, algOrder);
                        }
                        else
                        {
                            cube.Rotate(realRight, true, algOrder);
                        }
                    }
                    else
                        cube.Rotate(realRight, true, algOrder);
                }
                else if ((algOrder[0] == 'U') || (algOrder[0] == 'u'))
                {
                    if (algOrder.Length > 1)
                    {
                        if ((algOrder[1] == 'i') || (algOrder[1] == 'I') || (algOrder[1] == '\''))
                        {
                            cube.Rotate(Vector3.Up, false, algOrder);
                        }
                        else
                        {
                            cube.Rotate(Vector3.Up, true, algOrder);                            
                        }
                    }
                    else
                        cube.Rotate(Vector3.Up, true, algOrder);
                }
                else if ((algOrder[0] == 'D') || (algOrder[0] == 'd'))
                {
                    if (algOrder.Length > 1)
                    {
                        if ((algOrder[1] == 'i') || (algOrder[1] == 'I') || (algOrder[1] == '\''))
                        {
                            cube.Rotate(Vector3.Down, false, algOrder);
                        }
                        else
                        {
                            cube.Rotate(Vector3.Down, true, algOrder);                            
                        }
                    }
                    else
                        cube.Rotate(Vector3.Down, true, algOrder);
                }
                else if ((algOrder[0] == 'B') || (algOrder[0] == 'b'))
                {
                    if (algOrder.Length > 1)
                    {
                        if ((algOrder[1] == 'i') || (algOrder[1] == 'I') || (algOrder[1] == '\''))
                        {
                            cube.Rotate(realBackward, false, algOrder);
                        }
                        else
                        {
                            cube.Rotate(realBackward, true, algOrder);                            
                        }
                    }
                    else
                        cube.Rotate(realBackward, true, algOrder);
                }
                else if ((algOrder[0] == 'F') || (algOrder[0] == 'f'))
                {
                    if (algOrder.Length > 1)
                    {
                        if ((algOrder[1] == 'i') || (algOrder[1] == 'I') || (algOrder[1] == '\''))
                        {
                            cube.Rotate(realForward, false, algOrder);
                        }
                        else
                        {
                            cube.Rotate(realForward, true, algOrder);
                        }
                    }
                    else
                        cube.Rotate(realForward, true, algOrder);
                }
                else
                {
                    algOrder = algOrder.Substring(1);
                    Debug.WriteLine("AlgOrder unknown = " + algOrder);
                }



            }
        }

        public bool IsCubeCurrentlyRotating { get; set; }

        private void CameraMovement(MouseState mouseState, MouseState oldMouseState)
        {
            //if (!lockScreen)
            //{
            Vector2 currentMousePos = new Vector2(mouseState.X, mouseState.Y);
            previousMousePosition = new Vector2(oldMouseState.X, oldMouseState.Y);
            horizontalAngle += (currentMousePos.X - previousMousePosition.X) * 0.01f;
            verticalAngle += (currentMousePos.Y - previousMousePosition.Y) * 0.01f;
            view = Matrix.CreateLookAt(new Vector3(-(float)(radius * Math.Sin(horizontalAngle) * Math.Sin(verticalAngle)), -(float)(radius * Math.Cos(verticalAngle)), -(float)(radius * Math.Sin(verticalAngle) * Math.Cos(horizontalAngle))), new Vector3(0, 0, 0), Vector3.Up);
            if (mouseState.ScrollWheelValue < previousMouseWheel && radius < 107.32)
            {
                radius += 5;
            }
            if (mouseState.ScrollWheelValue > previousMouseWheel && radius > 17.33)
            {
                radius -= 5;
            }
            //}
        }

        private void OldState(ref MouseState mouseState, ref KeyboardState keyboardState)
        {
            oldMouseState = mouseState;
            oldKeyboardState = keyboardState;
            previousMouseWheel = mouseState.ScrollWheelValue;
        }

        private void SwitchUpdate(MouseState mouseState, KeyboardState keyboardState, GameTime gameTime)
        {
            switch (currentGameState)
            {
                case GameState.MainMenu:
                    if (button.btnFreePlay.isClicked) currentGameState = GameState.FreePlay;
                    if (button.btnTutorial.isClicked) currentGameState = GameState.Tutorial;
                    if (button.btnOptions.isClicked) currentGameState = GameState.Options;
                    button.btnOptions.Update(mouseState, false);
                    button.btnTutorial.Update(mouseState, false);
                    button.btnFreePlay.Update(mouseState, false);
                    break;
                case GameState.Tutorial:
                    if (keyboardState.IsKeyDown(Keys.Back)) currentGameState = GameState.MainMenu;
                    break;
                case GameState.Options:
                    if (keyboardState.IsKeyDown(Keys.Right) && oldKeyboardState.IsKeyUp(Keys.Right)) MediaPlayer.Stop();
                    if (button.btnHebrew.isClicked) lang.Hebrew();
                    if (button.btnEnglish.isClicked) lang.English();
                    if (button.ClassicBound.Contains(mousePos) && mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released && whichGenre != "classic")
                    {
                        whichGenre = "classic";
                        justSwitched = true;
                        MediaPlayer.Stop();
                    }
                    else if (whichGenre == "classic") justSwitched = false;
                    if (button.RockBound.Contains(mousePos) && mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released && whichGenre != "rock")
                    {
                        whichGenre = "rock";
                        justSwitched = true;
                        MediaPlayer.Stop();
                    }
                    else if (whichGenre == "rock") justSwitched = false;
                    music.Update(mouseState, whichGenre, justSwitched);
                    button.btnEnglish.Update(mouseState, false);
                    button.btnHebrew.Update(mouseState, false);
                    if (keyboardState.IsKeyDown(Keys.Back)) currentGameState = GameState.MainMenu;
                    break;
                case GameState.FreePlay:
                    if (keyboardState.IsKeyDown(Keys.Back)) currentGameState = GameState.MainMenu;
                    if (button.btnScramble.isClicked) shouldScramble = true;
                    if (button.btnSolve.isClicked) cube.Solve();
                    cube.Update(gameTime, shouldScramble, algOrder);
                    if (cube.ScrambleIndex >= 25)
                    {
                        shouldScramble = false;
                        cube.ScrambleIndex = 0;
                    }
                    button.btnScramble.Update(mouseState, false);
                    button.btnSolve.Update(mouseState, false);
                    break;
            }
        }

        private void SwitchDraw(SpriteBatch spriteBatch, GraphicsDevice GraphicsDevice)
        {
            switch (currentGameState)
            {
                case GameState.MainMenu:
                    spriteBatch.Begin();
                    spriteBatch.DrawString(text, lang.mainTitle, new Vector2(GraphicsDevice.Viewport.Width / 3, 10), Color.Black);
                    button.btnTutorial.Draw(spriteBatch);
                    button.btnOptions.Draw(spriteBatch);
                    button.btnFreePlay.Draw(spriteBatch);
                    spriteBatch.End();
                    break;
                case GameState.FreePlay:
                    spriteBatch.Begin();
                    spriteBatch.DrawString(text, lang.freePlayTitle, new Vector2(GraphicsDevice.Viewport.Width / 3, 10), Color.Black);
                    spriteBatch.DrawString(text, lang.freePlayScramble, new Vector2(GraphicsDevice.Viewport.Width / 13f, GraphicsDevice.Viewport.Height / 1.4f), Color.Black);
                    spriteBatch.DrawString(text, lang.freePlaySolve, new Vector2(GraphicsDevice.Viewport.Width / 4f, GraphicsDevice.Viewport.Height / 1.4f), Color.Black);
                    button.btnScramble.Draw(spriteBatch);
                    button.btnSolve.Draw(spriteBatch);
                    spriteBatch.End();
                    DrawModel(cube, world, view, projection, GraphicsDevice);
                    break;
                case GameState.Options:
                    spriteBatch.Begin();
                    button.btnHebrew.Draw(spriteBatch);
                    button.btnEnglish.Draw(spriteBatch);
                    spriteBatch.DrawString(text, lang.optionsTitle, new Vector2(GraphicsDevice.Viewport.Width / 3, 10), Color.Black);
                    spriteBatch.DrawString(text, lang.optionsFreeText, new Vector2(GraphicsDevice.Viewport.Width / 3, 40), Color.Black);
                    spriteBatch.DrawString(text, "Choose Music Genre:         Rock       Classic", new Vector2(GraphicsDevice.Viewport.Width / 3f, GraphicsDevice.Viewport.Height / 3f), Color.Black);
                    spriteBatch.DrawString(text, "English", new Vector2(GraphicsDevice.Viewport.Width / 2.5f, 440), Color.Black);
                    spriteBatch.DrawString(text, "ת י ר ב ע", new Vector2(GraphicsDevice.Viewport.Width / 1.85f, 440), Color.Black);
                    spriteBatch.End();
                    break;
                case GameState.Tutorial:
                    spriteBatch.Begin();
                    spriteBatch.DrawString(text, lang.tutorialTitle, new Vector2(GraphicsDevice.Viewport.Width / 3, 10), Color.Black);
                    spriteBatch.DrawString(text, lang.tutorialFreeText, new Vector2(GraphicsDevice.Viewport.Width / 3, 50), Color.Black);
                    spriteBatch.DrawString(text, lang.tutorialFreeText2, new Vector2(GraphicsDevice.Viewport.Width / 3, 90), Color.Black);
                    spriteBatch.End();
                    break;
            }
        }

        public void DrawModel(Cube cube, Matrix objectWorldMatrix, Matrix view, Matrix projection, GraphicsDevice GraphicsDevice)
        {
            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            for (int index = 0; index < cube.Model.Meshes.Count; index++)
            {
                ModelMesh mesh = cube.Model.Meshes[index];
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();   
                    effect.World = mesh.ParentBone.Transform * Matrix.CreateTranslation(Game1.CUBIE_SIZE, -Game1.CUBIE_SIZE, -Game1.CUBIE_SIZE) * cube.MeshTransforms[index] * objectWorldMatrix;
                    effect.View = view;
                    effect.Projection = projection;
                }
                mesh.Draw();
            }
        }
        public void UpdateAlgo(string algo)
        {
            algOrder = algo;
        }

    }
}
