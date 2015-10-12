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

namespace ATaleOfTwoHorns
{
    /// <summary>
    /// Player class, inherits from MovingObject, handles 
    /// the Player movement and attacks.
    /// </summary>
    class Player : MovingObject
    {
        //Added November 27,2013
        enum Facing
        {
            LEFT = -1,
            RIGHT = 1,
        }

        enum Animations
        {
            RUNNINGRIGHT = 0,
            RUNNINGLEFT,
            IDLERIGHT,
            IDLELEFT,
            HURTRIGHT,
            HURTLEFT,
            JUMPRIGHT,
            JUMPLEFT
        }

        #region Fields

        Rectangle m_LevelBounds;

        Texture2D m_Cross;
        
        Vector2 m_CrossPosition = new Vector2();
        Vector2 m_CrossThingy = new Vector2();

        const float CROSS_SPEED = 300.0f;

        LaserManager m_LaserManager = new LaserManager(Color.White, 3, 1.0f, new Rectangle(0, 0, Level.getWidthOfArray() * 32, Level.getHeightOfArray() * 32));

        private static Vector2 m_InitialPosition;

        //Hud related
        private static short m_Health = 0;
        private static float m_HealthTimer = 0.0f;
        const float HEALTH_DELAY_TIMER = 1.5f;

        //Added November 28,2013
        Input m_Input = new Input();

        //Added November 27,2013
        

        //Added November 27, 2013
        Particles m_Particles = new Particles(new Vector2(0, 0), Color.Navy, 60.0f, 15, 0.00f, new Vector2(0, 0), 10.0f, true, 255, 15, 1.0f, true);

        //Added November 26, 2013
        //Bools for States
        private static bool m_IsOnGround = true;
        private static bool m_IsJumping = false;

        bool m_CanShoot = true;
        float m_Shots = 6.0f;
        float m_LaserTimer = 0.0f;
        float m_RechargeTimer = 0.0f;
        const float TOTAL_RECHARGE_TIME = 30.0f;
        const float RECHARGE_DELAY_TIMER = 0.5f;

        //Static for Damage and HUD
        private static bool m_collectedHeart = false;
        private static bool m_collectedKey = false;
        public static bool m_hasKey = false;


        public static bool m_collectedStar = false;
        public static float m_Stars = 0;
        private static float m_StarTimer = 0.0f;
        const float STARS_DELAY_TIMER = 1.5f;

        //Constants 
        //Input
        //Added November 26,2013
        const float MOVE_STICK_SCALE = 1.0f;

        //Added November 27,2013
        const float MAX_SPEED = 450.0f;
        const float START_SPEED = 350.0f;

        const float MAX_SPEED_MOD = 1.5f;
        const float MIN_SPEED_MOD = 1.0f;
        float m_CurrentSpeedMod = MIN_SPEED_MOD;
        const float SPEED_MOD_INCREMENT = 0.1f;

        Facing m_Facing;
        Facing m_LastFacing;

        float m_Timer = 0.0f;
        const float m_SPEED_MOD_DELAY = 0.4f;

        const float JUMP_SPEED = 75.0f;
        const float JUMP_MOD = 16.0f;
        private static float m_JumpMod = JUMP_MOD;
        float m_JumpingIncrement = 0.55f;

        private static Rectangle m_collisionRectangle = new Rectangle();

        static Vector2 m_PlayerPosition = new Vector2();

        #endregion

        #region Properties

        public Vector2 Position
        {
            get { return m_PlayerPosition; }
            set { m_PlayerPosition = value; }
        }

        //Added November 26, 2013
        public float Speed
        {
            get { return m_Speed; }
            set { m_Speed = value; }
        }

        public bool IsOnGround
        {
            get { return m_IsOnGround; }
        }

        #endregion

        #region Constructor
        public Player(Vector2 initialPosition, Rectangle spriteSourceRectangle, Rectangle spriteDestinationRectangle) :
            base(initialPosition, spriteSourceRectangle, spriteDestinationRectangle)
        {
            //Added November 27, 2013
            m_Speed = START_SPEED;
            m_Particles.DominantDirection = m_PlayerPosition;
            m_PlayerPosition = initialPosition;
            m_IsActive = true;
            m_IsOnGround = true;
            m_Gravity = 350.0f;
            m_Health = 6;
        }
        #endregion

