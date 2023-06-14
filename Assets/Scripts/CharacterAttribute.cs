using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttribute
{
    //对象池id
    private int poolIdx = 0;
    //角色的基础移速
    private float rawMoveSpeed = 0.1f;
    //角色受击后的无敌时间
    private float immuneTime = 0.2f;
    //最大生命 生命回复 生命汲取
    private float maxHealth = 10;
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
    //扫描精度(增加掉落概率和商店与升级的品质)
    private float scanAccuracy = 0;
    //采集效率
    private float collectEfficiency = 0;

    public void setPoolIdx(int input)
    {
        poolIdx = input;
    }

    //所有属性的set方法
    public void setRawMoveSpeed(float input)
    {
        rawMoveSpeed = input;
    }

    public void setImmuneTime(float input)
    {
        immuneTime = input;
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

    //用于初始化角色
    public void setAllAttribute(float rawMoveSpeed, float maxHealth, float healthRecovery, float healthSteal, float attackAmplication, float meleeDamage,
        float rangedDamage, float abilityDamage, float attackSpeedAmplification, float criticalRate, float engieering, float attackRangeAmplification,
        float armorStrength, float dodgeRate, float moveSpeedAmplification, float scanAccuracy, float collectEfficiency)
    {
        setRawMoveSpeed(rawMoveSpeed);
        setMaxHealth(maxHealth);
        setHealthRecovery(healthRecovery);
        setHealthSteal(healthSteal);
        setAttackAmplification(attackAmplication);
        setMeleeDamage(meleeDamage);
        setRangedDamage(rangedDamage);
        setAbilityDamage(abilityDamage);
        setAttackSpeedAmplification(attackSpeedAmplification);
        setCriticalRate(criticalRate);
        setEngineering(engieering);
        setAttackRangedAmplification(attackRangeAmplification);
        setArmorStrength(armorStrength);
        setDodgeRate(dodgeRate);
        setMoveSpeedAmplification(moveSpeedAmplification);
        setScanAccuracy(scanAccuracy);
        setCollectEfficiency(collectEfficiency);
    }

    // 获取对象池id
    public int getPoolIdx()
    {
        return poolIdx;
    }

    //获取经过属性加成的移速
    public float getMoveSpeed()
    {
        return rawMoveSpeed * (1 + moveSpeedAmplification);
    }

    //获取基础移速
    public float getRawMoveSpeed()
    {
        return rawMoveSpeed;
    }

    public float getImmuneTime()
    {
        return immuneTime;
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
