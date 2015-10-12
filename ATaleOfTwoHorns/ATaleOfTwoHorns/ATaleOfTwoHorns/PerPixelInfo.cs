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
    /// holds data for in perpixel collision
    /// lowers the amount of things that need 
    /// to be passed in to the perpixel collision function
    /// </summary>
    class PerPixelInfo
    {
        public Texture2D m_SpriteSheet;
        public Rectangle m_SourceRectangle;
        public Rectangle m_DestinationRectangle;
    }
}
