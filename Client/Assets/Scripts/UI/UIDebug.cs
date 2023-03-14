using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/*
    Date:
    Name:
    Overview:Debug´°¿Ú
*/

public class UIDebug : UIWindow
{
    Text info;
    public CharController player;
    StringBuilder sb = new StringBuilder();
    protected override void OnStart()
    {
        info = GameObject.Find("info").GetComponent<Text>();
        player = GameObject.Find("Player").GetComponent<CharController>();
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        sb.Clear();
        Model.Attribute attribute = player.charBase.attributes.curAttribute;
        sb.AppendLine(string.Format("HP:{0}", attribute.HP.ToString()));
        sb.AppendLine(string.Format("SPD:{0}", attribute.SPD.ToString()));
        sb.AppendLine(string.Format("ATK:{0}", attribute.ATK.ToString()));
        sb.AppendLine(string.Format("DEF:{0}", attribute.DEF.ToString()));
        info.text = sb.ToString();
    }
}
