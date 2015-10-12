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
//Made by Zach dubuc
namespace ATaleOfTwoHorns
{
    class BackgroundManager
    {
        List<Background> m_Backgrounds = new List<Background>();

        public void addBackground(Background background)
        {
            m_Backgrounds.Add(background);
        }


        public void update(GameTime gameTime, Player player)
        {
            foreach (Background background in m_Backgrounds)
            {
                background.update(gameTime, player);
            }
        }
        public void draw(SpriteBatch spriteBatch, Camera camera)
        {
            for (int i = 0; i < m_Backgrounds.Count; i++)
            {
                m_Backgrounds[i].draw(spriteBatch, camera);
            }
        }

    }
}
