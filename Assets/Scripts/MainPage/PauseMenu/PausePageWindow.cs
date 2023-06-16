using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class PausePageWindow : BaseWindow
{
    //初始化暂停界面的参数
    public PausePageWindow()
    {
        resName = "UI/PauseWindow";
        isResident = true;
        isVisible = false;
        selfType = WindowType.PauseWindow;
        sceneType = SceneType.Pause;
    }

    protected override void Awake(string inputText = "")
    {
        btnList = transform.GetComponentsInChildren<Button>(true);
        textList = transform.GetComponentsInChildren<Text>(true);

        //注册UI事件(细节由子类实现)
        RegisterUIEvent();
        //填充文本内容(细节由子类实现)
        FillTextContent(inputText);

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
            Debug.Log(btn.name);
            switch (btn.name)
            {
                case "ButtonContinue":
                    Debug.Log("aaaaaaaaaaaaaaa");
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

    protected virtual void FillTextContent(string inputText)
    {
        foreach (Text txt in textList)
        {
            if (txt.name == "AttributeText")
            {
                txt.text = inputText;
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
        Debug.Log("点击了 设置 按钮");
    }

    private void OnExitBtn(Button btn)
    {
        Close();
        SceneLoader._instance.loadScene("MainPage");
    }

}
