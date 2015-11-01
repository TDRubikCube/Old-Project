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

namespace WindowsGame6
{
    /// <cube legend>
    /// 0 = white + blue                   //   white = 12 16 4
    /// 1 = green                          //           0  13 2
    /// 2 = white + green                  //           19 5  14
    /// 3 = blue                           //   
    /// 4 = white + red + green            //   red =   23 20 17
    /// 5 = white + orange                 //           9  25 7
    /// 6 = yellow + green + orange        //           12 16 4
    /// 7 = green + red                    //  
    /// 8 = yellow + orange                //   yellow =22 8  6
    /// 9 = red + blue                     //           11 21 18
    /// 10 = orange                        //           23 20 17
    /// 11 = blue + yellow                 //
    /// 12 = white + blue + red            //   orange =19 5  14 
    /// 13 = white                         //           24 10 15
    /// 14 = white + green + orange        //           22 8  6
    /// 15 = orange + green                //
    /// 16 = white + red                   //   blue =  12 0  19
    /// 17 = yellow + red + green          //           9  3  24
    /// 18 = yellow + green                //           23 11 22
    /// 19 = white + blue + orange         //
    /// 20 = yellow + red                  //   green = 4  2  14
    /// 21 = yellow                        //           15 1  7
    /// 22 = blue + yellow + orange        //           6  18 17
    /// 23 = red + blue + yellow           //
    /// 24 = blue + orange                 //
    /// 25 = red                           //
    /// </cube legend>
    class TurnCube
    {
        int counter = 0;
        public void TurnUp(Matrix[] cubeMeshes, KeyboardState keyboardState, KeyboardState oldKeyboardState)
        {

            if (keyboardState.IsKeyDown(Keys.Up) && oldKeyboardState.IsKeyUp(Keys.Up))
            {    
                Debug.WriteLine(counter);
                counter++;            
                float rotateAgain = MathHelper.PiOver2 + counter * MathHelper.PiOver2;
                if (counter == 4) counter = 0;
                Vector3 firstTurn = new Vector3(0, 0, 0);
                Vector3 secondTurn = new Vector3(0, 0, 0);
                if (counter < 2) { firstTurn = new Vector3(0, -2.85f, -4.7f); if (counter == 0) secondTurn = new Vector3(0, 2, 0); }
                if (counter == 1) secondTurn = new Vector3(0, 6.7f, -0.85f);
                if (counter == 2) firstTurn = new Vector3(0, -6.7f, 2.8f);
                Matrix rotate = Matrix.CreateTranslation(firstTurn) * Matrix.CreateRotationX(rotateAgain) * Matrix.CreateTranslation(secondTurn);
                cubeMeshes[12] = rotate;
                cubeMeshes[16] = rotate;
                cubeMeshes[4] = rotate;
                cubeMeshes[0] = rotate;
                cubeMeshes[13] = rotate;
                cubeMeshes[2] = rotate;
                cubeMeshes[19] = rotate;
                cubeMeshes[5] = rotate;
                cubeMeshes[14] = rotate;
            }
            if (keyboardState.IsKeyDown(Keys.Down) && oldKeyboardState.IsKeyUp(Keys.Down))
            {
                Debug.WriteLine(counter);
                float rotateAgain = -MathHelper.PiOver2 - counter * MathHelper.PiOver2;
                if (counter == 4) counter = 0;
                Vector3 firstTurn = new Vector3(0, 0, 0);
                Vector3 secondTurn = new Vector3(0, 0, 0);
                if (counter < 2) { firstTurn = new Vector3(0, -2.85f, -4.7f); if (counter == 0) secondTurn = new Vector3(0, 7.5f, 3.85f); }
                if (counter == 1) secondTurn = new Vector3(0, 6.7f, -0.85f);
                if (counter == 2) firstTurn = new Vector3(0, -2.7f, -6.6f);
                Matrix rotate = Matrix.CreateTranslation(firstTurn) * Matrix.CreateRotationX(rotateAgain) * Matrix.CreateTranslation(secondTurn);
                cubeMeshes[12] = rotate;
                cubeMeshes[16] = rotate;
                cubeMeshes[4] = rotate;
                cubeMeshes[0] = rotate;
                cubeMeshes[13] = rotate;
                cubeMeshes[2] = rotate;
                cubeMeshes[19] = rotate;
                cubeMeshes[5] = rotate;
                cubeMeshes[14] = rotate;
                counter++;
            }
        }
    }
}
