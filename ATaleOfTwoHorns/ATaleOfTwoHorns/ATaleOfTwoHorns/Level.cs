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
using System.IO;

//Made by Zach Dubuc
namespace ATaleOfTwoHorns
{
    class Level
    {

        Texture2D m_TileSet;

        static ContentManager Content;

        public static int levelCount = 1;

        Rectangle m_SourceRectangle;
        private static Rectangle m_DestinationRectangle;

        private static int m_LevelNumber = 0;

        private static char[,] m_TileData;

        public static int getWidthOfArray()
        {            
            return m_TileData.GetLength(1);          
        }

        public static int getHeightOfArray()
        {
            return m_TileData.GetLength(0);
        }

        public Rectangle Destination
        {
            get { return m_DestinationRectangle; }
        }

        public Level( Rectangle sourceRectangle, Rectangle destinationRectangle)
        {

            m_SourceRectangle = sourceRectangle;
            m_DestinationRectangle = destinationRectangle;

        }

        public void loadContent(ContentManager content, string tileSetName, string tileSetData)
        {
            m_TileSet = content.Load<Texture2D>(tileSetName);
            parseData(tileSetData);
            BackgroundMusic.playSong(0, 0.30f);

            Content = content;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
           for (int i = 0; i < m_TileData.GetLength(0); i++)
           {
               for (int j = 0; j < m_TileData.GetLength(1); j++)
               {
                   m_SourceRectangle.X = characterCheck(i, j) * m_SourceRectangle.Width;
                   m_SourceRectangle.Y = m_LevelNumber * m_SourceRectangle.Height;

                   m_DestinationRectangle.X = j * m_DestinationRectangle.Width;
                   m_DestinationRectangle.Y = i * m_DestinationRectangle.Height;

                   spriteBatch.Draw(m_TileSet, m_DestinationRectangle, m_SourceRectangle, Color.White);
               }
           }
        }

        private static void parseData(string levelData)
        {
            string[] mapData = File.ReadAllLines(levelData);
            int width = mapData[0].Length;
            int height = mapData.Length;

            m_TileData = new char[height, width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {

                    if (enemyCheck(i, j, mapData[i][j].ToString()))
                        {
                            m_TileData[i, j] = 'a';
                        }
                        else
                        {
                            m_TileData[i, j] = char.Parse(mapData[i][j].ToString());
                        }
                    
                }
            }
        }

        public static void levelIncrement()
        {
            if (m_LevelNumber < 3)
            { 
                m_LevelNumber++;
                levelCount++;
                loadLevel(m_LevelNumber);
                BackgroundMusic.playSong(m_LevelNumber, 0.30f);
                if (levelCount >= 3)
                {
                    Background.loadContent(Content, 3, 1);
                }
                else
                {
                    Background.loadContent(Content, levelCount, 1);
                }
            }
        }

        public static void loadLevel(int levelNumber)
        {
            EnemyManager.clear();
            PickUpManager.clear();
            if (levelNumber != m_LevelNumber)
            {
                m_LevelNumber = levelNumber;
            }
            //TODO: call enemy manager clear
            BackgroundMusic.playSong(levelNumber, 0.30f);
            string level = "Content/Level" + levelNumber + ".txt";
             parseData(level);
             if (levelNumber >= 3)
             {
                 Background.loadContent(Content, 3, 1);
             }
             else
             {
                 Background.loadContent(Content, levelNumber + 1, 1);
             }
             if (levelNumber == 3)
             {
                 Boss.IsActive = true; ;
             }
             Player.reset();
             Player.setInitialPosition();
             EnemyManager.loadContent(Content);
             GateManager.reset();
             GateManager.loadContent(Content);
             PickUpManager.loadContent(Content);
             
        }


        public  char[,] getTileData()
        {
            return m_TileData;
        }

        private static bool isTileCollideable(int i, int j)
        {
            switch (m_TileData[i, j])
            {
                case 'a':
                    return false;
                case 'b':
                    return true;
                case 'c':
                    return true;
                case 'd':
                    return true;
                case 'e':
                    return true;
                case 'f':
                    return true;
                case 'g':
                    return true;
                case 'h':
                    return false;
                case 'i':
                    return false;
                case 'j':
                    return false;
                case 'k':
                    return false;
                case 'l':
                    return false;
                case 'm':
                    return false;
                case 'n':
                    return true;
                case 'o':
                    return true;
                case 'p':
                    return true;
                case 'q':
                    return true;
                default:
                    return false;
            }
        }
        //This is for the loading of the text file
        public int characterCheck(int i, int j)
        {
            switch (m_TileData[i, j])
            {
                case 'a':
                    return 0;
                case 'b':
                    return 1;
                case 'c':
                    return 2;
                case 'd':
                    return 3;
                case 'e':
                    return 4;
                case 'f':
                    return 5;
                case 'g':
                    return 6;
                case 'h':
                    return 7;
                case 'i':
                    return 8;
                case 'j':
                    return 9;
                case 'k':
                    return 10;
                case 'l':
                    return 11;
                case 'm':
                    return 12;
                case 'n':
                    return 13;
                case 'o':
                    return 14;
                case 'p':
                    return 15;
                case 'q':
                    return 16;
                default:
                    return 0;
            }

        }

