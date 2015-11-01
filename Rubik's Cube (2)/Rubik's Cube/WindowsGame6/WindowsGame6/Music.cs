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
    class Music
    {
        ButtonSetUp button;
        //public Song[] classic;
        //public Song[] rock;
        private Song defaultSong;
        public bool isMuted = false;
        public int musicIndexClassic = 0;
        public int musicIndexRock = 0;
        public string whichGenre;
        //private bool isFirstTimeRock = true;
        //private bool isFirstTimeClassic = true;

        public Music(GraphicsDeviceManager graphics, GraphicsDevice graphicsDevice, ContentManager Content)
        {
            whichGenre = "default";
            defaultSong = Content.Load<Song>("music/bg-music");
            //MediaPlayer.Play(defaultSong);
            button = new ButtonSetUp(graphics, graphicsDevice, Content);
        }

        public void Update(MouseState mouseState, string whichGenre, bool justSwitched)
        {
            ChangeSongs(whichGenre, justSwitched);
        }

        private void ChangeSongs(string whichGenre, bool justSwitched)
        {
            if (MediaPlayer.State != MediaState.Playing && MediaPlayer.State != MediaState.Paused)
            {
                //if (whichGenre == "classic")
                //{
                //    isFirstTimeClassic = true;
                //    if (isFirstTimeClassic && !justSwitched)
                //        musicIndexClassic++;
                //    if (musicIndexClassic >= 6) musicIndexClassic = 0;
                //    MediaPlayer.Play(classic[musicIndexClassic]);
                //    isFirstTimeRock = false;
                //}
                //else if (whichGenre == "rock")
                //{
                //    isFirstTimeRock = true;
                //    if (isFirstTimeRock && !justSwitched)
                //        musicIndexRock++;
                //    if (musicIndexRock >= 6) musicIndexRock = 0;
                //    MediaPlayer.Play(rock[musicIndexRock]);
                //    isFirstTimeClassic = false;
                //}
                if (whichGenre == "default")
                {
                    MediaPlayer.Play(defaultSong);
                }
            }
        }
    }
}
