using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Reflection;
using System.Linq.Expressions;
using System;

/*
    Date:
    Name:
    Overview:
*/

public class EventManager : Singleton<EventManager>
{
    public static Action OnBossDefend = null;

    public void Init() { }

    public void Reset()
    {
        OnBossDefend = null;
    }
}
