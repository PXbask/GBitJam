using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;
using Define;

/*
    Date:
    Name:
    Overview:���&���˵Ļ���
*/

public class CharBase
{
    public CharacterDefine define;
    public Attributes attributes;//��������

    public bool IsPlayer => define.ID == 1;
    public CharBase(CharacterDefine define)
    {
        this.define = define;
        attributes = new Attributes(define);
    }
}
