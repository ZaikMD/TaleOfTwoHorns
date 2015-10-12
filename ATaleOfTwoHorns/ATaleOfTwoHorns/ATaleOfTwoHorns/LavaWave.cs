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
    class LavaWave
    {
        Animate m_Animate = new Animate(new Rectangle(0, 0, 250, 450), new Rectangle(0, 0, 1, 1));

        Rectangle m_LevelBounds;

        float m_Speed = 400.0f;

        const float MAX_HEIGHT = 500.0f;
        const float MAX_WIDTH = 300.0f;

        const float INCREMENT_AMOUNT = 0.025f;

        Vector2 m_Position = new Vector2();
        Vector2 m_StartIngPos = new Vector2();

        bool m_IsActive = false;

        public bool IsActive
        {
            get { return m_IsActive; }
        }

        public LavaWave()
        {
            m_LevelBounds = new Rectangle(0, 0, Level.getWidthOfArray() * 32, Level.getHeightOfArray() * 32);
        }

        public void reset()
        {
            m_Animate.DestinationRectangle = new Rectangle(0, 0, 1, 1);
            m_IsActive = false;
        }

        public void loadContent(ContentManager content)
        {
            m_Animate.loadContent("LavaWave", content);
            m_Animate.addAnimation(new Animation(32, new int[] { 0 }));
        }

        public void update(GameTime gameTime)
        {
            if (m_IsActive == true)
            {
                float elapsedTime = (float)gameTime.ElapsedGameTime.Milliseconds / 1000;

                m_Position.X -= m_Speed * elapsedTime;

                Rectangle temp = m_Animate.DestinationRectangle;

                if (temp.Width < MAX_WIDTH)
                {
                    temp.Width += (int)(MAX_WIDTH * INCREMENT_AMOUNT);
                }

                if (temp.Height < MAX_HEIGHT)
                {
                    temp.Height += (int)(MAX_HEIGHT * INCREMENT_AMOUNT);
                }

                m_Animate.DestinationRectangle = temp;

                m_Position.Y = m_StartIngPos.Y - m_Animate.DestinationRectangle.Height / 2;

                m_Animate.updatePosition(m_Position);

                if (CollisionCheck.collisionCheck(m_Animate.DestinationRectangle, m_LevelBounds) == false)
                {
                    m_IsActive = false;
                    reset();
                }

                collisionCheck();
            }
        }

        public void SpawnTheWave(Vector2 startingPos)
        {
            m_IsActive = true;
            m_StartIngPos = startingPos;
            m_Position = startingPos;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (m_IsActive == true)
            {
                m_Animate.draw(spriteBatch);
            }
        }

        private void collisionCheck()
        {
            if (CollisionCheck.collisionCheck(m_Animate.DestinationRectangle, Player.collisionRectangle()))
            {
                Player.takeDamage();
            }
        }
    }
}
