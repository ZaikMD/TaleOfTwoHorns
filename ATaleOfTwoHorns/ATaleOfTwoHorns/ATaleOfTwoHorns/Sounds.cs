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
    class Sounds
    {
        public static SoundEffect effect;
        static SoundEffectInstance m_EffectInstance;
        static ContentManager m_Content;
        static int m_PlayCounter = 0;

        public void sounds()
        {

        }

        public static void loadContent(ContentManager content)
        {
            m_Content = content;
        }

        public static void playSound(string soundName, float volume)
        {
            effect = m_Content.Load<SoundEffect>(soundName);


            if (m_EffectInstance == null)
            {
                m_EffectInstance = effect.CreateInstance();
                effect.Play(volume, 0, 0);
            }
            else
            {
                effect.Play(volume,0,0);
            }

                m_EffectInstance = null;
                

        }

        



    }
}
