using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


//Created by Adam Holloway


namespace ATaleOfTwoHorns
{

    class ScreenManager
    {
        #region MainMenu

        Button m_ButtonPlay;
        Button m_ButtonCredits;
        Button m_ButtonLevelSelect;
        Button m_ButtonExit;

        Texture2D m_MenuBG;
        Texture2D m_XButton;
        Texture2D m_YButton;
        Texture2D m_BackButton;
        Texture2D m_BButton;
        Texture2D m_AButton;

        #endregion

        #region LevelSelect
        Button m_LevelOne;
        Button m_LevelTwo;
        Button m_LevelThree;
        Button m_Boss;

        Texture2D m_LevelSelectScreen;
        #endregion

        #region Credits
        Button m_ButtonBack;
        Texture2D m_Credits;
        #endregion

        #region SplashScreen
        Button m_ButtonSpace;
        Texture2D m_StartButton;
        Texture2D m_SplashScreen;
        Color colour = new Color(255, 255, 255, 255);
        #endregion

        #region LevelComplete
        Button m_NextLevel;

        Texture2D m_LevelCompleteScreen;
        #endregion

        #region GameOver

        Texture2D m_Restart;
        Texture2D m_GameOver;

        #endregion

        #region PauseMenu

        Texture2D m_PauseMenu;
        Texture2D m_BResume;
        Texture2D m_SpaceResume;
        Texture2D m_EscapeExit;
        Texture2D m_BackExit;

        #endregion

        bool m_ButtonDown = false;
        bool m_Paused = false;
        bool m_PauseKeyDown = false;

        Texture2D m_OtherBG;

        int m_ScreenWidth = 1074, m_ScreenHeight = 768;

        private GameState currentGameState;

        public GameState CurrentGameState
        {
            get
            {
                return currentGameState;
            }
            set
            {
                currentGameState = value;
            }
        }

        public void LoadContent(ContentManager content, GraphicsDeviceManager graphics)
        {
            m_OtherBG = content.Load<Texture2D>("splashbg");

            #region SplashScreen
            m_SplashScreen = content.Load<Texture2D>("splash");
            m_StartButton = content.Load<Texture2D>("Start");
            m_ButtonSpace = new Button(content.Load<Texture2D>("SpaceButton"), graphics.GraphicsDevice);
            m_ButtonSpace.setSize(new Vector2(100, 20));
            m_ButtonSpace.setPosition(new Vector2(500, 727));
            #endregion

            #region MainMenu

            //m_MainMenu.LoadContent(content, graphics);

            m_MenuBG = content.Load<Texture2D>("mainmenu");

            m_ButtonPlay = new Button(content.Load<Texture2D>("StartButton"), graphics.GraphicsDevice);
            m_ButtonPlay.setSize(new Vector2(100, 20));
            m_ButtonPlay.setPosition(new Vector2(500, 300));

            m_ButtonCredits = new Button(content.Load<Texture2D>("CreditsButton"), graphics.GraphicsDevice);
            m_ButtonCredits.setSize(new Vector2(100, 20));
            m_ButtonCredits.setPosition(new Vector2(500, 330));

            m_ButtonLevelSelect = new Button(content.Load<Texture2D>("LevelSelectButton"), graphics.GraphicsDevice);
            m_ButtonLevelSelect.setSize(new Vector2(100, 20));
            m_ButtonLevelSelect.setPosition(new Vector2(500, 360));

            m_ButtonExit = new Button(content.Load<Texture2D>("Exit"), graphics.GraphicsDevice);
            m_ButtonExit.setSize(new Vector2(100, 20));
            m_ButtonExit.setPosition(new Vector2(970, 727));

            m_XButton = content.Load<Texture2D>("X");
            m_YButton = content.Load<Texture2D>("Y");
            m_BackButton = content.Load<Texture2D>("Back");
            m_BButton = content.Load<Texture2D>("B");
            m_AButton = content.Load<Texture2D>("A");

            #endregion

            #region Credits
            m_Credits = content.Load<Texture2D>("credits");

            m_ButtonBack = new Button(content.Load<Texture2D>("BackButton"), graphics.GraphicsDevice);
            m_ButtonBack.setSize(new Vector2(100, 20));
            m_ButtonBack.setPosition(new Vector2(487, 700));
            #endregion

            #region LevelSelect
            m_LevelSelectScreen = content.Load<Texture2D>("LevelSelect");

            m_LevelOne = new Button(content.Load<Texture2D>("LevelOne"), graphics.GraphicsDevice);
            m_LevelOne.setSize(new Vector2(150, 30));
            m_LevelOne.setPosition(new Vector2(150, 330));
            m_LevelTwo = new Button(content.Load<Texture2D>("LevelTwo"), graphics.GraphicsDevice);
            m_LevelTwo.setSize(new Vector2(150, 30));
            m_LevelTwo.setPosition(new Vector2(350, 330));
            m_LevelThree = new Button(content.Load<Texture2D>("LevelThree"), graphics.GraphicsDevice);
            m_LevelThree.setSize(new Vector2(150, 30));
            m_LevelThree.setPosition(new Vector2(550, 330));
            m_Boss = new Button(content.Load <Texture2D>("LevelBoss"), graphics.GraphicsDevice);
            m_Boss.setSize(new Vector2(150, 30));
            m_Boss.setPosition(new Vector2(750, 330));
            #endregion

            #region LevelComplete
            m_LevelCompleteScreen = content.Load<Texture2D>("LevelComplete");

            m_NextLevel = new Button(content.Load<Texture2D>("NextButton"), graphics.GraphicsDevice);
            m_NextLevel.setSize(new Vector2(150, 30));
            m_NextLevel.setPosition(new Vector2(468, 550));
            #endregion

            #region GameOver
            m_GameOver = content.Load<Texture2D>("gameover");
            m_Restart = content.Load<Texture2D>("Rrestart");
            #endregion

            #region PauseMenu

            m_PauseMenu = content.Load<Texture2D>("pause");
            m_BResume = content.Load<Texture2D>("BResume");
            m_SpaceResume = content.Load<Texture2D>("SpaceResume");
            m_EscapeExit = content.Load<Texture2D>("EscapeExit");
            m_BackExit = content.Load<Texture2D>("BackExit");

            #endregion
        }

