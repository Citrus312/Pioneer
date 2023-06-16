using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttributeUI : MonoBehaviour
{
    //�ı��е�����ֵ˵���ı�
    public TextMeshProUGUI AttributeText;
    //ָ��PlayerԤ�Ƶ�ָ��
    public GameObject _player;

    void Update()
    {
        getAttributeUI();
    }

    private void getAttributeUI()
    {
        //�����ı��е���������
        AttributeText.text = "Health : " + _player.GetComponent<CharacterAttribute>().getMaxHealth() +
                             "\nSpeed : " + _player.GetComponent<CharacterAttribute>().getMoveSpeed();
    }
}
