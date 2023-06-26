using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;

public class TalentTreeWindow : BaseWindow
{
    private static TalentTreeWindow instance;
    //属性字典用于计算角色的增幅，attribute1 2 3分别用于3行子符文的计算，attributeFinal返回最后的属性增幅
    private Dictionary<string, float> attribute1 = new();
    private Dictionary<string, float> attribute2 = new();
    private Dictionary<string, float> attribute3 = new();


    //初始化属性字典
    private void initDictionary(Dictionary<string, float> dictionary)
    {
        dictionary.Add("maxHealth", 10f);
        dictionary.Add("healthRecovery", 0f);
        dictionary.Add("healthSteal", 0f);
        dictionary.Add("attackAmplification", 0f);
        dictionary.Add("meleeDamage", 0f);
        dictionary.Add("rangedDamage", 0f);
        dictionary.Add("abilityDamage", 0f);
        dictionary.Add("attackSpeedAmplification", 0f);
        dictionary.Add("criticalRate", 0f);
        dictionary.Add("engineering", 0f);
        dictionary.Add("attackRangeAmplification", 0f);
        dictionary.Add("armorStrength", 0f);
        dictionary.Add("dodgeRate", 0f);
        dictionary.Add("moveSpeedAmplification", 0f);
        dictionary.Add("scanAccuracy", 0f);
        dictionary.Add("collectEfficiency", 0f);
    }

    //重置属性字典
    private void resetDictionary(Dictionary<string, float> dictionary)
    {
        dictionary["maxHealth"] = 10f;
        dictionary["healthRecovery"] = 0f;
        dictionary["healthSteal"] = 0f;
        dictionary["attackAmplification"] = 0f;
        dictionary["meleeDamage"] = 0f;
        dictionary["rangedDamage"] = 0f;
        dictionary["abilityDamage"] = 0f;
        dictionary["attackSpeedAmplification"] = 0f;
        dictionary["criticalRate"] = 0f;
        dictionary["engineering"] = 0f;
        dictionary["attackRangeAmplification"] = 0f;
        dictionary["armorStrength"] = 0f;
        dictionary["dodgeRate"] = 0f;
        dictionary["moveSpeedAmplification"] = 0f;
        dictionary["scanAccuracy"] = 0f;
        dictionary["collectEfficiency"] = 0f;
    }

    public static TalentTreeWindow Instance
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

    private TalentTreeWindow()
    {
        resName = "UI/TalentTreeWindow";
        isResident = true;
        isVisible = false;
        selfType = WindowType.TalentTreeWindow;
        sceneType = SceneType.TalentTree;
    }

    protected override void AwakeWindow()
    {
        btnList = transform.GetComponentsInChildren<Button>(true);
        textList = transform.GetComponentsInChildren<Text>(true);

        //注册UI事件
        RegisterUIEvent();
        //填充文本内容
        FillTextContent();
    }

    protected override void OnAddListener()
    {
        base.OnAddListener();
    }

