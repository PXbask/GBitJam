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
    public Text info;
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
        sb.AppendLine(string.Format("DamageResistence:{0}", attribute.DamageResistence.ToString()));
        sb.AppendLine(string.Format("Accuracy:{0}", attribute.Accuracy.ToString()));
        sb.AppendLine(string.Format("Dodge:{0}", attribute.Dodge.ToString()));
        sb.AppendLine(string.Format("MoveVelocityRatio:{0}", attribute.MoveVelocityRatio.ToString()));
        sb.AppendLine(string.Format("AttackVelocityRatio:{0}", attribute.AttackVelocityRatio.ToString()));
        sb.AppendLine(string.Format("DamageRatio:{0}", attribute.DamageRatio.ToString()));
        sb.AppendLine(string.Format("Damage:{0}", attribute.Damage.ToString()));
        info.text = sb.ToString();
    }
}
