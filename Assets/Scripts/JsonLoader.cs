using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System.Text;

public class JsonLoader
{
    //�������Գ� �������Գ�
    public static List<WeaponAttribute> weaponPool = new();
    public static List<PropAttribute> propPool = new();

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
            addAttr.setPropID((int)propConfig["ID"]);
            addAttr.setPropName((string)propConfig["name"]);
            addAttr.setPropIcon((string)propConfig["icon"]);
            addAttr.setPropBgIcon((string)propConfig["bgIcon"]);
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
}
