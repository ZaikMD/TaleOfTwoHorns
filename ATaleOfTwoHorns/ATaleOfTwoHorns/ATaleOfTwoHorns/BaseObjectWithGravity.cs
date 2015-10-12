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
    /// base object for an object with gravity
    /// </summary>
    abstract class BaseObjectWithGravity : BaseObject
    {
        protected float m_Gravity = 100.0f;

        public BaseObjectWithGravity(Vector2 initalPosition, Rectangle spriteSourceRectangle, Rectangle spriteDestinationRectangle) :
            base(initalPosition, spriteSourceRectangle, spriteDestinationRectangle)
        {

        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.Milliseconds/1000;

            m_Position.Y += m_Gravity * elapsedTime;

            base.update(gameTime);
        }
    }
}
