using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class upgradeWindow : BaseWindow
{
    private static upgradeWindow instance;

    public float value;//���������Ծ�����ֵ
    public string name;//��������������
    private upgradeWindow()
    {
        // �������ʼ��GameManager
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
