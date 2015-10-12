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
    class Imp : MovingObject
    {
        Rectangle destinationRectangle = new Rectangle(0, 0, 64, 64);
        Rectangle sourceRectangle = new Rectangle(0, 0, 64, 64);
        Vector2 Position { get; set; }
        Vector2 Direction { get; set; }
        public EasingType easingType = EasingType.Cubic;
        private bool m_IsAggroed = false;
        int m_impHealth = 3;
        ImpState m_State = ImpState.ALIVE;
        float m_Timer = 0.0f;
        float m_EndTimer = 1.0f;
        bool m_ImpAggroSoundPlayed = false;
        bool m_ImpDeathSoundPlayed = false;

        enum ImpAnimations
        {
            IDLE = 0,
            RIGHT,
            LEFT,
            IDLEDYING
        }

        public Imp(Vector2 initialPosition, Rectangle spriteSourceRectangle, Rectangle spriteDestinationRectangle) :
            base(initialPosition, spriteSourceRectangle, spriteDestinationRectangle)
        {
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
            m_Animate.addAnimation(new Animation(16, new int[] { 0 }));
            m_Animate.addAnimation(new Animation(8,new int[] {0,1,2,3,2,1}));
            m_Animate.addAnimation(new Animation(8, new int[] {0,1,2,3,2,1 }));
            m_Animate.addAnimation(new Animation(32, new int[] { 0, 1 }));

        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.Milliseconds / 1000;

            if (m_State == ImpState.ALIVE)
            {
                move(gameTime);
                animationRow(gameTime);

                if (CollisionCheck.collisionCheck(Destination, Player.collisionRectangle()))
                {
                    Player.takeDamage();
                }
            }
            else
            {
                m_Timer += elapsedTime;
                m_Animate.changAnimation((int)ImpAnimations.IDLEDYING);
                if (m_ImpDeathSoundPlayed == false)
                {
                    Sounds.playSound("ImpDeath", 0.5f);
                    m_ImpDeathSoundPlayed = true;
                }
            }

            base.update(gameTime);
        }

        private void move(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.Milliseconds / 1000;

            float m_aggroRange = 350.0f;

            Vector2 m_distanceFromPlayer = new Vector2(m_Position.X - Player.collisionRectangle().Center.X, m_Position.Y - Player.collisionRectangle().Center.Y);

            if (m_distanceFromPlayer.Length() <= m_aggroRange)
            {
                m_IsAggroed = true;
                m_distanceFromPlayer.Normalize();
                m_Position.X -= m_distanceFromPlayer.X * m_Speed * elapsedTime;

                if (m_ImpAggroSoundPlayed == false)
                {
                    Sounds.playSound("ImpAggro", 0.5f);
                    m_ImpAggroSoundPlayed = true;
                }
            }
            else if (m_distanceFromPlayer.Length() > m_aggroRange)
            {
                m_IsAggroed = false;
                m_ImpAggroSoundPlayed = false;
            }
        }

        private void animationRow(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.Milliseconds / 1000;

            if (m_IsAggroed == true)
            {

                if (m_Position.X <= Player.collisionRectangle().Center.X)
                {
                    m_Animate.changAnimation((int)ImpAnimations.LEFT);
                }
                else if (m_Position.X >= Player.collisionRectangle().Center.X)
                {
                    m_Animate.changAnimation((int)ImpAnimations.RIGHT);
                }

            }
            else
            {
                m_Animate.changAnimation((int)ImpAnimations.IDLE);
            }
        }

        public int currentHealth
        {
            get
            {
                return m_impHealth;
            }
            set
            {
                m_impHealth = value;
            }
        }

        public ImpState impState
        {
            get
            {
                return m_State;
            }
            set
            {
                m_State = value;
            }
        }

        public void takeDamage()
        {
            currentHealth--;
        }

        public float getTimer
        {
            get { return m_Timer; }
        }

        public float getEndTimer
        {
            get { return m_EndTimer; }
        }

        public override void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);
        }
    }
}
