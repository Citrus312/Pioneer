using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;

public class PausePageWindow : BaseWindow
{

    private EventTrigger eventTrigger;
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

        //注册UI事件
        RegisterUIEvent();
        //填充文本内容
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
        //foreach (Text text in textList)
        //{
        //    switch (text.name)
        //    {
        //        case "HealthText":
        //            // 获取 UI Text 对象上的 Event Trigger 组件
        //            EventTrigger eventTrigger = text.GetComponent<EventTrigger>();

        //            // 创建一个 Entry 并将其事件设置为 “PointerEnter”
        //            EventTrigger.Entry entry = new EventTrigger.Entry();
        //            entry.eventID = EventTriggerType.PointerEnter;

        //            // 添加一个回调函数到 Entry 中
        //            entry.callback.AddListener((data) => { OnHealthText(); });

        //            // 将 Entry 添加到 Event Trigger 的事件列表中
        //            eventTrigger.triggers.Add(entry);
        //            break;
        //    }
        //}
    }

    protected override void FillTextContent(string inputText)
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
        Close(true);
        SceneLoader._instance.loadScene("MainPage");
    }

    public void OnHealthText()
    {

    }

}