        #region Methods
        #region Load Content
        public void loadContent(ContentManager content)
        {
            //Added November 26, 2013
            m_Animate.loadContent("Unicorn", content);
            // m_Animate.addAnimation(new Animation(16, new int[] { 0 }));
            m_Animate.addAnimation(new Animation(16, new int[] { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 }));
            m_Animate.addAnimation(new Animation(16, new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }));
            m_Animate.addAnimation(new Animation(16, new int[] { 0 }));
            m_Animate.addAnimation(new Animation(16, new int[] { 0 }));
            m_Animate.addAnimation(new Animation(16, new int[] { 0,1 }));
            m_Animate.addAnimation(new Animation(16, new int[] { 0,1 }));
            m_Animate.addAnimation(new Animation(8, new int[] { 0,1,2,3 }));
            m_Animate.addAnimation(new Animation(8, new int[] { 0,1,2,3 }));
            //Add in animations

            m_Cross = content.Load<Texture2D>("cross");

            m_LaserManager.loadContent(content);

            m_LevelBounds = new Rectangle(0, 0, Level.getWidthOfArray() * 32, Level.getHeightOfArray() * 32);

            //Added November 27, 2013
            m_Particles.loadContent(content);
        }
        #endregion

        #region Update
        public override void update(GameTime gameTime)
        {
            m_LastFacing = m_Facing;

            float elapsedTime = (float)gameTime.ElapsedGameTime.Milliseconds / 1000;

            m_Timer += elapsedTime;

            m_HealthTimer += elapsedTime;

            m_StarTimer += elapsedTime;

            m_RechargeTimer += elapsedTime;

            //Added November 27, 2013
            Vector2 oldPos = new Vector2(m_PlayerPosition.X, m_PlayerPosition.Y);

            //Added November 26, 2013
            KeyboardState keyboardState = Keyboard.GetState();
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            MouseState mouseState = Mouse.GetState();
            CollisionType collisionType = new CollisionType();

            if (m_Health == 0)
            {
                instantDeath();             
            }

            if (m_HealthTimer < HEALTH_DELAY_TIMER)
            {
                //TODO: RED SCREEN GET HIT
            }

            crossState(gameTime);

            rechargeLaser(gameTime);

            m_LaserManager.update(gameTime);

            getInput(keyboardState, gamePadState, mouseState, gameTime);

            levelBounds();

            runState(gameTime);

            collectedHeart();

            collectedStars();

            jumpState(gameTime);

            m_Position = m_PlayerPosition;
            //Added November 27, 2013
            base.update(gameTime);
            m_PlayerPosition = m_Position;

            m_collisionRectangle = Destination;

            //Added November 27,2013
            m_Particles.update(gameTime);

            m_Particles.SpawnPoint = m_PlayerPosition;
            m_Particles.DominantDirection = new Vector2(oldPos.X - m_PlayerPosition.X, oldPos.Y - m_PlayerPosition.Y);

        }
        #endregion

        #region Reset
        public static void reset()
        {
            m_Health = 6;
            Hud.resetHealth();
            m_Stars = 0;
            Boss.reset();
        }
        #endregion

        #region Static Functions
        public static void setIsOnGround(bool isOnGround)
        {
            m_IsOnGround = isOnGround;
        }

        public static void setIsJumping(bool isJumping)
        {
            m_IsJumping = isJumping;
            m_JumpMod = JUMP_MOD;
        }

        public static void collectedHeart(bool collectedHeart)
        {
            m_collectedHeart = collectedHeart;
        }

        public static void takeDamage()
        {

            if (m_HealthTimer > HEALTH_DELAY_TIMER)
            {
                m_HealthTimer = 0;

                m_Health--;

                Sounds.playSound("UnicornHurt", 1);

                Hud.reduceHealth();
            }

        }

        public static void collectedKey(bool collectedKey)
        {
            m_collectedKey = collectedKey;
        }

