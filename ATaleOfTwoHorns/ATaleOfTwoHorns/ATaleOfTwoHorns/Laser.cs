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
    class Laser
    {
        #region Fields
        Vector2 m_Position;
        Vector2 m_Direction;
        private bool m_IsActive;
        Particles m_Particles;
        private Rectangle m_CollitionRect = new Rectangle(0,0,5,5);
        float m_Speed = 700;
        Rectangle m_LevelBoundries;
        #endregion

        #region Properties
        public Vector2 Position
        {
            get { return m_Position; }
            set 
            {
                m_Position = value;
                m_Particles.SpawnPoint = m_Position;

                m_CollitionRect.X = (int)(m_Position.X - m_CollitionRect.Width / 2);
                m_CollitionRect.Y = (int)(m_Position.Y - m_CollitionRect.Width / 2);
            }
        }

        public Vector2 Direction
        {
            get { return m_Direction; }
            set { m_Direction = value; }
        }
        
        public bool IsActive
        {
            get { return m_IsActive; }
            set 
            { 
                m_IsActive = value;
                if (value == true)
                {
                    m_Particles.Stop = false;
                }
                else
                {
                    m_Particles.Stop = true;
                }
            }
        }
        #endregion

        #region Constructor
        public Laser(Vector2 position, Vector2 direction, Color color,Rectangle levelBoundries)
        {
            m_Particles = new Particles(position, color, 40.0f, 10, 0.0f, new Vector2(), 10.0f, true, 500, 0, 2.1f, true);
            m_Position = position;
            m_Direction = direction;
            m_LevelBoundries = levelBoundries;
        }
        #endregion

        #region Methods
        public void activate(Vector2 position, Vector2 direction)
        {
            m_IsActive = true;
            m_Position = position;
            m_Direction = direction;
            m_Particles.Stop = false;
            m_Particles.DominantDirection = new Vector2(m_Direction.X * -1, m_Direction.Y * -1);
        }
        public void loadContent(ContentManager content)
        {
            m_Particles.loadContent(content);
        }

        public void update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.Milliseconds / 1000;

            if (IsActive == true)
            {
                Vector2 temp = m_Position;

                m_Direction.Normalize();

                temp.X += m_Direction.X * m_Speed * elapsedTime;
                temp.Y += m_Direction.Y * m_Speed * elapsedTime;

                Position = temp;


                if (CollisionCheck.collisionCheck(m_CollitionRect, m_LevelBoundries) != true)
                {
                    m_IsActive = false;
                    m_Particles.Stop = true;
                }

                CollisionType[,] tempy = Level.backgroundCollisionCheck(m_CollitionRect);

                for (int i = 0; i < tempy.GetLength(0); i++)
                {
                    for (int j = 0; j < tempy.GetLength(1); j++)
                    {
                        if (tempy[i, j].m_WasThereACollision == true)
                        {
                            m_IsActive = false;
                            m_Particles.Stop = true;
                        }
                    }
                }
            }
            m_Particles.update(gameTime);
            m_Particles.update(gameTime);
            m_Particles.update(gameTime);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (m_IsActive == true)
            {
                m_Particles.draw(spriteBatch);
            }
        }

        public bool collisionCheck(Rectangle target)
        {
            if (m_IsActive == true)
            {
                if (CollisionCheck.collisionCheck(m_CollitionRect, target) == true)
                {
                    m_IsActive = false;
                    m_Particles.Stop = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        #endregion
    }
}
