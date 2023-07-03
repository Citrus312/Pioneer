using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System.Text;

public class JsonLoader
{
    //武器属性池 道具属性池 角色属性池
    public static List<WeaponAttribute> weaponPool = new();
    public static List<PropAttribute> propPool = new();
    public static List<CharacterAttribute> rolePool = new();
    public static List<CharacterAttribute> monsterPool = new();

    //加载并解析游戏数据
    public static void LoadAndDecodeGameData()
    {
        //获取游戏内全局的游戏数据对象
        GameData gameData = GameController.getInstance().getGameData();
        //从json文件中读取的数据
        JsonData data = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/Config/GameData.json", Encoding.GetEncoding("utf-8")));
        //对游戏数据对象的成员依次赋值
        gameData._isFirstPlaying = (bool)data["isFirstPlaying"];
        gameData._playerID = (int)data["playerID"];
        gameData._wave = (int)data["wave"];
        gameData._level = (int)data["level"];
        gameData._money = (int)data["money"];
        gameData._playerLevel = (int)data["playerLevel"];
        gameData._exp = (int)data["exp"];
        gameData._difficulty = (int)data["difficulty"];
        gameData._scene = (string)data["scene"];
        JsonData temp1 = data["propList"];
        for (int i = 0; i < temp1.Count; i++)
        {
            gameData._propList.Add((int)temp1[i]);
        }
        JsonData temp2 = data["propCount"];
        for (int i = 0; i < temp2.Count; i++)
        {
            gameData._propCount.Add((int)temp2[i]);
        }
        JsonData temp3 = data["weaponList"];
        for (int i = 0; i < temp3.Count; i++)
        {
            gameData._weaponList.Add((int)temp3[i]);
        }
    }

    //保存当前的游戏数据
    public static void UpdateGameData()
    {
        File.WriteAllText(Application.dataPath + "/Config/GameData.json", JsonMapper.ToJson(GameController.getInstance().getGameData().Data2Dict()));
    }

