using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:
*/

public enum EnemyAttackStyle
{
    None=0,
    Melee=1,
    Rifle=2,
    ShotGun=3,
}
public enum TitleType
{
    None = 0,
    Attack = 1,
    Assist = 2,
    Item = 3,
}
public enum StoryType
{
    None = 0,
    Main = 1,
    Branch = 2,
    Hidden = 3,
}
public enum PlayerState
{
    None,
    Jump
}
public enum BattleStatus
{
    None = 0,
    Idle = 1,
    InBattle = 2,
}
public enum SkillTitleType
{
    None = 0,
    Positive = 1,
    Passive = 2,
}

public enum CheckDistanceResult
{
    None = 0,
    /// <summary>
    /// ĞèÒªÔ¶Àë
    /// </summary>
    NeedStayOff = 1,
    /// <summary>
    /// ¾àÀëÔ¶ÓÚ¼ì²â¾àÀë
    /// </summary>
    TooFar = 2,
    /// <summary>
    /// ²»ĞèÒªÔ¶Àë£¬ÇÒ¾àÀëÔÚ¼ì²â¾àÀëÄÚ
    /// </summary>
    Normal = 3,
    /// <summary>
    /// ¾àÀëÔÚ¼ì²â¾àÀëÄÚ
    /// </summary>
    Detected = 4,
}
public enum GameStatus
{
    None = 0,
    Menu = 1,
    Loading = 2,
    Game = 3,
    BeforeGame = 4,
    Dialoguing = 5,
    Novice = 6,
}
