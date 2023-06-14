using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttribute : MonoBehaviour
{
    //武器伤害类型的枚举
    public enum WeaponDamageType { Unknown = -1 ,Melee, Ranged, Ability }
    //武器分类的枚举
    public enum WeaponCategory { Unknown = -1, Gun, Ability, Heal }
    //物品品质的枚举
    public enum Quality { Unknown = -1, Normal, Senior, Elite, Legendary}
    // TODO 武器的所有者的属性需要默认是玩家的属性或者是副本，用以在商店显示受玩家属性影响后的武器属性
    //武器所有者的属性
    private CharacterAttribute ownerAttr = new();
    //武器基础伤害
    private float rawWeaponDamage = 0;
    //武器基础攻击范围
    private float rawAttackRange = 20f;
    //对应类型伤害的转换比例
    private float convertRatio = 0.8f;
    //暴击伤害的倍率
    private float criticalBonus = 1.5f;
    //每一次攻击所用的时间
    private float rawAttackSpeed = 1f;
    //武器基础暴击概率
    private float rawCriticalRate = 0.02f;
    //武器的伤害类型
    private WeaponDamageType weaponDamageType = WeaponDamageType.Melee;
    //武器的分类
    private List<WeaponCategory> weaponCategory = new();
    //武器的价格
    private float weaponPrice = 0;
    //武器的品质
    private Quality weaponQuality = Quality.Unknown;
    //武器的ID
    private int weaponID;
    //武器的名称
    private string weaponName;
    //武器的图标
    private string weaponIcon;
    //武器的品质背景
    private string weaponBgIcon;


    public void setOwnerAttr(CharacterAttribute input)
    {
        ownerAttr = input;
    }

    public void setRawWeaponDamage(float input)
    {
        rawWeaponDamage = input;
    }

    public void setRawAttackRange(float input)
    {
        rawAttackRange = input;
    }

    public void setConvertRatio(float input)
    {
        convertRatio = input;
    }

    public void setCriticalBonus(float input)
    {
        criticalBonus = input;
    }

    public void setRawAttackSpeed(float input)
    {
        rawAttackSpeed = input;
    }

    public void setRawCriticalRate(float input)
    {
        rawCriticalRate = input;
    }

    public void setWeaponDamageType(WeaponDamageType input)
    {
        weaponDamageType = input;
    }

    public void setWeaponCategory(List<WeaponCategory> input)
    {
        weaponCategory = input;
    }

    public void setWeaponPrice(float input)
    {
        weaponPrice = input;
    }

    public void setWeaponQuality(Quality input)
    {
        weaponQuality = input;
    }

    public void setWeaponID(int input)
    {
        weaponID = input;
    }

    public void setWeaponName(string input)
    {
        weaponName = input;
    }

    public void setWeaponIcon(string input)
    {
        weaponIcon = input;
    }

    public void setWeaponBgIcon(string input)
    {
        weaponBgIcon = input;
    }

    //用于初始化武器
    //public void setAllAttribute(CharacterAttribute ownerAttr, float rawWeaponDamage, float rawAttackRange, float convertRatio, float criticalBonus,
    //    float rawAttackSpeed, float rawCriticalRate, WeaponDamageType weaponType, List<WeaponCategory> weaponCategory, float weaponPrice,
    //    Quality weaponQuality, int weaponID, string weaponName, string weaponIcon, string weaponBgIcon)
    //{
    //    setOwnerAttr(ownerAttr);
    //    setRawWeaponDamage(rawWeaponDamage);
    //    setRawAttackRange(rawAttackRange);
    //    setConvertRatio(convertRatio);
    //    setCriticalBonus(criticalBonus);
    //    setRawAttackSpeed(rawAttackSpeed);
    //    setRawCriticalRate(rawCriticalRate);
    //    setWeaponDamageType(weaponType);
    //    setWeaponCategory(weaponCategory);
    //    setWeaponPrice(weaponPrice);
    //    setWeaponQuality(weaponQuality);
    //    setWeaponID(weaponID);
    //    setWeaponName(weaponName);
    //    setWeaponIcon(weaponIcon);
    //    setWeaponBgIcon(weaponBgIcon);
    //}



    //获取武器基础攻击范围
    public float getRawAttackRange()
    {
        return rawAttackRange;
    }

    //获取武器经过角色属性加成后的攻击范围
    public float getAttackRange()
    {
        return rawAttackRange * (1 + ownerAttr.getAttackRangeAmplification()) >= 0 ? rawAttackRange * (1 + ownerAttr.getAttackRangeAmplification()) : 0;
    }

    //获取武器基础伤害
    public float getRawWeaponDamage()
    {
        return rawWeaponDamage;
    }

    //获取武器经过角色属性加成后的面板攻击大小(真正的伤害还涉及暴击和敌方护甲)
    public float getWeaponDamage()
    {
        float temp = 0;
        switch (weaponDamageType)
        {
            case WeaponDamageType.Melee:
                temp = rawWeaponDamage + ownerAttr.getMeleeDamage() * convertRatio * (1 + ownerAttr.getAttackAmplification());
                break;
            case WeaponDamageType.Ranged:
                temp = rawWeaponDamage + ownerAttr.getRangedDamage() * convertRatio * (1 + ownerAttr.getAttackAmplification()); 
                break;
            case WeaponDamageType.Ability:
                temp = rawWeaponDamage + ownerAttr.getAbilityDamage() * convertRatio * (1 + ownerAttr.getAttackAmplification());
                break;
            default:
                Debug.Log("weapon type error");
                return 0;
        }
        return temp > 1 ? temp : 1;
    }

    //获取武器暴击倍率
    public float getCriticalBonus()
    {
        return criticalBonus;
    }

    //如果武器拥有者的攻击速度属性小于或等于-100，返回-1表示攻击间隔无穷大，不进行攻击
    public float getAttackSpeed()
    {
        float attackSpeedAmplification = ownerAttr.getAttackSpeedAmplification();
        if (attackSpeedAmplification == -100)
        {
            return -1;
        }
        float attackSpeed = rawAttackSpeed / (1 + ownerAttr.getAttackSpeedAmplification() * 0.01f);
        return attackSpeed;
    }

    //获取经角色属性加成后的武器攻击暴击概率
    public float getCriticalRate()
    {
        return rawCriticalRate + ownerAttr.getCriticalRate();
    }

    public float getWeaponPrice()
    {
        return weaponPrice;
    }

    public WeaponDamageType getWeaponDamageType()
    {
        return weaponDamageType;
    }

    public List<WeaponCategory> getWeaponCategory()
    {
        return weaponCategory;
    }

    public Quality getWeaponQuality()
    {
        return weaponQuality;
    }

    public int getWeaponID()
    {
        return weaponID;
    }

    public string getWeaponName()
    {
        return weaponName;
    }

    public string getWeaponIcon()
    {
        return weaponIcon;
    }

    public float getConvertRatio()
    {
        return convertRatio;
    }

    public string getWeaponBgIcon()
    {
        return weaponBgIcon;
    }
}
