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
    class BunnyManager : BaseManager
    {

        ContentManager Content;
        List<Bunnies> m_bunnyList = new List<Bunnies>();
        Rectangle m_Destination = new Rectangle(0, 0, 30, 24);
        Rectangle m_SourceRectangle = new Rectangle(0, 0, 30, 24);

        public BunnyManager()
        {

        }
        public void addEnemy(Vector2 location)
        {
            m_bunnyList.Add(new Bunnies(location, m_SourceRectangle, m_Destination));

        }
      
        public void clear()
        {
            m_bunnyList.Clear();
        }
        public void setBunny(Vector2 position, Vector2 direction)
        {
            foreach (Bunnies butterfly in m_bunnyList)
            {
                if (butterfly.IsActive == false)
                {
                    return;
                }
            }
        }
        public void update(GameTime gameTime)
        {
            for (int i = 0; i < m_bunnyList.Count; i++)
            {
                m_bunnyList[i].update(gameTime);
            }
        }
        public void loadContent(ContentManager content)
        {
            for (int i = 0; i < m_bunnyList.Count; i++)
            {
                m_bunnyList[i].loadContent("BunniesSprites", content);
            }
        }
        public void draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < m_bunnyList.Count; i++)
            {
                m_bunnyList[i].draw(spriteBatch);
            }
        }
        public bool collisionCheck(Rectangle playerRect)
        {
            return false;
        }


    }
}
