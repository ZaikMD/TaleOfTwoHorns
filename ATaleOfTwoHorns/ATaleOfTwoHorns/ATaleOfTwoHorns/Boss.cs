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
    class Boss
    {
        public enum BossState
        {
            IDLE = 0,
            CHARGING,
            LAVA_WAVE,
            NARWHAL,
            COUNT,
            FIRING,
            DYING,
            DEAD
        }

        public static bool IsActive {  get;  set; }

        private static BossState m_BossState = BossState.IDLE;

        const float INITIAL_STATE_DELAY = 1.50f;
        float m_StateDelay = INITIAL_STATE_DELAY;
        float m_StateChangeTimer = 0.0f;

        Animate m_Animate = new Animate(new Rectangle(0, 0, 416, 700), new Rectangle(0, 0, 416, 700));
        Rectangle m_CollisionRect = new Rectangle(0, 0, 410, 500);

        private static Vector2 m_StartPos = new Vector2(800, 950);
        float m_IdleMoveDist = 50.0f;
        Vector2 m_TargetPos = new Vector2(32, 0);

        float m_LerpAmount = 0.005f;
        float m_LerpDist = 0.9f;

        private static Vector2 m_Position;

        Texture2D m_SpriteSheet;

        private static Particles[] m_Particles = new Particles[10];
        float m_ChargeDist = 50.0f;

        Rectangle m_HornDamageRect = new Rectangle(0, 0, 20, 20);

        Random m_Random = new Random();

        Vector2 m_PlayerPos=new Vector2();

        float m_Speed = 500.0f;

        MovementValue m_Direction = MovementValue.NONE;

        private static BeamManager m_BeamManager;

        Vector2 m_RetreatPos = new Vector2(300, -450);
        bool m_Drop = false;

        private static LavaWave m_LavaWave = new LavaWave();

        Vector2 m_RainPos = new Vector2(250, -100);
        float m_RainJumpDist = 10.0f;
        const float RAIN_LENGTH = 2.0f;

        private static NarwhalGun m_NarwhalGun = new NarwhalGun();
        Vector2 m_GunPos = new Vector2(-32, 80);

        Lava m_Lava = new Lava(new Vector2(158, Level.getHeightOfArray() * 32 - 32 * 4));

        const int INITIAL_HEALTH = 5;
        private static int m_Health = INITIAL_HEALTH;

        private static Vector2 m_Hidden = new Vector2(0, 420);
        Vector2 m_MoreHidden = new Vector2(0, 100);

        //Sound stuff
        bool m_BossChargingSoundPlayer = false;
        bool m_BossDeathPlayed = false;
        bool m_BossLaserPlayed = false;
        bool m_BossRoarPlayed = false;
        bool m_BossWavePlayed = false;
        
        private Vector2 Position
        {
            get { return m_Position; }
            set
            {
                m_Position = value;
                
                m_Animate.updatePosition(value);

                m_CollisionRect.X = m_Animate.DestinationRectangle.X + (m_Animate.DestinationRectangle.Width - m_CollisionRect.Width) / 2;
                m_CollisionRect.Y = m_Animate.DestinationRectangle.Y + (m_Animate.DestinationRectangle.Height - m_CollisionRect.Height);

                m_HornDamageRect.X = m_Animate.DestinationRectangle.X;
                m_HornDamageRect.Y = m_Animate.DestinationRectangle.Y;
            }
        }

        public Boss()
        {
            IsActive = false;            

            Position = m_StartPos + m_Hidden;
            
            for (int i = 0; i < m_Particles.Length; i++)
            {
                m_Particles[i] = new Particles(new Vector2(), Color.Red, 20, 5, 0.0f, new Vector2(), 3.0f, true, 70, 5, 2, true);
                m_Particles[i].Stop = true;
            }

            m_BeamManager = new BeamManager(new Rectangle(0, 0, Level.getWidthOfArray() * 32, Level.getHeightOfArray() * 32), Color.DarkRed, Color.OrangeRed);
        }

        public void loadContent(ContentManager content)
        {
            m_SpriteSheet = content.Load<Texture2D>("Narwhal");

            m_Animate.loadContent("Narwhal", content);
            m_Animate.addAnimation(new Animation(32, new int[] { 0 }));
            m_Animate.addAnimation(new Animation(8, new int[] { 0, 1, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }));

            m_BeamManager.loadContent(content);

            m_LavaWave.loadContent(content);

            m_NarwhalGun.loadContent(content);

            m_Lava.loadContent(content);

            for (int i = 0; i < m_Particles.Length; i++)
            {
                m_Particles[i].loadContent(content);
            }
        }

        public static void reset()
        {
            m_Position = m_StartPos + m_Hidden;
            m_Health = INITIAL_HEALTH;
            m_BossState = BossState.IDLE;
            m_NarwhalGun.clear();
            m_LavaWave.reset();
            m_BeamManager.reset();
            
            for (int i = 0; i < m_Particles.Length; i++)
            {
                m_Particles[i].Stop = true;
            }
        }

        public void update(GameTime gameTime)
        {
            if (IsActive == true)
            {
                if (m_BossState != BossState.DYING && m_BossState != BossState.DEAD)
                {
                    float elapsedTime = (float)gameTime.ElapsedGameTime.Milliseconds / 1000;

                    if (m_BossState == BossState.IDLE)
                    {
                        Vector2 temp;
                        Vector2.Subtract(ref m_Position, ref m_StartPos, out temp);
                        if (temp.Length() > (m_IdleMoveDist - (m_IdleMoveDist * m_LerpDist)))
                        {
                            do
                            {
                                m_TargetPos = new Vector2(m_Random.Next(100) - 50, m_Random.Next(100) - 50);
                            } while (m_TargetPos.X == 0 && m_TargetPos.Y == 0);
                            m_TargetPos.Normalize();
                            m_TargetPos.X = m_TargetPos.X * m_IdleMoveDist;
                            m_TargetPos.Y = m_TargetPos.Y * m_IdleMoveDist;

                        }
                        m_Position = Vector2.Lerp(m_Position, m_StartPos + m_TargetPos, m_LerpAmount);
                        Position = m_Position;

                        m_StateChangeTimer += elapsedTime;

                        if (m_StateChangeTimer > m_StateDelay)
                        {
                            m_StateChangeTimer = 0.0f;
                            do
                            {
                                m_BossState = (BossState)(m_Random.Next((int)BossState.COUNT));
                                //m_BossState = BossState.CHARGING;
                            } while (m_BossState == BossState.LAVA_WAVE && m_LavaWave.IsActive == true);
                        }
                        m_Animate.changAnimation(1);
                    }
                    else if (m_BossState == BossState.CHARGING)
                    {
                        //boss is charging here ------------------------------------------------------------------------------------------------

                        for (int i = 0; i < m_Particles.Length; i++)
                        {
                            Vector2 temp;
                            temp = new Vector2(m_Random.Next(100) - 50, m_Random.Next(100) - 50);
                            temp.Normalize();
                            temp.X = temp.X * m_ChargeDist;
                            temp.Y = temp.Y * m_ChargeDist;

                            m_Particles[i].SpawnPoint = new Vector2(m_Animate.DestinationRectangle.X, m_Animate.DestinationRectangle.Y) + temp;
                            m_Particles[i].DominantDirection = new Vector2(m_Animate.DestinationRectangle.X, m_Animate.DestinationRectangle.Y) - m_Particles[i].SpawnPoint;

                            m_Particles[i].Stop = false;

                            m_BossLaserPlayed = false;
                            if (m_BossChargingSoundPlayer == false)
                            {
                                Sounds.playSound("BossCharging", 1.0f);
                                m_BossChargingSoundPlayer = true;
                            }
                        }

                        m_StateChangeTimer += elapsedTime;

                        if (m_StateChangeTimer > m_StateDelay)
                        {
                            m_StateChangeTimer = 0.0f;

                            m_BossState = BossState.FIRING;

                            for (int i = 0; i < m_Particles.Length; i++)
                            {
                                m_Particles[i].Stop = true;
                            }

                            m_PlayerPos = new Vector2(Player.collisionRectangle().Center.X, Player.collisionRectangle().Center.Y);
                            if (m_Animate.DestinationRectangle.Y < m_PlayerPos.Y)
                            {
                                m_Direction = MovementValue.DOWN;
                            }
                            else
                            {
                                m_Direction = MovementValue.UP;
                            }
                        }
                    }
                    else if (m_BossState == BossState.FIRING)
                    {
                        m_Position.Y += m_Speed * elapsedTime * (int)m_Direction;
                        Position = m_Position;

                        m_BossChargingSoundPlayer = false;

                        if (m_Direction == MovementValue.DOWN && m_Animate.DestinationRectangle.Y > m_PlayerPos.Y || m_Direction == MovementValue.UP && m_Animate.DestinationRectangle.Y < m_PlayerPos.Y)
                        {
                            //boss shoots here ---------------------------------------------------------------------------
                            if (m_BossLaserPlayed == false)
                            {
                                Sounds.playSound("BossLaser", 1.0f);
                                m_BossLaserPlayed = true;
                            }

                            m_BeamManager.addBeam(new Vector2(m_Animate.DestinationRectangle.X, m_Animate.DestinationRectangle.Y), m_PlayerPos);

                            m_BossState = BossState.IDLE;
                            m_Direction = MovementValue.NONE;
                        }
                    }
                    else if (m_BossState == BossState.LAVA_WAVE)
                    {
                        if (m_Position.X < m_StartPos.X + m_RetreatPos.X)
                        {
                            m_Position.X += m_Speed * elapsedTime;
                        }
                        else if ((m_Position.Y - m_Animate.DestinationRectangle.Height / 2 > m_StartPos.Y + m_RetreatPos.Y) && m_Drop == false)
                        {
                            m_Position.Y -= m_Speed * elapsedTime;


                            if (m_Position.Y - m_Animate.DestinationRectangle.Height / 2 < m_StartPos.Y + m_RetreatPos.Y)
                            {
                                m_Drop = true;
                            }
                        }
                        else if (m_Position.Y < m_StartPos.Y && m_Drop == true)
                        {
                            m_Position.Y += m_Speed * elapsedTime;
                            if (m_BossWavePlayed == false)
                                {
                                    Sounds.playSound("Wave", 1.0f);
                                    m_BossWavePlayed = true;
                                }

                            if (m_Position.Y > m_StartPos.Y)
                            {
                                m_LavaWave.SpawnTheWave(new Vector2(m_Position.X, Level.getHeightOfArray() * 32 - 2));
                                m_Drop = false;
                                m_BossState = BossState.IDLE;
                                m_BossWavePlayed = false;


                                
                            }
                        }
                        Position = m_Position;
                    }
                    else if (m_BossState == BossState.NARWHAL)
                    {
                        if (Vector2.Subtract(m_Position, m_StartPos + m_RainPos).Length() > m_RainJumpDist)
                        {
                            Vector2 temp = new Vector2(m_StartPos.X + m_RainPos.X - m_Position.X, m_StartPos.Y + m_RainPos.Y - m_Position.Y);
                            temp.Normalize();
                            m_Position += temp * m_Speed / 4.0f * elapsedTime;
                            
                        }
                        else
                        {
                            if (m_BossRoarPlayed == false)
                            {
                                Sounds.playSound("BossRoar", 1.0f);
                                m_BossRoarPlayed = true;
                            }
                            m_StateChangeTimer += elapsedTime;

                            Vector2 temp;
                            do
                            {
                                temp = new Vector2(m_Random.Next(100) - 50, m_Random.Next(100) - 50);
                            } while (temp.X == 0 && temp.Y == 0);

                            temp.Normalize();
                            temp.X *= m_RainJumpDist;
                            temp.Y *= m_RainJumpDist;

                            m_Position = m_StartPos + m_RainPos + temp;

                            m_NarwhalGun.NARWHAL(new Vector2(m_Position.X + m_GunPos.X, m_Position.Y + m_GunPos.Y));

                            //spitting narwhals here------------------------------------------------------------------------
                            


                            if (m_StateChangeTimer > RAIN_LENGTH)
                            {
                                m_StateChangeTimer = 0.0f;
                                m_BossState = BossState.IDLE;
                                m_BossRoarPlayed = false;
                            }
                        }
                        Position = m_Position;
                    }
                }
                else if (m_BossState == BossState.DYING)
                {
                    m_Position = Vector2.Lerp(m_Position, m_StartPos + m_Hidden, m_LerpAmount);

                    //boss is dead here------------------------------------------------------------------------------------------
                    if (m_BossDeathPlayed == false)
                    {
                        Sounds.playSound("BossDeath", 1.0f);
                        m_BossDeathPlayed = true;
                    }

                    Vector2 temp;
                    do
                    {
                        temp = new Vector2(m_Random.Next(100) - 50, m_Random.Next(100) - 50);
                    } while (temp.X == 0 && temp.Y == 0);

                    temp.Normalize();
                    temp.X *= m_RainJumpDist / 2;
                    temp.Y *= m_RainJumpDist / 2;

                    m_Position += temp;
                    Position = m_Position;

                    if (m_Position.Y > m_StartPos.Y + m_Hidden.Y)
                    {
                        m_BossState = BossState.DEAD;                       
                    }
                }
                else
                {
                    if (m_Position.Y < m_StartPos.Y + m_Hidden.Y + m_MoreHidden.Y)
                    {
                        m_Position.Y++;
                        Position = m_Position;
                        Vector2 tempy = m_Lava.Pos;
                        tempy.Y++;
                        m_Lava.Pos = tempy;
                    }
                }

                collisioncheck();

                m_NarwhalGun.update(gameTime);
                m_BeamManager.update(gameTime);
                m_LavaWave.update(gameTime);
                m_Animate.update(gameTime);

                m_Lava.update(gameTime);

                for (int i = 0; i < m_Particles.Length; i++)
                {
                    m_Particles[i].update(gameTime);
                }
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (IsActive == true)
            {
                m_NarwhalGun.draw(spriteBatch);
                m_Animate.draw(spriteBatch);
                m_LavaWave.draw(spriteBatch);
                m_Lava.draw(spriteBatch);
            }
        }

        public void drawParticles(SpriteBatch spriteBatch)
        {
            if (IsActive == true)
            {
                for (int i = 0; i < m_Particles.Length; i++)
                {
                    m_Particles[i].draw(spriteBatch);
                }
                m_BeamManager.draw(spriteBatch);
            }
        }

        private void collisioncheck()
        {
            if (IsActive == true)
            {
                if (m_BossState == BossState.CHARGING)
                {
                    if (LaserManager.collisionCheck(m_HornDamageRect))
                    {
                        //take damge
                        m_Health--;
                        if (m_Health <= 0)
                        {
                            m_BossState = BossState.DYING;

                            for (int i = 0; i < m_Particles.Length; i++)
                            {
                                m_Particles[i].Stop = true;
                            }
                        }
                    }
                }

                if (CollisionCheck.collisionCheck(m_CollisionRect, Player.collisionRectangle()))
                {
                    //for overkill damage
                    Player.instantDeath();
                }
            }
        }
    }
}
