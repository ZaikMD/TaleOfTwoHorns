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
    /// the base object for anything that has a sprite/animations
    /// </summary>
    abstract class BaseObject
    {
        //the centre location of the object
        protected Vector2 m_Position;
        protected Animate m_Animate;

        protected bool m_IsActive = false;

        public bool IsActive
        {
            get { return m_IsActive; }
            set { m_IsActive = value; }
        }

        public PerPixelInfo PerPixelInfo
        {
            get { return m_Animate.PerPixelInfo; }
        }

        public Rectangle Destination
        {
            get { return m_Animate.DestinationRectangle; }
        }

        public BaseObject(Vector2 initalPosition, Rectangle spriteSourceRectangle, Rectangle spriteDestinationRectangle)
        {
            m_Animate = new Animate(spriteSourceRectangle,spriteDestinationRectangle);
            m_Position = initalPosition;
        }

        virtual public void loadContent(string spriteSheetName, ContentManager content)
        {
            m_Animate.loadContent(spriteSheetName, content);
        }

        virtual public void update(GameTime gameTime)
        {
            m_Animate.updatePosition(m_Position);

            CollisionType[,] temp = Level.backgroundCollisionCheck(Destination);

            int count = 0;

            for (int i = 0; i < temp.GetLength(0); i++)
            {
                for (int j = 0; j < temp.GetLength(1); j++)
                {
                    if (temp[i, j].m_WasThereACollision == true)
                    {
                        count++;
                        switch (temp[i, j].m_FirstObjectSideCollided)
                        {
                                
                            case SideCollided.BOTTOM:
                                if (this is Player)
                                {
                                    Player.setIsOnGround(true);
                                }
                                m_Position.Y -= temp[i, j].m_OverlapArea.Height;
                                break;
                            case SideCollided.TOP:
                                if (this is Player)
                                {
                                    Player.setIsJumping(false);
                                }
                                m_Position.Y += temp[i, j].m_OverlapArea.Height;
                                break;
                            case SideCollided.LEFT:
                                m_Position.X += temp[i, j].m_OverlapArea.Width;
                                break;
                            case SideCollided.RIGHT:
                                m_Position.X -= temp[i, j].m_OverlapArea.Width;
                                break;
                        }
                        if (count >= 3)
                        {
                            break;
                        }
                    }                    
                }
                if (count >= 3)
                {
                    break;
                }
            }
            m_Animate.update(gameTime);
            m_Animate.updatePosition(m_Position);
        }


        virtual public void draw(SpriteBatch spriteBatch)
        {
            m_Animate.draw(spriteBatch);
        }
    }
}
