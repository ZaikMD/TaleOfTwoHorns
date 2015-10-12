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
    class Butterfly : FlyingMovingObject
    {
        Vector2 m_Direction = new Vector2();
        Random m_Random = new Random();

        float m_retreatingDistance = 60.0f;

        public Butterfly(Vector2 initialPosition, Rectangle spriteSourceRectangle, Rectangle spriteDestinationRectangle) :
            base(initialPosition, spriteSourceRectangle, spriteDestinationRectangle)
        {

        }
        public void activate(Vector2 position)
        {
            IsActive = true;
            m_Position = position;
            m_Direction = new Vector2((m_Random.Next(100) - 50), (m_Random.Next(50) * -1));
        }

        public override void loadContent(string spriteSheetName, Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.loadContent(spriteSheetName, content);
            m_Animate.addAnimation(new Animation(32, new int[] { 0, 1, 2, 3 }));
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.Milliseconds / 1000;
            MouseState mousePosition = Mouse.GetState();
             
            Vector2 distance = new Vector2(m_Position.X - mousePosition.X, m_Position.Y - mousePosition.Y);
         

            if (distance.Length() <= m_retreatingDistance)
            {
                distance.Normalize();
                m_Position.Y += distance.Y * m_Speed * elapsedTime;
                m_Position.X += distance.X * m_Speed * elapsedTime;
            }

            base.update(gameTime);
        }



        public override void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);
        }
    }
}
