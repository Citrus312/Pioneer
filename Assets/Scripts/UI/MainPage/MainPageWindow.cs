using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class MainPageWindow : BaseWindow
{
    //窗体的单例实例
    private static MainPageWindow instance;

    //初始化主页窗体的参数
    private MainPageWindow()
    {
        resName = "UI/MainPageWindow";
        isResident = true;
        isVisible = false;
        selfType = WindowType.MainPageWindow;
        sceneType = SceneType.MainPage;
    }
    //实例的自动属性
    public static MainPageWindow Instance
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
    protected override void OnAddListener()
    {
        base.OnAddListener();
    }
    protected override void OnRemoveListener()
    {
        base.OnRemoveListener();
    }
    //打开窗口时判断“继续”按钮是否允许点击
    protected override void OnEnable()
    {
        base.OnEnable();
        //遍历按钮列表寻找“继续”按钮
        foreach (Button btn in btnList)
        {
            if (btn.name == "ContinueBtn")
            {
                //找到按钮后根据当前玩家的武器列表长度判断当前是否有存档
                //因为进入主界面时会加载一次本地的游戏数据的json文件，这个文件会根据游戏状态自动保存当时的游戏数据
                //当游玩中途返回主界面或者关闭游戏后，再次进入游戏，点击“继续”按钮就能恢复之前的游戏状态
                if (GameController.getInstance().getGameData()._weaponList.Count == 0)
                {
                    btn.interactable = false;
                }
                else
                {
                    btn.interactable = true;
                }

            }
        }
    }
    protected override void OnDisable()
    {
        base.OnDisable();
    }
    //给按钮绑定点击事件
    protected override void RegisterUIEvent()
    {
        base.RegisterUIEvent();
        //给UI中的按钮绑定点击事件
        foreach (Button btn in btnList)
        {
            switch (btn.name)
            {
                case "ContinueBtn":
                    btn.onClick.AddListener(() => { OnContinueBtn(); });
                    break;
                case "StartBtn":
                    btn.onClick.AddListener(() => { OnStartBtn(); });
                    break;
                case "SettingBtn":
                    btn.onClick.AddListener(() => { OnSettingBtn(); });
                    break;
                case "TalentBtn":
                    btn.onClick.AddListener(() => { OnTalentBtn(); });
                    break;
                case "ExitBtn":
                    btn.onClick.AddListener(() => { OnExitBtn(); });
                    break;
                case "ModBtn":
                    btn.onClick.AddListener(() => { OnModBtn(); });
                    break;
                case "MoreProductBtn":
                    btn.onClick.AddListener(() => { OnMoreProductBtn(); });
                    break;
                case "NewMsgBtn":
                    btn.onClick.AddListener(() => { OnNewMsgBtn(); });
                    break;
                case "CommunityBtn":
                    btn.onClick.AddListener(() => { OnCommunityBtn(); });
                    break;
                case "NameListBtn":
                    btn.onClick.AddListener(() => { OnNameListBtn(); });
                    break;
                default:
                    Debug.LogError("An unexpected button exists!");
                    break;
            }
        }
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override void FillTextContent()
    {
        base.FillTextContent();
    }

    //以下是按钮的点击事件
    private void OnContinueBtn()
    {
        Debug.Log("点击了 继续 按钮");
    }
    //开始按钮的点击事件
    private void OnStartBtn()
    {
        Debug.Log("点击了 开始 按钮");
        //重置当前游戏数据
        GameController.getInstance().getGameData().ResetGameData();
        //判断是否第一次游玩，是则播放开场动画
        if (GameController.getInstance().getGameData()._isFirstPlaying)
        {
            SceneLoader._instance.loadScene("OpeningAnimation");
        }
        else
        {
            SceneLoader._instance.loadScene("LevelSelect");
        }
        Close();
    }
    private void OnSettingBtn()
    {
        Debug.Log("点击了 设置 按钮");
        SettingWindow.Instance.Open();
    }
    private void OnTalentBtn()
    {

        Debug.Log("点击了 天赋 按钮");
        SceneLoader._instance.loadScene("TalentTree");
        DelayToInvoke.DelayToInvokeBySecond(() => { TalentTreeWindow.Instance.Open(); }, 1.8f);

    }
    private void OnExitBtn()
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
    //以下的按钮点击事件都是打开一个提示窗
    //实现逻辑一致
    private void OnModBtn()
    {
        Debug.Log("点击了 Mod 按钮");
        //创建一个提示窗
        TipsWindow window = new();
        List<string> text = new();
        text.Add("此游戏不会有任何Mod :)");
        //设置提示窗的文本
        window.inputText = text;
        //打开提示窗
        window.Open();
    }
    private void OnMoreProductBtn()
    {
        Debug.Log("点击了 更多作品 按钮");
        TipsWindow window = new();
        List<string> text = new();
        text.Add("更多作品? 不可能！绝对不可能！！");
        window.inputText = text;
        window.Open();
    }
    private void OnNewMsgBtn()
    {
        Debug.Log("点击了 新消息 按钮");
        TipsWindow window = new();
        List<string> text = new();
        text.Add("这里空空如也，请回吧 :)");
        window.inputText = text;
        window.Open();
    }
    private void OnCommunityBtn()
    {
        Debug.Log("点击了 社区 按钮");
        TipsWindow window = new();
        List<string> text = new();
        text.Add("社区? 那是什么?");
        window.inputText = text;
        window.Open();
    }
    private void OnNameListBtn()
    {
        Debug.Log("点击了 制作人名单 按钮");
        TipsWindow window = new();
        List<string> text = new();
        text.Add("主策划/技美/编程/多面手: 余嘉森\n" + "主程序/大佬: 莫迅\n" + "主程序/状况百出: 刘宇菲\n" + "数值策划/素材苦手: 郑涛\n" + "UI设计/日常被催: 黄俊霖");
        window.inputText = text;
        window.Open();
    }

    public override void Open()
    {
        // 主页窗口延迟显示
        DelayToInvoke.DelayToInvokeBySecond(()=>{base.Open();}, 1.0f);
    }
}