        public void Update(GameTime gameTime, Game game)
        {
            MouseState mouse = Mouse.GetState();
            KeyboardState keyboard = Keyboard.GetState();
            GamePadState gamepad = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.None);

            switch (CurrentGameState)
            {
                case GameState.SplashScreen:
                    if (keyboard.IsKeyDown(Keys.Space) || m_ButtonSpace.isClicked == true || gamepad.IsButtonDown(Buttons.Start))
                    {
                        currentGameState = GameState.MainMenu;
                    }
                    m_ButtonSpace.Update(mouse);
                    break;

                case GameState.MainMenu:
                    if (m_ButtonPlay.isClicked == true || gamepad.IsButtonDown(Buttons.A) && m_ButtonDown == false)
                    {
                        m_ButtonDown = true;
                      //  Level.loadLevel(0);
                        
                        currentGameState = GameState.Game;
                    }
                    else if (gamepad.IsButtonUp(Buttons.A))
                    {
                        m_ButtonDown = false;
                    }
                    m_ButtonPlay.Update(mouse);
                    if (m_ButtonCredits.isClicked == true || gamepad.IsButtonDown(Buttons.X) && m_ButtonDown == false)
                    {
                        m_ButtonDown = true;
                        currentGameState = GameState.Credits;
                    }
                    else if (gamepad.IsButtonUp(Buttons.X))
                    {
                        m_ButtonDown = false;
                    }
                    m_ButtonCredits.Update(mouse);
                    if (m_ButtonLevelSelect.isClicked == true || gamepad.IsButtonDown(Buttons.Y) && m_ButtonDown == false)
                    {
                        m_ButtonDown = true;
                        currentGameState = GameState.LevelSelect;
                    }
                    else if (gamepad.IsButtonUp(Buttons.Y))
                    {
                        m_ButtonDown = false;
                    }
                    m_ButtonLevelSelect.Update(mouse);
                    if (m_ButtonExit.isClicked == true || gamepad.IsButtonDown(Buttons.Back) && m_ButtonDown == false)
                    {
                        m_ButtonDown = true;
                        game.Exit();
                    }
                    else if (gamepad.IsButtonUp(Buttons.Back))
                    {
                        m_ButtonDown = false;
                    }
                    m_ButtonExit.Update(mouse);
                    break;

                case GameState.Credits:
                    if (m_ButtonBack.isClicked == true || gamepad.IsButtonDown(Buttons.B) && m_ButtonDown == false)
                    {
                        m_ButtonDown = true;
                        CurrentGameState = GameState.MainMenu;
                    }
                    else if (gamepad.IsButtonDown(Buttons.B))
                    {
                        m_ButtonDown = false;
                    }
                    m_ButtonBack.Update(mouse);
                    break;

                case GameState.LevelSelect:
                    if (m_LevelOne.isClicked == true || gamepad.IsButtonDown(Buttons.A) && m_ButtonDown == false)
                    {
                        m_ButtonDown = true;
                        Level.loadLevel(0);
                        CurrentGameState = GameState.Game;
                    }
                    else if (gamepad.IsButtonDown(Buttons.A))
                    {
                        m_ButtonDown = false;
                    }
                    m_LevelOne.Update(mouse);
                    if (m_LevelTwo.isClicked == true || gamepad.IsButtonDown(Buttons.X) && m_ButtonDown == false)
                    {
                        m_ButtonDown = true;
                        Level.loadLevel(1);
                        CurrentGameState = GameState.Game;
                    }
                    m_LevelTwo.Update(mouse);
                    if (m_LevelThree.isClicked == true || gamepad.IsButtonDown(Buttons.Y) && m_ButtonDown == false)
                    {
                        m_ButtonDown = true;
                        Level.loadLevel(2);
                        CurrentGameState = GameState.Game;
                    }
                    m_LevelThree.Update(mouse);
                    if (m_ButtonBack.isClicked == true)
                    {
                        //m_ButtonDown = false;
                        CurrentGameState = GameState.MainMenu;
                    }
                    m_ButtonBack.Update(mouse);
                    if (m_Boss.isClicked == true || gamepad.IsButtonDown(Buttons.B) && m_ButtonDown == false)
                    {
                        m_ButtonDown = true;
                        Level.loadLevel(3);
                        CurrentGameState = GameState.Game;
                    }
                    m_Boss.Update(mouse);
                    break;

                case GameState.LevelComplete:
                    if (m_NextLevel.isClicked == true)
                    {
                        CurrentGameState = GameState.Game;
                    }
                    m_NextLevel.Update(mouse);
                    break;

                case GameState.GameOver:
                    if (keyboard.IsKeyDown(Keys.Escape) || gamepad.IsButtonDown(Buttons.B) && m_ButtonDown == false)
                    {
                        m_ButtonDown = true;
                        CurrentGameState = GameState.MainMenu;
                    }
                    if (keyboard.IsKeyDown(Keys.R) && m_ButtonDown == false || gamepad.IsButtonDown(Buttons.B) && m_ButtonDown == false)
                    {
                        m_ButtonDown = true;
                        CurrentGameState = GameState.Game;
                        EndPause();
                    }
                    break;

                case GameState.PauseMenu:
                    if (keyboard.IsKeyDown(Keys.Escape) || gamepad.IsButtonDown(Buttons.Back) && m_ButtonDown == false)
                    {
                        Player.reset();
                        m_ButtonDown = true;
                        CurrentGameState = GameState.MainMenu;
                    }
                    else if (keyboard.IsKeyDown(Keys.Escape))
                    {
                        m_ButtonDown = false;
                    }
                    else if (gamepad.IsButtonDown(Buttons.Back))
                    {
                        m_ButtonDown = false;
                    }
                    if (keyboard.IsKeyDown(Keys.Space) || gamepad.IsButtonDown(Buttons.B) && m_ButtonDown == false)
                    {
                        m_ButtonDown = true;
                        CurrentGameState = GameState.Game;
                    }
                    break;

                case GameState.Game:
                    if (keyboard.IsKeyDown(Keys.P) || gamepad.IsButtonDown(Buttons.Start) && m_ButtonDown == false)
                    {
                        m_ButtonDown = true;
                        CurrentGameState = GameState.PauseMenu;
                        BeginPause();
                    }
                    break;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            GamePadState gamepad = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.None);

