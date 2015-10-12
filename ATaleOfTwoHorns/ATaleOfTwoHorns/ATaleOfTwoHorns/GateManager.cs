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
    class GateManager 
    {
        static ContentManager Content;
        public static List<Gate> gates { get; private set; }
        private static Rectangle m_Destination = new Rectangle(0, 0, 128, 180);
        private static Rectangle m_SourceRectangle = new Rectangle(0, 0, 128, 180);


        public GateManager()
        {
            gates = new List<Gate>();
        }

        public static  void addEnemy(Vector2 location)
        {
            gates.Add(new Gate(location, m_SourceRectangle, m_Destination));
        }

        public static void reset()
        {
            for(int i = 0; i < gates.Count; i++)
            {
                gates.Clear();
                GateManager.loadContent(Content);
            }
        }

        public static void loadContent(ContentManager content)
        {
            for (int i = 0; i < gates.Count; i++)
            {
                gates[i].loadContent("Gate", content);
            }
            Content = content;
        }

        public void update(GameTime gameTime)
        {
            for (int i = 0; i < gates.Count; i++)
            {
                gates[i].update(gameTime);
            }
        }

        public bool collisionCheck(Rectangle player)
        {
            return false;
        }


        public void draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < gates.Count; i++)
            {
                gates[i].draw(spriteBatch);
            }
        }
    }
}
