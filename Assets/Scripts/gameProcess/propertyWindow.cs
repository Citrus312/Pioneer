using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class propertyWindow : BaseWindow
{
    private static propertyWindow instance;

    public propertyWindow()
    {
        // �������ʼ��GameManager
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
       

        //ע��UI�¼�(ϸ��������ʵ��)
        RegisterUIEvent();
        //����ı�����(ϸ��������ʵ��)
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
