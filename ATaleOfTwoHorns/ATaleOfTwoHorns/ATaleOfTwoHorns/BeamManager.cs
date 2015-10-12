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



namespace ATaleOfTwoHorns
{
    class BeamManager
    {
        SinusoidalBeam[] m_Beams = new SinusoidalBeam[5];

        float m_SpawnDelay = 0.5f;
        float m_Timer = 0.0f;

        public BeamManager(Rectangle levelBoundries, Color color1, Color color2)
        {
            for (int i = 0; i < m_Beams.Length; i++)
            {
                m_Beams[i] = new SinusoidalBeam(levelBoundries, new Color[] {color1, color2});
            }
        }

        public void loadContent(ContentManager contentmanager)
        {
            for (int i = 0; i < m_Beams.Length; i++)
            {
                m_Beams[i].loadContent(contentmanager);
            }
        }


        public void update(GameTime gameTime)
        {
            m_Timer += (float)gameTime.ElapsedGameTime.Milliseconds / 1000;

            for (int i = 0; i < m_Beams.Length; i++)
            {
                m_Beams[i].update(gameTime);
            }
        }

        public void addBeam(Vector2 start, Vector2 target)
        {            
            if (m_Timer > m_SpawnDelay)
            {
                m_Timer = 0.0f;
                for (int i = 0; i < m_Beams.Length; i++)
                {
                    if (m_Beams[i].IsAvtive == false)
                    {
                        m_Beams[i].IsAvtive = true;
                        m_Beams[i].fire(start, target);
                        break;
                    }
                }
            }            
        }

        public void draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < m_Beams.Length; i++)
            {
                m_Beams[i].draw(spriteBatch); 
            }
        }

        public void reset()
        {
            for (int i = 0; i < m_Beams.Length; i++)
            {
                m_Beams[i].IsAvtive = false;
            }
        }
    }
}
