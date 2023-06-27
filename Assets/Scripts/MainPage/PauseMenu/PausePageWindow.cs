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

    private EventTrigger eventTrigger;
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
    }

    private void OnRestartBtn(Button btn)
    {
        Close();
        SceneLoader._instance.loadScene("SampleScene");
    }

    private void OnSettingBtn(Button btn)
    {
        SettingWindow.Instance.Open();
    }

    private void OnExitBtn(Button btn)
    {
        Close(true);
        SceneLoader._instance.loadScene("MainPage");
    }
}
