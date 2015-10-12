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
    class ButterflyManager : BaseManager
    {
        ContentManager Content;
        List<Butterfly> m_butterflyList = new List<Butterfly>();
        Rectangle m_Destination = new Rectangle(0, 0, 11, 12);
        Rectangle m_SourceRectangle = new Rectangle(0, 0, 22, 23);

        public ButterflyManager()
        {

        }

        public void addEnemy(Vector2 location)
        {
            m_butterflyList.Add(new Butterfly(location, m_SourceRectangle, m_Destination));

        }
        public void addButterflyPosition(Vector2 location)
        {
            m_butterflyList.Add(new Butterfly(location, m_SourceRectangle, m_Destination));
        }

        public void clear()
        {
            m_butterflyList.Clear();
        }


        public void setButterfly(Vector2 position, Vector2 direction)
        {
            foreach (Butterfly butterfly in m_butterflyList)
            {
                if (butterfly.IsActive == false)
                {
                    return;
                }
            }
        }
        public void update(GameTime gameTime)
        {
            for (int i = 0; i < m_butterflyList.Count; i++)
            {
                m_butterflyList[i].update(gameTime);
            }
        }
        public void loadContent(ContentManager content)
        {
            for (int i = 0; i < m_butterflyList.Count; i++)
            {
                m_butterflyList[i].loadContent("ButterflyAllsides", content);
            }
        }
        public void draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < m_butterflyList.Count; i++)
            {
                m_butterflyList[i].draw(spriteBatch);
            }
        }
        public bool collisionCheck(Rectangle playerRect)
        {
            return false;
        }
    }
}



