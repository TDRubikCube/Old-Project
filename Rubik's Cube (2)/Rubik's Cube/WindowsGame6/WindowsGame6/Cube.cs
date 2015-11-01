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

    class Cube
    {
        readonly CubeState cubeState = new CubeState();
        readonly Random rand = new Random();
        public string ScramblingVectors;
        //float timer = 500;
        public int Angle;
        public int ScrambleIndex = 0;
        const int RotationSpeed = 10;
        int counter;
        public void Scramble()
        {
            GameTime gameTime = new GameTime();
            string[] possibleTurns = new string[6];
            possibleTurns[0] = "L";
            possibleTurns[1] = "R";
            possibleTurns[2] = "U";
            possibleTurns[3] = "D";
            possibleTurns[4] = "B";
            possibleTurns[5] = "F";
            for (int i = 0; i < 50; i++)
            {
                ScramblingVectors += possibleTurns[rand.Next(0, 5)];
            }
            //shouldScramble = false;
            ShouldAddScrambleToOrder = true;
        }

        public bool ShouldAddScrambleToOrder { get; set; }

        public void Solve()
        {
            cubeState.OriginalCubeState();

            OriginalCubeDraw();

        }

        public Model Model { get; set; }

        public Matrix[] MeshTransforms { get; set; }

        public Cube()
        {
            MeshTransforms = new Matrix[26];
            OriginalCubeDraw();
            Angle = 0;
            ScramblingVectors = "";
            Model = null;
        }

        public void Update(GameTime gameTime, bool shouldScramble, string algo)
        {
            if (shouldScramble)
            {
                Scramble();
                if (ScrambleIndex <= 25)
                    ScrambleIndex++;
            }
            //if (shouldScramble)
            //{
            //    Scramble();
            //    float elpased = (float)(gameTime.ElapsedGameTime.Milliseconds);
            //    timer -= elpased;
            //    if (timer < 0 && ScramblingVectors != null)
            //    {
            //        Rotate(ScramblingVectors[ScrambleIndex], isClockWise[rand.Next(0, 2)], algo);
            //        if (ScrambleIndex <= 25)
            //            ScrambleIndex++;
            //        timer = 500;
            //    }
            //}
        }

        private void OriginalCubeDraw()
        {
            for (int i = 0; i < 26; i++)
            {
                MeshTransforms[i] = Matrix.CreateTranslation(new Vector3(0, 0, 0));
            }
        }

        public void Rotate(Vector3 side, bool isClockWise, string algOrder)
        {
            const float sidePosition = Game1.CUBIE_SIZE;
            Angle -= RotationSpeed;
            //Debug.WriteLine("animAngle=     " + Angle);
            //Debug.WriteLine("Angle " + MathHelper.ToRadians(Angle));
            if (side == Vector3.Left)
            {
                counter++;
                foreach (int i in cubeState.FindCubiesOnSide(side, isClockWise))
                {
                    Debug.WriteLine(i);
                    if (isClockWise)
                    {
                        RotateSide(i, MathHelper.PiOver2 / 10f, 1.5f * sidePosition, -1.5f * sidePosition, 0, 'x');
                    }
                    else
                    {
                        RotateSide(i, -MathHelper.PiOver2 / 10f, 1.5f * sidePosition, -1.5f * sidePosition, 0, 'x');
                    }
                }
            }
            else if (side == Vector3.Right)
            {
                counter++;
                foreach (int i in cubeState.FindCubiesOnSide(side, isClockWise))
                {
                    if (isClockWise)
                    {
                        RotateSide(i, -MathHelper.PiOver2 / 10f, -1.5f * sidePosition, -1.5f * sidePosition, 0, 'x');
                    }
                    else
                    {
                        RotateSide(i, MathHelper.PiOver2 / 10f, -1.5f * sidePosition, -1.5f * sidePosition, 0, 'x');
                    }
                }

            }
            else if (side == Vector3.Up)
            {
                counter++;
                foreach (int i in cubeState.FindCubiesOnSide(side, isClockWise))
                {
                    if (isClockWise)
                    {
                        RotateSide(i, -MathHelper.PiOver2 / 10f, 0, 0, 0, 'y');
                    }
                    else
                    {
                        RotateSide(i, MathHelper.PiOver2 / 10f, 0, 0, 0, 'y');
                    }
                }
            }
            else if (side == Vector3.Down)
            {
                counter++;
                foreach (int i in cubeState.FindCubiesOnSide(side, isClockWise))
                {
                    if (isClockWise)
                    {
                        RotateSide(i, -MathHelper.PiOver2 / 10f, 0, 0, 0, 'y');
                    }
                    else
                    {
                        RotateSide(i, MathHelper.PiOver2 / 10f, 0, 0, 0, 'y');
                    }
                }
            }
            else if (side == Vector3.Forward)
            {
                counter++;
                foreach (int i in cubeState.FindCubiesOnSide(side, isClockWise))
                {
                    if (isClockWise)
                    {
                        RotateSide(i, -MathHelper.PiOver2 / 10f, 0, -1.5f * sidePosition, -1.5f * sidePosition, 'z');
                    }
                    else
                    {
                        RotateSide(i, MathHelper.PiOver2 / 10f, 0, -1.5f * sidePosition, 1.5f * sidePosition, 'z');
                    }
                }
            }
            else if (side == Vector3.Backward)
            {
                counter++;
                foreach (int i in cubeState.FindCubiesOnSide(side, isClockWise))
                {
                    if (isClockWise)
                    {
                        RotateSide(i, MathHelper.PiOver2 / 10f, 0, -1.5f * sidePosition, 1.5f * sidePosition, 'z');
                    }
                    else
                    {
                        RotateSide(i, -MathHelper.PiOver2 / 10f, 0, -1.5f * sidePosition, -1.5f * sidePosition, 'z');
                    }
                }
            }
            if (counter == 10)
            {
                if ((side == Vector3.Backward || side == Vector3.Forward))
                {
                    if (isClockWise)
                        isClockWise = false;
                    else
                        isClockWise = true;
                }
                cubeState.Rotate(side, isClockWise);
                counter = 0;
            }
        }

        private void RotateSide(int i, float angle, float x, float y, float z, char rotationAxis)
        {
            if (rotationAxis == 'x')
            {
                MeshTransforms[i] *= Matrix.CreateTranslation(new Vector3(x, y, z)) * Matrix.CreateRotationX(angle) * Matrix.CreateTranslation(new Vector3(-x, -y, -z));
            }
            else if (rotationAxis == 'y')
            {
                MeshTransforms[i] *= Matrix.CreateTranslation(new Vector3(x, y, z)) * Matrix.CreateRotationY(angle) * Matrix.CreateTranslation(new Vector3(-x, -y, -z));
            }
            else if (rotationAxis == 'z')
            {
                MeshTransforms[i] *= Matrix.CreateTranslation(new Vector3(x, y, z)) * Matrix.CreateRotationZ(angle) * Matrix.CreateTranslation(new Vector3(-x, -y, -z));
            }

        }
    }
}
