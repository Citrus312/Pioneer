using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class textController : MonoBehaviour
{
    //public List<WeaponAttribute> WeaponPropList=JsonLoader.weaponPool
    public void Start()
    {
        //string weaponName = WeaponPropList[0].getWeaponName();
        TextMeshProUGUI myText = GetComponent<TextMeshProUGUI>();
        myText.text = "<color=yellow>伤害</color>:\n" + "" + "<color=yellow>范围</color>:\n" + "" + "<color=yellow>转化比率</color>:\n" + ""
            + "<color=yellow>暴击倍率</color>:\n" + "" + "<color=yellow>暴击概率</color>:\n" + "" + "<color=yellow>攻速</color>:\n" + "" + "<color=yellow>品质</color>:\n" + "";
    }

    
}
