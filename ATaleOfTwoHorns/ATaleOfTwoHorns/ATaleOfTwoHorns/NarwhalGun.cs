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
    class NarwhalGun
    {
        NarwhalBullet[] m_Narwhals = new NarwhalBullet[100];

        int m_AngleRangeThing = 30;

        Random m_Random = new Random();

        public NarwhalGun()
        {
            for (int i = 0; i < m_Narwhals.Length; i++)
            {
                m_Narwhals[i] = new NarwhalBullet();
            }
        }

        public void loadContent(ContentManager content)
        {
            for (int i = 0; i < m_Narwhals.Length; i++)
            {
                m_Narwhals[i].loadContent(content);
            }
        }

        public void update(GameTime gameTime)
        {
            for (int i = 0; i < m_Narwhals.Length; i++)
            {
                m_Narwhals[i].update(gameTime);
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < m_Narwhals.Length; i++)
            {
                m_Narwhals[i].draw(spriteBatch);
            }
        }

        public void NARWHAL(Vector2 startPos)
        {
            for (int i = 0; i < m_Narwhals.Length; i++)
            {
                if (m_Narwhals[i].IsActive == false)
                {
                    m_Narwhals[i].activate(startPos, new Vector2(-50, m_Random.Next(m_AngleRangeThing) - m_AngleRangeThing * 0.85f));
                    break;
                }
            }         
        }
        public void clear()
        {
            for (int i = 0; i < m_Narwhals.Length; i++)
            {
                m_Narwhals[i].IsActive = false;
            }
        }
    }
}
