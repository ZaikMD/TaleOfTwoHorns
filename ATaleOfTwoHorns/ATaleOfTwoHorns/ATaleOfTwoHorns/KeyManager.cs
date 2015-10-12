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
    class KeyManager : BaseManager
    {
        public List<KeyItem> keys { get; private set; }
        Rectangle m_Destination = new Rectangle(0, 0, 44, 21);
        Rectangle m_SourceRectangle = new Rectangle(0, 0, 46, 21);


        public KeyManager()
        {
            keys = new List<KeyItem>();
        }

        public void addEnemy(Vector2 location)
        {
            keys.Add(new KeyItem(location, m_SourceRectangle, m_Destination));
        }

        public void clear()
        {
            keys.Clear();
        }

        public void loadContent(ContentManager content)
        {
            foreach (KeyItem key in keys)
            {
                key.loadContent("Key", content);
            }
        }

        public void update(GameTime gameTime)
        {
            for (int i = 0; i < keys.Count; i++)
            {
                keys[i].update(gameTime);
                if (CollisionCheck.collisionCheck(Player.collisionRectangle(), keys[i].Destination))
                {
                    Player.m_hasKey = true;
                    Player.collectedKey(true);
                    Sounds.playSound("Pickup_Key", 0.10f);
                    keys.RemoveAt(i);
                }
            }
        }

        public bool collisionCheck(Rectangle player)
        {
            return false;
        }


        public void draw(SpriteBatch spriteBatch)
        {
            foreach (KeyItem heart in keys)
            {
                heart.draw(spriteBatch);
            }
        }



    }

}
