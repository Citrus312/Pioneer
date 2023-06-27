using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameoverWindow : BaseWindow
{
    private static GameoverWindow instance;
    private Text attributeText;
    private Transform weaponDisplay;
    private Transform propDisplay;
    public string titleText;

    private GameoverWindow()
    {
        resName = "UI/GameoverWindow";
        isResident = true;
        isVisible = false;
        selfType = WindowType.GameoverWindow;
        sceneType = SceneType.Battle;
    }

    public static GameoverWindow Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new();
            }
            return instance;
        }
    }

    protected override void AwakeWindow()
    {
        base.AwakeWindow();

        attributeText = transform.Find("AttributeDisplay").GetChild(0).GetComponent<Text>();
        weaponDisplay = transform.Find("WeaponDisplay");
        propDisplay = transform.Find("PropDisplay");
        transform.Find("TitleText").GetComponent<Text>().text = titleText;

        CharacterAttribute roleAttr = GameController.getInstance()._player.GetComponent<CharacterAttribute>();
        //Debug.Log(roleAttr.get);
        attributeText.text = $"最大生命  <color={(roleAttr.getMaxHealth() > 0 ? "green" : "red")}> {roleAttr.getMaxHealth()} </color>\n" +
                             $"生命回复  <color={(roleAttr.getHealthRecovery() > 0 ? "green" : "red")}> {roleAttr.getHealthRecovery()} </color>\n" +
                             $"生命汲取  <color={(roleAttr.getHealthSteal() > 0 ? "green" : "red")}> {roleAttr.getHealthSteal()} </color>\n" +
                             $"攻击增幅  <color={(roleAttr.getAttackAmplification() > 0 ? "green" : "red")}> {roleAttr.getAttackAmplification()} </color>\n" +
                             $"近战伤害  <color={(roleAttr.getMeleeDamage() > 0 ? "green" : "red")}> {roleAttr.getMeleeDamage()} </color>\n" +
                             $"远程伤害  <color={(roleAttr.getRangedDamage() > 0 ? "green" : "red")}> {roleAttr.getRangedDamage()} </color>\n" +
                             $"属性伤害  <color={(roleAttr.getAbilityDamage() > 0 ? "green" : "red")}> {roleAttr.getAbilityDamage()} </color>\n" +
                             $"攻击速度  <color={(roleAttr.getAttackSpeedAmplification() > 0 ? "green" : "red")}> {roleAttr.getAttackSpeedAmplification()} </color>\n" +
                             $"暴击概率  <color={(roleAttr.getCriticalRate() > 0 ? "green" : "red")}> {roleAttr.getCriticalRate()} </color>\n" +
                             $"工程机械  <color={(roleAttr.getEngineering() > 0 ? "green" : "red")}> {roleAttr.getEngineering()} </color>\n" +
                             $"攻击范围  <color={(roleAttr.getAttackRangeAmplification() > 0 ? "green" : "red")}> {roleAttr.getAttackRangeAmplification()} </color>\n" +
                             $"机甲强度  <color={(roleAttr.getArmorStrength() > 0 ? "green" : "red")}> {roleAttr.getArmorStrength()} </color>\n" +
                             $"闪避概率  <color={(roleAttr.getDodgeRate() > 0 ? "green" : "red")}> {roleAttr.getDodgeRate()} </color>\n" +
                             $"移动速度  <color={(roleAttr.getMoveSpeedAmplification() > 0 ? "green" : "red")}> {roleAttr.getMoveSpeedAmplification()} </color>\n" +
                             $"扫描精度  <color={(roleAttr.getScanAccuracy() > 0 ? "green" : "red")}> {roleAttr.getScanAccuracy()} </color>\n" +
                             $"采集效率  <color={(roleAttr.getCollectEfficiency() > 0 ? "green" : "red")}> {roleAttr.getCollectEfficiency()} </color>\n";

        for (int i = 0; i < 6; i++)
        {
            if (i > GameController.getInstance().getGameData()._weaponList.Count - 1)
            {
                weaponDisplay.GetChild(i).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
            }
            else
            {
                string prePath = "";
                //根据武器的攻击类型确定武器图片的路径
                switch (JsonLoader.weaponPool[GameController.getInstance().getGameData()._weaponList[i]].getWeaponDamageType())
                {
                    case WeaponAttribute.WeaponDamageType.Melee:
                        prePath = "Assets/Sprites/Weapon/Melee Weapon/";
                        break;
                    case WeaponAttribute.WeaponDamageType.Ranged:
                        prePath = "Assets/Sprites/Weapon/Ranged Weapon/";
                        break;
                    case WeaponAttribute.WeaponDamageType.Ability:
                        prePath = "Assets/Sprites/Weapon/Ability Weapon/";
                        break;
                    default:
                        break;
                }
                ImageLoader.LoadImage(prePath + JsonLoader.weaponPool[GameController.getInstance().getGameData()._weaponList[i]].getWeaponIcon(), weaponDisplay.GetChild(i).GetChild(0).GetComponent<Image>());
                ImageLoader.LoadImage("Assets/Sprites/Weapon/" + JsonLoader.weaponPool[GameController.getInstance().getGameData()._weaponList[i]].getWeaponBgIcon(), weaponDisplay.GetChild(i).GetComponent<Image>());
            }
        }
    }
    protected override void FillTextContent()
    {
        base.FillTextContent();
    }
    protected override void OnAddListener()
    {
        base.OnAddListener();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    protected override void OnRemoveListener()
    {
        base.OnRemoveListener();
    }
    protected override void RegisterUIEvent()
    {
        base.RegisterUIEvent();
    }
    protected override void Update()
    {
        base.Update();
    }
}
