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

//created by Joe Burchill

//Added November 28,2013

namespace ATaleOfTwoHorns
{
    class Input
    {
        //Added November 28,2013
        #region Fields
        public bool m_WasMoveLeftButtonDown = false;
        public bool m_WasMoveRightButtonDown = false;
        public bool m_WasMoveCrossLeftButtonDown = false;
        public bool m_WasMoveCrossRightButtonDown = false;
        public bool m_WasMoveCrossUpButtonDown = false;
        public bool m_WasMoveCrossDownButtonDown = false;
        public bool m_WasJumpButtonDown = false;
        public bool m_WasAttackButtonDown = false;
        public bool m_WasRainbowButtonDown = false;

        #endregion

        #region Methods

        //Added November 28,2013
        public void reset()
        {
            m_WasMoveLeftButtonDown = false;
            m_WasMoveRightButtonDown = false;
            m_WasMoveCrossLeftButtonDown = false;
            m_WasMoveCrossRightButtonDown = false;
            m_WasMoveCrossUpButtonDown = false;
            m_WasMoveCrossDownButtonDown = false;
            m_WasJumpButtonDown = false;
            m_WasAttackButtonDown = false;
            m_WasRainbowButtonDown = false;
        }

        //Added November 28,2013
        #region Controller Input
        public void getControllerInput(GamePadState gamePadState)
        {
            reset();

            if (gamePadState.IsButtonUp(Buttons.DPadLeft))
            {
                m_WasMoveLeftButtonDown = false;
            }
            else if (gamePadState.IsButtonUp(Buttons.DPadRight))
            {
                 m_WasMoveRightButtonDown = false;
            }

            if (gamePadState.IsButtonUp(Buttons.RightThumbstickLeft))
            {
                m_WasMoveCrossLeftButtonDown = false;
            }
            else if(gamePadState.IsButtonUp(Buttons.RightThumbstickRight))
            {
                m_WasMoveCrossRightButtonDown = false;
            }

            if (gamePadState.IsButtonUp(Buttons.RightThumbstickUp))
            {
                m_WasMoveCrossUpButtonDown = false;
            }
            else if (gamePadState.IsButtonUp(Buttons.RightThumbstickDown))
            {
                m_WasMoveCrossDownButtonDown = false;
            }

            if (gamePadState.IsButtonUp(Buttons.A))
            {
                m_WasJumpButtonDown = false;
            }

            if (gamePadState.IsButtonUp(Buttons.RightStick))
            {
                m_WasRainbowButtonDown = false;
            }

            if (gamePadState.IsButtonUp(Buttons.RightTrigger))
            {
                m_WasAttackButtonDown = false;
            }

            if (gamePadState.IsButtonDown(Buttons.DPadLeft) || gamePadState.IsButtonDown(Buttons.LeftThumbstickLeft))
            {
                m_WasMoveLeftButtonDown = true;
            }
            else if (gamePadState.IsButtonDown(Buttons.DPadRight) || gamePadState.IsButtonDown(Buttons.LeftThumbstickRight))
            {
                m_WasMoveRightButtonDown = true;
            }

            if (gamePadState.IsButtonDown(Buttons.RightThumbstickLeft))
            {
                m_WasMoveCrossLeftButtonDown = true;
            }
            else if (gamePadState.IsButtonDown(Buttons.RightThumbstickRight))
            {
                m_WasMoveCrossRightButtonDown = true;
            }

            if (gamePadState.IsButtonDown(Buttons.RightThumbstickUp))
            {
                m_WasMoveCrossUpButtonDown = true;
            }
            else if (gamePadState.IsButtonDown(Buttons.RightThumbstickDown))
            {
                m_WasMoveCrossDownButtonDown = true;
            }

            if (gamePadState.IsButtonDown(Buttons.A))
            {
                m_WasJumpButtonDown = true;
            }

            if (gamePadState.IsButtonDown(Buttons.RightStick))
            {
                m_WasRainbowButtonDown = true;
            }

            if (gamePadState.IsButtonDown(Buttons.RightTrigger))
            {
                m_WasAttackButtonDown = true;
            }
        }
        #endregion

        #region Keyboard Input
        //Added November 28,2013
        public void getKeyBoardInput(KeyboardState keyboardState, MouseState mouseState)
        {
            reset();

            if (keyboardState.IsKeyUp(Keys.A))
            {
                m_WasMoveLeftButtonDown = false;
            }
            else if (keyboardState.IsKeyUp(Keys.D))
            {
                m_WasMoveRightButtonDown = false;
            }

            if (keyboardState.IsKeyUp(Keys.Left))
            {
                m_WasMoveCrossLeftButtonDown = false;
            }
            else if (keyboardState.IsKeyUp(Keys.Right))
            {
                m_WasMoveCrossRightButtonDown = false;
            }

            if (keyboardState.IsKeyUp(Keys.Up))
            {
                m_WasMoveCrossUpButtonDown = false;
            }
            else if (keyboardState.IsKeyUp(Keys.Down))
            {
                m_WasMoveCrossDownButtonDown = false;
            }

            if (keyboardState.IsKeyUp(Keys.W))
            {
                m_WasJumpButtonDown = false;
            }

            if (keyboardState.IsKeyUp(Keys.LeftShift))
            {
                m_WasRainbowButtonDown = false;
            }

            if (keyboardState.IsKeyUp(Keys.Space))
            {
                m_WasAttackButtonDown = false;
            }


            if (keyboardState.IsKeyDown(Keys.A))
            {
                m_WasMoveLeftButtonDown = true;
            }
            else if (keyboardState.IsKeyDown(Keys.D))
            {
                m_WasMoveRightButtonDown = true;
            }

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                m_WasMoveCrossLeftButtonDown = true;
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                m_WasMoveCrossRightButtonDown = true;
            }

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                m_WasMoveCrossUpButtonDown = true;
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                m_WasMoveCrossDownButtonDown = true;
            }

            if (keyboardState.IsKeyDown(Keys.W))
            {
                m_WasJumpButtonDown = true;
            }

            if (keyboardState.IsKeyDown(Keys.LeftShift))
            {
                m_WasRainbowButtonDown = true;
            }

            if (keyboardState.IsKeyDown(Keys.Space))
            {
                m_WasAttackButtonDown = true;
            }
        }
        #endregion
        #endregion


    }
}
