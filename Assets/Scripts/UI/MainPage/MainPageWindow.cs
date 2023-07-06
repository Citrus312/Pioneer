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

        GameController.getInstance().getGameData().ResetGameData();
        JsonLoader.LoadAndDecodeGameData();

        GameObject.Find("Manager").GetComponent<AudioSource>().clip = GameController.getInstance().themeMusic;
        GameObject.Find("Manager").GetComponent<AudioSource>().Play();

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
        if (SceneLoader._instance.LoadAble)
        {
            Debug.Log("点击了 继续 按钮");
            CharacterAttribute attr = new();
            attr.setAllPlayerAttribute(JsonLoader.rolePool[GameController.getInstance().getGameData()._playerID]);
            foreach (KeyValuePair<string, float> item in TalentTreeWindow.Instance.getAttributeFinal())
            {
                switch (item.Key)
                {
                    case "maxHealth":
                        attr.setMaxHealth(attr.getMaxHealth() + item.Value);
                        break;
                    case "healthRecovery":
                        attr.setHealthRecovery(attr.getHealthRecovery() + item.Value);
                        break;
                    case "healthSteal":
                        attr.setHealthSteal(attr.getHealthSteal() + item.Value);
                        break;
                    case "attackAmplification":
                        attr.setAttackAmplification(attr.getAttackAmplification() + item.Value);
                        break;
                    case "meleeDamage":
                        attr.setMeleeDamage(attr.getMeleeDamage() + item.Value);
                        break;
                    case "rangedDamage":
                        attr.setRangedDamage(attr.getRangedDamage() + item.Value);
                        break;
                    case "abilityDamage":
                        attr.setAbilityDamage(attr.getAbilityDamage() + item.Value);
                        break;
                    case "attackSpeedAmplification":
                        attr.setAttackSpeedAmplification(attr.getAttackSpeedAmplification() + item.Value);
                        break;
                    case "criticalRate":
                        attr.setCriticalRate(attr.getCriticalRate() + item.Value);
                        break;
                    case "engineering":
                        attr.setEngineering(attr.getEngineering() + item.Value);
                        break;
                    case "attackRangeAmplification":
                        attr.setAttackRangeAmplification(attr.getAttackRangeAmplification() + item.Value);
                        break;
                    case "armorStrength":
                        attr.setArmorStrength(attr.getArmorStrength() + item.Value);
                        break;
                    case "dodgeRate":
                        attr.setDodgeRate(attr.getDodgeRate() + item.Value);
                        break;
                    case "moveSpeedAmplification":
                        attr.setMoveSpeedAmplification(attr.getMoveSpeedAmplification() + item.Value);
                        break;
                    case "scanAccuracy":
                        attr.setScanAccuracy(attr.getScanAccuracy() + item.Value);
                        break;
                    case "collectEfficiency":
                        attr.setCollectEfficiency(attr.getCollectEfficiency() + item.Value);
                        break;
                    default:
                        break;
                }
            }
            GameController.getInstance().getGameData()._attr.setAllPlayerAttribute(attr);
            SceneLoader._instance.loadScene(GameController.getInstance().getGameData()._scene);
            DelayToInvoke.DelayToInvokeBySecond(() => { instance.Close(); }, 1.0f);
        }
    }
    //开始按钮的点击事件
    private void OnStartBtn()
    {
        if (SceneLoader._instance.LoadAble)
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
            instance.Close();
        }
    }
    private void OnSettingBtn()
    {
        if (SceneLoader._instance.LoadAble)
        {
            Debug.Log("点击了 设置 按钮");
            SettingWindow.Instance.Open();
        }
    }
    private void OnTalentBtn()
    {
        if (SceneLoader._instance.LoadAble)
        {
            Debug.Log("点击了 天赋 按钮");
            SceneLoader._instance.loadScene("TalentTree");
            TalentTreeWindow.Instance.Open();
            MainPageWindow.Instance.Close();
            //DelayToInvoke.DelayToInvokeBySecond(() => { TalentTreeWindow.Instance.Open(); }, 1.8f);
        }
    }
    private void OnExitBtn()
    {
        if (SceneLoader._instance.LoadAble)
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
    }
    //以下的按钮点击事件都是打开一个提示窗
    //实现逻辑一致
    private void OnModBtn()
    {
        if (SceneLoader._instance.LoadAble)
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
    }
    private void OnMoreProductBtn()
    {
        if (SceneLoader._instance.LoadAble)
        {
            Debug.Log("点击了 更多作品 按钮");
            TipsWindow window = new();
            List<string> text = new();
            text.Add("更多作品? 不可能！绝对不可能！！");
            window.inputText = text;
            window.Open();
        }
    }
    private void OnNewMsgBtn()
    {
        if (SceneLoader._instance.LoadAble)
        {
            Debug.Log("点击了 新消息 按钮");
            TipsWindow window = new();
            List<string> text = new();
            text.Add("这里空空如也，请回吧 :)");
            window.inputText = text;
            window.Open();
        }
    }
    private void OnCommunityBtn()
    {
        if (SceneLoader._instance.LoadAble)
        {
            Debug.Log("点击了 社区 按钮");
            TipsWindow window = new();
            List<string> text = new();
            text.Add("社区? 那是什么?");
            window.inputText = text;
            window.Open();
        }
    }
    private void OnNameListBtn()
    {
        if (SceneLoader._instance.LoadAble)
        {
            Debug.Log("点击了 制作人名单 按钮");
            TipsWindow window = new();
            List<string> text = new();
            text.Add("主策划/技美/编程/多面手: 余嘉森\n" + "主程序/大佬: 莫迅\n" + "主程序/状况百出: 刘宇菲\n" + "数值策划/素材苦手: 郑涛\n" + "UI设计/日常被催: 黄俊霖");
            window.inputText = text;
            window.Open();
        }
    }

    public override void Open()
    {
        // 主页窗口延迟显示
        DelayToInvoke.DelayToInvokeBySecond(() => { base.Open(); }, 1.0f);
    }
}
