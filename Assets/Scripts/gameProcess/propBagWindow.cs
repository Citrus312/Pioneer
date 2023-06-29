using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class propBagWindow : BaseWindow
{
    private static propBagWindow instance;
    public int buyedProp;
    public List<int> ownPropList = new();

    private propBagWindow()
    {
        // 在这里初始化GameManager
        resName = "UI/propBagWindow";
        isResident = true;
        isVisible = false;
        selfType = WindowType.propBagWindow;
        sceneType = SceneType.gameProcess;

    }

    public static propBagWindow Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new propBagWindow();
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