        public static Rectangle collisionRectangle()
        {
            return m_collisionRectangle;
        }

        public static Vector2 setInitialPosition()
        {
            m_InitialPosition = new Vector2(200, 1000);
            m_PlayerPosition = m_InitialPosition;
            return m_InitialPosition;
        }

        public static void collectedStar(bool collectedStar)
        {
            m_collectedStar = collectedStar;
        }

        public static void instantDeath()
        {
            reset();
            setInitialPosition(); 
        }
        
        public void collectedStars()
        {
            if(m_collectedStar == true)
            {
                if (m_StarTimer > STARS_DELAY_TIMER)
                {
                    m_StarTimer = 0;

                    m_Stars++;

                    //Hud.increaseStar();

                    m_collectedStar = false;
                }
            }
        }

        public void collectedHeart()
        {
            if (m_collectedHeart == true)
            {
                if (m_HealthTimer > HEALTH_DELAY_TIMER)
                {
                    m_HealthTimer = 0;

                    m_Health+=2;

                    Hud.increaseHealth();
                    Hud.increaseHealth();

                    m_collectedHeart = false;
                }
            }
        }

        public void levelBounds()
        {
            if (m_PlayerPosition.Y > m_LevelBounds.Height)
            {
                instantDeath();
            }           

        }
        #endregion

        #region Movement States
        //Added November 27, 2013
        private void jumpState(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.Milliseconds / 1000;

            if (m_Input.m_WasJumpButtonDown == true)
            {
                if (m_IsJumping == true)
                {
                    m_PlayerPosition.Y -= JUMP_SPEED * m_JumpMod * elapsedTime;

                    m_JumpMod -= m_JumpingIncrement;
                                       
                    if (m_JumpMod < 0.0f)
                    {
                        m_IsJumping = false;
                        m_JumpMod = JUMP_MOD;
                    }
                }
            }
            else
            {
                m_IsJumping = false;
                m_JumpMod = JUMP_MOD;
            }
        }

        private void runState(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.Milliseconds / 1000;

            if (m_Input.m_WasMoveRightButtonDown == false && m_Input.m_WasMoveLeftButtonDown == false)
            {
                m_CurrentSpeedMod = MIN_SPEED_MOD;
            }

            if (m_LastFacing != m_Facing)
            {
                m_CurrentSpeedMod = MIN_SPEED_MOD;
            }

            if (m_Input.m_WasMoveRightButtonDown == true && m_PlayerPosition.X < m_LevelBounds.Width - 64)
            {
                m_PlayerPosition.X += m_Speed * m_CurrentSpeedMod * elapsedTime;

                if (m_Timer > m_SPEED_MOD_DELAY)
                {
                    m_Timer = 0;

                    m_CurrentSpeedMod += SPEED_MOD_INCREMENT;

                    if (m_CurrentSpeedMod > MAX_SPEED_MOD)
                    {
                        m_CurrentSpeedMod = MAX_SPEED_MOD;
                    }
                }
            }
            else if (m_Input.m_WasMoveLeftButtonDown == true && m_PlayerPosition.X > 64)
            {
                m_PlayerPosition.X -= m_Speed * m_CurrentSpeedMod * elapsedTime;

                if (m_Timer > m_SPEED_MOD_DELAY)
                {
                    m_Timer = 0;

                    m_CurrentSpeedMod += SPEED_MOD_INCREMENT;

                    if (m_CurrentSpeedMod > MAX_SPEED_MOD)
                    {
                        m_CurrentSpeedMod = MAX_SPEED_MOD;
                    }
                }
            }
        }

