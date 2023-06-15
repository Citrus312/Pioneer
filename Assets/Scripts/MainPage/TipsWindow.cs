using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsWindow : BaseWindow
{
    public TipsWindow()
    {
        resName = "UI/TipsWindow";
        isResident = false;
        isVisible = false;
        selfType = WindowType.TipsWindow;
        sceneType = SceneType.None;
    }

    protected override void Awake(string inputText = "")
    {
        base.Awake(inputText);
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

    protected override void Update(float deltaTime)
    {
        base.Update(deltaTime);
    }

    protected override void FillTextContent(string inputText)
    {
        base.FillTextContent(inputText);

        foreach (Text text in textList)
        {
            switch (text.name)
            {
                case "TipsText":
                    text.text = inputText;
                    break;
                default:
                    break;
            }
        }
    }

    protected override void RegisterUIEvent()
    {
        base.RegisterUIEvent();
        //给UI中的按钮绑定点击事件
        foreach (Button btn in btnList)
        {
            switch (btn.name)
            {
                case "CloseBtn":
                    btn.onClick.AddListener(() => { OnCloseBtn(); });
                    break;
                default:
                    Debug.LogError("An unexpected button exists!");
                    break;
            }
        }
    }

    private void OnCloseBtn()
    {
        Debug.Log("点击了 提示窗关闭 按钮");
        Close();
    }
}
