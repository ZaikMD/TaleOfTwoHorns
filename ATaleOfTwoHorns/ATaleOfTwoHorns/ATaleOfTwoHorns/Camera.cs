using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ATaleOfTwoHorns
{
    class Camera
    {

        Viewport m_Viewport;

        private static Vector2 m_CurrentPosition;

        Vector2 m_DestinationPosition;

        Rectangle m_OuterBonds;

        private static Vector2 m_Origin;

        private Vector2 CurrentPosition
        {
            get { return m_CurrentPosition; }
            set
            {
                m_CurrentPosition = value;
                if (m_OuterBonds != Rectangle.Empty)
                {
                    m_CurrentPosition.X = MathHelper.Clamp(m_CurrentPosition.X, m_OuterBonds.X, m_OuterBonds.X + m_OuterBonds.Width - m_Viewport.Width);
                    m_CurrentPosition.Y = MathHelper.Clamp(m_CurrentPosition.Y, m_OuterBonds.Y, m_OuterBonds.Y + m_OuterBonds.Height - m_Viewport.Height);
                }
            }
        }

        public Rectangle Limits
        {
            get { return m_OuterBonds; }
            set
            {
                if (value != null)
                {
                    m_OuterBonds = new Rectangle();
                    m_OuterBonds.X = value.X;
                    m_OuterBonds.Y = value.Y;
                    m_OuterBonds.Width = Math.Max(m_Viewport.Width, value.Width);
                    m_OuterBonds.Height = Math.Max(m_Viewport.Height, value.Height);
                }
                else
                {
                    m_OuterBonds = Rectangle.Empty;
                }
            }
        }

        public static Matrix ViewMatrix
        {
            get
            {
                return Matrix.CreateTranslation(new Vector3(-m_CurrentPosition, 0)) *
                       Matrix.CreateTranslation(new Vector3(-m_Origin, 0)) *
                       Matrix.CreateTranslation(new Vector3(m_Origin, 0));
            }
        }

        public Camera(Viewport viewport)
        {
            m_Viewport = viewport;
            m_Origin = new Vector2(m_Viewport.Width / 2, m_Viewport.Height / 2);
        }

        public Matrix getParallaxMatrix(Vector2 parallaxValue)
        {
            return Matrix.CreateTranslation(new Vector3(-m_CurrentPosition * parallaxValue, 0));
        }

        public void lookAt(Vector2 targetPosition)
        {
            m_DestinationPosition = targetPosition - m_Origin;
        }

        public void Update(GameTime gameTime)
        {
            float elaspedTime = (float)gameTime.ElapsedGameTime.Milliseconds / 1000;
            Vector2 newPos = new Vector2(MathHelper.Lerp(m_CurrentPosition.X, m_DestinationPosition.X, 2.0f * elaspedTime),
                                         MathHelper.Lerp(m_CurrentPosition.Y, m_DestinationPosition.Y, 2.0f * elaspedTime));


            if (newPos.X <= m_OuterBonds.X)
            {
                newPos.X = m_OuterBonds.X;
            }
            else if (newPos.X + m_Viewport.Width >= m_OuterBonds.X + m_OuterBonds.Width)
            {
                newPos.X = m_OuterBonds.Width;
            }

            CurrentPosition = newPos;            
        }
    }
}
