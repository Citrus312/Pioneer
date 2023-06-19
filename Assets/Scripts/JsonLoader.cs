using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System.Text;

public class JsonLoader : MonoBehaviour
{
    //�������Գ� �������Գ�
    public static List<WeaponAttribute> weaponPool = new();
    public static List<PropAttribute> propPool = new();
    //
    public static Dictionary<string, dynamic> gameData = new();

    //
    public static void LoadAndDecodeGameData()
    {
        List<int> tempList = new();
        JsonData data = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/Config/LocalData.json", Encoding.GetEncoding("GB2312")));
        gameData.Add("isFirstPlaying", (bool)data["isFirstPlaying"]);
        gameData.Add("playerID", (int)data["playerID"]);
        gameData.Add("wave", (int)data["wave"]);
        gameData.Add("level", (int)data["level"]);
        gameData.Add("money", (int)data["money"]);
        gameData.Add("playerLevel", (int)data["playerLevel"]);
        gameData.Add("exp", (int)data["exp"]);
        JsonData temp = data["propList"];
        for (int i = 0; i < temp.Count; i++)
        {
            tempList.Add((int)temp[i]);
        }
        gameData.Add("propList", tempList);
        tempList.Clear();
        temp.Clear();
        temp = data["propCount"];
        for (int i = 0; i < temp.Count; i++)
        {
            tempList.Add((int)temp[i]);
        }
        gameData.Add("propCount", tempList);
        tempList.Clear();
        temp.Clear();
        temp = data["weaponList"];
        for (int i = 0; i < temp.Count; i++)
        {
            tempList.Add((int)temp[i]);
        }
        gameData.Add("weaponList", tempList);
    }

    //
    public static void UpdateGameData()
    {
        File.WriteAllText(Application.dataPath + "/Config/GameData.json", JsonMapper.ToJson(gameData));
    }

    //���ز�������������
    public static void LoadAndDecodeWeaponConfig()
    {
        //json�ļ��ж�ȡ������������
        JsonData weaponsConfig = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/Config/Weapons.json", Encoding.GetEncoding("GB2312")));
        //�洢��json��ȡ������������ʱ����
        List<WeaponAttribute.WeaponCategory> tempCategory = new();
        //��json�ļ��л�ȡ��һ������������
        JsonData weaponConfig;
        JsonData weaponCategory;
        double temp;
        for (int i = 0; i < weaponsConfig.Count; i++)
        {
            //��ӽ����е�����������ʱ����
            WeaponAttribute addAttr = new();
            weaponConfig = weaponsConfig[i];
            //����LitJson��֧��float���͵�ǿת���ʴ˴���ǿתΪdouble������ʱ�����ٽ���ʱ����ǿתΪfloatʹ��
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
            addAttr.setWeaponID((int)weaponConfig["ID"]);
            addAttr.setWeaponName((string)weaponConfig["name"]);
            addAttr.setWeaponIcon((string)weaponConfig["icon"]);
            addAttr.setWeaponBgIcon((string)weaponConfig["bgIcon"]);
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
                    default:
                        Debug.Log("weapon json config " + i + ": weapon category" + (string)weaponCategory[i] + " error");
                        break;
                }
            }
            addAttr.setWeaponCategory(tempCategory);
            //���洢����ʱ�����е��������Դ����������Գ�
            weaponPool.Add(addAttr);
        }
    }

    //���ز�������������
    public static void LoadAndDecodePropConfig()
    {
        JsonData propsConfig = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/Config/Props.json", Encoding.GetEncoding("GB2312")));
        JsonData propConfig;
        double temp;
        for (int i = 0; i < propsConfig.Count; i++)
        {
            PropAttribute addAttr = new();
            propConfig = propsConfig[i];
            addAttr.setPropID((int)propConfig[i]["ID"]);
            addAttr.setPropName((string)propConfig[i]["name"]);
            addAttr.setPropIcon((string)propConfig[i]["icon"]);
            addAttr.setPropBgIcon((string)propConfig[i]["bgIcon"]);
            temp = (double)propConfig[i]["maxHealth"];
            addAttr.setMaxHealth((float)temp);
            temp = (double)propConfig[i]["healthRecovery"];
            addAttr.setHealthRecovery((float)temp);
            temp = (double)propConfig[i]["healthSteal"];
            addAttr.setHealthSteal((float)temp);
            temp = (double)propConfig[i]["attackAmplification"];
            addAttr.setAttackAmplification((float)temp);
            temp = (double)propConfig[i]["meleeDamage"];
            addAttr.setMeleeDamage((float)temp);
            temp = (double)propConfig[i]["rangedDamage"];
            addAttr.setRangedDamage((float)temp);
            temp = (double)propConfig[i]["abilityDamage"];
            addAttr.setAbilityDamage((float)temp);
            temp = (double)propConfig[i]["attackSpeedAmplification"];
            addAttr.setAttackSpeedAmplification((float)temp);
            temp = (double)propConfig[i]["criticalRate"];
            addAttr.setCriticalRate((float)temp);
            temp = (double)propConfig[i]["engineering"];
            addAttr.setEngineering((float)temp);
            temp = (double)propConfig[i]["attackRangeAmplification"];
            addAttr.setAttackRangedAmplification((float)temp);
            temp = (double)propConfig[i]["armorStrength"];
            addAttr.setArmorStrength((float)temp);
            temp = (double)propConfig[i]["dodgeRate"];
            addAttr.setDodgeRate((float)temp);
            temp = (double)propConfig[i]["moveSpeedAmplification"];
            addAttr.setMoveSpeedAmplification((float)temp);
            temp = (double)propConfig[i]["scanAccuracy"];
            addAttr.setScanAccuracy((float)temp);
            temp = (double)propConfig[i]["collectEfficiency"];
            addAttr.setCollectEfficiency((float)temp);
            switch ((int)propConfig[i]["quality"])
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
                    Debug.Log("prop json config " + i + ": quality type" + (int)propConfig[i]["quality"] + " error");
                    break;
            }
            propPool.Add(addAttr);
        }
    }
}
