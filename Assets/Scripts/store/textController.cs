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
        myText.text = "<color=yellow>�˺�</color>:\n" + "" + "<color=yellow>��Χ</color>:\n" + "" + "<color=yellow>ת������</color>:\n" + ""
            + "<color=yellow>��������</color>:\n" + "" + "<color=yellow>��������</color>:\n" + "" + "<color=yellow>����</color>:\n" + "" + "<color=yellow>Ʒ��</color>:\n" + "";
    }

    
}
