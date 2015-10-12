using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
//Made by Zach Dubuc
namespace ATaleOfTwoHorns
{
    class BackgroundMusic
    {
        static ContentManager m_Content;
        static Song m_Song;

        public static void loadContent(ContentManager content)
        {
            m_Content = content;
        }

        public static void playSong(int levelCount, float volume)
        {
            if (levelCount == 0)
            {
                m_Song = m_Content.Load<Song>("LevelOne-Music");
                MediaPlayer.Volume = volume;
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Play(m_Song);
            }
            if (levelCount == 1)
            {
                m_Song = m_Content.Load<Song>("LevelTwo-Music");
                MediaPlayer.Volume = volume;
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Play(m_Song);
            }
            if (levelCount == 2)
            {
                m_Song = m_Content.Load<Song>("LevelThree-Music");
                MediaPlayer.Volume = volume;
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Play(m_Song);
            }
            if (levelCount == 3)
            {
                m_Song = m_Content.Load<Song>("Boss-Music");
                MediaPlayer.Volume = volume;
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Play(m_Song);
            } 
        }
    }
}
