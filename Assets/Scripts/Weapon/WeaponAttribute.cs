using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttribute : MonoBehaviour
{
    //武器类型的枚举
    public enum WeaponType { Melee, Ranged }
    // TODO 武器的所有者的属性需要默认是玩家的属性或者是副本，用以在商店显示受玩家属性影响后的武器属性
    //武器的所有者的属性
    [SerializeField] private CharacterAttribute ownerAttr;
    //武器基础攻击范围
    [SerializeField] private float rawAttackRange = 20f;
    //对应类型伤害的转换比例
    private float convertRatio = 0.8f;
    //暴击伤害的倍率
    private float criticalBonus = 1.5f;
    //每一次攻击所用的时间
    private float rawAttackSpeed = 1f;
    //武器基础暴击概率
    private float rawCriticalRate = 0.02f;
    //武器的类型
    private WeaponType weaponType = WeaponType.Melee;


    public void setOwnerAttr(CharacterAttribute input)
    {
        ownerAttr = input;
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

    public void setWeaponType(WeaponType input)
    {
        weaponType = input;
    }

    //用于初始化武器
    public void setAllAttribute(CharacterAttribute ownerAttr, float rawAttackRange, float convertRatio, float criticalBonus,
        float rawAttackSpeed, float rawCriticalRate, WeaponType weaponType)
    {
        setOwnerAttr(ownerAttr);
        setRawAttackRange(rawAttackRange);
        setConvertRatio(convertRatio);
        setCriticalBonus(criticalBonus);
        setRawAttackSpeed(rawAttackSpeed);
        setRawCriticalRate(rawCriticalRate);
        setWeaponType(weaponType);
    }



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

    //获取武器经过角色属性加成后的面板攻击大小(真正的伤害还涉及暴击和敌方护甲)
    public float getWeaponDamage()
    {
        float temp = 0;
        switch (weaponType)
        {
            case WeaponType.Melee:
                temp = ownerAttr.getMeleeDamage() * convertRatio * (1 + ownerAttr.getAttackAmplification());
                break;
            case WeaponType.Ranged:
                temp = ownerAttr.getRangedDamage() * convertRatio * (1 + ownerAttr.getAttackAmplification());
                break;
            default:
                Debug.Log("weapon type error");
                return 0;
        }
        return temp > 1 ? temp : 1;
    }

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
}
