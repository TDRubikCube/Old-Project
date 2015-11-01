#region Includes
using System;
using System.Collections.Generic;
using System.Text;
using MbUnit.Framework;
using RubikCube;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;
#endregion

///////////////////////////////////////////////////////////////////////////////
// Copyright 2015 (c) by Rubik Cube All Rights Reserved.
//  
// Project:      
// Module:       CubeStateTest.cs
// Description:  Tests for the Cube State class in the Rubik% 2 7s Cube assembly.
//  
// Date:       Author:           Comments:
// 27/01/2015 20:38  User     Module created.
///////////////////////////////////////////////////////////////////////////////
namespace RubikCubeTest
{

    /// <summary>
    /// Tests for the Cube State Class
    /// Documentation: 
    /// </summary>
    [TestFixture, Description("Tests for Cube State")]
    public class CubeStateTest
    {
        #region Class Variables
        private CubeState _cubeState;
        #endregion

        #region Setup/Teardown

        /// <summary>
        /// Code that is run before each test
        /// </summary>
        [SetUp]
        public void Initialize()
        {
            //New instance of Cube State
            _cubeState = new CubeState();
        }

        /// <summary>
        /// Code that is run after each test
        /// </summary>
        [TearDown]
        public void Cleanup()
        {
            //TODO:  Put dispose in here for _cubeState or delete this line
        }
        #endregion

        #region Property Tests

        #region GeneratedProperties

        // No public properties were found. No tests are generated for non-public scoped properties.

        #endregion // End of GeneratedProperties

        #endregion

        #region Method Tests

        #region GeneratedMethods

        /// <summary>
        /// Get Cubie Method Test
        /// Documentation   :  
        /// Method Signature:  int GetCubie(int x, int y, int z)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void GetCubieTest()
        {
            DateTime methodStartTime = DateTime.Now;
            int expected = 123;

            //Parameters
            int x = 123;
            int y = 123;
            int z = 123;

            int results = _cubeState.GetCubie(x, y, z);
            Assert.AreEqual(expected, results, "RubikCube.CubeState.GetCubie method test failed");

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("RubikCube.CubeState.GetCubie Time Elapsed: {0}", methodDuration));
        }

        /// <summary>
        /// Find Cubies On Side Method Test
        /// Documentation   :  
        /// Method Signature:  int[] FindCubiesOnSide(Vector3 side)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void FindCubiesOnSideTest()
        {
            DateTime methodStartTime = DateTime.Now;
            int[] expected = new int[20];

            //Parameters
            Vector3 side = new Vector3();

            int[] results = _cubeState.FindCubiesOnSide(side);
            Assert.AreEqual(expected, results, "RubikCube.CubeState.FindCubiesOnSide method test failed");

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("RubikCube.CubeState.FindCubiesOnSide Time Elapsed: {0}", methodDuration));
        }

        /// <summary>
        /// Rotate Method Test
        /// Documentation   :  
        /// Method Signature:  void Rotate(Vector3 side)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void RotateTest()
        {
            DateTime methodStartTime = DateTime.Now;

            //Parameters
            Vector3 side = new Vector3();

            _cubeState.Rotate(side);

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("RubikCube.CubeState.Rotate Time Elapsed: {0}", methodDuration));
        }

        #endregion // End of GeneratedMethods

        #endregion

    }
}