            switch (CurrentGameState)
            {
                case GameState.SplashScreen:
                    spriteBatch.Draw(m_OtherBG, new Rectangle(0, 0, m_ScreenWidth, m_ScreenHeight), Color.White);
                    spriteBatch.Draw(m_SplashScreen, new Rectangle(0, 0, m_ScreenWidth, m_ScreenHeight), colour);
                    if (gamepad.IsConnected)
                    {
                        spriteBatch.Draw(m_StartButton, new Vector2(526, 580), new Rectangle(0, 0, 32, 16), Color.White);
                    }
                    else
                    {
                        m_ButtonSpace.Draw(spriteBatch);
                    }
                    break;

                case GameState.MainMenu:
                    spriteBatch.Draw(m_MenuBG, new Rectangle(0, 0, m_ScreenWidth, m_ScreenHeight), Color.White);
                    m_ButtonPlay.Draw(spriteBatch);
                    m_ButtonCredits.Draw(spriteBatch);
                    m_ButtonLevelSelect.Draw(spriteBatch);
                    m_ButtonExit.Draw(spriteBatch);

                    if (gamepad.IsConnected)
                    {
                        //468
                        //938
                        spriteBatch.Draw(m_AButton, new Vector2(465, 301), new Rectangle(0, 0, 16, 16), Color.White);
                        spriteBatch.Draw(m_XButton, new Vector2(465, 332), new Rectangle(0, 0, 16, 16), Color.White);
                        spriteBatch.Draw(m_YButton, new Vector2(465, 362), new Rectangle(0, 0, 16, 16), Color.White);
                        spriteBatch.Draw(m_BackButton, new Vector2(930, 729), new Rectangle(0, 0, 32, 16), Color.White);
                    }
                    break;

                case GameState.Credits:
                    spriteBatch.Draw(m_Credits, new Rectangle(0, 0, m_ScreenWidth, m_ScreenHeight), Color.White);
                    m_ButtonBack.Draw(spriteBatch);

                    if (gamepad.IsConnected)
                    {
                        spriteBatch.Draw(m_BButton, new Vector2(473, 452), new Rectangle(0, 0, 16, 16), Color.White);
                    }
                    break;

                case GameState.LevelSelect:
                    spriteBatch.Draw(m_LevelSelectScreen, new Rectangle(0, 0, m_ScreenWidth, m_ScreenHeight), Color.White);
                    m_LevelOne.Draw(spriteBatch);
                    m_LevelTwo.Draw(spriteBatch);
                    m_LevelThree.Draw(spriteBatch);
                    m_ButtonBack.Draw(spriteBatch);
                    m_Boss.Draw(spriteBatch);

                    if (gamepad.IsConnected)
                    {
                        spriteBatch.Draw(m_AButton, new Vector2(125, 337), new Rectangle(0, 0, 16, 16), Color.White);
                        spriteBatch.Draw(m_XButton, new Vector2(325, 337), new Rectangle(0, 0, 16, 16), Color.White);
                        spriteBatch.Draw(m_YButton, new Vector2(525, 337), new Rectangle(0, 0, 16, 16), Color.White);
                        spriteBatch.Draw(m_BButton, new Vector2(725, 337), new Rectangle(0, 0, 16, 16), Color.White);
                    }
                    break;

                case GameState.LevelComplete:
                    spriteBatch.Draw(m_LevelCompleteScreen, new Rectangle(0, 0, m_ScreenWidth, m_ScreenHeight), Color.White);
                    m_NextLevel.Draw(spriteBatch);
                    break;

                case GameState.GameOver:
                    spriteBatch.Draw(m_PauseMenu, new Rectangle(0, 0, m_ScreenWidth, m_ScreenHeight), Color.White);

                    if (gamepad.IsConnected)
                    {
                        spriteBatch.Draw(m_BResume, new Vector2(320, 500), new Rectangle(0, 0, 400, 52), Color.White);
                        spriteBatch.Draw(m_BackExit, new Vector2(320, 600), new Rectangle(0, 0, 400, 52), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(m_Restart, new Vector2(300, 500), new Rectangle(0, 0, 500, 52), Color.White);
                        spriteBatch.Draw(m_EscapeExit, new Vector2(325, 600), new Rectangle(0, 0, 446, 52), Color.White);
                    }
                    break;

                case GameState.PauseMenu:
                    spriteBatch.Draw(m_PauseMenu, new Rectangle(0, 0, m_ScreenWidth, m_ScreenHeight), Color.White);

                    if (gamepad.IsConnected)
                    {
                        spriteBatch.Draw(m_BResume, new Vector2(320, 500), new Rectangle(0, 0, 400, 52), Color.White);
                        spriteBatch.Draw(m_BackExit, new Vector2(320, 600), new Rectangle(0, 0, 400, 52), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(m_SpaceResume, new Vector2(300, 500), new Rectangle(0, 0, 500, 52), Color.White);
                        spriteBatch.Draw(m_EscapeExit, new Vector2(325, 600), new Rectangle(0, 0, 446, 52), Color.White);
                    }


                    break;

                case GameState.Game:

                    break;
            }
        }

        private void BeginPause()
        {
            m_Paused = true;
        }

        private void EndPause()
        {
            m_Paused = false;
        }
    }
}
