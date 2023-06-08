using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropAttribute : MonoBehaviour
{
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

    public void setAttackRangedAmplification(float input)
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
}
