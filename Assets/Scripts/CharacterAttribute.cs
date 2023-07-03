using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttribute : MonoBehaviour
{
    //角色或怪物的种类ID
    private int id = 0;
    //角色的名字
    private string characterName = "";
    //角色的图标
    private string icon = "";
    //角色允许使用的武器分类
    private List<WeaponAttribute.WeaponCategory> weaponCategory = new();
    //角色自带的弹道数
    private int bulletCount = 0;
    //角色的基础移速
    private float rawMoveSpeed = 4.0f;
    //角色受击后的无敌时间
    private float immuneTime = 0.2f;
    //角色的当前生命值
    private float currentHealth = 10f;
    //角色的当前经验值
    private float currentExp = 0;
    //角色的当前等级
    private int currentPlayerLevel = 0;
    //角色在最低难度从0级升到1级需要的经验值
    private float basicUpgradeExp = 15f;
    //怪物预制体的路径
    private string monsterPrefabPath = "";
    //怪物在最低难度每经过一波袭击后增加的最大生命值
    private float healthIncPerWave = 0;
    //怪物在最低难度每经过一波袭击后增加的伤害值
    private float damageIncPerWave = 0;
    //击败怪物后掉落的货币数量
    private float lootCount = 1;
    //击败怪物后掉落消耗品的概率
    private float dropRate = 0.5f;
    //击败怪物后掉落箱子的概率
    private float crateRate = 0.1f;
    //怪物允许被生成的最早波次
    private int firstGenWave = 1;
    //怪物的生成间隔
    private float interval = 0.5f;
    //怪物的最小生成数量
    private float minGenCount = 1f;
    //怪物的最大生成数量
    private float maxGenCount = 2f;
    //怪物属于的关卡
    private string belongLevel = "";
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

    //所有属性的set方法
    public void setID(int input)
    {
        id = input;
    }

    public void setName(string input)
    {
        characterName = input;
    }

    public void setIcon(string input)
    {
        icon = input;
    }

    public void setWeaponCategory(List<WeaponAttribute.WeaponCategory> input)
    {
        weaponCategory = input;
    }

    public void setBulletCount(int input)
    {
        bulletCount = input;
    }

    public void setRawMoveSpeed(float input)
    {
        rawMoveSpeed = input;
    }

    public void setImmuneTime(float input)
    {
        immuneTime = input;
    }

    public void setCurrentHealth(float input)
    {
        currentHealth = input;
    }

    public void setCurrentExp(float input)
    {
        currentExp = input;
    }

    public void setCurrentPlayerLevel(int input)
    {
        currentPlayerLevel = input;
    }

    public void setBasicUpgradeExp(float input)
    {
        basicUpgradeExp = input;
    }

    public void setMonsterPrefabPath(string input)
    {
        monsterPrefabPath = input;
    }

    public void setHealthIncPerWave(float input)
    {
        healthIncPerWave = input;
    }

    public void setDamageIncPerWave(float input)
    {
        damageIncPerWave = input;
    }

    public void setLootCount(float input)
    {
        lootCount = input;
    }

    public void setDropRate(float input)
    {
        dropRate = input;
    }

    public void setCrateRate(float input)
    {
        crateRate = input;
    }

    public void setFirstGenWave(int input)
    {
        firstGenWave = input;
    }

    public void setInterval(float input)
    {
        interval = input;
    }

    public void setMinGenCount(float input)
    {
        minGenCount = input;
    }

    public void setMaxGenCount(float input)
    {
        maxGenCount = input;
    }

    public void setBelongLevel(string input)
    {
        belongLevel = input;
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

    //用于初始化角色
    public void setAllPlayerAttribute(int id, float rawMoveSpeed, float currentHealth, float currentExp, int currentPlayerLevel,
        float basicUpgradeExp, float maxHealth, float healthRecovery, float healthSteal, float attackAmplication,
        float meleeDamage, float rangedDamage, float abilityDamage, float attackSpeedAmplification, float criticalRate,
        float engieering, float attackRangeAmplification, float armorStrength, float dodgeRate, float moveSpeedAmplification,
        float scanAccuracy, float collectEfficiency, int bulletCount)
    {
        setID(id);
        setRawMoveSpeed(rawMoveSpeed);
        setCurrentHealth(currentHealth);
        setCurrentExp(currentExp);
        setCurrentPlayerLevel(currentPlayerLevel);
        setBasicUpgradeExp(basicUpgradeExp);
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
        setAttackRangeAmplification(attackRangeAmplification);
        setArmorStrength(armorStrength);
        setDodgeRate(dodgeRate);
        setMoveSpeedAmplification(moveSpeedAmplification);
        setScanAccuracy(scanAccuracy);
        setCollectEfficiency(collectEfficiency);
        setBulletCount(bulletCount);
    }

    public void setAllPlayerAttribute(CharacterAttribute input)
    {
        setAllPlayerAttribute(input.getID(), input.getRawMoveSpeed(), input.getCurrentHealth(), input.getCurrentExp(), input.getCurrentPlayerLevel(),
                              input.getBasicUpgradeExp(), input.getMaxHealth(), input.getHealthRecovery(), input.getHealthSteal(), input.getAttackAmplification(),
                              input.getMeleeDamage(), input.getRangedDamage(), input.getAbilityDamage(), input.getAttackSpeedAmplification(), input.getCriticalRate(),
                              input.getEngineering(), input.getAttackRangeAmplification(), input.getArmorStrength(), input.getDodgeRate(), input.getMoveSpeedAmplification(),
                              input.getScanAccuracy(), input.getCollectEfficiency(), input.getBulletCount());
    }

    //用于初始化怪物
    public void setAllMonsterAttribute(int id, float maxHealth, float healthIncPerWave, float speed,
        float meleeDamage, float rangedDamage, float damageIncPerWave, float lootCount, float dropRate,
        float crateRate, int firstGenWave, float interval, float minGenCount, float maxGenCount, string prefabPath,
        string belongLevel, float armorStrength, float attackRange)
    {
        setID(id);
        setMaxHealth(maxHealth);
        setCurrentHealth(maxHealth);
        setHealthIncPerWave(healthIncPerWave);
        setRawMoveSpeed(speed);
        setMeleeDamage(meleeDamage);
        setRangedDamage(rangedDamage);
        setDamageIncPerWave(damageIncPerWave);
        setLootCount(lootCount);
        setDropRate(dropRate);
        setCrateRate(crateRate);
        setFirstGenWave(firstGenWave);
        setInterval(interval);
        setMinGenCount(minGenCount);
        setMaxGenCount(maxGenCount);
        setMonsterPrefabPath(prefabPath);
        setBelongLevel(belongLevel);
        setArmorStrength(armorStrength);
        setAttackRangeAmplification(attackRange);
    }

    public void setAllMonsterAttribute(CharacterAttribute input)
    {
        setAllMonsterAttribute(input.getID(), input.getMaxHealth(), input.getHealthIncPerWave(), input.getRawMoveSpeed(), input.getMeleeDamage(),
                               input.getRangedDamage(), input.getDamageIncPerWave(), input.getLootCount(), input.getDropRate(), input.getCrateRate(),
                               input.getFirstGenWave(), input.getInterval(), input.getMinGenCount(), input.getMaxGenCount(), input.getMonsterPrefabPath(),
                               input.getBelongLevel(), input.getArmorStrength(), input.getAttackRangeAmplification());
    }

    //获取经过属性加成的移速
    public float getMoveSpeed()
    {
        return rawMoveSpeed * (1 + moveSpeedAmplification * 0.01f);
    }

    //获取基础移速
    public float getRawMoveSpeed()
    {
        return rawMoveSpeed;
    }

    public int getID()
    {
        return id;
    }

    public string getName()
    {
        return characterName;
    }

    public string getIcon()
    {
        return icon;
    }

    public List<WeaponAttribute.WeaponCategory> getWeaponCategory()
    {
        return weaponCategory;
    }

    public int getBulletCount()
    {
        return bulletCount;
    }

    public float getImmuneTime()
    {
        return immuneTime;
    }

    public float getCurrentHealth()
    {
        return currentHealth;
    }

    public float getCurrentExp()
    {
        return currentExp;
    }

    public int getCurrentPlayerLevel()
    {
        return currentPlayerLevel;
    }

    public float getBasicUpgradeExp()
    {
        return basicUpgradeExp;
    }

    public string getMonsterPrefabPath()
    {
        return monsterPrefabPath;
    }

    public float getHealthIncPerWave()
    {
        return healthIncPerWave;
    }

    public float getDamageIncPerWave()
    {
        return damageIncPerWave;
    }

    public float getLootCount()
    {
        return lootCount;
    }

    public float getDropRate()
    {
        return dropRate;
    }

    public float getCrateRate()
    {
        return crateRate;
    }

    public int getFirstGenWave()
    {
        return firstGenWave;
    }

    public float getInterval()
    {
        return interval;
    }

    public float getMinGenCount()
    {
        return minGenCount;
    }

    public float getMaxGenCount()
    {
        return maxGenCount;
    }

    public string getBelongLevel()
    {
        return belongLevel;
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

    public void propModifyAttribute(int propIndex, int count = 1)
    {
        PropAttribute prop = JsonLoader.propPool[propIndex];
        maxHealth += prop.getMaxHealth() * count;
        healthRecovery += prop.getHealthRecovery() * count;
        healthSteal += prop.getHealthSteal() * count;
        attackAmplification += prop.getAttackAmplification() * count;
        meleeDamage += prop.getMeleeDamage() * count;
        rangedDamage += prop.getRangedDamage() * count;
        abilityDamage += prop.getAbilityDamage() * count;
        attackSpeedAmplification += prop.getAttackSpeedAmplification() * count;
        criticalRate += prop.getCriticalRate() * count;
        engineering += prop.getEngineering() * count;
        attackRangeAmplification += prop.getAttackRangeAmplification() * count;
        armorStrength += prop.getArmorStrength() * count;
        dodgeRate += prop.getDodgeRate() * count;
        moveSpeedAmplification += prop.getMoveSpeedAmplification() * count;
        scanAccuracy += prop.getScanAccuracy() * count;
        collectEfficiency += prop.getCollectEfficiency() * count;
    }
}
