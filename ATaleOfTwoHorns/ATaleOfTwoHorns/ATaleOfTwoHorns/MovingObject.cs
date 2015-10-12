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
    /// <summary>
    /// base class for a moveing object
    /// </summary>
    abstract class MovingObject : BaseObjectWithGravity
    {       
        protected float m_Speed = 200;
        protected MovementValue m_HorizontalDirection = MovementValue.NONE;
        protected MovementValue m_VerticalDirection = MovementValue.NONE;

        public MovingObject(Vector2 initalPosition, Rectangle spriteSourceRectangle, Rectangle spriteDestinationRectangle) :
            base(initalPosition, spriteSourceRectangle, spriteDestinationRectangle)
        {

        }

        public MovementValue Horizonatal
        {
            set { m_HorizontalDirection = value; }
        }

        public MovementValue Vertical
        {
            set { m_VerticalDirection = value; }
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.Milliseconds/1000;

            m_Position.Y += m_Speed * (int)m_VerticalDirection * elapsedTime;
            m_Position.X += m_Speed * (int)m_HorizontalDirection * elapsedTime;

            base.update(gameTime);
        }
    }
}