    protected override void OnRemoveListener()
    {
        base.OnRemoveListener();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void RegisterUIEvent()
    {
        base.RegisterUIEvent();

        //初始化属性字典
        initDictionary(attribute1);
        initDictionary(attribute2);
        initDictionary(attribute3);

        //初始化界面
        resetFirstBtn();
        transform.Find("RecoveryButton").transform.GetComponent<CustomUI.CircularImage>().color = new Color(255f, 255f, 255f, 1f);
        transform.Find("RecoveryPanel").gameObject.SetActive(true);
        transform.Find("RecoveryButton").transform.localScale = new Vector3(1f, 1f, 1f);

        foreach (Button btn in btnList)
        {
            switch (btn.name)
            {
                case "AttackButton":
                    btn.onClick.AddListener(() => { OnFirstBtn(btn); });
                    break;
                case "DefenseButton":
                    btn.onClick.AddListener(() => { OnFirstBtn(btn); });
                    break;
                case "ComprehensiveButton":
                    btn.onClick.AddListener(() => { OnFirstBtn(btn); });
                    break;
                case "RecoveryButton":
                    btn.onClick.AddListener(() => { OnFirstBtn(btn); });
                    break;
                case "ExitButton":
                    btn.onClick.AddListener(() => { OnExitButton(btn); });
                    break;
                case "ConfirmButton":
                    btn.onClick.AddListener(() => { OnConfirmButton(btn); });
                    break;
                //其它均为子符文图标按钮
                default:
                    btn.onClick.AddListener(() => { OnSecondButton(btn); });
                    break;
            }
        }

    }

    //点击三个主符文 "AttackButton"、"DefenseButton"、"ComprehensiveButton"时进行的操作
    private void OnFirstBtn(Button button)
    {
        resetFirstBtn();
        setFirstBtn(button);
    }

    private void setFirstBtn(Button button)
    {
        Transform transform = GameObject.Find(button.name).transform;
        //点击后显示子符文页
        switch (button.name)
        {
            case "AttackButton":
                this.transform.Find("AttackPanel").gameObject.SetActive(true);
                break;
            case "DefenseButton":
                this.transform.Find("DefensePanel").gameObject.SetActive(true);
                break;
            case "ComprehensiveButton":
                this.transform.Find("ComprehensivePanel").gameObject.SetActive(true);
                break;
            case "RecoveryButton":
                this.transform.Find("RecoveryPanel").gameObject.SetActive(true);
                break;
            default:
                break;
        }
        //点击后图片半径变大
        transform.localScale = new Vector3(1f, 1f, 1f);
        //点击后颜色加深
        transform.GetComponent<CustomUI.CircularImage>().color = new Color(1f, 1f, 1f, 1f);
    }

    //重置符文页的Panel，radius，color
    private void resetFirstBtn()
    {
        transform.Find("AttackButton").transform.GetComponent<CustomUI.CircularImage>().color = new Color(0.8f, 0.8f, 0.8f, 0.5f);
        transform.Find("DefenseButton").transform.GetComponent<CustomUI.CircularImage>().color = new Color(0.8f, 0.8f, 0.8f, 0.5f);
        transform.Find("ComprehensiveButton").transform.GetComponent<CustomUI.CircularImage>().color = new Color(0.8f, 0.8f, 0.8f, 0.5f);
        transform.Find("RecoveryButton").transform.GetComponent<CustomUI.CircularImage>().color = new Color(0.8f, 0.8f, 0.8f, 0.5f);

        transform.Find("RecoveryPanel").gameObject.SetActive(false);
        transform.Find("AttackPanel").gameObject.SetActive(false);
        transform.Find("DefensePanel").gameObject.SetActive(false);
        transform.Find("ComprehensivePanel").gameObject.SetActive(false);

        transform.Find("AttackButton").transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        transform.Find("DefenseButton").transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        transform.Find("ComprehensiveButton").transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        transform.Find("RecoveryButton").transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

    }

    //鼠标点击子符文上的图标
    private void OnSecondButton(Button button)
    {
        resetSecondBtn(button);
        setSecondBtn(button);
    }

    //更改显示页的内容，同时更改所点击图标的radius和color
    private void setSecondBtn(Button second)
    {
        //Debug.Log(GameObject.Find(second.name));
        GameObject.Find("ContentPanel").transform.Find("Image").GetComponent<CustomUI.CircularImage>().sprite = GameObject.Find(second.name).GetComponent<CustomUI.CircularImage>().sprite;
        GameObject.Find("ContentPanel").transform.Find("Title").GetComponent<Text>().text = GameObject.Find(second.name).transform.Find("name").GetComponent<Text>().text;
        GameObject.Find("ContentPanel").transform.Find("Content").GetComponent<Text>().text = GameObject.Find(second.name).transform.Find("content").GetComponent<Text>().text;

        GameObject.Find(second.name).transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);



        switch (second.name)
        {
            //生命符文
            case "EButton1":
                second.onClick.AddListener(() => { attribute1["maxHealth"] += 1f; });
                GameObject.Find(second.name).transform.GetComponent<CustomUI.CircularImage>().color = new Color(0.6f, 0.6f, 0.6f, 1f);
                break;
            case "EButton2":
                second.onClick.AddListener(() => { attribute1["healthRecovery"] += 1f; });
                GameObject.Find(second.name).transform.GetComponent<CustomUI.CircularImage>().color = new Color(0.6f, 0.6f, 0.6f, 1f);
                break;
            case "EButton3":
                second.onClick.AddListener(() => { attribute1["healthSteal"] += 0.01f; });
                GameObject.Find(second.name).transform.GetComponent<CustomUI.CircularImage>().color = new Color(0.6f, 0.6f, 0.6f, 1f);
                break;
            case "EButton4":
                second.onClick.AddListener(() => { attribute2["maxHealth"] += 2f; });
                GameObject.Find(second.name).transform.GetComponent<CustomUI.CircularImage>().color = new Color(0.8f, 0.8f, 0.8f, 1f);
                break;
            case "EButton5":
                second.onClick.AddListener(() => { attribute2["healthRecovery"] += 2f; });
                GameObject.Find(second.name).transform.GetComponent<CustomUI.CircularImage>().color = new Color(0.8f, 0.8f, 0.8f, 1f);
                break;
            case "EButton6":
                second.onClick.AddListener(() => { attribute2["healthSteal"] += 0.02f; });
                GameObject.Find(second.name).transform.GetComponent<CustomUI.CircularImage>().color = new Color(0.8f, 0.8f, 0.8f, 1f);
                break;
            case "EButton7":
                second.onClick.AddListener(() => { attribute3["maxHealth"] += 4f; });
                GameObject.Find(second.name).transform.GetComponent<CustomUI.CircularImage>().color = new Color(1f, 1f, 1f, 1f);
                break;
            case "EButton8":
                second.onClick.AddListener(() => { attribute3["healthRecovery"] += 4f; });
                GameObject.Find(second.name).transform.GetComponent<CustomUI.CircularImage>().color = new Color(1f, 1f, 1f, 1f);
                break;
            case "EButton9":
                second.onClick.AddListener(() => { attribute3["healthSteal"] += 0.04f; });
                GameObject.Find(second.name).transform.GetComponent<CustomUI.CircularImage>().color = new Color(1f, 1f, 1f, 1f);
                break;
            //防御符文
            case "DButton1":
                second.onClick.AddListener(() => { attribute1["armorStrength"] += 1f; });
                GameObject.Find(second.name).transform.GetComponent<CustomUI.CircularImage>().color = new Color(0.6f, 0.6f, 0.6f, 1f);
                break;
            case "DButton2":
                second.onClick.AddListener(() => { attribute1["dodgeRate"] += 0.02f; });
                GameObject.Find(second.name).transform.GetComponent<CustomUI.CircularImage>().color = new Color(0.6f, 0.6f, 0.6f, 1f);
                break;
            case "DButton3":
                second.onClick.AddListener(() => { attribute1["moveSpeedAmplification"] += 0.02f; });
                GameObject.Find(second.name).transform.GetComponent<CustomUI.CircularImage>().color = new Color(0.6f, 0.6f, 0.6f, 1f);
                break;
            case "DButton4":
                second.onClick.AddListener(() => { attribute2["armorStrength"] += 2f; });
                GameObject.Find(second.name).transform.GetComponent<CustomUI.CircularImage>().color = new Color(0.8f, 0.8f, 0.8f, 1f);
                break;
            case "DButton5":
                second.onClick.AddListener(() => { attribute2["dodgeRate"] += 0.04f; });
                GameObject.Find(second.name).transform.GetComponent<CustomUI.CircularImage>().color = new Color(0.8f, 0.8f, 0.8f, 1f);
                break;
            case "DButton6":
                second.onClick.AddListener(() => { attribute2["moveSpeedAmplification"] += 0.04f; });
                GameObject.Find(second.name).transform.GetComponent<CustomUI.CircularImage>().color = new Color(0.8f, 0.8f, 0.8f, 1f);
                break;
            case "DButton7":
                second.onClick.AddListener(() => { attribute3["armorStrength"] += 3f; });
                GameObject.Find(second.name).transform.GetComponent<CustomUI.CircularImage>().color = new Color(1f, 1f, 1f, 1f);
                break;
            case "DButton8":
                second.onClick.AddListener(() => { attribute3["dodgeRate"] += 0.06f; });
                GameObject.Find(second.name).transform.GetComponent<CustomUI.CircularImage>().color = new Color(1f, 1f, 1f, 1f);
                break;
            case "DButton9":
                second.onClick.AddListener(() => { attribute3["moveSpeedAmplification"] += 0.06f; });
                GameObject.Find(second.name).transform.GetComponent<CustomUI.CircularImage>().color = new Color(1f, 1f, 1f, 1f);
                break;
            //攻击符文1（暴击率，伤害增幅）
            case "AButton1":
                second.onClick.AddListener(() => { attribute1["criticalRate"] += 0.04f; });
                GameObject.Find(second.name).transform.GetComponent<CustomUI.CircularImage>().color = new Color(0.6f, 0.6f, 0.6f, 1f);
                break;
            case "AButton2":
                second.onClick.AddListener(() => { attribute1["attackAmplification"] += 0.04f; });
                GameObject.Find(second.name).transform.GetComponent<CustomUI.CircularImage>().color = new Color(0.6f, 0.6f, 0.6f, 1f);
                break;
            case "AButton3":
                second.onClick.AddListener(() => { attribute1["meleeDamage"] += 2f; });
                GameObject.Find(second.name).transform.GetComponent<CustomUI.CircularImage>().color = new Color(0.6f, 0.6f, 0.6f, 1f);
                break;
            case "AButton4":
                second.onClick.AddListener(() => { attribute2["criticalRate"] += 0.06f; });
                GameObject.Find(second.name).transform.GetComponent<CustomUI.CircularImage>().color = new Color(0.8f, 0.8f, 0.8f, 1f);
                break;
            case "AButton5":
                second.onClick.AddListener(() => { attribute2["attackAmplification"] += 0.08f; });
                GameObject.Find(second.name).transform.GetComponent<CustomUI.CircularImage>().color = new Color(0.8f, 0.8f, 0.8f, 1f);
                break;
            case "AButton6":
                second.onClick.AddListener(() => { attribute2["rangedDamage"] += 1f; });
                GameObject.Find(second.name).transform.GetComponent<CustomUI.CircularImage>().color = new Color(0.8f, 0.8f, 0.8f, 1f);
                break;
            case "AButton7":
                second.onClick.AddListener(() => { attribute3["criticalRate"] += 0.08f; });
                GameObject.Find(second.name).transform.GetComponent<CustomUI.CircularImage>().color = new Color(1f, 1f, 1f, 1f);
                break;
            case "AButton8":
                second.onClick.AddListener(() => { attribute3["attackAmplification"] += 0.12f; });
                GameObject.Find(second.name).transform.GetComponent<CustomUI.CircularImage>().color = new Color(1f, 1f, 1f, 1f);
                break;
            case "AButton9":
                second.onClick.AddListener(() => { attribute3["abilityDamage"] += 2f; });
                GameObject.Find(second.name).transform.GetComponent<CustomUI.CircularImage>().color = new Color(1f, 1f, 1f, 1f);
                break;
            //攻击符文2（综合：范围，攻速）
            case "CButton1":
                second.onClick.AddListener(() => { attribute1["meleeDamage"] += 2f; });
                GameObject.Find(second.name).transform.GetComponent<CustomUI.CircularImage>().color = new Color(0.6f, 0.6f, 0.6f, 1f);
                break;
            case "CButton2":
                second.onClick.AddListener(() => { attribute1["attackRangeAmplification"] += 10f; });
                GameObject.Find(second.name).transform.GetComponent<CustomUI.CircularImage>().color = new Color(0.6f, 0.6f, 0.6f, 1f);
                break;
            case "CButton3":
                second.onClick.AddListener(() => { attribute1["attackSpeedAmplification"] += 0.08f; });
                GameObject.Find(second.name).transform.GetComponent<CustomUI.CircularImage>().color = new Color(0.6f, 0.6f, 0.6f, 1f);
                break;
            case "CButton4":
                second.onClick.AddListener(() => { attribute2["rangedDamage"] += 1f; });
                GameObject.Find(second.name).transform.GetComponent<CustomUI.CircularImage>().color = new Color(0.8f, 0.8f, 0.8f, 1f);
                break;
            case "CButton5":
                second.onClick.AddListener(() => { attribute2["attackRangeAmplification"] += 40f; });
                GameObject.Find(second.name).transform.GetComponent<CustomUI.CircularImage>().color = new Color(0.8f, 0.8f, 0.8f, 1f);
                break;
            case "CButton6":
                second.onClick.AddListener(() => { attribute2["attackSpeedAmplification"] += 0.1f; });
                GameObject.Find(second.name).transform.GetComponent<CustomUI.CircularImage>().color = new Color(0.8f, 0.8f, 0.8f, 1f);
                break;
            case "CButton7":
                second.onClick.AddListener(() => { attribute3["abilityDamage"] += 2f; });
                GameObject.Find(second.name).transform.GetComponent<CustomUI.CircularImage>().color = new Color(1f, 1f, 1f, 1f);
                break;
            case "CButton8":
                second.onClick.AddListener(() => { attribute3["attackRangeAmplification"] += 20f; });
                GameObject.Find(second.name).transform.GetComponent<CustomUI.CircularImage>().color = new Color(1f, 1f, 1f, 1f);
                break;
            case "CButton9":
                second.onClick.AddListener(() => { attribute3["attackSpeedAmplification"] += 0.06f; });
                GameObject.Find(second.name).transform.GetComponent<CustomUI.CircularImage>().color = new Color(1f, 1f, 1f, 1f);
                break;
        }

    }

