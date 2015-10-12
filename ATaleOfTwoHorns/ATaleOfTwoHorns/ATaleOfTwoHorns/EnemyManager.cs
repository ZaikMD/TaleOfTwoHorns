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
    class EnemyManager
    {
        private static BaseManager[] m_Managers= new BaseManager[7];
        
        public EnemyManager()
        {
            m_Managers[0] = new BeesManager();
            m_Managers[1] = new SpiderManager();
            m_Managers[2] = new PixieManager();
            m_Managers[3] = new ImpManager();
            m_Managers[4] = new UndeadUnicornManager();
            m_Managers[5] = new BunnyManager();
            m_Managers[6] = new ButterflyManager();

           /* addEnemy(new Vector2(10, 600), EnemyType.BEES);
            addEnemy(new Vector2(10, 400), EnemyType.SPIDER);
            addEnemy(new Vector2(40, 400), EnemyType.SPIDER);
            addEnemy(new Vector2(10, 600), EnemyType.IMP);
            addEnemy(new Vector2(10, 600), EnemyType.PIXIE); */
          //  addEnemy(new Vector2(10, 600), EnemyType.UNDEADUNICORN);


            
        }

        public static void clear()
        {
            for (int i = 0; i < m_Managers.Length; i++)
            {
                m_Managers[i].clear();
            }
        }

        public static void addEnemy(Vector2 location, EnemyType enemyType)
        {
            m_Managers[(int)enemyType].addEnemy(location);
        }

        public void ActivateEnemies(Vector2 position, Vector2 direction)
        {
            foreach (BaseManager baseManager in m_Managers)
            {

            }
        }

        public static void loadContent(ContentManager content)
        {
            foreach (BaseManager baseManager in m_Managers)
            {
                baseManager.loadContent(content);
            }
        }

        public void update(GameTime gameTime)
        {
            foreach (BaseManager baseManager in m_Managers)
            {
                baseManager.update(gameTime);
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            foreach (BaseManager baseManager in m_Managers)
            {
                baseManager.draw(spriteBatch);
            }
        }

        public static bool collisionCheck(Rectangle playerRect)
        {
            for (int i = 0; i < m_Managers.Length; i++)
            {
                if(m_Managers[i].collisionCheck(playerRect) == true)
                {
                    return true;
                }
                //TODO: call indevifual checks here
            }
            return false;
        }


    }
}
