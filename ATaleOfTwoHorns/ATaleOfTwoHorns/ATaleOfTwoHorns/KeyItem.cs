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
//Made by zach dubuc
namespace ATaleOfTwoHorns
{
    class KeyItem : FlyingMovingObject
    {
        float m_MaxHeight;
        float m_MinHeight;
        float m_MoveSpeed = 0.5f;
        bool m_HasBeenPickedUp = false;
        const float m_HeightModifier = 20.0f;



        public KeyItem(Vector2 initialPosition, Rectangle spriteSourceRectangle, Rectangle spriteDestinationRectangle)
            : base(initialPosition, spriteSourceRectangle, spriteDestinationRectangle)
        {
            m_IsActive = true;
            m_MaxHeight = m_Position.Y - m_HeightModifier;
            m_MinHeight = m_Position.Y + m_HeightModifier;
        }

        public override void loadContent(string spriteSheetName, ContentManager content)
        {
            base.loadContent(spriteSheetName, content);
            m_Animate.addAnimation(new Animation(16, new int[] { 0 }));
        }

        public override void update(GameTime gameTime)
        {
            m_Position.Y += m_MoveSpeed; //This makes the key float up and down, preeettty nifty
            if (m_Position.Y >= m_MinHeight)
            {
                m_MoveSpeed *= -1;
            }
            if (m_Position.Y <= m_MaxHeight)
            {
                m_MoveSpeed *= -1;
            }

            base.update(gameTime);
            Console.WriteLine(Destination);
            
            
        }

        public CollisionType keyCollision(Rectangle playerRect)
        {
            CollisionType collision = new CollisionType();
            if (CollisionCheck.collisionCheck(playerRect, Destination, ref collision))
            {
                //TODO::add type of object that player collided with
            }
             return collision;
        }


        public override void draw(SpriteBatch spriteBatch)
        {
            if (!m_HasBeenPickedUp)
            {
                base.draw(spriteBatch);
            }
        }
    }
}
