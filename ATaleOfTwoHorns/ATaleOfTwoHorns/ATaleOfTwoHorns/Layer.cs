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
    class Layer
    {

        enum LayerNames
        {
            A = 0,
            B,
            C
        }


        LayerContent[] layerContents;

        float m_ParallaxSpeed;



        public void loadContent(ContentManager content, string texture, Vector2 position, float parallaxSpeed)
        {

            m_ParallaxSpeed = parallaxSpeed;
            

            layerContents = new LayerContent[3];

            for (int i = 0; i < layerContents.Length; i++)
            {
                layerContents[i] = new LayerContent();
            }

            foreach (LayerContent layer in layerContents)
            {
                layer.loadContent(content, texture);
            }



            layerContents[(int)LayerNames.A].position = new Vector2(position.X - layerContents[(int)LayerNames.A].Bounds.Width, position.Y);
            layerContents[(int)LayerNames.B].position = position;
            layerContents[(int)LayerNames.C].position = new Vector2(layerContents[(int)LayerNames.A].Bounds.Width, position.Y);
        }

        public void resetPositions(Vector2 position)
        {
            layerContents[(int)LayerNames.A].position = new Vector2(position.X - layerContents[(int)LayerNames.A].Bounds.Width, position.Y);
            layerContents[(int)LayerNames.B].position = position;
            layerContents[(int)LayerNames.C].position = new Vector2(layerContents[(int)LayerNames.A].Bounds.Width, position.Y);
        }

        public void update(GameTime gameTime, Player player)
        {
            for (int i = 0; i < layerContents.Length; i++)
            {
                if (player.Destination.Center.X * m_ParallaxSpeed >= layerContents[i].Bounds.X && player.Destination.Center.X * m_ParallaxSpeed <= layerContents[i].Bounds.Right)
                {
                    layerContents[i].isActive = true;
                }
                else
                {
                    layerContents[i].isActive = false;
                }

            }

            if(layerContents[(int)LayerNames.A].isActive)
            {
                layerContents[(int)LayerNames.B].position.X = layerContents[(int)LayerNames.A].Bounds.Right;
                layerContents[(int)LayerNames.C].position.X = layerContents[(int)LayerNames.A].Bounds.Left - layerContents[(int)LayerNames.A].Bounds.Width;
            }

            if (layerContents[(int)LayerNames.B].isActive)
            {
                layerContents[(int)LayerNames.C].position.X = layerContents[(int)LayerNames.B].Bounds.Right;
                layerContents[(int)LayerNames.A].position.X = layerContents[(int)LayerNames.B].Bounds.Left - layerContents[(int)LayerNames.B].Bounds.Width;
            }

            if (layerContents[(int)LayerNames.C].isActive)
            {
                layerContents[(int)LayerNames.A].position.X = layerContents[(int)LayerNames.C].Bounds.Right;
                layerContents[(int)LayerNames.B].position.X = layerContents[(int)LayerNames.C].Bounds.Left - layerContents[(int)LayerNames.C].Bounds.Width;
            }   
        }

        public void draw(SpriteBatch spriteBatch)
        {
            foreach (LayerContent layer in layerContents)
            {
                layer.draw(spriteBatch);
            }
        }
    }
}
