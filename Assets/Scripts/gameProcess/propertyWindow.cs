using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;

public class propertyWindow : BaseWindow
{
    private static propertyWindow instance;
    private EventTrigger eventTrigger;

    private propertyWindow()
    {
        // 在这里初始化GameManager
        resName = "UI/propertyWindow";
        isResident = true;
        isVisible = false;
        selfType = WindowType.propertyWindow;
        sceneType = SceneType.gameProcess;

    }

    public static propertyWindow Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new propertyWindow();
            }
            return instance;
        }
    }

    protected override void AwakeWindow()
    {
        btnList = transform.GetComponentsInChildren<Button>(true);
        textList = transform.GetComponentsInChildren<Text>(true);

        //注册UI事件(细节由子类实现)
        RegisterUIEvent();
        //填充文本内容(细节由子类实现)
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

        //Time.timeScale = 0;

        RegisterUIEvent();
    }

    protected override void OnDisable()
    {
        //Time.timeScale = 1f;
    }

    protected override void RegisterUIEvent()
    {
        base.RegisterUIEvent();

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

    public void RefreshText()
    {
        FillTextContent();
    }
}
