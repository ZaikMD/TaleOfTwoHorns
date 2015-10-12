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
    class ImpManager : BaseManager
    {
        //Imp[] m_impArray;
        List<Imp> m_impList = new List<Imp>();

        Rectangle m_Destination = new Rectangle(0, 0, 64, 64);
        Rectangle m_SourceRect = new Rectangle(0, 0, 64, 64);


        public ImpManager()
        {

        }

        public void addEnemy(Vector2 location)
        {
            m_impList.Add(new Imp(location, m_SourceRect, m_Destination));
        }

        public void clear()
        {
            m_impList.Clear();
        }

        public void ActivateSpider(Vector2 position, Vector2 direction)
        {
            foreach (Imp imp in m_impList)
            {
                if (imp.IsActive == false)
                {
                    imp.activate(position, direction);

                    return;
                }
            }
        }

        public void update(GameTime gameTime)
        {
            for (int i = 0; i < m_impList.Count; i++)
            {
                m_impList[i].update(gameTime);

                if (LaserManager.collisionCheck(m_impList[i].Destination))
                {

                    m_impList[i].takeDamage();

                    if (m_impList[i].currentHealth == 0)
                    {
                        m_impList[i].impState = ImpState.DYING;
                    }
                }

                if (m_impList[i].impState == ImpState.DYING && m_impList[i].getTimer >= m_impList[i].getEndTimer)
                {
                    m_impList.RemoveAt(i);
                }
            }
        }

        public void loadContent(ContentManager content)
        {
            foreach (Imp imp in m_impList)
            {
                imp.loadContent("ForestImp", content);
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            foreach (Imp imp in m_impList)
            {
                imp.draw(spriteBatch);
            }
        }

        public bool collisionCheck(Rectangle playerRect)
        {
            for (int i = 0; i < m_impList.Count; i++)
            {
                if (CollisionCheck.collisionCheck(m_impList[i].Destination, playerRect))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
