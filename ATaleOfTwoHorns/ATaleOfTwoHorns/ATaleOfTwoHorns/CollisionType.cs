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

//created by Kris Matis

namespace ATaleOfTwoHorns
{
    /// <summary>
    /// will be used to store various bits of
    /// information on a collision 
    /// </summary>
    /// 
    class CollisionType
    {
        public bool m_WasThereACollision = false;
        public Rectangle m_OverlapArea = Rectangle.Empty;

        public SideCollided m_FirstObjectSideCollided = SideCollided.NONE;
        public SideCollided m_SecondObjectSideCollided = SideCollided.NONE;

        public void clearData()
        {
            m_WasThereACollision = false;
            m_OverlapArea = Rectangle.Empty;
            m_FirstObjectSideCollided = SideCollided.NONE;
            m_SecondObjectSideCollided = SideCollided.NONE;
        }        
    }
}
