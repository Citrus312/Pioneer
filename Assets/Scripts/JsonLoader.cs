using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System.Text;

public class JsonLoader : MonoBehaviour
{
    public static List<WeaponAttribute> weaponPool = new();
    public static List<PropAttribute> propPool = new();

    public static void LoadAndDecodeWeaponConfig()
    {
        JsonData weaponConfig = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/Config/Weapons.json", Encoding.GetEncoding("GB2312")));
        List<WeaponAttribute.WeaponCategory> temp = new();
        for (int i = 0; i < weaponConfig.Count; i++)
        {
            weaponPool[i].setRawWeaponDamage((float)weaponConfig[i]["damage"]);
            weaponPool[i].setRawAttackRange((float)weaponConfig[i]["range"]);
            weaponPool[i].setRawAttackSpeed((float)weaponConfig[i]["speed"]);
            weaponPool[i].setConvertRatio((float)weaponConfig[i]["ratio"]);
            weaponPool[i].setCriticalBonus((float)weaponConfig[i]["bonus"]);
            weaponPool[i].setRawCriticalRate((float)weaponConfig[i]["rate"]);
            weaponPool[i].setWeaponPrice((float)weaponConfig[i]["price"]);
            weaponPool[i].setWeaponName((string)weaponConfig[i]["name"]);
            weaponPool[i].setWeaponID((int)weaponConfig[i]["ID"]);
            weaponPool[i].setWeaponIcon((string)weaponConfig[i]["icon"]);
            weaponPool[i].setWeaponBgIcon((string)weaponConfig[i]["bgIcon"]);
            switch ((string)weaponConfig[i]["type"])
            {
                case "melee":
                    weaponPool[i].setWeaponDamageType(WeaponAttribute.WeaponDamageType.Melee);
                    break;
                case "ranged":
                    weaponPool[i].setWeaponDamageType(WeaponAttribute.WeaponDamageType.Ranged);
                    break;
                case "ability":
                    weaponPool[i].setWeaponDamageType(WeaponAttribute.WeaponDamageType.Ability);
                    break;
                default:
                    Debug.Log("weapon json config " + i + ": damage type" + (string)weaponConfig[i]["type"] + " error");
                    break;
            }
            switch ((int)weaponConfig[i]["quality"])
            {
                case 0:
                    weaponPool[i].setWeaponQuality(WeaponAttribute.WeaponQuality.Normal);
                    break;
                case 1:
                    weaponPool[i].setWeaponQuality(WeaponAttribute.WeaponQuality.Senior);
                    break;
                case 2:
                    weaponPool[i].setWeaponQuality(WeaponAttribute.WeaponQuality.Elite);
                    break;
                case 3:
                    weaponPool[i].setWeaponQuality(WeaponAttribute.WeaponQuality.Legendary);
                    break;
                default:
                    Debug.Log("weapon json config " + i + ": quality type" + (int)weaponConfig[i]["quality"] + " error");
                    break;
            }
            JsonData weaponCategory = weaponConfig[i]["category"];
            for (int j = 0; j < weaponCategory.Count; j++)
            {
                switch ((string)weaponCategory[j])
                {
                    case "gun":
                        temp[j] = WeaponAttribute.WeaponCategory.Gun;
                        break;
                    case "ability":
                        temp[j] = WeaponAttribute.WeaponCategory.Ability;
                        break;
                    case "heal":
                        temp[j] = WeaponAttribute.WeaponCategory.Heal;
                        break;
                    default:
                        Debug.Log("weapon json config " + i + ": weapon category" + (string)weaponCategory[i] + " error");
                        break;
                }
            }
            weaponPool[i].setWeaponCategory(temp);
        }
    }

    public static void LoadAndDecodePropConfig()
    {
        JsonData propConfig = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/Config/Props.json", Encoding.GetEncoding("GB2312")));
        for (int i = 0; i < propConfig.Count; i++)
        {

        }
    }
}
