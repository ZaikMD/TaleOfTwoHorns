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
    class Particle
    {
        Texture2D m_Sprite;
        Color m_Color;
        int m_Alpha;
        bool m_IsActive = false;
        Vector2 m_Position;
        Vector2 m_Direction;
        float m_Speed;

        bool m_FadeOut;
        float m_LifeSpan;
        float m_LifeSpanTimer = 0.0f;

        public Color Color
        {
            set 
            {
                m_Alpha = 255;
                m_Color = value; 
            }
        }

        public bool IsActive
        {
            get { return m_IsActive; }
            set 
            { 
                m_IsActive = value;
                if (value == true)
                {
                    m_LifeSpanTimer = 0.0f;
                }
            }
        }

        public Vector2 Position
        {
            set { m_Position = value; }
        }

        public Vector2 Direction
        {            
            set 
            {
                value.Normalize();
                m_Direction = value; 
            }
        }

        public Particle(float lifeSpan, bool fadeOut = true)
        {
            m_LifeSpan = lifeSpan;
            m_FadeOut = fadeOut;
        }

        public float Speed
        {
            set { m_Speed = value; }
        }

        public void loadContent(ContentManager content)
        {
            m_Sprite = content.Load<Texture2D>("particle");
        }

        public void update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.Milliseconds/1000;

            m_LifeSpanTimer += elapsedTime;
            if (m_LifeSpanTimer > m_LifeSpan)
            {
                m_IsActive = false;
                m_LifeSpanTimer = 0.0f;
            }
            else
            {
                float lifeLeft = m_LifeSpanTimer/m_LifeSpan;
                /*
                m_Color = new Color(m_Color.R,
                                    m_Color.G,
                                    m_Color.B,
                                    1 - lifeLeft);
                lifeLeft = 1 - lifeLeft;

                if (lifeLeft < 0.35f)
                {
                    lifeLeft = 0.35f;
                }*/

                m_Color.A = (byte)(m_Color.A - 1);
                //m_Alpha--;
            }

            m_Position.Y += m_Direction.Y * m_Speed * elapsedTime;
            m_Position.X += m_Direction.X * m_Speed * elapsedTime;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Camera.ViewMatrix);
            spriteBatch.Draw(m_Sprite, m_Position, m_Color);
            spriteBatch.End();
        }
    }
}
