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
    class NarwhalBullet
    {
        Texture2D m_Sprite;
        Rectangle m_SourceRect = new Rectangle(0, 0, 50, 50);
        Rectangle m_DestinationRect = new Rectangle(0, 0, 50, 50);

        Vector2 m_Position = new Vector2();
        Vector2 m_Direction = new Vector2();
        float m_Speed = 500.0f;

        bool m_IsActive = false;

        Rectangle m_LevelBounds = new Rectangle(0, 0, Level.getWidthOfArray() * 32, Level.getHeightOfArray() * 32);

        private Vector2 Position
        {
            set
            {
                m_Position = value;

                m_DestinationRect.X = (int)value.X - m_DestinationRect.Width / 2;
                m_DestinationRect.Y = (int)value.Y - m_DestinationRect.Height / 2;
            }
        }

        public bool IsActive
        {
            get { return m_IsActive; }
            set { m_IsActive = value; }
        }

        public void loadContent(ContentManager content)
        {
            m_Sprite=content.Load<Texture2D>("NarwhalBullet");
        }

        public void update(GameTime gameTime)
        {
            if (m_IsActive == true)
            {
                float elapsedTime = (float)gameTime.ElapsedGameTime.Milliseconds / 1000;

                m_Position += m_Direction * m_Speed * elapsedTime;

                Position = m_Position;

                collisionCheck();
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (m_IsActive == true)
            {
                spriteBatch.Draw(m_Sprite, m_DestinationRect, m_SourceRect, Color.White);
            }
        }

        public void activate(Vector2 startPos, Vector2 direction)
        {
            m_Position = startPos;
            direction.Normalize();
            m_Direction = direction;
            m_IsActive = true;
        }

        private void collisionCheck()
        {
            if (CollisionCheck.collisionCheck(m_DestinationRect, Player.collisionRectangle()))
            {
                Player.takeDamage();
            }

            if (CollisionCheck.collisionCheck(m_DestinationRect, m_LevelBounds) != true)
            {
                m_IsActive = false;
            }
        }
    }
}
