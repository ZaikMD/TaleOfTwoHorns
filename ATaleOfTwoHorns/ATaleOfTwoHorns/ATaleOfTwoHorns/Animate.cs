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
    /// <summary>
    /// used with animation
    /// keeps track of which sprite should
    /// drawn and the drawing of said sprite
    /// </summary>
    class Animate
    {
        float m_Timer = 0.0f;
        int m_CurrentAnimation = 0;
        int m_CurrentFrame = 0;

        Rectangle m_SourceRectangle;
        Rectangle m_DestinationRectangle;

        Texture2D m_SpriteSheet;

        Animation[] m_Animations = new Animation[0];

        PerPixelInfo m_PerPixelInfo = new PerPixelInfo();

        public Rectangle DestinationRectangle
        {
            get { return m_DestinationRectangle; }
            set { m_DestinationRectangle = value; }
        }

        public PerPixelInfo PerPixelInfo
        {
            get { return m_PerPixelInfo; }
        }

        public Animate(Rectangle sourceRectangle, Rectangle destinationRectangle)
        {
            m_SourceRectangle = sourceRectangle;
            m_DestinationRectangle = destinationRectangle;

            m_PerPixelInfo.m_SourceRectangle = m_SourceRectangle;
            m_PerPixelInfo.m_DestinationRectangle = m_DestinationRectangle;
        }

        public void loadContent(string spriteSheetName, ContentManager content)
        {
            m_SpriteSheet = content.Load<Texture2D>(spriteSheetName);
            m_PerPixelInfo.m_SpriteSheet = m_SpriteSheet;
        }

        public void addAnimation(Animation animation)
        {
            Animation[] oldAnimations = m_Animations;

            m_Animations = new Animation[m_Animations.Length+1];

            for (int i = 0; i < oldAnimations.Length; i++)
            {
                m_Animations[i] = oldAnimations[i];
            }

            m_Animations[oldAnimations.Length] = animation;
        }

        public void changAnimation(int animationToBePlayed)
        {
            if (animationToBePlayed>=0 && animationToBePlayed < m_Animations.Length)
            {
                if (animationToBePlayed == m_CurrentAnimation)
                {
                    return;
                }

                m_CurrentAnimation = animationToBePlayed;
            }
            else
            {
                m_CurrentAnimation = 0;
            }

            m_CurrentFrame = 0;
            m_Timer = 0.0f;
        }

        public void updatePosition(Vector2 newPosition)
        {
            m_DestinationRectangle.X = (int)newPosition.X - (m_DestinationRectangle.Width / 2);
            m_DestinationRectangle.Y = (int)newPosition.Y - (m_DestinationRectangle.Height / 2);
        }

        public void update(GameTime gameTime)
        {            
            //update the timer
            m_Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            //check if its time for the next image
            if (m_Timer > 1.0f / m_Animations[m_CurrentAnimation].FramesPerSecond)
            {
                //reset the timer
                m_Timer = 0.0f;

                //increment the position and check that its still in range
                m_CurrentFrame++;
                if (m_CurrentFrame >= m_Animations[m_CurrentAnimation].Frames.Length)
                {
                    m_CurrentFrame = 0;
                }
                //update the source rectangle

                m_SourceRectangle.X = m_Animations[m_CurrentAnimation].Frames[m_CurrentFrame] * m_SourceRectangle.Width;
                m_SourceRectangle.Y = m_CurrentAnimation * m_SourceRectangle.Height;
            }               
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(m_SpriteSheet, m_DestinationRectangle, m_SourceRectangle, Color.White);
        }
    }
}