    //重置子符文页的图标
    private void resetSecondBtn(Button second)
    {
        Transform secondTrasform = GameObject.Find(second.name).transform;

        secondTrasform.parent.GetChild(0).transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        secondTrasform.parent.GetChild(1).transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        secondTrasform.parent.GetChild(2).transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

        switch (secondTrasform.parent.name)
        {
            case "Panel1":
                secondTrasform.parent.GetChild(0).transform.GetComponent<CustomUI.CircularImage>().color = new Color(0.6f, 0.6f, 0.6f, 0.5f);
                secondTrasform.parent.GetChild(1).transform.GetComponent<CustomUI.CircularImage>().color = new Color(0.6f, 0.6f, 0.6f, 0.5f);
                secondTrasform.parent.GetChild(2).transform.GetComponent<CustomUI.CircularImage>().color = new Color(0.6f, 0.6f, 0.6f, 0.5f);
                resetDictionary(attribute1);
                break;
            case "Panel2":
                secondTrasform.parent.GetChild(0).transform.GetComponent<CustomUI.CircularImage>().color = new Color(0.8f, 0.8f, 0.8f, 0.5f);
                secondTrasform.parent.GetChild(1).transform.GetComponent<CustomUI.CircularImage>().color = new Color(0.8f, 0.8f, 0.8f, 0.5f);
                secondTrasform.parent.GetChild(2).transform.GetComponent<CustomUI.CircularImage>().color = new Color(0.8f, 0.8f, 0.8f, 0.5f);
                resetDictionary(attribute2);
                break;
            case "Panel3":
                secondTrasform.parent.GetChild(0).transform.GetComponent<CustomUI.CircularImage>().color = new Color(1f, 1f, 1f, 0.5f);
                secondTrasform.parent.GetChild(1).transform.GetComponent<CustomUI.CircularImage>().color = new Color(1f, 1f, 1f, 0.5f);
                secondTrasform.parent.GetChild(2).transform.GetComponent<CustomUI.CircularImage>().color = new Color(1f, 1f, 1f, 0.5f);
                resetDictionary(attribute3);
                break;
            default:
                Debug.Log("没有重置属性");
                break;
        }
    }

