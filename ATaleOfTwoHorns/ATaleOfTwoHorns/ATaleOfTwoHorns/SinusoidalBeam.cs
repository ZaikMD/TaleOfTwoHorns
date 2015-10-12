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

//created by Kris Matis

namespace ATaleOfTwoHorns
{
    class SinusoidalBeam
    {
        SinFunction[] m_Sin = new SinFunction[2];

        Vector2[] m_Positions = new Vector2[2];
        Particles[] m_Particles = new Particles[2];
        Rectangle[] m_CollisionRects = new Rectangle[2];
        Texture2D[] m_things= new Texture2D[2];
        Color[] m_Colors = new Color[2];

        float m_Speed = 100.0f;
        MovementValue m_Direction = MovementValue.NONE;

        float m_PosXInFunction;

        bool m_IsActive = false;

        Rectangle m_Boundries;

        public bool IsAvtive
        {
            set { m_IsActive = value; }
            get { return m_IsActive; }
        }

        public SinusoidalBeam(Rectangle boundries, Color[] color)
        {
            m_Boundries = boundries;

            for (int i = 0; i < m_CollisionRects.Length; i++)
            {
                m_CollisionRects[i] = new Rectangle(0, 0, 4, 4);
                m_Colors[i] = color[i];
                m_Sin[i] = new SinFunction();                
            }

            for (int i = 0; i < m_Particles.Length; i++)
            {
                m_Particles[i] = new Particles(new Vector2(), color[i%2], 15.0f, 5, 0.0f, new Vector2(0, 0), 5.0f, true, 70, 3, 3.0f);
            }
        }

        public void fire(Vector2 start, Vector2 target)
        {
            m_Positions[0] = start;
            m_Positions[1] = start;

            m_PosXInFunction = start.X;

            if (target.X > start.X)
            {
                m_Direction = MovementValue.RIGHT;
            }
            else
            {
                m_Direction = MovementValue.LEFT;
            }

            m_Sin[0] = new SinFunction(50.0f, 0.08f, 0.0f, start.Y);
            m_Sin[1] = new SinFunction(50.0f, 0.08f, SinFunction.degreesToRadians(180.0f), start.Y);

            m_Positions[0] = new Vector2(start.X, 0);
            m_Positions[1] = new Vector2(start.X, 0);

            for (int i = 0; i < m_CollisionRects.Length; i++)
            {
                m_Particles[i].Stop = false;
            }
        }
        
        public void loadContent(ContentManager content)
        {
            for (int i = 0; i < m_Particles.Length; i++)
            {
                m_Particles[i].loadContent(content);                
            }

            for (int i = 0; i < m_things.Length; i++)
            {
                m_things[i] = content.Load<Texture2D>("thingy");
            }
        }

        public void update(GameTime gameTime)
        {
            
                float elapsedTime = (float)gameTime.ElapsedGameTime.Milliseconds / 1000;

                Vector2[] oldPos = m_Positions;

                for (int i = 0; i < m_Positions.Length; i++)
                {
                    m_PosXInFunction += m_Speed * elapsedTime * (int)m_Direction;

                    m_Positions[i].X = m_PosXInFunction;

                    m_Positions[i].Y = m_Sin[i].getYAtPosX(m_Positions[i].X);

                    m_CollisionRects[i].X = (int)m_Positions[i].X - m_CollisionRects[i].Width;
                    m_CollisionRects[i].Y = (int)m_Positions[i].Y - m_CollisionRects[i].Height;
                }

                for (int i = 0; i < m_Particles.Length; i++)
                {
                    m_Particles[i].SpawnPoint = m_Positions[i % 2];

                    m_Particles[i].DominantDirection = (oldPos[i % 2] - m_Positions[i % 2]);

                    //did this to make the beam spawn more particles
                    m_Particles[i].update(gameTime);
                    m_Particles[i].update(gameTime);
                    m_Particles[i].update(gameTime);

                    if (CollisionCheck.collisionCheck(m_CollisionRects[i % 2], m_Boundries) == false)
                    {
                        m_IsActive = false;
                        m_Particles[i].Stop = true;
                    }
                }
                collisionCheck();
            
        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (m_IsActive == true)
            {
                for (int i = 0; i < m_Particles.Length; i++)
                {
                    m_Particles[i].draw(spriteBatch);

                    if (m_Particles[i].Stop != true)
                    {
                        spriteBatch.Begin(SpriteSortMode.Immediate, null, SamplerState.PointClamp, null, null, null, Camera.ViewMatrix);
                        spriteBatch.Draw(m_things[i], m_CollisionRects[i], m_Colors[i]);
                        spriteBatch.End();
                    }
                }
            }
        }

        private void collisionCheck()
        {
            if (m_IsActive == true)
            {
                for (int i = 0; i < m_CollisionRects.Length; i++)
                {
                    if (CollisionCheck.collisionCheck(m_CollisionRects[i], Player.collisionRectangle()))
                    {
                        Player.takeDamage();
                    }
                }
            }
        }

        public void reset()
        {
            for (int i = 0; i < m_Positions.Length; i++)
            {
                m_Positions[i].X = -99999;
            }
        }
    }
}
