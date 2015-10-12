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
//Made by Zach Dubuc
namespace ATaleOfTwoHorns
{
    class LayerContent
    {
        Texture2D m_Texture;
        public Vector2 position;
        public const float speed = 5.0f;
        Rectangle m_Bounds = Rectangle.Empty;

        bool m_IsActive = false;

        public bool isActive
        {
            get
            {
                return m_IsActive;
            }

            set
            {
                m_IsActive = value;
            }
        }


        public void loadContent(ContentManager content, string texture)
        {
            m_Texture = content.Load<Texture2D>(texture);
            m_IsActive = false;
        }

        public Rectangle Bounds
        {
            get
            {
                return m_Bounds = new Rectangle((int)position.X, (int)position.Y, m_Texture.Width, m_Texture.Height);
            }
        }

        public Point centerPoint
        {
            get
            {
                return m_Bounds.Center;
            }
        }
        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(m_Texture, position, Color.White); 
        }
    }
}
