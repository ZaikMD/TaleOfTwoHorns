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
    class Hud
    {
        // Player player = new Player();

        Texture2D health;
        private static Rectangle healthDestRect = new Rectangle(10, 10, 96, 32);
        private static Rectangle healthSourceRect = new Rectangle(0, 0, 96, 32);
        Rectangle healthFullWidth = new Rectangle(0, 0, 0, 32);

        Texture2D healthBase;
        Rectangle healthBaseDestRect = new Rectangle(10, 10, 96, 32);
        private static Rectangle healthBaseSourceRect = new Rectangle(0, 0, 96, 32);

        Texture2D Key;
        Rectangle keyDestRect = new Rectangle(720, 10, 33, 32);
        Rectangle keySourceRect = new Rectangle(0, 0, 33, 32);

        Texture2D KeyBase;
        Rectangle keyBaseDestRect = new Rectangle(720, 10, 33, 32);
        Rectangle keyBaseSourceRect = new Rectangle(0, 0, 33, 32);

        Texture2D StarOne;
        Rectangle starOneDestRect = new Rectangle(300, 10, 20, 20);
        Rectangle starOneSourceRect = new Rectangle(0, 0, 20, 20);

        Texture2D StarTwo;
        Rectangle starTwoDestRect = new Rectangle(320, 10, 20, 20);
        Rectangle starTwoSourceRect = new Rectangle(0, 0, 20, 20);

        Texture2D StarThree;
        Rectangle starThreeDestRect = new Rectangle(340, 10, 20, 20);
        Rectangle starThreeSourceRect = new Rectangle(0, 0, 20, 20);

        Texture2D StarBase;
        private static Rectangle starBaseDestRect = new Rectangle(300, 10, 60, 20);
        private static Rectangle starBaseSourceRect = new Rectangle(0, 0, 60, 20);

        Texture2D rainbowMeter;
        private static Rectangle rainbowDestRect = new Rectangle(960, 10, 94, 20);
        private static Rectangle rainbowSourceRect = new Rectangle(0, 0, 94, 20);

        Texture2D rainbowMeterBase;
        Rectangle rainbowBaseDestRect = new Rectangle(960, 10, 94, 20);
        private static Rectangle rainbowBaseSourceRect = new Rectangle(0, 0, 94, 20);

        private static short m_StarGet;

        public Texture2D HealthSprite
        {
            get { return health; }
            set { health = value; }
        }

        public Texture2D LaserCharge
        {
            get { return KeyBase; }
            set { KeyBase = value; }
        }

        public Texture2D RainbowMeter
        {
            get { return rainbowMeter; }
            set { rainbowMeter = value; }
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void LoadContent(ContentManager Content)
        {
            health = Content.Load<Texture2D>("Hearts");
            healthBase = Content.Load<Texture2D>("HeartsBase");
            Key = Content.Load<Texture2D>("KeyHud-On");
            KeyBase = Content.Load<Texture2D>("KeyHud");
            StarOne = Content.Load<Texture2D>("Star");
            StarTwo = Content.Load<Texture2D>("Star");
            StarThree = Content.Load<Texture2D>("Star");
            StarBase = Content.Load<Texture2D>("StarHudBase");
            rainbowMeter = Content.Load<Texture2D>("RainbowMeterFill");
            rainbowMeterBase = Content.Load<Texture2D>("RainbowMeterBase");
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(rainbowMeterBase, rainbowBaseDestRect, rainbowBaseSourceRect, Color.White);
            spriteBatch.Draw(rainbowMeter, rainbowDestRect, rainbowSourceRect, Color.White);

            spriteBatch.Draw(healthBase, healthBaseDestRect, healthBaseSourceRect, Color.White);
            spriteBatch.Draw(health, healthDestRect, healthSourceRect, Color.White);

            spriteBatch.Draw(StarBase, starBaseDestRect, starBaseSourceRect, Color.White);

            if (Player.m_Stars >= 1)
            {
                spriteBatch.Draw(StarOne, starOneDestRect, starOneSourceRect, Color.White);
            }
            if (Player.m_Stars >= 2)
            {
                spriteBatch.Draw(StarTwo, starTwoDestRect, starTwoSourceRect, Color.White);
            }
            if (Player.m_Stars >= 3)
            {
                spriteBatch.Draw(StarThree, starThreeDestRect, starThreeSourceRect, Color.White);
            }

            if (Player.m_hasKey == false)
            {
                spriteBatch.Draw(KeyBase, keyBaseDestRect, keyBaseSourceRect, Color.White);
            }
            else if (Player.m_hasKey == true)
            {
                spriteBatch.Draw(Key, keyDestRect, keySourceRect, Color.White);
            }
        }

        public static void reduceHealth()
        {
            healthSourceRect.Width -= healthBaseSourceRect.Width / 6;
            healthDestRect.Width = healthSourceRect.Width; 
        }

        public static void increaseHealth()
        {
            healthSourceRect.Width += healthBaseSourceRect.Width / 6;
            healthDestRect.Width = healthSourceRect.Width; 
        }

        public static void resetHealth()
        {
            healthSourceRect.Width = healthBaseSourceRect.Width;
            healthDestRect.Width = healthSourceRect.Width;
        }

        public static void reduceLaser()
        {
            rainbowSourceRect.Width -= rainbowBaseSourceRect.Width / 6;
            rainbowDestRect.Width = rainbowSourceRect.Width;
        }

        public static void increaseLaser()
        {
            rainbowSourceRect.Width += rainbowBaseSourceRect.Width / 6;
            rainbowDestRect.Width = rainbowSourceRect.Width;
        }

        public static void resetLaser()
        {
            rainbowSourceRect.Width = rainbowBaseSourceRect.Width;
            rainbowDestRect.Width = rainbowSourceRect.Width;
        }
    }
}
