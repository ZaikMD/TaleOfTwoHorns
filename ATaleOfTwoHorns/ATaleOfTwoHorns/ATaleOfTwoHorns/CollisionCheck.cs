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
    /// used to check for collisions and record the collision data
    /// </summary>
    static class CollisionCheck
    {
        /// <summary>
        /// tells you if the two rectangles overlap
        /// </summary>
        /// <param name="object1"></param>
        /// <param name="object2"></param>
        /// <returns>bool</returns>
        public static bool collisionCheck(Rectangle object1, Rectangle object2)
        {
            //check if they intersect or not
            if(object1.Intersects(object2))
            {
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// the collision data you enter gets changed through
        /// pass by reference and will hold additional data about the collision
        /// </summary>
        /// <param name="rect1"></param>
        /// <param name="rect2"></param>
        /// <param name="collisonData"></param>
        /// <returns>bool</returns>
        public static bool collisionCheck(Rectangle rect1, Rectangle rect2, ref CollisionType collisonData)
        {
            collisonData.clearData();

            //get the overlap area
            Rectangle.Intersect(ref rect1, ref rect2, out collisonData.m_OverlapArea);

            //if the width and height are bigger than zero it means they collided
            if (collisonData.m_OverlapArea.Width > 0 && collisonData.m_OverlapArea.Height > 0)
            {
                collisonData.m_WasThereACollision = true;

                if (collisonData.m_OverlapArea.Width > collisonData.m_OverlapArea.Height)
                {
                    //either top or bottom collision

                    if (rect1.Bottom > rect2.Top && rect1.Center.Y < rect2.Center.Y)
                    {
                        collisonData.m_FirstObjectSideCollided = SideCollided.BOTTOM;
                        collisonData.m_SecondObjectSideCollided = SideCollided.TOP;
                    }
                    else if (rect1.Top < rect2.Bottom && rect1.Center.Y > rect2.Center.Y)
                    {
                        collisonData.m_FirstObjectSideCollided = SideCollided.TOP;
                        collisonData.m_SecondObjectSideCollided = SideCollided.BOTTOM;
                    }
                }
                else
                {
                    //left or right collision

                    if (rect1.Right > rect2.Left && rect1.Center.X < rect2.Center.X)
                    {
                        collisonData.m_FirstObjectSideCollided = SideCollided.RIGHT;
                        collisonData.m_SecondObjectSideCollided = SideCollided.LEFT;
                    }
                    else if (rect1.Left < rect2.Right && rect1.Center.X > rect2.Center.X)
                    {
                        collisonData.m_FirstObjectSideCollided = SideCollided.LEFT;
                        collisonData.m_SecondObjectSideCollided = SideCollided.RIGHT;
                    }
                }
                return true;
            }
            return false;
        }

        
        /// <summary>
        /// should be used only for player collision
        /// it only returns a bool so if you need more info
        /// then use the rectagle collision
        /// 
        /// i am not sure if this will work or if i just made something useless
        /// </summary>
        /// <param name="object1"></param>
        /// <param name="object2"></param>
        /// <returns>bool</returns>
        public static bool collisionCheck(PerPixelInfo object1, PerPixelInfo object2)
        {
            //based of the perpixel collision from mcguiggan

            //Getting Color data from Texture and assigning it to
            // an array as big as the number of pixels in the image
            Color[] fullBits1 = new Color[object1.m_SpriteSheet.Width * object1.m_SpriteSheet.Height];
            object1.m_SpriteSheet.GetData(fullBits1);

            Color[] bits1 = new Color[object1.m_SourceRectangle.Width * object1.m_SourceRectangle.Height];

            for (int x = object1.m_SourceRectangle.X; x < object1.m_SourceRectangle.X + object1.m_SourceRectangle.Width; x++)
            {
                for (int y = object1.m_SourceRectangle.Y; y < object1.m_SourceRectangle.Y + object1.m_SourceRectangle.Height; y++)
                {
                    bits1[(x - object1.m_SourceRectangle.X) + (y - object1.m_SourceRectangle.Y) * object1.m_SpriteSheet.Width] = 
                    fullBits1[(x - object1.m_SourceRectangle.X) + (y - object1.m_SourceRectangle.Y) * object1.m_SpriteSheet.Width];
                }
            }

            Color[] fullBits2 = new Color[object2.m_SpriteSheet.Width * object2.m_SpriteSheet.Height];
            object1.m_SpriteSheet.GetData(fullBits2);

            Color[] bits2 = new Color[object2.m_SourceRectangle.Width * object2.m_SourceRectangle.Height];

            for (int x = object2.m_SourceRectangle.X; x < object2.m_SourceRectangle.X + object2.m_SourceRectangle.Width; x++)
            {
                for (int y = object2.m_SourceRectangle.Y; y < object2.m_SourceRectangle.Y + object2.m_SourceRectangle.Height; y++)
                {
                    bits2[(x - object2.m_SourceRectangle.X) + (y - object2.m_SourceRectangle.Y) * object2.m_SpriteSheet.Width] =
                    fullBits2[(x - object2.m_SourceRectangle.X) + (y - object2.m_SourceRectangle.Y) * object2.m_SpriteSheet.Width];
                }
            }


            //Determine the left side of the collision rectangle
            int x1 = Math.Max(object1.m_DestinationRectangle.X, object2.m_DestinationRectangle.X);
            //Determine the right side of our collision rectangle
            int x2 = Math.Min(object1.m_DestinationRectangle.X + object1.m_SourceRectangle.Width,
                              object2.m_DestinationRectangle.X + object2.m_SourceRectangle.Width);
            //Determine the Top of the collision rectangle
            int y1 = Math.Max(object1.m_DestinationRectangle.Y, object2.m_DestinationRectangle.Y);
            //Determine the Bottom of our collision rectangle
            int y2 = Math.Min(object1.m_DestinationRectangle.Y + object1.m_SourceRectangle.Height,
                              object2.m_DestinationRectangle.Y + object2.m_SourceRectangle.Height);

            for (int y = y1; y < y2; y++)
            {
                for (int x = x1; x < x2; x++)
                {
                    Color a1 = bits1[(x - object1.m_DestinationRectangle.X) + (y - object1.m_DestinationRectangle.Y) * object1.m_SpriteSheet.Width];
                    Color b1 = bits2[(x - object2.m_DestinationRectangle.X) + (y - object2.m_DestinationRectangle.Y) * object2.m_SpriteSheet.Width];

                    if (a1.A != 0 && b1.A != 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