        public static bool enemyCheck(int i, int j, string character)
        {
            Vector2 temp = new Vector2(m_DestinationRectangle.Width / 2, m_DestinationRectangle.Height / 2);
            switch (character)
            {
                case "0":
                    temp.Y += m_DestinationRectangle.Height * i;
                    temp.X += m_DestinationRectangle.Width * j;
                    EnemyManager.addEnemy(temp, EnemyType.BEES);
                    return true;
                case "1":
                    temp.Y += m_DestinationRectangle.Height * i;
                    temp.X += m_DestinationRectangle.Width * j;
                    EnemyManager.addEnemy(temp, EnemyType.SPIDER);
                    return true;
                case "2": 
                    temp.Y += m_DestinationRectangle.Height * i;
                    temp.X += m_DestinationRectangle.Width * j;
                    EnemyManager.addEnemy(temp, EnemyType.PIXIE);
                    return true;
                case "3":
                    temp.Y += m_DestinationRectangle.Height * i;
                    temp.X += m_DestinationRectangle.Width * j;
                    EnemyManager.addEnemy(temp, EnemyType.IMP);
                    return true;
                case "4":
                    temp.Y += m_DestinationRectangle.Height * i;
                    temp.X += m_DestinationRectangle.Width * j;
                    EnemyManager.addEnemy(temp, EnemyType.UNDEADUNICORN);
                    return true;
                case "6":
                    temp.Y += m_DestinationRectangle.Height * i;
                    temp.X += m_DestinationRectangle.Width * j;
                    PickUpManager.addEnemy(temp, PickUpType.STAR);
                    return true; 
                case "7":
                    temp.Y += m_DestinationRectangle.Height * i;
                    temp.X += m_DestinationRectangle.Width * j;
                    GateManager.addEnemy(temp);
                    return true;
                case "8":
                    temp.Y += m_DestinationRectangle.Height * i;
                    temp.X += m_DestinationRectangle.Width * j;
                    PickUpManager.addEnemy(temp, PickUpType.KEY);
                    return true;          
                case "9":
                    temp.Y += m_DestinationRectangle.Height * i;
                    temp.X += m_DestinationRectangle.Width * j;
                    PickUpManager.addEnemy(temp, PickUpType.HEART);
                    return true;
                case "!":
                    temp.Y += m_DestinationRectangle.Height * i;
                    temp.X += m_DestinationRectangle.Width * j;
                    EnemyManager.addEnemy(temp, EnemyType.BUNNY);
                    return true;
                case "@":
                    temp.Y += m_DestinationRectangle.Height * i;
                    temp.X += m_DestinationRectangle.Width * j;
                    EnemyManager.addEnemy(temp, EnemyType.BUTTERFLY);
                    return true;
                default:
                    return false;
                    
            }
        }

        public static CollisionType[,] backgroundCollisionCheck(Rectangle objectDestinationRectangle)
        {
            CollisionType[,] temp = new CollisionType[10, 10];

            Vector2 tempy = new Vector2(objectDestinationRectangle.X + objectDestinationRectangle.Width / 2, objectDestinationRectangle.Y + objectDestinationRectangle.Height / 2);

            int posX = (int)(tempy.X / 32);
            if (posX < 5)
            {
                posX = 5;
                
            }

            int posY = (int)(tempy.Y / 32);
            if (posY < 5)
            {
                posY = 5;
            }

            for (int i = 0; i < temp.GetLength(0); i++)
            {
                for (int j = 0; j < temp.GetLength(1); j++)
                {
                    temp[i, j] = new CollisionType();
                }
            }

            int x = 0;
            int y = 0;

            Rectangle tempRect = Rectangle.Empty;

            for (int i = posY - 5; i < m_TileData.GetLength(0) && i < posY + 5; i++)
            {
                for (int j = posX - 5; j < m_TileData.GetLength(1) && j < posX + 5; j++)
                {
                    if (Level.isTileCollideable(i, j) == true)
                    {
                        tempRect.Width = m_DestinationRectangle.Width;
                        tempRect.Height = m_DestinationRectangle.Height;

                        tempRect.X = j * m_DestinationRectangle.Width;
                        tempRect.Y = i * m_DestinationRectangle.Height;

                        if (j < (m_TileData.GetLength(1) - 1))
                        {
                            j++;
                            magicRectangleExtentionFunctionWithExtraRecursionForTheHellOfItButNotReallySinceWithoutThisRecursionThenTheCollisionWouldStillBeBrokenAndABrokenCollisionIsntAnyFunForAnyoneUnlessYoureAGhostSinceGhostsDontNeedCollisionButIfAGhostCouldCollideWithThingsThenSaidGhostWouldAlsoApreciateThisFunctionAndAllOfItsGlory(ref tempRect, i, ref j);
                        }

                        CollisionCheck.collisionCheck(objectDestinationRectangle, tempRect, ref temp[y, x]);

                    }
                    x++;
                }
                x = 0;
                y++;
            }
            return temp;
        }

        private static void magicRectangleExtentionFunctionWithExtraRecursionForTheHellOfItButNotReallySinceWithoutThisRecursionThenTheCollisionWouldStillBeBrokenAndABrokenCollisionIsntAnyFunForAnyoneUnlessYoureAGhostSinceGhostsDontNeedCollisionButIfAGhostCouldCollideWithThingsThenSaidGhostWouldAlsoApreciateThisFunctionAndAllOfItsGlory(ref Rectangle rect, int i, ref int j)
        {
            if (isTileCollideable(i, j) == true)
            {
                rect.Width += m_DestinationRectangle.Width;
                if (j < (m_TileData.GetLength(1) - 1))
                {
                    j++;
                    magicRectangleExtentionFunctionWithExtraRecursionForTheHellOfItButNotReallySinceWithoutThisRecursionThenTheCollisionWouldStillBeBrokenAndABrokenCollisionIsntAnyFunForAnyoneUnlessYoureAGhostSinceGhostsDontNeedCollisionButIfAGhostCouldCollideWithThingsThenSaidGhostWouldAlsoApreciateThisFunctionAndAllOfItsGlory(ref rect, i,ref j);
                }
            }
        }
    }
}
