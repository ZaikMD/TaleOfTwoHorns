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
    class UndeadUnicorn : MovingObject
    {
        Rectangle destinationRectangle = new Rectangle(0, 0, 64, 64);
        Rectangle sourceRectangle = new Rectangle(0, 0, 64, 64);
        Vector2 Position { get; set; }
        Vector2 Direction { get; set; }
        public EasingType easingType = EasingType.Cubic;
        private bool m_IsAggroed = false;
        int m_undeadUnicornHealth = 5;
        UndeadUnicornState m_State = UndeadUnicornState.ALIVE;
        UndeadUnicornAnimations m_LastAnimation;
        float m_Timer = 0.0f;
        float m_EndTimer = 0.5f;
        bool m_AggroSoundPlayed = false;

        enum UndeadUnicornAnimations
        {
            LEFT = 0,
            RIGHT,
            IDLERIGHT,
            IDLELEFT,
            SOMETHING,
            SOMETHING2,
            JUMPRIGHT,
            JUMPLEFT,
            DYINGRIGHT,
            DYINGLEFT
        }

        public UndeadUnicorn(Vector2 initialPosition, Rectangle spriteSourceRectangle, Rectangle spriteDestinationRectangle) :
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
            m_Animate.addAnimation(new Animation(16, new int[] { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 }));
            m_Animate.addAnimation(new Animation(16, new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }));
            m_Animate.addAnimation(new Animation(16, new int[] { 0 }));
            m_Animate.addAnimation(new Animation(16, new int[] { 0 }));
            m_Animate.addAnimation(new Animation(16, new int[] { 0, 1, 2, 3, 4 }));
            m_Animate.addAnimation(new Animation(16, new int[] { 0, 1, 2, 3, 4 }));
            m_Animate.addAnimation(new Animation(16, new int[] { 0, 1, 2, 3 }));
            m_Animate.addAnimation(new Animation(16, new int[] { 0, 1, 2, 3 }));
            m_Animate.addAnimation(new Animation(8, new int[] { 0, 1, 2, 3, 4 }));
            m_Animate.addAnimation(new Animation(8, new int[] { 0, 1, 2, 3, 4 }));

        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.Milliseconds / 1000;

            if (m_State == UndeadUnicornState.ALIVE)
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
                m_Animate.changAnimation((int)UndeadUnicornAnimations.DYINGRIGHT);
            }

            base.update(gameTime);
        }

        private void move(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.Milliseconds / 1000;

            float m_aggroRange = 350.0f;

            m_Speed = 300.0f;

            Vector2 m_distanceFromPlayer = new Vector2(m_Position.X - Player.collisionRectangle().Center.X, m_Position.Y - Player.collisionRectangle().Center.Y);

            if (m_distanceFromPlayer.Length() <= m_aggroRange)
            {
                if (m_AggroSoundPlayed == false)
                {
                    Sounds.playSound("UndeadUnicornAggro", 0.50f);
                    m_AggroSoundPlayed = true;
                }

                m_IsAggroed = true;
                m_distanceFromPlayer.Normalize();
                m_Position.X -= m_distanceFromPlayer.X * m_Speed * elapsedTime;
            }
            else if (m_distanceFromPlayer.Length() > m_aggroRange)
            {
                m_IsAggroed = false;
                m_AggroSoundPlayed = false;
            }


        }

        private void animationRow(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.Milliseconds / 1000;

            if (m_IsAggroed == true)
            {

                if (m_Position.X <= Player.collisionRectangle().Center.X)
                {
                    m_Animate.changAnimation((int)UndeadUnicornAnimations.LEFT);
                    m_LastAnimation = UndeadUnicornAnimations.LEFT;
                }
                else if (m_Position.X >= Player.collisionRectangle().Center.X)
                {
                    m_Animate.changAnimation((int)UndeadUnicornAnimations.RIGHT);
                    m_LastAnimation = UndeadUnicornAnimations.RIGHT;
                }

            }
            else if(m_LastAnimation == UndeadUnicornAnimations.RIGHT)
            {
                m_Animate.changAnimation((int)UndeadUnicornAnimations.IDLERIGHT);
            }
            else if (m_LastAnimation == UndeadUnicornAnimations.LEFT)
            {
                m_Animate.changAnimation((int)UndeadUnicornAnimations.IDLELEFT);
            }
        }

        public int currentHealth
        {
            get
            {
                return m_undeadUnicornHealth;
            }
            set
            {
                m_undeadUnicornHealth = value;
            }
        }

        public UndeadUnicornState undeadUnicornState
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
 

