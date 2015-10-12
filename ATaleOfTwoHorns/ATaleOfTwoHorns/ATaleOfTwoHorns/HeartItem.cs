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
    class HeartItem : MovingObject
    {

        public HeartItem(Vector2 initialPosition, Rectangle spriteSourceRectangle, Rectangle spriteDestinationRectangle)
            : base(initialPosition, spriteSourceRectangle, spriteDestinationRectangle)
        {
            m_IsActive = true;
        }

        public override void loadContent(string spriteSheetName, ContentManager content)
        {
            base.loadContent(spriteSheetName, content);
            m_Animate.addAnimation(new Animation(16, new int[] { 0 }));
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);
        }

    }
}