    private void OnExitButton(Button button)
    {
        Close(false);
    }

    private void OnConfirmButton(Button button)
    {
        CharacterAttribute playerAttr = GameController.getInstance()._player.GetComponent<CharacterAttribute>();
        playerAttr.setMaxHealth(playerAttr.getMaxHealth() + attribute1["maxHealth"] + attribute2["maxHealth"] + attribute3["maxHealth"]);
        playerAttr.setHealthRecovery(playerAttr.getHealthRecovery() + attribute1["healthRecovery"] + attribute2["healthRecovery"] + attribute3["healthRecovery"]);
        playerAttr.setHealthSteal(playerAttr.getHealthSteal() + attribute1["healthSteal"] + attribute2["healthSteal"] + attribute3["healthSteal"]);
        playerAttr.setAttackAmplification(playerAttr.getAttackAmplification() + attribute1["attackAmplification"] + attribute2["attackAmplification"] + attribute3["attackAmplification"]);
        playerAttr.setMeleeDamage(playerAttr.getMeleeDamage() + attribute1["meleeDamage"] + attribute2["meleeDamage"] + attribute3["meleeDamage"]);
        playerAttr.setRangedDamage(playerAttr.getRangedDamage() + attribute1["rangedDamage"] + attribute2["rangedDamage"] + attribute3["rangedDamage"]);
        playerAttr.setAbilityDamage(playerAttr.getAbilityDamage() + attribute1["abilityDamage"] + attribute2["abilityDamage"] + attribute3["abilityDamage"]);
        playerAttr.setAttackSpeedAmplification(playerAttr.getAttackSpeedAmplification() + attribute1["attackSpeedAmplification"] + attribute2["attackSpeedAmplification"] + attribute3["attackSpeedAmplification"]);
        playerAttr.setCriticalRate(playerAttr.getCriticalRate() + attribute1["criticalRate"] + attribute2["criticalRate"] + attribute3["criticalRate"]);
        playerAttr.setEngineering(playerAttr.getEngineering() + attribute1["engineering"] + attribute2["engineering"] + attribute3["engineering"]);
        playerAttr.setAttackRangedAmplification(playerAttr.getAttackRangeAmplification() + attribute1["attackRangeAmplification"] + attribute2["attackRangeAmplification"] + attribute3["attackRangeAmplification"]);
        playerAttr.setArmorStrength(playerAttr.getArmorStrength() + attribute1["armorStrength"] + attribute2["armorStrength"] + attribute3["armorStrength"]);
        playerAttr.setDodgeRate(playerAttr.getDodgeRate() + attribute1["dodgeRate"] + attribute2["dodgeRate"] + attribute3["dodgeRate"]);
        playerAttr.setMoveSpeedAmplification(playerAttr.getMoveSpeedAmplification() + attribute1["moveSpeedAmplification"] + attribute2["moveSpeedAmplification"] + attribute3["moveSpeedAmplification"]);
        playerAttr.setScanAccuracy(playerAttr.getScanAccuracy() + attribute1["scanAccuracy"] + attribute2["scanAccuracy"] + attribute3["scanAccuracy"]);
        playerAttr.setCollectEfficiency(playerAttr.getCollectEfficiency() + attribute1["collectEfficiency"] + attribute2["collectEfficiency"] + attribute3["collectEfficiency"]);
        TalentTreeWindow.Instance.Close(false);
    }
}

