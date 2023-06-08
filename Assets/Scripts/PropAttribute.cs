using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropAttribute : MonoBehaviour
{
    //������� �����ظ� ������ȡ
    private float maxHealth = 0;
    private float healthRecovery = 0;
    private float healthSteal = 0;
    //��������
    private float attackAmplification = 0;
    //��ս�˺� Զ���˺� �����˺�
    private float meleeDamage = 0;
    private float rangedDamage = 0;
    private float abilityDamage = 0;
    //�����ٶȼӳ�
    private float attackSpeedAmplification = 0;
    //������
    private float criticalRate = 0;
    //���̻�е
    private float engineering = 0;
    //������Χ�ӳ�
    private float attackRangeAmplification = 0;
    //����ǿ��
    private float armorStrength = 0;
    //���ܸ���
    private float dodgeRate = 0;
    //���ټӳ�
    private float moveSpeedAmplification = 0;
    //ɨ�辫��
    private float scanAccuracy = 0;
    //�ɼ�Ч��
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
