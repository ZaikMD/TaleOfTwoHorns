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
    class BeesManager : BaseManager
    {
        //Bees[] m_beesArray = new Bees[0];
        List<Bees> m_beesList = new List<Bees>();

        Rectangle m_Destination = new Rectangle(0, 0, 32, 32);
        Rectangle m_SourceRect = new Rectangle(0, 0, 32, 32);

        public BeesManager()
        {

        }

        public void addEnemy(Vector2 location)
        {
            m_beesList.Add(new Bees(location, m_SourceRect, m_Destination));

        }

        public void clear()
        {
            m_beesList.Clear();
        }

        public void ActivateBees(Vector2 position, Vector2 direction)
        {
            foreach (Bees bees in m_beesList)
            {
                if (bees.IsActive == false)
                {
                    bees.activate(position, direction);

                    return;
                }
            }
        }

        public void update(GameTime gameTime)
        {

            for (int i = 0; i < m_beesList.Count; i++)
            {
                m_beesList[i].update(gameTime);

                if (LaserManager.collisionCheck(m_beesList[i].Destination))
                {
                    m_beesList.RemoveAt(i);
                }
            }
        }

        public void loadContent(ContentManager content)
        {
            foreach (Bees bees in m_beesList)
            {
                bees.loadContent("BeeSheet", content);
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            foreach (Bees bees in m_beesList)
            {
                bees.draw(spriteBatch);
            }
        }

        public bool collisionCheck(Rectangle playerRect)
        {
            for (int i = 0; i < m_beesList.Count; i++)
            {
                if (CollisionCheck.collisionCheck(m_beesList[i].Destination, playerRect))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
