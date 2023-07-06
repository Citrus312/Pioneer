using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class titleWindow : BaseWindow
{
    private static titleWindow instance;

    private titleWindow()
    {
        // 在这里初始化GameManager
        resName = "UI/titleWindow";
        isResident = true;
        isVisible = false;
        selfType = WindowType.titleWindow;
        sceneType = SceneType.gameProcess;
         
    }

    public static titleWindow Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new titleWindow();
            }
            return instance;
        }
    }

    protected override void AwakeWindow()
    {


        //注册UI事件(细节由子类实现)
        RegisterUIEvent();
        //填充文本内容(细节由子类实现)
        //FillTextContent(inputText);

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


}
