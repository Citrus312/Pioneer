using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weaponBagWindow : BaseWindow
{
    private  static weaponBagWindow instance;
    public int buyedWeapon;
    public List<int> ownWeaponList = new();
    public bool addWeapon=true;//判断能否继续添加装备
    public bool isWeapon=true;//购买的是否是武器

    private weaponBagWindow()
    {
        // 在这里初始化GameManager
        resName = "UI/weaponBagWindow";
        isResident = true;
        isVisible = false;
        selfType = WindowType.weaponBagWindow;
        sceneType = SceneType.gameProcess;

    }

    public static weaponBagWindow Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new weaponBagWindow();
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