    //加载并解析武器数据
    public static void LoadAndDecodeWeaponConfig()
    {
        //json文件中读取到的所有数据
        JsonData weaponsConfig = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/Config/Weapons.json", Encoding.GetEncoding("utf-8")));
        //存储从json获取的武器分类临时变量
        List<WeaponAttribute.WeaponCategory> tempCategory = new();
        //从json文件中获取的一个武器的数据
        JsonData weaponConfig;
        JsonData weaponCategory;
        double temp;
        for (int i = 0; i < weaponsConfig.Count; i++)
        {
            //添加进池中的武器属性临时变量
            WeaponAttribute addAttr = new();
            weaponConfig = weaponsConfig[i];
            //由于LitJson不支持float类型的强转，故此处先强转为double存入临时变量再将临时变量强转为float使用
            temp = (double)weaponConfig["damage"];
            addAttr.setRawWeaponDamage((float)temp);
            temp = (double)weaponConfig["bonus"];
            addAttr.setCriticalBonus((float)temp);
            temp = (double)weaponConfig["range"];
            addAttr.setRawAttackRange((float)temp);
            temp = (double)weaponConfig["speed"];
            addAttr.setRawAttackSpeed((float)temp);
            temp = (double)weaponConfig["ratio"];
            addAttr.setConvertRatio((float)temp);
            temp = (double)weaponConfig["rate"];
            addAttr.setRawCriticalRate((float)temp);
            temp = (double)weaponConfig["price"];
            addAttr.setWeaponPrice((float)temp);
            addAttr.setBulletCount((int)weaponConfig["bulletCount"]);
            addAttr.setWeaponID((int)weaponConfig["ID"]);
            addAttr.setBulletCount((int)weaponConfig["bulletCount"]);
            addAttr.setWeaponName((string)weaponConfig["name"]);
            addAttr.setWeaponIcon((string)weaponConfig["icon"]);
            addAttr.setWeaponBgIcon((string)weaponConfig["bgIcon"]);
            addAttr.setWeaponPrefabPath((string)weaponConfig["prefabPath"]);
            switch ((string)weaponConfig["type"])
            {
                case "melee":
                    addAttr.setWeaponDamageType(WeaponAttribute.WeaponDamageType.Melee);
                    break;
                case "ranged":
                    addAttr.setWeaponDamageType(WeaponAttribute.WeaponDamageType.Ranged);
                    break;
                case "ability":
                    addAttr.setWeaponDamageType(WeaponAttribute.WeaponDamageType.Ability);
                    break;
                default:
                    Debug.Log("weapon json config " + i + ": damage type" + (string)weaponConfig["type"] + " error");
                    break;
            }
            switch ((int)weaponConfig["quality"])
            {
                case 0:
                    addAttr.setWeaponQuality(WeaponAttribute.Quality.Normal);
                    break;
                case 1:
                    addAttr.setWeaponQuality(WeaponAttribute.Quality.Senior);
                    break;
                case 2:
                    addAttr.setWeaponQuality(WeaponAttribute.Quality.Elite);
                    break;
                case 3:
                    addAttr.setWeaponQuality(WeaponAttribute.Quality.Legendary);
                    break;
                default:
                    Debug.Log("weapon json config " + i + ": quality type" + (int)weaponConfig["quality"] + " error");
                    break;
            }
            weaponCategory = weaponConfig["category"];
            for (int j = 0; j < weaponCategory.Count; j++)
            {
                switch ((string)weaponCategory[j])
                {
                    case "gun":
                        tempCategory.Add(WeaponAttribute.WeaponCategory.Gun);
                        break;
                    case "ability":
                        tempCategory.Add(WeaponAttribute.WeaponCategory.Ability);
                        break;
                    case "heal":
                        tempCategory.Add(WeaponAttribute.WeaponCategory.Heal);
                        break;
                    case "wand":
                        tempCategory.Add(WeaponAttribute.WeaponCategory.Wand);
                        break;
                    case "machete":
                        tempCategory.Add(WeaponAttribute.WeaponCategory.Machete);
                        break;
                    case "polearms":
                        tempCategory.Add(WeaponAttribute.WeaponCategory.Polearms);
                        break;
                    case "sword":
                        tempCategory.Add(WeaponAttribute.WeaponCategory.Sword);
                        break;
                    default:
                        Debug.Log("weapon json config " + i + ": weapon category" + (string)weaponCategory[i] + " error");
                        break;
                }
            }
            addAttr.setWeaponCategory(tempCategory);
            //将存储在临时变量中的武器属性存入武器属性池
            weaponPool.Add(addAttr);
        }
    }

