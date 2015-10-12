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

//created by Joe Burchill

namespace ATaleOfTwoHorns
{
    class LaserManager
    {
        #region Fields
        private static Laser[] m_LaserArray;

        float m_delay;
        float timer = 0.0f;

        Rectangle m_LevelBoundries;
        #endregion

        #region Constructor
        public LaserManager(Color color, int numberOfProjectiles,float delay, Rectangle levelBoundries)
        {
            m_delay = delay;

            m_LaserArray = new Laser[numberOfProjectiles];
            for (int i = 0; i < m_LaserArray.Length; i++)
            {
                m_LaserArray[i] = new Laser(new Vector2(),new Vector2(1,1),color, levelBoundries);
                m_LaserArray[i].IsActive = false;
            }
        }
        #endregion

        #region Methods
        public void activateProjectile(Vector2 position, Vector2 direction)
        {
            if (timer > m_delay)
            {
                timer = 0.0f;
                foreach (Laser laser in m_LaserArray)
                {
                    if (!laser.IsActive)
                    {
                        laser.activate(position, direction);
                        return;
                    }
                }
            }
        }

        public void loadContent(ContentManager content)
        {
            foreach (Laser baseProjectile in m_LaserArray)
            {
                baseProjectile.loadContent(content);
            }
        }

        public void update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.Milliseconds / 1000;

            foreach (Laser baseProjectile in m_LaserArray)
            {                
                baseProjectile.update(gameTime);                
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            foreach (Laser baseProjectile in m_LaserArray)
            {                
                baseProjectile.draw(spriteBatch);                
            }
        }

        public static bool collisionCheck(Rectangle target)
        {
            foreach (Laser thing in m_LaserArray)
            {
                if (thing.collisionCheck(target))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}
