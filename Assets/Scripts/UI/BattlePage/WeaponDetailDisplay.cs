using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponDetailDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //详细信息显示面板
    public GameObject detailDisplay;

    public void OnPointerEnter(PointerEventData eventData)
    {
        //将面板移动到指定位置
        detailDisplay.transform.GetChild(0).position = new Vector3(transform.position.x - 125f, transform.position.y > 95f ? transform.position.y : 95f, transform.position.z);
        //根据物体名字获取对应的武器数据
        WeaponAttribute weapon = JsonLoader.weaponPool[int.Parse(this.transform.parent.name)];
        //加载武器背景
        ImageLoader.LoadImage($"Assets/Sprites/Weapon/{weapon.getWeaponBgIcon()}", detailDisplay.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>());
        string prePath = "";
        //根据武器的攻击类型确定武器图片的路径
        switch (weapon.getWeaponDamageType())
        {
            case WeaponAttribute.WeaponDamageType.Melee:
                prePath = "Assets/Sprites/Weapon/Melee Weapon/";
                break;
            case WeaponAttribute.WeaponDamageType.Ranged:
                prePath = "Assets/Sprites/Weapon/Ranged Weapon/";
                break;
            case WeaponAttribute.WeaponDamageType.Ability:
                prePath = "Assets/Sprites/Weapon/Ability Weapon/";
                break;
            default:
                break;
        }
        //加载武器图标
        ImageLoader.LoadImage($"{prePath}{weapon.getWeaponIcon()}", detailDisplay.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>());
        //设置武器名字
        detailDisplay.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text = weapon.getWeaponName();
        //设置武器属性文本
        Text weaponAttrText = detailDisplay.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>();
        float temp = 0;
        CharacterAttribute player = GameController.getInstance().getPlayer().GetComponent<CharacterAttribute>();
        switch (weapon.getWeaponDamageType())
        {
            case WeaponAttribute.WeaponDamageType.Melee:
                temp = weapon.getRawWeaponDamage() + player.getMeleeDamage() * weapon.getConvertRatio() * (1 + player.getAttackAmplification() * 0.01f);
                break;
            case WeaponAttribute.WeaponDamageType.Ranged:
                temp = weapon.getRawWeaponDamage() + player.getRangedDamage() * weapon.getConvertRatio() * (1 + player.getAttackAmplification() * 0.01f);
                break;
            case WeaponAttribute.WeaponDamageType.Ability:
                temp = weapon.getRawWeaponDamage() + player.getAbilityDamage() * weapon.getConvertRatio() * (1 + player.getAttackAmplification() * 0.01f);
                break;
            default:
                Debug.Log("weapon type error");
                break;
        }
        weaponAttrText.text = $"<color=yellow>伤害</color>:  {Mathf.Ceil(temp)} | {weapon.getRawWeaponDamage()}\n" +
                              $"<color=yellow>范围</color>:  {weapon.getAttackRange()} | {weapon.getRawAttackRange()}\n" +
                              $"<color=yellow>转化</color>:  {weapon.getConvertRatio() * 100}%\n" +
                              $"<color=yellow>暴击</color>:  x{weapon.getCriticalBonus()} ({weapon.getRawCriticalRate() * 100}%)\n" +
                              $"<color=yellow>攻速</color>:  {weapon.getAttackSpeed()}s\n";
        //激活面板
        detailDisplay.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //隐藏面板
        detailDisplay.SetActive(false);
    }
}