    //加载并解析道具属性
    public static void LoadAndDecodePropConfig()
    {
        JsonData propsConfig = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/Config/Props.json", Encoding.GetEncoding("utf-8")));
        JsonData propConfig;
        double temp;
        for (int i = 0; i < propsConfig.Count; i++)
        {
            PropAttribute addAttr = new();
            propConfig = propsConfig[i];
            addAttr.setPropID((int)propConfig["ID"]);
            GameController.getInstance().getGameData()._propCountPerQuality[(int)Mathf.Floor(((int)propConfig["ID"] - 40000) / 10000)] += 1;
            addAttr.setPropName((string)propConfig["name"]);
            addAttr.setPropIcon((string)propConfig["icon"]);
            addAttr.setPropBgIcon((string)propConfig["bgIcon"]);
            temp = (double)propConfig["price"];
            addAttr.setPropPrice((float)temp);
            temp = (double)propConfig["maxHealth"];
            addAttr.setMaxHealth((float)temp);
            temp = (double)propConfig["healthRecovery"];
            addAttr.setHealthRecovery((float)temp);
            temp = (double)propConfig["healthSteal"];
            addAttr.setHealthSteal((float)temp);
            temp = (double)propConfig["attackAmplification"];
            addAttr.setAttackAmplification((float)temp);
            temp = (double)propConfig["meleeDamage"];
            addAttr.setMeleeDamage((float)temp);
            temp = (double)propConfig["rangedDamage"];
            addAttr.setRangedDamage((float)temp);
            temp = (double)propConfig["abilityDamage"];
            addAttr.setAbilityDamage((float)temp);
            temp = (double)propConfig["attackSpeedAmplification"];
            addAttr.setAttackSpeedAmplification((float)temp);
            temp = (double)propConfig["criticalRate"];
            addAttr.setCriticalRate((float)temp);
            temp = (double)propConfig["engineering"];
            addAttr.setEngineering((float)temp);
            temp = (double)propConfig["attackRangeAmplification"];
            addAttr.setAttackRangedAmplification((float)temp);
            temp = (double)propConfig["armorStrength"];
            addAttr.setArmorStrength((float)temp);
            temp = (double)propConfig["dodgeRate"];
            addAttr.setDodgeRate((float)temp);
            temp = (double)propConfig["moveSpeedAmplification"];
            addAttr.setMoveSpeedAmplification((float)temp);
            temp = (double)propConfig["scanAccuracy"];
            addAttr.setScanAccuracy((float)temp);
            temp = (double)propConfig["collectEfficiency"];
            addAttr.setCollectEfficiency((float)temp);
            switch ((int)propConfig["quality"])
            {
                case 0:
                    addAttr.setPropQuality(WeaponAttribute.Quality.Normal);
                    break;
                case 1:
                    addAttr.setPropQuality(WeaponAttribute.Quality.Senior);
                    break;
                case 2:
                    addAttr.setPropQuality(WeaponAttribute.Quality.Elite);
                    break;
                case 3:
                    addAttr.setPropQuality(WeaponAttribute.Quality.Legendary);
                    break;
                default:
                    Debug.Log("prop json config " + i + ": quality type" + (int)propConfig["quality"] + " error");
                    break;
            }
            propPool.Add(addAttr);
        }
    }

    //加载并解析角色属性
    public static void LoadAndDecodeRoleConfig()
    {
        JsonData rolesConfig = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/Config/Roles.json", Encoding.GetEncoding("utf-8")));
        JsonData roleConfig;
        JsonData weaponCategory;
        List<WeaponAttribute.WeaponCategory> tempCategory = new();
        double temp;

        for (int i = 0; i < rolesConfig.Count; i++)
        {
            CharacterAttribute addAttr = new();
            roleConfig = rolesConfig[i];
            addAttr.setID((int)roleConfig["ID"]);
            addAttr.setBulletCount((int)roleConfig["bulletCount"]);
            addAttr.setName((string)roleConfig["name"]);
            addAttr.setIcon((string)roleConfig["icon"]);
            temp = (double)roleConfig["rawMoveSpeed"];
            addAttr.setRawMoveSpeed((float)temp);
            temp = (double)roleConfig["maxHealth"];
            addAttr.setMaxHealth((float)temp);
            temp = (double)roleConfig["healthRecovery"];
            addAttr.setHealthRecovery((float)temp);
            temp = (double)roleConfig["healthSteal"];
            addAttr.setHealthSteal((float)temp);
            temp = (double)roleConfig["attackAmplification"];
            addAttr.setAttackAmplification((float)temp);
            temp = (double)roleConfig["meleeDamage"];
            addAttr.setMeleeDamage((float)temp);
            temp = (double)roleConfig["rangedDamage"];
            addAttr.setRangedDamage((float)temp);
            temp = (double)roleConfig["abilityDamage"];
            addAttr.setAbilityDamage((float)temp);
            temp = (double)roleConfig["attackSpeedAmplification"];
            addAttr.setAttackSpeedAmplification((float)temp);
            temp = (double)roleConfig["criticalRate"];
            addAttr.setCriticalRate((float)temp);
            temp = (double)roleConfig["engineering"];
            addAttr.setEngineering((float)temp);
            temp = (double)roleConfig["attackRangeAmplification"];
            addAttr.setAttackRangeAmplification((float)temp);
            temp = (double)roleConfig["armorStrength"];
            addAttr.setArmorStrength((float)temp);
            temp = (double)roleConfig["dodgeRate"];
            addAttr.setDodgeRate((float)temp);
            temp = (double)roleConfig["moveSpeedAmplification"];
            addAttr.setMoveSpeedAmplification((float)temp);
            temp = (double)roleConfig["scanAccuracy"];
            addAttr.setScanAccuracy((float)temp);
            temp = (double)roleConfig["collectEfficiency"];
            addAttr.setCollectEfficiency((float)temp);
            weaponCategory = roleConfig["weaponCategory"];
            for (int j = 0; j < weaponCategory.Count; j++)
            {
                switch ((string)weaponCategory[j])
                {
                    case "all":
                        tempCategory.Add(WeaponAttribute.WeaponCategory.All);
                        break;
                    case "gun":
                        tempCategory.Add(WeaponAttribute.WeaponCategory.Gun);
                        break;
                    case "ability":
                        tempCategory.Add(WeaponAttribute.WeaponCategory.Ability);
                        break;
                    case "heal":
                        tempCategory.Add(WeaponAttribute.WeaponCategory.Heal);
                        break;
                    case "wand":
                        tempCategory.Add(WeaponAttribute.WeaponCategory.Wand);
                        break;
                    case "machete":
                        tempCategory.Add(WeaponAttribute.WeaponCategory.Machete);
                        break;
                    case "polearms":
                        tempCategory.Add(WeaponAttribute.WeaponCategory.Polearms);
                        break;
                    case "sword":
                        tempCategory.Add(WeaponAttribute.WeaponCategory.Sword);
                        break;
                    default:
                        Debug.Log("role json config " + i + ": weapon category" + (string)weaponCategory[i] + " error");
                        break;
                }
            }
            addAttr.setWeaponCategory(tempCategory);
            rolePool.Add(addAttr);
        }
    }

