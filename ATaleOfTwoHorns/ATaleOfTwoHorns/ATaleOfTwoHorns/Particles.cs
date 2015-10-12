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

//created by kris matis

namespace ATaleOfTwoHorns
{
    class Particles
    {
        Particle[] m_Particles = new Particle[150];

        float m_Speed;
        int m_SpeedVariance;

        Vector2 m_SpawnPoint;
        int m_SpawnRadius;        

        float m_SpawnDelay;
        float m_SpawnTimer = 0.0f;

        Color m_Color;
        bool m_VaryingColors;
        int m_ColorVariance;

        Vector2 m_DominantDirection;
        float m_DirectionVariance;

        Random m_Random;

        bool m_Stop = false;

        public Vector2 SpawnPoint
        {
            set { m_SpawnPoint = value; }
            get { return m_SpawnPoint; }
        }

        public Vector2 DominantDirection
        {
            set { m_DominantDirection = value; }
        }

        public bool Stop
        {
            get { return m_Stop; }
            set { m_Stop = value; }
        }

        public Particles(Vector2 spawnPoint, Color dominantColor, float speed = 40.0f, int speedVariance = 15,
                         float spawnDelay = 0.05f, Vector2 dominantDirection = new Vector2(), 
                         float directionVariance = 10.0f, bool varyingColors = true, int colorVariance = 15,
                         int spawnRadius = 0, float lifeSpan = 3.0f, bool fadeOut = true)
        {
            m_SpawnPoint = spawnPoint; 
            m_Color = dominantColor;
            m_Speed = speed; 
            m_SpeedVariance = speedVariance;
            m_SpawnDelay = spawnDelay;
            m_DominantDirection = dominantDirection;
            m_DirectionVariance = directionVariance;
            m_VaryingColors = varyingColors;
            m_ColorVariance = colorVariance;
            m_SpawnRadius = spawnRadius;            

            for(int i = 0; i < m_Particles.Length; i++)
            {
                m_Particles[i]= new Particle(lifeSpan, fadeOut);
            }

            m_Random = new Random();
        }

        public void loadContent(ContentManager content)
        {
            for (int i = 0; i < m_Particles.Length; i++)
            {
                m_Particles[i].loadContent(content);
            }
        }

        public void clear()
        {
            for (int i = 0; i < m_Particles.Length; i++)
            {
                m_Particles[i].IsActive = false;
            }
        }

        public void update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.Milliseconds/1000;

            m_SpawnTimer += elapsedTime;
            if (m_Stop != true)
            {
                if (m_SpawnTimer > m_SpawnDelay)
                {
                    m_SpawnTimer = 0.0f;
                    spawnParticle();
                }
            }

            for (int i = 0; i < m_Particles.Length; i++)
            {
                m_Particles[i].update(gameTime);
            }
        }

        public void spawnParticle()
        {
            for (int i = 0; i < m_Particles.Length; i++)
            {
                if (m_Particles[i].IsActive == false)
                {
                    m_Particles[i].IsActive = true;


                    m_Particles[i].Position = new Vector2(m_SpawnPoint.X + m_Random.Next(m_SpawnRadius * 2) - m_SpawnRadius,
                                                          m_SpawnPoint.Y + m_Random.Next(m_SpawnRadius * 2) - m_SpawnRadius);
                    //varying colors is broken
                    if(m_VaryingColors == true)
                    {
                        int r;
                        int g;
                        int b;
                        int a;

                        if (m_Color.R > m_ColorVariance)
                        {
                            r = (m_Color.R - m_Random.Next(m_ColorVariance));
                        }
                        else
                        {
                            r = m_Random.Next(m_ColorVariance);
                        }

                        if (m_Color.G > m_ColorVariance)
                        {
                            g = (m_Color.G - m_Random.Next(m_ColorVariance));
                        }
                        else
                        {
                            g = m_Random.Next(m_ColorVariance);
                        }

                        if (m_Color.B > m_ColorVariance)
                        {
                            b = (m_Color.B - m_Random.Next(m_ColorVariance));
                        }
                        else
                        {
                            b = m_Random.Next(m_ColorVariance);
                        }                        
                        
                        a = m_Color.A;

                        m_Particles[i].Color = new Color((float)r / 255.0f, (float)g / 255.0f, (float)b / 255.0f, (float)a/255.0f);
                    }
                    else
                    {
                        m_Particles[i].Color = m_Color;
                    }

                    Vector2 temp = new Vector2(m_Random.Next(100)-50, m_Random.Next(100)-50);
                    temp.Normalize();
                    
                    m_Particles[i].Direction = new Vector2(m_DominantDirection.X + temp.X * m_DirectionVariance,
                                                           m_DominantDirection.Y + temp.Y * m_DirectionVariance);

                    m_Particles[i].Speed = m_Speed + m_Random.Next(m_SpeedVariance * 2) - m_SpeedVariance;

                    break;
                }
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < m_Particles.Length; i++)
            {
                if (m_Particles[i].IsActive == true)
                {
                    m_Particles[i].draw(spriteBatch);
                }
            }
        }
    }
}
