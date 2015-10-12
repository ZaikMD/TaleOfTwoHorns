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
    class StarManager : BaseManager
    {
        public List<StarItem> stars { get; private set; }
        Rectangle m_Destination = new Rectangle(0, 0, 20, 20);
        Rectangle m_SourceRectangle = new Rectangle(0, 0, 20, 20);

        public StarManager()
        {
            stars = new List<StarItem>();
        }

        public void addEnemy(Vector2 location)
        {
            stars.Add(new StarItem(location, m_SourceRectangle, m_Destination));
        }

        public void clear()
        {

            stars.Clear();
        }

        public void loadContent(ContentManager content)
        {
            for (int i = 0; i < stars.Count; i++)
            {
                stars[i].loadContent("Star", content);
            }
        }

        public void update(GameTime gameTime)
        {
            for (int i = 0; i < stars.Count; i++)
            {
                stars[i].update(gameTime);

                if(CollisionCheck.collisionCheck(stars[i].Destination, Player.collisionRectangle()))
                {
                    stars.RemoveAt(i);
                    Player.collectedStar(true);
                    Sounds.playSound("Pickup_Star", 0.5f);
                    
                }
            }
        }

        public bool collisionCheck(Rectangle player)
        {
            return false;
        }


        public void draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < stars.Count; i++)
            {
                stars[i].draw(spriteBatch);
            }
        }

    }
}
