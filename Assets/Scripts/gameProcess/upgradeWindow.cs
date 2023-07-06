using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class upgradeWindow : BaseWindow
{
    private static upgradeWindow instance;

    public float value;//升级的属性具体数值
    public string name;//升级的属性名字
    public int freshCount = 0;
    public int freshValue;
    private upgradeWindow()
    {
        // 在这里初始化GameManager
        resName = "UI/upgradeWindow";
        isResident = true;
        isVisible = false;
        selfType = WindowType.upgradeWindow;
        sceneType = SceneType.gameProcess;

    }

    public static upgradeWindow Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new upgradeWindow();
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
