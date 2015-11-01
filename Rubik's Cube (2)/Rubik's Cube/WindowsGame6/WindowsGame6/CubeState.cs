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
    public class CubeState
    {
        int[, ,] cubeArray = new int[3, 3, 3];

        public CubeState()
        {
            OriginalCubeState();
        }

        public void OriginalCubeState()
        {
            cubeArray[0, 0, 0] = 14;
            cubeArray[0, 0, 1] = 5;
            cubeArray[0, 0, 2] = 19;
            cubeArray[0, 1, 0] = 2;
            cubeArray[0, 1, 1] = 13;
            cubeArray[0, 1, 2] = 0;
            cubeArray[0, 2, 0] = 4;
            cubeArray[0, 2, 1] = 16;
            cubeArray[0, 2, 2] = 12;
            //check
            cubeArray[1, 0, 0] = 15;
            cubeArray[1, 1, 0] = 1;
            cubeArray[1, 2, 0] = 7;
            cubeArray[1, 2, 1] = 25;
            cubeArray[1, 2, 2] = 9;
            cubeArray[1, 1, 2] = 3;
            cubeArray[1, 0, 2] = 24;
            cubeArray[1, 0, 1] = 10;
            //check
            cubeArray[2, 0, 0] = 6;
            cubeArray[2, 0, 1] = 8;
            cubeArray[2, 0, 2] = 22;
            cubeArray[2, 1, 0] = 18;
            cubeArray[2, 1, 1] = 21;
            cubeArray[2, 1, 2] = 11;
            cubeArray[2, 2, 0] = 17;
            cubeArray[2, 2, 1] = 20;
            cubeArray[2, 2, 2] = 23;
        }

        public int GetCubie(int x, int y, int z)
        {
            return cubeArray[x, y, z];
        }

        public int[] FindCubiesOnSide(Vector3 side, bool isClockWise)
        {
            int x = 0, y = 0, z = 0;
            int limitX = 0, limitY = 0, limitZ = 0;
            FindLimitsX(ref side, ref x, ref limitX);
            FindLimitsY(ref side, ref y, ref limitY);
            FindLimitsZ(ref side, ref z, ref limitZ);
            List<int> returnValue = new List<int>();
            for (; x < limitX; x++)
            {
                if (side.Z >= 0) z = 0;
                if (side.Z < 0) z = 2;
                for (; z < limitZ; z++)
                {
                    if (side.Y <= 0) y = 0;
                    if (side.Y > 0) y = 2;
                    for (; y < limitY; y++)
                    {
                        returnValue.Add(cubeArray[x, y, z]);
                    }
                }
            }

            //Rotate(side, isClockWise);
            return returnValue.ToArray();
        }

        public void Rotate(Vector3 side, bool isClockWise)
        {
            if (side == Vector3.Backward)
            {
                if (isClockWise)
                {
                    int originalCenterNum = cubeArray[1, 0, 0];
                    cubeArray[1, 0, 0] = cubeArray[2, 1, 0];
                    cubeArray[2, 1, 0] = cubeArray[1, 2, 0];
                    cubeArray[1, 2, 0] = cubeArray[0, 1, 0];
                    cubeArray[0, 1, 0] = originalCenterNum;
                    int originalOuterNum = cubeArray[0, 0, 0];
                    cubeArray[0, 0, 0] = cubeArray[2, 0, 0];
                    cubeArray[2, 0, 0] = cubeArray[2, 2, 0];
                    cubeArray[2, 2, 0] = cubeArray[0, 2, 0];
                    cubeArray[0, 2, 0] = originalOuterNum;
                }
                else
                {
                    int originalCenterNum = cubeArray[1, 0, 0];
                    cubeArray[1, 0, 0] = cubeArray[0, 1, 0];
                    cubeArray[0, 1, 0] = cubeArray[1, 2, 0];
                    cubeArray[1, 2, 0] = cubeArray[2, 1, 0];
                    cubeArray[2, 1, 0] = originalCenterNum;
                    int originalOuterNum = cubeArray[0, 0, 0];
                    cubeArray[0, 0, 0] = cubeArray[0, 2, 0];
                    cubeArray[0, 2, 0] = cubeArray[2, 2, 0];
                    cubeArray[2, 2, 0] = cubeArray[2, 0, 0];
                    cubeArray[2, 0, 0] = originalOuterNum;
                }
            }
            else if (side == Vector3.Forward)
            {
                if (isClockWise)
                {
                    int originalCenterNum = cubeArray[1, 0, 2];
                    cubeArray[1, 0, 2] = cubeArray[0, 1, 2];
                    cubeArray[0, 1, 2] = cubeArray[1, 2, 2];
                    cubeArray[1, 2, 2] = cubeArray[2, 1, 2];
                    cubeArray[2, 1, 2] = originalCenterNum;
                    int originalOuterNum = cubeArray[0, 0, 2];
                    cubeArray[0, 0, 2] = cubeArray[0, 2, 2];
                    cubeArray[0, 2, 2] = cubeArray[2, 2, 2];
                    cubeArray[2, 2, 2] = cubeArray[2, 0, 2];
                    cubeArray[2, 0, 2] = originalOuterNum;
                }
                else
                {
                    int originalCenterNum = cubeArray[1, 0, 2];
                    cubeArray[1, 0, 2] = cubeArray[2, 1, 2];
                    cubeArray[2, 1, 2] = cubeArray[1, 2, 2];
                    cubeArray[1, 2, 2] = cubeArray[0, 1, 2];
                    cubeArray[0, 1, 2] = originalCenterNum;
                    int originalOuterNum = cubeArray[0, 0, 2];
                    cubeArray[0, 0, 2] = cubeArray[2, 0, 2];
                    cubeArray[2, 0, 2] = cubeArray[2, 2, 2];
                    cubeArray[2, 2, 2] = cubeArray[0, 2, 2];
                    cubeArray[0, 2, 2] = originalOuterNum;
                }
            }
            else if (side == Vector3.Up)
            {
                if (isClockWise)
                {
                    int originalCenterNum = cubeArray[1, 2, 0];
                    cubeArray[1, 2, 0] = cubeArray[2, 2, 1];
                    cubeArray[2, 2, 1] = cubeArray[1, 2, 2];
                    cubeArray[1, 2, 2] = cubeArray[0, 2, 1];
                    cubeArray[0, 2, 1] = originalCenterNum;
                    int originalOuterNum = cubeArray[0, 2, 0];
                    cubeArray[0, 2, 0] = cubeArray[2, 2, 0];
                    cubeArray[2, 2, 0] = cubeArray[2, 2, 2];
                    cubeArray[2, 2, 2] = cubeArray[0, 2, 2];
                    cubeArray[0, 2, 2] = originalOuterNum;
                }
                else
                {
                    int originalCenterNum = cubeArray[1, 2, 0];
                    cubeArray[1, 2, 0] = cubeArray[0, 2, 1];
                    cubeArray[0, 2, 1] = cubeArray[1, 2, 2];
                    cubeArray[1, 2, 2] = cubeArray[2, 2, 1];
                    cubeArray[2, 2, 1] = originalCenterNum;
                    int originalOuterNum = cubeArray[0, 2, 0];
                    cubeArray[0, 2, 0] = cubeArray[0, 2, 2];
                    cubeArray[0, 2, 2] = cubeArray[2, 2, 2];
                    cubeArray[2, 2, 2] = cubeArray[2, 2, 0];
                    cubeArray[2, 2, 0] = originalOuterNum;
                }
            }
            else if (side == Vector3.Down)
            {
                if (isClockWise)
                {
                    int originalCenterNum = cubeArray[1, 0, 0];
                    cubeArray[1, 0, 0] = cubeArray[2, 0, 1];
                    cubeArray[2, 0, 1] = cubeArray[1, 0, 2];
                    cubeArray[1, 0, 2] = cubeArray[0, 0, 1];
                    cubeArray[0, 0, 1] = originalCenterNum;
                    int originalOuterNum = cubeArray[0, 0, 0];
                    cubeArray[0, 0, 0] = cubeArray[2, 0, 0];
                    cubeArray[2, 0, 0] = cubeArray[2, 0, 2];
                    cubeArray[2, 0, 2] = cubeArray[0, 0, 2];
                    cubeArray[0, 0, 2] = originalOuterNum;
                }
                else
                {
                    int originalCenterNum = cubeArray[1, 0, 0];
                    cubeArray[1, 0, 0] = cubeArray[0, 0, 1];
                    cubeArray[0, 0, 1] = cubeArray[1, 0, 2];
                    cubeArray[1, 0, 2] = cubeArray[2, 0, 1];
                    cubeArray[2, 0, 1] = originalCenterNum;
                    int originalOuterNum = cubeArray[0, 0, 0];
                    cubeArray[0, 0, 0] = cubeArray[0, 0, 2];
                    cubeArray[0, 0, 2] = cubeArray[2, 0, 2];
                    cubeArray[2, 0, 2] = cubeArray[2, 0, 0];
                    cubeArray[2, 0, 0] = originalOuterNum;
                }
            }
            else if (side == Vector3.Left)
            {
                if (isClockWise)
                {
                    int originalCenterNum = cubeArray[0, 1, 0];
                    cubeArray[0, 1, 0] = cubeArray[0, 2, 1];
                    cubeArray[0, 2, 1] = cubeArray[0, 1, 2];
                    cubeArray[0, 1, 2] = cubeArray[0, 0, 1];
                    cubeArray[0, 0, 1] = originalCenterNum;
                    int originalOuterNum = cubeArray[0, 0, 0];
                    cubeArray[0, 0, 0] = cubeArray[0, 2, 0];
                    cubeArray[0, 2, 0] = cubeArray[0, 2, 2];
                    cubeArray[0, 2, 2] = cubeArray[0, 0, 2];
                    cubeArray[0, 0, 2] = originalOuterNum;
                }
                else
                {
                    int originalCenterNum = cubeArray[0, 1, 0];
                    cubeArray[0, 1, 0] = cubeArray[0, 0, 1];
                    cubeArray[0, 0, 1] = cubeArray[0, 1, 2];
                    cubeArray[0, 1, 2] = cubeArray[0, 2, 1];
                    cubeArray[0, 2, 1] = originalCenterNum;
                    int originalOuterNum = cubeArray[0, 0, 0];
                    cubeArray[0, 0, 0] = cubeArray[0, 0, 2];
                    cubeArray[0, 0, 2] = cubeArray[0, 2, 2];
                    cubeArray[0, 2, 2] = cubeArray[0, 2, 0];
                    cubeArray[0, 2, 0] = originalOuterNum;
                }
            }
            else if (side == Vector3.Right)
            {
                if (isClockWise)
                {
                    int originalCenterNum = cubeArray[2, 1, 0];
                    cubeArray[2, 1, 0] = cubeArray[2, 0, 1];
                    cubeArray[2, 0, 1] = cubeArray[2, 1, 2];
                    cubeArray[2, 1, 2] = cubeArray[2, 2, 1];
                    cubeArray[2, 2, 1] = originalCenterNum;
                    int originalOuterNum = cubeArray[2, 0, 0];
                    cubeArray[2, 0, 0] = cubeArray[2, 0, 2];
                    cubeArray[2, 0, 2] = cubeArray[2, 2, 2];
                    cubeArray[2, 2, 2] = cubeArray[2, 2, 0];
                    cubeArray[2, 2, 0] = originalOuterNum;
                }
                else
                {
                    int originalCenterNum = cubeArray[2, 1, 0];
                    cubeArray[2, 1, 0] = cubeArray[2, 2, 1];
                    cubeArray[2, 2, 1] = cubeArray[2, 1, 2];
                    cubeArray[2, 1, 2] = cubeArray[2, 0, 1];
                    cubeArray[2, 0, 1] = originalCenterNum;
                    int originalOuterNum = cubeArray[2, 0, 0];
                    cubeArray[2, 0, 0] = cubeArray[2, 2, 0];
                    cubeArray[2, 2, 0] = cubeArray[2, 2, 2];
                    cubeArray[2, 2, 2] = cubeArray[2, 0, 2];
                    cubeArray[2, 0, 2] = originalOuterNum;
                }
            }
        }

        private static void FindLimitsX(ref Vector3 side, ref int x, ref int limitX)
        {
            if (side.X == 0)
            {
                x = 0; limitX = 3;
            }
            if (side.X < 0)
            {
                x = 0; limitX = 1;
            }
            if (side.X > 0)
            {
                x = 2; limitX = 3;
            }
        }
        private static void FindLimitsY(ref Vector3 side, ref int y, ref int limitY)
        {
            if (side.Y == 0)
            {
                y = 0; limitY = 3;
            }
            if (side.Y < 0)
            {
                y = 0; limitY = 1;
            }
            if (side.Y > 0)
            {
                y = 2; limitY = 3;
            }
        }
        private static void FindLimitsZ(ref Vector3 side, ref int z, ref int limitZ)
        {
            if (side.Z == 0)
            {
                z = 0; limitZ = 3;
            }
            if (side.Z < 0)
            {
                z = 2; limitZ = 3;
            }
            if (side.Z > 0)
            {
                z = 0; limitZ = 1;
            }
        }

        //white
        public const int WHITE_TOP_LEFT = 12;
        public const int WHITE_TOP_MID = 16;
        public const int WHITE_TOP_RIGHT = 4;
        public const int WHITE_MID_LEFT = 0;
        public const int WHITE_MID_MID = 13;
        public const int WHITE_MID_RIGHT = 2;
        public const int WHITE_BOTTOM_LEFT = 19;
        public const int WHITE_BOTTOM_MID = 5;
        public const int WHITE_BOTTOM_RIGHT = 14;
        //BLUE
        public const int BLUE_TOP_LEFT = 12;
        public const int BLUE_TOP_MID = 0;
        public const int BLUE_TOP_RIGHT = 19;
        public const int BLUE_MID_LEFT = 9;
        public const int BLUE_MID_MID = 3;
        public const int BLUE_MID_RIGHT = 24;
        public const int BLUE_BOTTOM_LEFT = 23;
        public const int BLUE_BOTTOM_MID = 11;
        public const int BLUE_BOTTOM_RIGHT = 22;
        //RED
        public const int RED_TOP_LEFT = 23;
        public const int RED_TOP_MID = 20;
        public const int RED_TOP_RIGHT = 17;
        public const int RED_MID_LEFT = 9;
        public const int RED_MID_MID = 25;
        public const int RED_MID_RIGHT = 7;
        public const int RED_BOTTOM_LEFT = 12;
        public const int RED_BOTTOM_MID = 16;
        public const int RED_BOTTOM_RIGHT = 4;
        //ORANGE
        public const int ORANGE_TOP_LEFT = 19;
        public const int ORANGE_TOP_MID = 5;
        public const int ORANGE_TOP_RIGHT = 14;
        public const int ORANGE_MID_LEFT = 24;
        public const int ORANGE_MID_MID = 10;
        public const int ORANGE_MID_RIGHT = 15;
        public const int ORANGE_BOTTOM_LEFT = 22;
        public const int ORANGE_BOTTOM_MID = 8;
        public const int ORANGE_BOTTOM_RIGHT = 6;
        //GREEN
        public const int GREEN_TOP_LEFT = 4;
        public const int GREEN_TOP_MID = 2;
        public const int GREEN_TOP_RIGHT = 14;
        public const int GREEN_MID_LEFT = 15;
        public const int GREEN_MID_MID = 1;
        public const int GREEN_MID_RIGHT = 7;
        public const int GREEN_BOTTOM_LEFT = 6;
        public const int GREEN_BOTTOM_MID = 18;
        public const int GREEN_BOTTOM_RIGHT = 17;
        //YELLOW
        public const int YELLOW_TOP_LEFT = 22;
        public const int YELLOW_TOP_MID = 8;
        public const int YELLOW_TOP_RIGHT = 6;
        public const int YELLOW_MID_LEFT = 11;
        public const int YELLOW_MID_MID = 21;
        public const int YELLOW_MID_RIGHT = 18;
        public const int YELLOW_BOTTOM_LEFT = 23;
        public const int YELLOW_BOTTOM_MID = 20;
        public const int YELLOW_BOTTOM_RIGHT = 17;
    }
}
