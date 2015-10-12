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
    class UndeadUnicornManager : BaseManager
    {
        //Imp[] m_impArray;
        List<UndeadUnicorn> m_undeadUnicornList = new List<UndeadUnicorn>();

        Rectangle m_Destination = new Rectangle(0, 0, 128, 64);
        Rectangle m_SourceRect = new Rectangle(0, 0, 128, 64);


        public UndeadUnicornManager()
        {

        }

        public void addEnemy(Vector2 location)
        {
            m_undeadUnicornList.Add(new UndeadUnicorn(location, m_SourceRect, m_Destination));
        }

        public void clear()
        {
            m_undeadUnicornList.Clear();
        }

        public void ActivateUndeadUnicorn(Vector2 position, Vector2 direction)
        {
            foreach (UndeadUnicorn undeadUnicorn in m_undeadUnicornList)
            {
                if (undeadUnicorn.IsActive == false)
                {
                    undeadUnicorn.activate(position, direction);

                    return;
                }
            }
        }

        public void update(GameTime gameTime)
        {
            for (int i = 0; i < m_undeadUnicornList.Count; i++)
            {
                m_undeadUnicornList[i].update(gameTime);

                if (LaserManager.collisionCheck(m_undeadUnicornList[i].Destination))
                {

                    m_undeadUnicornList[i].takeDamage();

                    if (m_undeadUnicornList[i].currentHealth == 0)
                    {
                        m_undeadUnicornList[i].undeadUnicornState = UndeadUnicornState.DYING;
                    }
                }


                if (m_undeadUnicornList[i].undeadUnicornState == UndeadUnicornState.DYING && m_undeadUnicornList[i].getTimer >= m_undeadUnicornList[i].getEndTimer)
                {
                    m_undeadUnicornList.RemoveAt(i);
                }
            }
        }

        public void loadContent(ContentManager content)
        {
            foreach (UndeadUnicorn undeadUnicorn in m_undeadUnicornList)
            {
                undeadUnicorn.loadContent("UndeadUnicorn", content);
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            foreach (UndeadUnicorn undeadUnicorn in m_undeadUnicornList)
            {
                undeadUnicorn.draw(spriteBatch);
            }
        }

        public bool collisionCheck(Rectangle playerRect)
        {
            for (int i = 0; i < m_undeadUnicornList.Count; i++)
            {
                if (CollisionCheck.collisionCheck(m_undeadUnicornList[i].Destination, playerRect))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
