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
    class Pixie : FlyingMovingObject
    {
        int m_pixieHealth = 2;

        enum PixieAnimations
        {
            LEFT = 0,
            RIGHT,
            POOF
        }



        PixieState m_State = PixieState.ALIVE;
        float m_Timer = 0.0f;
        float m_Poof = 0.5f;

        Rectangle destinationRectangle = new Rectangle(0, 0, 64, 64);
        Rectangle sourceRectangle = new Rectangle(0, 0, 64, 64);
        Vector2 Position { get; set; }
        Vector2 Direction { get; set; }

        bool m_PixieDeathPlayed = false;
        bool m_PixieAggroPlayed = false;

        public Pixie(Vector2 initialRectangle, Rectangle spriteSourceRectangle, Rectangle spriteDestinationRectangle) :
            base(initialRectangle, spriteSourceRectangle, spriteDestinationRectangle)
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
            m_Animate.addAnimation(new Animation(8,new int[] {0,1,2,3}));
            m_Animate.addAnimation(new Animation(8, new int[] { 0, 1, 2, 3}));
            m_Animate.addAnimation(new Animation(8, new int[] { 0 }));
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.Milliseconds / 1000;

            if (m_State == PixieState.ALIVE)
            {
                move(gameTime);

                if (Destination.Intersects(Player.collisionRectangle()) == true)
                {
                   

                }
                else
                {
                    animationRow(gameTime);
                }

                if (CollisionCheck.collisionCheck(Destination, Player.collisionRectangle()))
                {
                    Player.takeDamage();
                }                
            }
            else
            {
                m_Timer += elapsedTime;
                m_Animate.changAnimation((int)PixieAnimations.POOF);
                if (m_PixieDeathPlayed == false) 
                {
                    Sounds.playSound("PixieDeath", 0.50f);
                    m_PixieDeathPlayed = true;
                }

            }

            base.update(gameTime);
        }

        private void move(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.Milliseconds / 1000;

            float m_aggroRange = 300.0f;

            Vector2 m_distanceFromPlayer = new Vector2(m_Position.X - Player.collisionRectangle().Center.X, m_Position.Y - Player.collisionRectangle().Center.Y);

            if (m_distanceFromPlayer.Length() <= m_aggroRange)
            {
                m_distanceFromPlayer.Normalize();
                m_Position.Y -= m_distanceFromPlayer.Y * m_Speed * elapsedTime;
                m_Position.X -= m_distanceFromPlayer.X * m_Speed * elapsedTime;

                if (m_PixieAggroPlayed == false)
                {
                    Sounds.playSound("PixieAggro", 0.50f);
                    m_PixieAggroPlayed = true;
                }
            }
            else
            {
                m_PixieAggroPlayed = false;
            }
        }

        private void animationRow(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.Milliseconds / 1000;

            if (m_Position.X <= Player.collisionRectangle().Center.X && currentHealth > 0)
            {
                m_Animate.changAnimation((int)PixieAnimations.RIGHT);
            }
            else if (m_Position.X >= Player.collisionRectangle().Center.X && currentHealth > 0)
            {
                m_Animate.changAnimation((int)PixieAnimations.LEFT);
            }
        }

        public int currentHealth
        {
            get
            {
                return m_pixieHealth;
            }
            set
            {
                m_pixieHealth = value;
            }
        }

        public PixieState pixieState
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

        public float getTimer
        {
            get { return m_Timer; }
        }

        public float getPoof
        {
            get { return m_Poof; }
        }

        public void takeDamage()
        {
            currentHealth--;
        }
        
        public override void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);
        }
    }
}