        private void crossState(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            float elapsedTime = (float)gameTime.ElapsedGameTime.Milliseconds / 1000;
            
            if (m_Input.m_WasMoveCrossLeftButtonDown == true)
            {
                m_CrossThingy.X -= CROSS_SPEED * elapsedTime;
            }
            else if (m_Input.m_WasMoveCrossRightButtonDown == true)
            {
                m_CrossThingy.X += CROSS_SPEED * elapsedTime;
            }
            
            if (m_Input.m_WasMoveCrossUpButtonDown == true)
            {
                m_CrossThingy.Y -= CROSS_SPEED * elapsedTime;
            }
            else if (m_Input.m_WasMoveCrossDownButtonDown == true)
            {
                m_CrossThingy.Y += CROSS_SPEED * elapsedTime;
            }

            if (m_CrossThingy.X < -150)
            {
                m_CrossThingy.X = -150;
            }
            else if (m_CrossThingy.X > 150)
            {
                m_CrossThingy.X = 150;
            }


            if (m_CrossThingy.Y < -150)
            {
                m_CrossThingy.Y = -150.0f;
            }
            else if (m_CrossThingy.Y > 150)
            {
                m_CrossThingy.Y = 150.0f;
            }

            m_CrossPosition = m_PlayerPosition + new Vector2(m_CrossThingy.X, m_CrossThingy.Y);
        }

        public void laserState(GameTime gameTime)
        {
            if (m_Facing == Facing.LEFT)
            {
                if (m_CrossThingy.X > 0)
                {
                    m_CanShoot = false;
                }
                else if (m_CrossThingy.X < 0 && m_Shots > 0)
                {
                    m_CanShoot = true;
                    if (m_RechargeTimer > RECHARGE_DELAY_TIMER)
                    {
                        m_RechargeTimer = 0;
                        m_LaserTimer = m_Shots / TOTAL_RECHARGE_TIME;
                        m_LaserManager.activateProjectile(m_PlayerPosition, new Vector2(m_CrossPosition.X - m_PlayerPosition.X + m_Cross.Width / 2, m_CrossPosition.Y - m_PlayerPosition.Y + m_Cross.Height / 2));
                        Sounds.playSound("Laser", 1);
                        m_Shots--;
                        Hud.reduceLaser();
                    }
                }
            }
            
            if (m_Facing == Facing.RIGHT)
            {
                if (m_CrossThingy.X < 0)
                {
                    m_CanShoot = false;
                }
                else if (m_CrossThingy.X > 0 && m_Shots > 0)
                {
                    m_CanShoot = true;
                    if (m_RechargeTimer > RECHARGE_DELAY_TIMER)
                    {
                        m_RechargeTimer = 0;
                        m_LaserTimer = m_Shots / TOTAL_RECHARGE_TIME;
                        m_LaserManager.activateProjectile(m_PlayerPosition, new Vector2(m_CrossPosition.X - m_PlayerPosition.X + m_Cross.Width / 2, m_CrossPosition.Y - m_PlayerPosition.Y + m_Cross.Height / 2));
                        Sounds.playSound("Laser", 1);
                        m_Shots--;
                        Hud.reduceLaser();
                    }
                }
            }
                       
        }

        public void rechargeLaser(GameTime gameTime)
        {
            if (m_Shots < 6)
            {
                if (m_RechargeTimer > RECHARGE_DELAY_TIMER)
                {
                    m_LaserTimer++;
                }
                if (m_LaserTimer > TOTAL_RECHARGE_TIME)
                {
                    m_LaserTimer = 0;
                    m_Shots++;
                    Hud.increaseLaser();
                }
            }
        }

        #endregion

