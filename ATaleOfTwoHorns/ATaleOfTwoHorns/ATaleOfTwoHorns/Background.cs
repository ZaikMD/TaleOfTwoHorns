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
    class Background
    {
        const int layer1Y = -550;
        const int layer2Y = 100;
        const int layer3Y = 200;
        const int layer4Y = 900;

        const float Layer1Speed = 0.15f;
        const float Layer2Speed = 0.25f;
        const float Layer3Speed = 0.50f;
        const float Layer4Speed = 0.75f;


        public Vector2 Parallax1 { get; set; }
        public Vector2 Parallax2 { get; set; }
        public Vector2 Parallax3 { get; set; }
        public Vector2 Parallax4 { get; set; }
        public List<Layer> layers { get; private set; }





        private static Layer[] m_Layers;


        public Background(Camera camera)
        {

            Parallax1 = Vector2.One;
            Parallax2 = Vector2.One;
            Parallax3 = Vector2.One;
            Parallax4 = Vector2.One;

            layers = new List<Layer>();
            m_Layers = new Layer[4];

            for (int i = 0; i < m_Layers.Length; i++)
            {
                m_Layers[i] = new Layer();
            }
        }

        public static  void loadContent(ContentManager content, int levelNumber, int layerNumber)
        {
           
            m_Layers[0].loadContent(content, levelNumber + "PB" + (layerNumber + 3), new Vector2(0, layer1Y), Layer1Speed);
            m_Layers[1].loadContent(content, levelNumber + "PB" + (layerNumber+2), new Vector2(0, layer2Y), Layer2Speed);
            m_Layers[2].loadContent(content, levelNumber + "PB" + (layerNumber+1), new Vector2(0, layer3Y), Layer3Speed);
            m_Layers[3].loadContent(content, levelNumber + "PB" + layerNumber, new Vector2(0, layer4Y), Layer4Speed);

            m_Layers[0].resetPositions(new Vector2(0, layer1Y));
            m_Layers[1].resetPositions(new Vector2(0, layer2Y));
            m_Layers[2].resetPositions(new Vector2(0, layer3Y));
            m_Layers[3].resetPositions(new Vector2(0, layer4Y));

        }
        public void update(GameTime gameTime, Player player)
        {
            for (int i = 0; i < m_Layers.Length; i++)
            {
                m_Layers[i].update(gameTime, player);
            }
        }

        public void draw(SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.getParallaxMatrix(Parallax1));
            m_Layers[0].draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.getParallaxMatrix(Parallax2));
            m_Layers[1].draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.getParallaxMatrix(Parallax3));
            m_Layers[2].draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.getParallaxMatrix(Parallax4));
            m_Layers[3].draw(spriteBatch);
            spriteBatch.End();
        }

        public void setParallaxes()
        {
            Parallax1 = new Vector2(Layer1Speed, Layer1Speed);
            Parallax2 = new Vector2(Layer2Speed, Layer2Speed);
            Parallax3 = new Vector2(Layer3Speed, Layer3Speed);
            Parallax4 = new Vector2(Layer4Speed, Layer4Speed);

        }

    }
}
