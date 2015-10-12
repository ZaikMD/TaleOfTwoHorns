using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATaleOfTwoHorns
{
    enum SideCollided
    {
        NONE=0,
        LEFT,
        TOP,
        RIGHT,
        BOTTOM
    }

    enum MovementValue
    {
        NONE = 0,
        LEFT = -1,
        UP = -1,
        RIGHT = 1,
        DOWN = 1
    }

    enum EnemyType
    {
        BEES = 0,
        SPIDER,
        PIXIE,
        IMP,
        UNDEADUNICORN,
        BUNNY,
        BUTTERFLY,
        COUNT
    }
  

    enum PickUpType
    {
        KEY = 0,
        HEART,
        STAR,
        COUNT
    }

    enum GameState
    {
        SplashScreen,
        MainMenu,
        Loading,
        Credits,
        LevelSelect,
        PauseMenu,
        GameOver,
        Game,
        LevelComplete
    }

    enum PixieState
    {
        ALIVE = 0,
        DYING
    }

    enum ImpState
    {
        ALIVE = 0,
        DYING
    }

    enum UndeadUnicornState
    {
        ALIVE = 0,
        DYING
    }

    enum SpiderState
    {
        ALIVE = 0, 
        DYING
    }

    enum SpiderAggroState
    {
        NONE = 0,
        AGGROED,
        RETREATING
    }

}
