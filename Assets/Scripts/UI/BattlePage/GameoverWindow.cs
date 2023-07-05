using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameoverWindow : BaseWindow
{
    private static GameoverWindow instance;
    private Text attributeText;
    private Text roleNameText;
    private Image roleIcon;
    private Transform weaponDisplay;
    private Transform propDisplay;
    public string titleText;
    public GameObject detailDisplay;

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
        //初始化武器和道具详细信息的面板
        if (GameObject.Find("DetailPanel") == null)
        {
            GameObject obj = Resources.Load<GameObject>("UI/DetailPanel");
            obj = GameObject.Instantiate(obj);
            obj.SetActive(false);
            detailDisplay = obj;
        }
        //获取各UI组件
        attributeText = transform.Find("AttributeDisplay").GetChild(0).GetComponent<Text>();
        roleNameText = transform.Find("AttributeDisplay").GetChild(1).GetComponent<Text>();
        roleIcon = transform.Find("AttributeDisplay").GetChild(2).GetComponent<Image>();
        weaponDisplay = transform.Find("WeaponDisplay");
        propDisplay = transform.Find("PropDisplay");
        //设置窗口标题
        transform.Find("TitleText").GetComponent<Text>().text = titleText;

        //设置角色属性文本
        CharacterAttribute roleAttr = GameController.getInstance().getPlayer().GetComponent<CharacterAttribute>();
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
        //设置角色图标和名字文本
        roleNameText.text = roleAttr.getName();
        ImageLoader.LoadImage($"Assets/Sprites/Player/{roleAttr.getIcon()}", roleIcon);

        //设置武器图标
        for (int i = 0; i < GameController.getInstance().getGameData()._weaponList.Count; i++)
        {
            //将索引作为物体名字方便后续根据名字获取对应数据，以下相似做法皆是如此
            weaponDisplay.GetChild(i).name = $"{GameController.getInstance().getGameData()._weaponList[i]}";
            //激活预设的游戏物体
            weaponDisplay.GetChild(i).gameObject.SetActive(true);
            weaponDisplay.GetChild(i).GetChild(0).GetComponent<WeaponDetailDisplay>().detailDisplay = detailDisplay;
            weaponDisplay.GetChild(i).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            weaponDisplay.GetChild(i).GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
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
            //加载武器背景图标和自身图标
            ImageLoader.LoadImage(prePath + JsonLoader.weaponPool[GameController.getInstance().getGameData()._weaponList[i]].getWeaponIcon(), weaponDisplay.GetChild(i).GetChild(0).GetComponent<Image>());
            ImageLoader.LoadImage("Assets/Sprites/Weapon/" + JsonLoader.weaponPool[GameController.getInstance().getGameData()._weaponList[i]].getWeaponBgIcon(), weaponDisplay.GetChild(i).GetComponent<Image>());
        }
        //设置道具图标
        for (int i = 0; i < GameController.getInstance().getGameData()._propList.Count; i++)
        {
            //获取指定的道具属性
            PropAttribute prop = new();
            prop.setPropAttribute(JsonLoader.propPool[GameController.getInstance().getGameData()._propList[i]]);
            //加载用于显示道具图标的预制体
            GameObject obj = Resources.Load<GameObject>("UI/prop");
            obj = GameObject.Instantiate(obj);
            //见上
            obj.name = $"{GameController.getInstance().getGameData()._propList[i]}";
            //添加详细信息显示控制脚本
            obj.AddComponent<PropDetailDisplay>();
            obj.GetComponent<PropDetailDisplay>().detailDisplay = detailDisplay;
            //加载道具背景和自身图标
            ImageLoader.LoadImage($"Assets/Sprites/Weapon/{prop.getPropBgIcon()}", obj.GetComponent<Image>());
            ImageLoader.LoadImage($"Assets/Sprites/Prop/{prop.getPropIcon()}", obj.transform.GetChild(0).GetComponent<Image>());
            //设置道具数量文本
            obj.transform.GetChild(1).GetComponent<Text>().text = $"x{GameController.getInstance().getGameData()._propCount[i]}";
            //将道具挂载到滚动窗口
            obj.transform.SetParent(propDisplay.GetChild(0).GetChild(0));
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

        btnList[0].onClick.AddListener(() => { OnExitBtn(); });
    }
    protected override void Update()
    {
        base.Update();
    }

    public void OnExitBtn()
    {
        GameController.getInstance().getGameData().ResetGameData();
        JsonLoader.UpdateGameData();
        Time.timeScale = 1f;
        SceneLoader._instance.loadScene("MainPage");

        DelayToInvoke.DelayToInvokeBySecond(() =>
        {
            Close(); MainPageWindow.Instance.Open();
        }, 1.0f);
    }
}
