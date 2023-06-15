using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class MainPageWindow : BaseWindow
{
    //初始化主页窗体的参数
    public MainPageWindow()
    {
        resName = "UI/MainPageWindow";
        isResident = true;
        isVisible = false;
        selfType = WindowType.MainPageWindow;
        sceneType = SceneType.MainPage;
    }

    protected override void Awake(string inputText = "")
    {
        base.Awake();
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

    protected override void RegisterUIEvent()
    {
        base.RegisterUIEvent();
        //给UI中的按钮绑定点击事件
        foreach (Button btn in btnList)
        {
            switch (btn.name)
            {
                case "ContinueBtn":
                    btn.onClick.AddListener(()=> { OnContinueBtn(btn); });
                    break;
                case "StartBtn":
                    btn.onClick.AddListener(() => { OnStartBtn(btn); });
                    break;
                case "SettingBtn":
                    btn.onClick.AddListener(() => { OnSettingBtn(btn); });
                    break;
                case "TalentBtn":
                    btn.onClick.AddListener(() => { OnTalentBtn(btn); });
                    break;
                case "ExitBtn":
                    btn.onClick.AddListener(() => { OnExitBtn(btn); });
                    break;
                case "ModBtn":
                    btn.onClick.AddListener(() => { OnModBtn(btn); });
                    break;
                case "MoreProductBtn":
                    btn.onClick.AddListener(() => { OnMoreProductBtn(btn); });
                    break;
                case "NewMsgBtn":
                    btn.onClick.AddListener(() => { OnNewMsgBtn(btn); });
                    break;
                case "CommunityBtn":
                    btn.onClick.AddListener(() => { OnCommunityBtn(btn); });
                    break;
                case "NameListBtn":
                    btn.onClick.AddListener(() => { OnNameListBtn(btn); });
                    break;
                default:
                    Debug.LogError("An unexpected button exists!");
                    break;
            }
        }
    }

    protected override void Update(float deltaTime)
    {
        base.Update(deltaTime);
    }

    protected override void FillTextContent(string inputText)
    {
        base.FillTextContent(inputText);
    }

    private void OnContinueBtn(Button btn)
    {
        Debug.Log("点击了 继续 按钮");
    }

    private void OnStartBtn(Button btn)
    {
        Debug.Log("点击了 开始 按钮");
    }

    private void OnSettingBtn(Button btn)
    {
        Debug.Log("点击了 设置 按钮");
    }

    private void OnTalentBtn(Button btn)
    {
        Debug.Log("点击了 天赋 按钮");
    }

    private void OnExitBtn(Button btn)
    {
        Debug.Log("点击了 退出 按钮");
#if UNITY_EDITOR
        //unity编辑器中调试使用
        EditorApplication.isPlaying = false;
#else
        //导出游戏后使用
        Application.Quit();
#endif
    }

    private void OnModBtn(Button btn)
    {
        Debug.Log("点击了 Mod 按钮");
        TipsWindow window = new TipsWindow();
        window.Open("此游戏不会有任何Mod :)");
    }

    private void OnMoreProductBtn(Button btn)
    {
        Debug.Log("点击了 更多作品 按钮");
        TipsWindow window = new TipsWindow();
        window.Open("更多作品? 不可能！绝对不可能！！");
    }

    private void OnNewMsgBtn(Button btn)
    {
        Debug.Log("点击了 新消息 按钮");
        TipsWindow window = new TipsWindow();
        window.Open("这里空空如也，请回吧 :)");
    }

    private void OnCommunityBtn(Button btn)
    {
        Debug.Log("点击了 社区 按钮");
        TipsWindow window = new TipsWindow();
        window.Open("社区? 那是什么?");
    }

    private void OnNameListBtn(Button btn)
    {
        Debug.Log("点击了 制作人名单 按钮");
        TipsWindow window = new TipsWindow();
        window.Open("主策划/技美/编程/多面手: 余嘉森\n" + "主程序/大佬: 莫迅\n" + "主程序/状况百出: 刘宇菲\n" + "数值策划/素材苦手: 郑涛\n" + "UI设计/日常被催: 黄俊霖");
    }
}
