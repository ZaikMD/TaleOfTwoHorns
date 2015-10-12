using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//created by Kris Matis

/// <summary>
/// used in conjunction with a sprite sheet
///and a source rectangle to store the positions
///of all the different sprites
/// </summary>
namespace ATaleOfTwoHorns
{
    //very similar to the one from class
    class Animation
    {
        //the frames persecond
        int m_FramesPerSecond = 0;
        //and the array that holds the location of each frame in the animation
        int[] m_Frames = new int[0];

        public int FramesPerSecond
        {
            get { return m_FramesPerSecond; }
        }

        public int[] Frames
        {
            get { return m_Frames; }
            set { m_Frames = value; }
        }

        public Animation(int timePerFrame, int[] frames)
        {
            m_FramesPerSecond = timePerFrame;
            m_Frames = frames;
        }
    }
}
