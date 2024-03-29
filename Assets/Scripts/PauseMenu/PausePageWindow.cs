using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;

public class PausePageWindow : BaseWindow
{
    private static PausePageWindow instance;

    public static PausePageWindow Instance
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

    //初始化暂停界面的参数
    private PausePageWindow()
    {
        resName = "UI/PauseWindow";
        isResident = true;
        isVisible = false;
        selfType = WindowType.PauseWindow;
        sceneType = SceneType.Pause;
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

        Time.timeScale = 0;

        RegisterUIEvent();
    }

    protected override void OnDisable()
    {
        Time.timeScale = 1f;
    }

    protected override void RegisterUIEvent()
    {
        base.RegisterUIEvent();
        foreach (Button btn in btnList)
        {
            switch (btn.name)
            {
                case "ButtonContinue":
                    btn.onClick.AddListener(() => { OnContinueBtn(btn); });
                    break;
                case "ButtonRestart":
                    btn.onClick.AddListener(() => { OnRestartBtn(btn); });
                    break;
                case "ButtonSetting":
                    btn.onClick.AddListener(() => { OnSettingBtn(btn); });
                    break;
                case "ButtonExit":
                    btn.onClick.AddListener(() => { OnExitBtn(btn); });
                    break;
                default:
                    Debug.LogError("An unexpected button exists!");
                    break;
            }
        }

    }

    protected override void FillTextContent()
    {

        foreach (Text txt in textList)
        {

            switch (txt.name)
            {
                case "CurrentPlayerLevel":
                    txt.text = inputText[0];
                    break;
                case "MaxHealth":
                    txt.text = inputText[1];
                    break;
                case "HealthRecovery":
                    txt.text = inputText[2];
                    break;
                case "HealthSteal":
                    txt.text = inputText[3];
                    break;
                case "AttackAmplification":
                    txt.text = inputText[4];
                    break;
                case "MeleeDamage":
                    txt.text = inputText[5];
                    break;
                case "RangedDamage":
                    txt.text = inputText[6];
                    break;
                case "AbilityDamage":
                    txt.text = inputText[7];
                    break;
                case "AttackSpeedAmplification":
                    txt.text = inputText[8];
                    break;
                case "CriticalRate":
                    txt.text = inputText[9];
                    break;
                case "Engineering":
                    txt.text = inputText[10];
                    break;
                case "AttackRangeAmplification":
                    txt.text = inputText[11];
                    break;
                case "ArmorStrength":
                    txt.text = inputText[12];
                    break;
                case "DodgeRate":
                    txt.text = inputText[13];
                    break;
                case "MoveSpeedAmplification":
                    txt.text = inputText[14];
                    break;
                case "ScanAccuracy":
                    txt.text = inputText[15];
                    break;
                case "CollectEfficiency":
                    txt.text = inputText[16];
                    break;
                default:
                    break;
            }
        }
    }

    private void OnContinueBtn(Button btn)
    {
        Close();
        if (storeWindow.Instance.getVisible() == false)
        {
            weaponBagWindow.Instance.Close();
            propBagWindow.Instance.Close();
            propertyWindow.Instance.Close();

        }
    }

    private void OnRestartBtn(Button btn)
    {
        Close();
        weaponBagWindow.Instance.Close();
        propBagWindow.Instance.Close();
        propertyWindow.Instance.Close();
        DifficultySelectWindow.Instance.Open();
        SceneLoader._instance.loadScene("LevelSelect");
    }

    private void OnSettingBtn(Button btn)
    {
        SettingWindow.Instance.Open();
    }

    private void OnExitBtn(Button btn)
    {
        GameController.getInstance().waveEnd();
        Close();
        weaponBagWindow.Instance.Close();
        propBagWindow.Instance.Close();
        propertyWindow.Instance.Close();
        if (storeWindow.Instance.getVisible() == true)
            storeWindow.Instance.Close();
        if (countDownTimerWindow.Instance.getVisible() == true)
            countDownTimerWindow.Instance.Close();
        if (titleWindow.Instance.getVisible() == true)
            titleWindow.Instance.Close();
        if (roleStateWindow.Instance.getVisible() == true)
            roleStateWindow.Instance.Close();
        if (upgradeWindow.Instance.getVisible() == true)
            upgradeWindow.Instance.Close();
        gameProcessController.Instance.gameObject.SetActive(false);
        Time.timeScale = 1f;
        SceneLoader._instance.loadScene("MainPage");
        //GameController.getInstance().getGameData().ResetGameData();
        JsonLoader.UpdateGameData();

        DelayToInvoke.DelayToInvokeBySecond(() =>
        {
            Close(); MainPageWindow.Instance.Open();
        }, 1.0f);

        Transform weaponBag = weaponBagWindow.Instance.getTransform().Find("weaponBag");
        Transform propBag = propBagWindow.Instance.getTransform().Find("PropDisplay");
        Transform viewport = propBag.Find("Viewport");
        Transform listContent = viewport.Find("Content");
        if (weaponBag.childCount > 0)
        {
            for (int i = 0; i < weaponBag.childCount; i++)
            {
                GameObject.Destroy(weaponBag.GetChild(i).gameObject);
            }
        }
        if (listContent.childCount > 0)
        {
            for (int i = 0; i < listContent.childCount; i++)
            {
                GameObject.Destroy(listContent.GetChild(i).gameObject);
            }
        }
    }
}
