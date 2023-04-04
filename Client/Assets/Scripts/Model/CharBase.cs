using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;
using Define;

/*
    Date:
    Name:
    Overview:玩家&敌人的基类
*/

public class CharBase
{
    public CharacterDefine define;
    public Attributes attributes;//属性总类

    public bool IsPlayer => define.ID == 1;
    public CharBase(CharacterDefine define)
    {
        this.define = define;
        attributes = new Attributes(define);
    }
}
