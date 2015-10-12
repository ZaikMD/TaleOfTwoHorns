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
    class Bees : FlyingMovingObject
    {
        Rectangle destinationRectangle = new Rectangle(0, 0, 64, 64);
        Rectangle sourceRectangle = new Rectangle(0, 0, 64, 64);
        Vector2 Position { get; set; }
        Vector2 Direction { get; set; }
        bool m_SoundPlayed = false;

        Vector2 m_ResetPoint= new Vector2();

        public Bees(Vector2 initialPosition, Rectangle spriteSourceRectangle, Rectangle spriteDestinationRectangle) :
            base(initialPosition, spriteSourceRectangle, spriteDestinationRectangle)
        {
            m_ResetPoint.X = initialPosition.X;
            m_ResetPoint.Y = initialPosition.Y;
        }

        public void activate(Vector2 position, Vector2 direction)
        {
            IsActive = true;
            Position = position;
            Direction = direction;
        }

        public override void loadContent(string spriteSheetName, Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.loadContent(spriteSheetName, content);
            m_Animate.addAnimation(new Animation(32,new int[] {0,1,2,3}));
        }

        public override void update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.Milliseconds / 1000;

            float aggroRange = 300.0f;

            Vector2 m_distanceFromPlayer = new Vector2(m_Position.X - Player.collisionRectangle().Center.X, m_Position.Y - Player.collisionRectangle().Center.Y);
            Vector2 m_distanceFromResetPoint = new Vector2(m_Position.X - m_ResetPoint.X, m_Position.Y - m_ResetPoint.Y);

            if (m_distanceFromPlayer.Length() <= aggroRange)
            {
                m_distanceFromPlayer.Normalize();
                m_Position.Y -= m_distanceFromPlayer.Y * m_Speed * elapsedTime;
                m_Position.X -= m_distanceFromPlayer.X * m_Speed * elapsedTime;
                if(m_SoundPlayed == false)
                {
                    Sounds.playSound("BeeAggro", 0.25f);
                    m_SoundPlayed = true;
                }
            }
            else
            {
                if (m_distanceFromResetPoint.Length() != 0.0f)
                {
                    m_distanceFromResetPoint.Normalize();
                    m_Position.Y -= m_distanceFromResetPoint.Y * m_Speed * elapsedTime;
                    m_Position.X -= m_distanceFromResetPoint.X * m_Speed * elapsedTime;
                    m_SoundPlayed = false;

                    
                }
            }

            if (CollisionCheck.collisionCheck(Destination, Player.collisionRectangle()))
            {
                Player.takeDamage();
            }


            base.update(gameTime);
        }

        public override void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);
        }
    }
}
