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
    class Gate : BaseObject
    {
        ScreenManager m_ScreenManager = new ScreenManager();

        public Gate(Vector2 initialPosition, Rectangle spriteSourceRectangle, Rectangle spriteDestinationRectangle)
            : base(initialPosition, spriteSourceRectangle, spriteDestinationRectangle)
        {
            m_IsActive = true;

        }

        public override void loadContent(string spriteSheetName, ContentManager content)
        {
            base.loadContent(spriteSheetName, content);
            m_Animate.addAnimation(new Animation(16, new int[] { 0 }));
        }

        public override void  update(GameTime gameTime)
        {
 	         base.update(gameTime);

             if(CollisionCheck.collisionCheck(Player.collisionRectangle(), Destination)) 
            {
                if (Player.m_hasKey == true)
                {
                    Player.m_hasKey = false;
                    m_ScreenManager.CurrentGameState = GameState.LevelComplete;
                    Player.reset();
                    Player.setInitialPosition();
                    Level.levelIncrement();
                    

                }
            }
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);
        }
        
    }
}
