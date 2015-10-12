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
    class Spider : FlyingMovingObject
    {

        enum SpiderAnimations
        {
            IDLE = 0,
            MOVING
        }

        SpiderAggroState m_AggroState = SpiderAggroState.NONE;
        SpiderState m_State = SpiderState.ALIVE;
        
        float m_AggroRange = 50.0f;
        Rectangle m_UnAggroedRectangle;

        public Spider(Vector2 initialPosition, Rectangle spriteSourceRectangle, Rectangle spriteDestinationRectangle) :
            base(initialPosition, spriteSourceRectangle, spriteDestinationRectangle)
        {
            m_Position = initialPosition;
            m_UnAggroedRectangle = new Rectangle((int)(m_Position.X - 30), (int)(m_Position.Y - Destination.Height/2), 60, 10);

            m_Speed = 400;
        }

        public override void loadContent(string spriteSheetName, Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.loadContent(spriteSheetName, content);
            m_Animate.addAnimation(new Animation(16, new int[] { 0 }));
            m_Animate.addAnimation(new Animation(16,new int[] {0,1,2,3,4,5,6,7,8,9}));

        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.Milliseconds/1000;

            //determine if the player is in range and the spider is in an aggroeable state
            if (Math.Abs(m_Position.X - Player.collisionRectangle().Center.X)<=m_AggroRange)
            {
                if (m_AggroState == SpiderAggroState.NONE)
                {
                    m_AggroState = SpiderAggroState.AGGROED;
                }
            }

            //move spider
            if (m_AggroState == SpiderAggroState.AGGROED || m_State == SpiderState.DYING)
            {
                m_Position.Y += m_Speed * elapsedTime;
            }   
            else if(m_AggroState == SpiderAggroState.RETREATING)
            {
                m_Position.Y -= m_Speed * elapsedTime;
            }


            if(CollisionCheck.collisionCheck(Destination,m_UnAggroedRectangle))
            {
                m_AggroState=SpiderAggroState.NONE;
            }

            m_Animate.updatePosition(m_Position);

            CollisionType[,] temp = Level.backgroundCollisionCheck(Destination);

            for (int i = 0; i < temp.GetLength(0); i++)
            {
                for (int j = 0; j < temp.GetLength(1); j++)
                {
                    if (temp[i, j].m_WasThereACollision == true)
                    {
                        m_AggroState = SpiderAggroState.RETREATING;
                    }
                }
            }

            if(CollisionCheck.collisionCheck(Destination, Player.collisionRectangle()))
            {
                Player.takeDamage();
            }


            base.update(gameTime);
        }

        public SpiderState spiderState
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

        public SpiderAggroState spiderAggroState
        {
            get
            {
                return m_AggroState;
            }
            set
            {
                m_AggroState = value;
            }
        }



        public override void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);
        }
    }
}
