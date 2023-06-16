using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttributeUI : MonoBehaviour
{
    //文本中的生命值说明文本
    public TextMeshProUGUI AttributeText;
    //指向Player预制的指针
    public GameObject _player;

    void Update()
    {
        getAttributeUI();
    }

    private void getAttributeUI()
    {
        //更新文本中的所有属性
        AttributeText.text = "Health : " + _player.GetComponent<CharacterAttribute>().getMaxHealth() +
                             "\nSpeed : " + _player.GetComponent<CharacterAttribute>().getMoveSpeed();
    }
}
