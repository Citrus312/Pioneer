using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySelectWindow : BaseWindow
{
    //窗体的单例实例
    private static DifficultySelectWindow instance;

    //构造函数，负责参数初始化
    private DifficultySelectWindow()
    {
        resName = "UI/DifficultySelectWindow";
        isResident = true;
        isVisible = false;
        selfType = WindowType.DifficultySelectWindow;
        sceneType = SceneType.Select;
    }
    //实例的自动属性
    public static DifficultySelectWindow Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new();
            }
            return instance;
        }
    }

    protected override void AwakeWindow()
    {
        base.AwakeWindow();
    }
    protected override void FillTextContent()
    {
        base.FillTextContent();
    }
    protected override void OnAddListener()
    {
        base.OnAddListener();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    protected override void OnRemoveListener()
    {
        base.OnRemoveListener();
    }
    //注册难度选择按钮的点击事件
    protected override void RegisterUIEvent()
    {
        base.RegisterUIEvent();
        //为按钮列表中的每一个按钮绑定对应的点击事件
        foreach (Button btn in btnList)
        {
            //根据按钮名称来确定需要绑定的点击事件
            switch (btn.name)
            {
                case "Lv1Btn":
                    btn.onClick.AddListener(() => { OnLv1Btn(); });
                    break;
                case "Lv2Btn":
                    btn.onClick.AddListener(() => { OnLv2Btn(); });
                    break;
                case "Lv3Btn":
                    btn.onClick.AddListener(() => { OnLv3Btn(); });
                    break;
                case "Lv4Btn":
                    btn.onClick.AddListener(() => { OnLv4Btn(); });
                    break;
                default:
                    break;
            }
        }
    }
    protected override void Update()
    {
        base.Update();
    }
    //打开窗口并将窗口移动到指定位置
    public void OpenAndMove(float x, float y)
    {
        Instance.Open();
        transform.position = new Vector3(x, y, transform.position.z);
    }

    //以下是按钮的点击事件
    //逻辑一致，只有部分参数不同
    public void OnLv1Btn()
    {
        //点击难度选择按钮后将全局游戏数据的_difficulty设为1
        GameController.getInstance().getGameData()._difficulty = 1;
        //关闭难度选择窗口
        Instance.Close();
        //加载角色和武器选择的场景
        SceneLoader._instance.loadScene("RoleAndWeaponSelect");
        //延迟打开角色和武器选择窗口
        DelayToInvoke.DelayToInvokeBySecond(() =>
        {
            RoleAndWeaponSelectWindow.Instance.Open();
            //角色和武器选择窗口中的滚动选择区域内容的显示需要窗口已打开，故也安排在此处
            RoleAndWeaponSelectWindow.Instance.DisplayRoleScrollContent();
        }, 1.8f);
    }

    public void OnLv2Btn()
    {
        GameController.getInstance().getGameData()._difficulty = 2;
        Instance.Close();
        SceneLoader._instance.loadScene("RoleAndWeaponSelect");
        DelayToInvoke.DelayToInvokeBySecond(() =>
        {
            RoleAndWeaponSelectWindow.Instance.Open();
            RoleAndWeaponSelectWindow.Instance.DisplayRoleScrollContent();
        }, 1.8f);
    }

    public void OnLv3Btn()
    {
        GameController.getInstance().getGameData()._difficulty = 3;
        Instance.Close();
        SceneLoader._instance.loadScene("RoleAndWeaponSelect");
        DelayToInvoke.DelayToInvokeBySecond(() =>
        {
            RoleAndWeaponSelectWindow.Instance.Open();
            RoleAndWeaponSelectWindow.Instance.DisplayRoleScrollContent();
        }, 1.8f);
    }

    public void OnLv4Btn()
    {
        GameController.getInstance().getGameData()._difficulty = 4;
        Instance.Close();
        SceneLoader._instance.loadScene("RoleAndWeaponSelect");
        DelayToInvoke.DelayToInvokeBySecond(() =>
        {
            RoleAndWeaponSelectWindow.Instance.Open();
            RoleAndWeaponSelectWindow.Instance.DisplayRoleScrollContent();
        }, 1.8f);
    }

}
