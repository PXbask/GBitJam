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
    #region bools
    bool isFirstEnterGame;
    public bool IsFirstEnterGame
    {
        get { return isFirstEnterGame; }
        set
        {
            isFirstEnterGame = value;
            BooleanDics[nameof(isFirstEnterGame)] = true;
        }
    }
    #endregion
    #region events
    
    #endregion
    public Dictionary<string, bool> BooleanDics = new Dictionary<string, bool>();
    public EventManager()
    {
        isFirstEnterGame = BooleanDics[nameof(isFirstEnterGame)];
        
    }
}
