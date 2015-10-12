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
    class PixieManager : BaseManager
    {
        List<Pixie> m_pixieList = new List<Pixie>();

        Rectangle m_Destination = new Rectangle(0, 0, 64, 64);
        Rectangle m_SourceRect = new Rectangle(0, 0, 64, 64);

        public PixieManager()
        {

        }

        public void addEnemy(Vector2 location)
        {
            m_pixieList.Add(new Pixie(location, m_SourceRect, m_Destination));
        }

        public void clear()
        {
            m_pixieList.Clear();
        }

        public void ActivatePixie(Vector2 position, Vector2 direction)
        {
            foreach (Pixie pixie in m_pixieList)
            {
                if (pixie.IsActive == false)
                {
                    pixie.activate(position, direction);

                    return;
                }
            }
        }

        public void update(GameTime gameTime)
        {
            for (int i = 0; i < m_pixieList.Count; i++)
            {
                m_pixieList[i].update(gameTime);

                if (LaserManager.collisionCheck(m_pixieList[i].Destination))
                {

                    m_pixieList[i].takeDamage();

                    if (m_pixieList[i].currentHealth == 0)
                    {
                        m_pixieList[i].pixieState = PixieState.DYING;
                    }
                }

                if (m_pixieList[i].pixieState == PixieState.DYING && m_pixieList[i].getTimer >= m_pixieList[i].getPoof)
                {
                    m_pixieList.RemoveAt(i);
                }
            }
        }

        public void loadContent(ContentManager content)
        {
            foreach (Pixie pixie in m_pixieList)
            {
                pixie.loadContent("Pixie", content);
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            foreach (Pixie pixie in m_pixieList)
            {
                pixie.draw(spriteBatch);
            }
        }

        public bool collisionCheck(Rectangle playerRect)
        {
            for (int i = 0; i < m_pixieList.Count; i++)
            {
                if (CollisionCheck.collisionCheck(m_pixieList[i].Destination, playerRect))
                {
                    return true;
                }
            }
            return false;
        }
    }    
}
