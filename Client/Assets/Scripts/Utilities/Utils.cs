using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal static class Utils
{ 
    private static GameObject m_bossObject= null;

    public static GameObject GetNoviceInstanceObject() => NoviceManager.Instance.gameObject;

    public static GameObject GetBossObject()
    {
        if(m_bossObject!=null) return m_bossObject;

        GameObject obj = GameObject.Find("Boss(Clone)");
        if (obj == null) Debug.LogWarning("Can't Find GetBossObject in this Scene");
        else m_bossObject = obj;

        return obj;
    }

    public static Vector3 GetDefaultPointPosition() => new Vector3(5.21f, 0.3f, -0.2f);
}
