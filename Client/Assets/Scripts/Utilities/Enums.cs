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
public enum PlayerStatus
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