    //加载并解析怪物属性
    public static void LoadAndDecodeMonsterConfig()
    {
        JsonData monstersConfig = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/Config/Monsters.json", Encoding.GetEncoding("utf-8")));
        JsonData monsterConfig;
        double temp;
        for (int i = 0; i < monstersConfig.Count; i++)
        {
            CharacterAttribute addAttr = new();
            monsterConfig = monstersConfig[i];
            addAttr.setID((int)monsterConfig["ID"]);
            addAttr.setMonsterPrefabPath((string)monsterConfig["prefabPath"]);
            addAttr.setFirstGenWave((int)monsterConfig["firstGenWave"]);
            addAttr.setBelongLevel((string)monsterConfig["belongLevel"]);
            temp = (double)monsterConfig["attackRange"];
            addAttr.setAttackRangeAmplification((float)temp);
            temp = (double)monsterConfig["armorStrength"];
            addAttr.setArmorStrength((float)temp);
            temp = (double)monsterConfig["maxHealth"];
            addAttr.setMaxHealth((float)temp);
            temp = (double)monsterConfig["healthIncPerWave"];
            addAttr.setHealthIncPerWave((float)temp);
            temp = (double)monsterConfig["speed"];
            addAttr.setRawMoveSpeed((float)temp);
            temp = (double)monsterConfig["meleeDamage"];
            addAttr.setMeleeDamage((float)temp);
            temp = (double)monsterConfig["rangedDamage"];
            addAttr.setRangedDamage((float)temp);
            temp = (double)monsterConfig["damageIncPerWave"];
            addAttr.setDamageIncPerWave((float)temp);
            temp = (double)monsterConfig["lootCount"];
            addAttr.setLootCount((float)temp);
            temp = (double)monsterConfig["dropRate"];
            addAttr.setDropRate((float)temp);
            temp = (double)monsterConfig["crateRate"];
            addAttr.setCrateRate((float)temp);
            temp = (double)monsterConfig["interval"];
            addAttr.setInterval((float)temp);
            temp = (double)monsterConfig["minGenCount"];
            addAttr.setMinGenCount((float)temp);
            temp = (double)monsterConfig["maxGenCount"];
            addAttr.setMaxGenCount((float)temp);

            monsterPool.Add(addAttr);
        }
    }
}
