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
    class HeartManager : BaseManager
    {
        static ContentManager Content;
        public List<HeartItem> hearts { get; private set; }
        Rectangle m_Destination = new Rectangle(0,0,29,27);
        Rectangle m_SourceRectangle = new Rectangle(0, 0, 29, 27);


        public HeartManager()
        {
            hearts = new List<HeartItem>();
        }

        public void addEnemy(Vector2 location)
        {
            hearts.Add(new HeartItem(location, m_SourceRectangle, m_Destination));
        }

        public void clear()
        {
            hearts.Clear();
        }

        public void loadContent(ContentManager content)
        {
            for(int i = 0; i < hearts.Count; i++)
            {
                hearts[i].loadContent("HeartDrop", content);
            }
            Content = content;
        }

        public void update(GameTime gameTime)
        {
            for (int i = 0; i < hearts.Count; i++)
            {
                hearts[i].update(gameTime);

                if(CollisionCheck.collisionCheck(hearts[i].Destination, Player.collisionRectangle()))
                {
                    hearts.RemoveAt(i);
                    Sounds.playSound("Pickup_Heart", 1);
                    Player.collectedHeart(true);
                    
                }
            }
        }

        public bool collisionCheck(Rectangle player)
        {
            return false;
        }


        public void draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < hearts.Count; i++)
            {
                hearts[i].draw(spriteBatch);
            }
        }

        

    }
}
