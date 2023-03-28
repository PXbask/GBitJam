using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:
*/

public class PXSceneManager : Singleton<PXSceneManager>
{
    public Dictionary<int, PXScene> Scenes= new Dictionary<int, PXScene>();
    public PXScene GetScene(int id)
    {
        PXScene pXScene = null;
        if(Scenes.TryGetValue(id, out pXScene)) 
            return pXScene;
        return null;
    }
    public void Init()
    {
        Scenes.Add(2, new PXScene());
    }
}
