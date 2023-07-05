using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropAttribute : MonoBehaviour
{
    //道具ID
    private int propID;
    //道具名称
    private string propName;
    //道具品质
    private WeaponAttribute.Quality propQuality;
    //道具图标
    private string propIcon;
    //道具品质背景
    private string propBgIcon;
    //道具价格
    private float propPrice;
    //以下是道具对于角色属性的加成或减益
    //最大生命 生命回复 生命汲取
    private float maxHealth = 0;
    private float healthRecovery = 0;
    private float healthSteal = 0;
    //攻击增幅
    private float attackAmplification = 0;
    //近战伤害 远程伤害 属性伤害
    private float meleeDamage = 0;
    private float rangedDamage = 0;
    private float abilityDamage = 0;
    //攻击速度加成
    private float attackSpeedAmplification = 0;
    //暴击率
    private float criticalRate = 0;
    //工程机械
    private float engineering = 0;
    //攻击范围加成
    private float attackRangeAmplification = 0;
    //机甲强度
    private float armorStrength = 0;
    //闪避概率
    private float dodgeRate = 0;
    //移速加成
    private float moveSpeedAmplification = 0;
    //扫描精度
    private float scanAccuracy = 0;
    //采集效率
    private float collectEfficiency = 0;

    //以下是对道具属性的设置
    public void setPropID(int input)
    {
        propID = input;
    }
    public void setPropName(string input)
    {
        propName = input;
    }
    public void setPropQuality(WeaponAttribute.Quality input)
    {
        propQuality = input;
    }
    public void setPropIcon(string input)
    {
        propIcon = input;
    }
    public void setPropBgIcon(string input)
    {
        propBgIcon = input;
    }
    public void setPropPrice(float input)
    {
        propPrice = input;
    }
    public void setMaxHealth(float input)
    {
        maxHealth = input;
    }
    public void setHealthRecovery(float input)
    {
        healthRecovery = input;
    }
    public void setHealthSteal(float input)
    {
        healthSteal = input;
    }
    public void setAttackAmplification(float input)
    {
        attackAmplification = input;
    }
    public void setMeleeDamage(float input)
    {
        meleeDamage = input;
    }
    public void setRangedDamage(float input)
    {
        rangedDamage = input;
    }
    public void setAbilityDamage(float input)
    {
        abilityDamage = input;
    }
    public void setAttackSpeedAmplification(float input)
    {
        attackSpeedAmplification = input;
    }
    public void setCriticalRate(float input)
    {
        criticalRate = input;
    }
    public void setEngineering(float input)
    {
        engineering = input;
    }
    public void setAttackRangeAmplification(float input)
    {
        attackRangeAmplification = input;
    }
    public void setArmorStrength(float input)
    {
        armorStrength = input;
    }
    public void setDodgeRate(float input)
    {
        dodgeRate = input;
    }
    public void setMoveSpeedAmplification(float input)
    {
        moveSpeedAmplification = input;
    }
    public void setScanAccuracy(float input)
    {
        scanAccuracy = input;
    }
    public void setCollectEfficiency(float input)
    {
        collectEfficiency = input;
    }

    //以下是对道具属性的获取
    public int getPropID()
    {
        return propID;
    }
    public string getPropName()
    {
        return propName;
    }
    public WeaponAttribute.Quality getPropQuality()
    {
        return propQuality;
    }
    public string getPropIcon()
    {
        return propIcon;
    }
    public string getPropBgIcon()
    {
        return propBgIcon;
    }
    public float getPropPrice()
    {
        return propPrice;
    }
    public float getMaxHealth()
    {
        return maxHealth;
    }
    public float getHealthRecovery()
    {
        return healthRecovery;
    }
    public float getHealthSteal()
    {
        return healthSteal;
    }
    public float getAttackAmplification()
    {
        return attackAmplification;
    }
    public float getMeleeDamage()
    {
        return meleeDamage;
    }
    public float getRangedDamage()
    {
        return rangedDamage;
    }
    public float getAbilityDamage()
    {
        return abilityDamage;
    }
    public float getAttackSpeedAmplification()
    {
        return attackSpeedAmplification;
    }
    public float getCriticalRate()
    {
        return criticalRate;
    }
    public float getEngineering()
    {
        return engineering;
    }
    public float getAttackRangeAmplification()
    {
        return attackRangeAmplification;
    }
    public float getArmorStrength()
    {
        return armorStrength;
    }
    public float getDodgeRate()
    {
        return dodgeRate;
    }
    public float getMoveSpeedAmplification()
    {
        return moveSpeedAmplification;
    }
    public float getScanAccuracy()
    {
        return scanAccuracy;
    }
    public float getCollectEfficiency()
    {
        return collectEfficiency;
    }

    public void setPropAttribute(PropAttribute prop)
    {
        setPropID(prop.getPropID());
        setPropName(prop.getPropName());
        setPropQuality(prop.getPropQuality());
        setPropIcon(prop.getPropIcon());
        setPropBgIcon(prop.getPropBgIcon());
        setPropPrice(prop.getPropPrice());
        setMaxHealth(prop.getMaxHealth());
        setHealthRecovery(prop.getHealthRecovery());
        setHealthSteal(prop.getHealthSteal());
        setAttackAmplification(prop.getAttackAmplification());
        setMeleeDamage(prop.getMeleeDamage());
        setRangedDamage(prop.getRangedDamage());
        setAbilityDamage(prop.getAbilityDamage());
        setCriticalRate(prop.getCriticalRate());
        setEngineering(prop.getEngineering());
        setAttackRangeAmplification(prop.getAttackRangeAmplification());
        setArmorStrength(prop.getArmorStrength());
        setDodgeRate(prop.getDodgeRate());
        setMoveSpeedAmplification(prop.getMoveSpeedAmplification());
        setScanAccuracy(prop.getScanAccuracy());
        setCollectEfficiency(prop.getCollectEfficiency());
    }
}
