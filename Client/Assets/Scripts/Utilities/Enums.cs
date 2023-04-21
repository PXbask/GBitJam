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
    /// –Ë“™‘∂¿Î
    /// </summary>
    NeedStayOff = 1,
    /// <summary>
    /// æ‡¿Î‘∂”⁄ºÏ≤‚æ‡¿Î
    /// </summary>
    TooFar = 2,
    /// <summary>
    /// ≤ª–Ë“™‘∂¿Î£¨«“æ‡¿Î‘⁄ºÏ≤‚æ‡¿Îƒ⁄
    /// </summary>
    Normal = 3,
    /// <summary>
    /// æ‡¿Î‘⁄ºÏ≤‚æ‡¿Îƒ⁄
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
public enum VideoQuality
{
    None,
    Low,
    Inferior,
    Middle,
    High,
    Extreme,
}
