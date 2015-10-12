using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace ATaleOfTwoHorns
{
    class Button
    {

        //Created by Adam Holloway

        Texture2D m_Texture;
        Vector2 m_Position;
        Rectangle m_Rectangle;

        Color colour = new Color(255, 255, 255, 255);

        bool down;
        public bool isClicked;

        public Vector2 m_Size;

        public Button(Texture2D texture, GraphicsDevice graphics)
        {
            m_Texture = texture;
        }

        public void Update(MouseState mouse)
        {
            m_Rectangle = new Rectangle((int)m_Position.X, (int)m_Position.Y, (int)m_Size.X, (int)m_Size.Y);

            Rectangle mouseRect = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRect.Intersects(m_Rectangle))
            {
                if (colour.A == 255) //If button is fully visible
                {
                    down = false;
                }
                if (colour.A == 0)
                {
                    down = true;
                }
                if (down)
                {
                    colour.A += 5;
                }
                else
                {
                    colour.A -= 5;
                }
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    isClicked = true;
                }
            }
            else if (colour.A < 255)
            {
                colour.A += 5;
                isClicked = false;
            }
        }

        public void setSize(Vector2 size)
        {
            m_Size = size;
        }

        public void setPosition(Vector2 position)
        {
            m_Position = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(m_Texture, m_Rectangle, colour);
        }
    }
}