        #region Input
        //Added November 26, 2013
        //Handles all the input for controller and keyboard
        private void getInput(KeyboardState keyboardState, GamePadState gamePadState, MouseState mouseState, GameTime gameTime)
        {
            //Horizontal movement
            float stickInput = gamePadState.ThumbSticks.Left.X * MOVE_STICK_SCALE;

            if (gamePadState.IsConnected)
            {

                m_Input.getControllerInput(gamePadState);

                if (m_Input.m_WasMoveRightButtonDown == true || m_Input.m_WasMoveLeftButtonDown == true)
                {
                    if (stickInput != 0.0f)
                    {
                        m_Speed = START_SPEED * Math.Abs(stickInput);
                    }
                    else
                    {
                        m_Speed = START_SPEED;
                    }
                }
            }
            else
            {
                m_Speed = START_SPEED;
                m_Input.getKeyBoardInput(keyboardState, mouseState);
            }
            #region Button Is Up
            if (m_Input.m_WasMoveLeftButtonDown == false && m_Input.m_WasMoveRightButtonDown == false && m_Input.m_WasJumpButtonDown ==false)
            {
                m_Particles.Stop = true;
                if (m_LastFacing == Facing.RIGHT)
                {
                    m_Facing = Facing.RIGHT;
                    m_Animate.changAnimation((int)Animations.IDLERIGHT);
                }
                else if (m_LastFacing == Facing.LEFT)
                {
                    m_Facing = Facing.LEFT;
                    m_Animate.changAnimation((int)Animations.IDLELEFT);
                }
            }

            if (m_Input.m_WasJumpButtonDown == false)
            {
                m_IsJumping = false;

                if (m_Facing == Facing.LEFT)
                {
                    //m_Animate.changAnimation((int)Facing.LEFT);
                }
                else if (m_Facing == Facing.RIGHT)
                {
                    //m_Animate.changAnimation((int)Facing.RIGHT);
                }
            }

            if (m_Input.m_WasRainbowButtonDown == false)
            {
                //TODO: Rainbow Teleport logic
            }

            if (m_Input.m_WasAttackButtonDown == false)
            {
                //TODO: Laser Projectile logic
            }
            #endregion

            #region Button Is Down
            if (m_Input.m_WasMoveLeftButtonDown == true && m_Input.m_WasJumpButtonDown == false)
            {
                //Added November 27, 2013
                m_Particles.Stop = false;

                m_Facing = Facing.LEFT;
                m_Animate.changAnimation((int)Animations.RUNNINGLEFT);
            }
            else if (m_Input.m_WasMoveRightButtonDown == true && m_Input.m_WasJumpButtonDown == false)
            {
                //Added November 27, 2013
                m_Particles.Stop = false;

                m_Facing = Facing.RIGHT;
                m_Animate.changAnimation((int)Animations.RUNNINGRIGHT);
            }

            if (m_Input.m_WasJumpButtonDown == true && m_Input.m_WasMoveLeftButtonDown == false && m_Input.m_WasMoveRightButtonDown == false)
            {
                if (m_IsOnGround == true)
                {
                    m_IsOnGround = false;
                    m_IsJumping = true;

                    Sounds.playSound("Jump", 1);
                }
                if (m_Facing == Facing.LEFT)
                {
                    m_Animate.changAnimation((int)Animations.JUMPLEFT);
                }
                else if (m_Facing == Facing.RIGHT)
                {
                    m_Animate.changAnimation((int)Animations.JUMPRIGHT);
                }
            }
           
            if (m_Input.m_WasJumpButtonDown == true && m_Input.m_WasMoveLeftButtonDown == true)
            {
                if (m_IsOnGround == true)
                {
                    m_IsOnGround = false;
                    m_IsJumping = true;

                    Sounds.playSound("Jump", 1);
                }
                m_Facing = Facing.LEFT;
                m_Animate.changAnimation((int)Animations.JUMPLEFT);
            }
            else if (m_Input.m_WasJumpButtonDown == true && m_Input.m_WasMoveRightButtonDown == true)
            {
                if (m_IsOnGround == true)
                {
                    m_IsOnGround = false;
                    m_IsJumping = true;

                    Sounds.playSound("Jump", 1);
                }
                m_Facing = Facing.RIGHT;
                m_Animate.changAnimation((int)Animations.JUMPRIGHT);
            }

            if (m_Input.m_WasRainbowButtonDown == true)
            {
                //TODO: Rainbow Logic
            }

            if (m_Input.m_WasAttackButtonDown == true)
            {
                laserState(gameTime);
            }
        }
            #endregion
        #endregion

        #region Draw
        public override void draw(SpriteBatch spriteBatch)
        {
            //Added November 27, 2013

            spriteBatch.Draw(m_Cross, m_CrossPosition, Color.White);
          
            base.draw(spriteBatch);
        }

        public void drawParticles(SpriteBatch spriteBatch)
        {
            m_LaserManager.draw(spriteBatch);

            m_Particles.draw(spriteBatch);
        }
        #endregion
        #endregion
    }
}
