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
    class SpiderManager : BaseManager
    {
        //Spider[] m_spiderList;

        List<Spider> m_spiderList = new List<Spider>();

        Rectangle m_Destination = new Rectangle(0, 0, 30, 30);
        Rectangle m_SourceRect = new Rectangle(0, 0, 60, 60);

        public SpiderManager()
        {

        }

        public void addEnemy(Vector2 location)
        {
            m_spiderList.Add(new Spider(location, m_SourceRect, m_Destination));
        }

        public void clear()
        {
            m_spiderList.Clear();
        }

        public void ActivateSpider(Vector2 position, Vector2 direction)
        {
            foreach (Spider spider in m_spiderList)
            {
                if (spider.IsActive == false)
                {
                    //TODO: this is useless

                    return;
                }
            }
        }

        public void update(GameTime gameTime)
        {
            for (int i = 0; i < m_spiderList.Count; i++)
            {
                m_spiderList[i].update(gameTime);

                if (LaserManager.collisionCheck(m_spiderList[i].Destination))
                {
                    m_spiderList[i].spiderState = SpiderState.DYING;
                }

                if (m_spiderList[i].spiderState == SpiderState.DYING && m_spiderList[i].spiderAggroState == SpiderAggroState.RETREATING)
                {
                    m_spiderList.RemoveAt(i);
                }
            }
        }

        public void loadContent(ContentManager content)
        {
            foreach (Spider spider in m_spiderList)
            {
                spider.loadContent("Spider_Strip", content);
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            foreach (Spider spider in m_spiderList)
            {
                spider.draw(spriteBatch);
            }
        }

        public bool collisionCheck(Rectangle playerRect)
        {
            for (int i = 0; i < m_spiderList.Count; i++)
            {
                if (CollisionCheck.collisionCheck(m_spiderList[i].Destination, playerRect))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
