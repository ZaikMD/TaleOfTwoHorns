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
    class Lava
    {
        Vector2 m_Start;

        Rectangle m_CollisionRect;

        Texture2D m_SpriteSheet;

        Rectangle m_SourceRect = new Rectangle(0, 0, 1, 128);
        Rectangle m_DestinationRect = new Rectangle(0, 0, 1, 150);

        int m_PosX = 0;
        int m_SpriteWidth;

        public Vector2 Pos
        {
            get { return m_Start; }
            set
            {
                m_Start = value;
                m_DestinationRect.Y = (int)value.Y;
                m_DestinationRect.X = (int)value.X;

                m_CollisionRect.Y = (int)value.Y;
                m_CollisionRect.X = (int)value.X;
            }
        }

        public Lava(Vector2 startPos)
        {
            m_Start = startPos;
            m_DestinationRect.Y = (int)m_Start.Y;
        }

        public void loadContent(ContentManager content)
        {
            m_SpriteSheet = content.Load<Texture2D>("Lava");
            m_SpriteWidth = m_SpriteSheet.Width;
            m_CollisionRect = new Rectangle((int)m_Start.X, (int)m_Start.Y, m_SpriteSheet.Width, m_SpriteSheet.Height);
        }

        public void update(GameTime gameTime)
        {
            m_PosX += m_SourceRect.Width;
            if (m_PosX >= m_SpriteWidth)
            {
                m_PosX = 0;
            }
            if(CollisionCheck.collisionCheck(Player.collisionRectangle(),m_CollisionRect))
            {
                Player.instantDeath();
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            m_DestinationRect.X = (int)m_Start.X;
            for (int i = m_PosX; i - m_PosX < m_SpriteWidth; i++)
            {
                if (i < m_SpriteWidth)
                {
                    m_SourceRect.X = i;
                }
                else
                {
                    m_SourceRect.X = i - m_SpriteWidth;
                }
                m_DestinationRect.X ++;
                spriteBatch.Draw(m_SpriteSheet, m_DestinationRect, m_SourceRect, Color.White);
            }
        }        
    }
}
