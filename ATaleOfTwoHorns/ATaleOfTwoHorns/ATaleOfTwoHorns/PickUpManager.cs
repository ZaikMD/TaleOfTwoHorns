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
    class PickUpManager
    {

        private static BaseManager[] m_PickUpManagers = new BaseManager[3];

        public PickUpManager()
        {
            m_PickUpManagers[0] = new HeartManager();
            m_PickUpManagers[1] = new KeyManager();
            m_PickUpManagers[2] = new StarManager();
        }

        public static void addEnemy(Vector2 location, PickUpType pickUpType)
        {
            m_PickUpManagers[(int)pickUpType].addEnemy(location);
        }

        public static  void loadContent(ContentManager Content)
        {
            foreach (BaseManager manager in m_PickUpManagers)
            {
                manager.loadContent(Content);
            }
        }

        public void update(GameTime gameTime)
        {
            foreach (BaseManager manager in m_PickUpManagers)
            {
                manager.update(gameTime);
            }
        }

        public static void clear()
        {
            for (int i = 0; i < m_PickUpManagers.Length; i++)
            {
                m_PickUpManagers[i].clear();
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            foreach (BaseManager manager in m_PickUpManagers)
            {
                manager.draw(spriteBatch);
            }

        }
             

    }
}
