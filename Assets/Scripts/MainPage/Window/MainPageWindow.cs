using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class MainPageWindow : BaseWindow
{
    private static MainPageWindow instance;

    //��ʼ����ҳ����Ĳ���
    private MainPageWindow()
    {
        resName = "UI/MainPageWindow";
        isResident = true;
        isVisible = false;
        selfType = WindowType.MainPageWindow;
        sceneType = SceneType.MainPage;
    }

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

    protected override void OnEnable()
    {
        base.OnEnable();

        foreach (Button btn in btnList)
        {
            if (btn.name == "ContinueBtn")
            {
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

    protected override void RegisterUIEvent()
    {
        base.RegisterUIEvent();
        //��UI�еİ�ť�󶨵���¼�
        foreach (Button btn in btnList)
        {
            switch (btn.name)
            {
                case "ContinueBtn":
                    btn.onClick.AddListener(()=> { OnContinueBtn(); });
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

    private void OnContinueBtn()
    {
        Debug.Log("����� ���� ��ť");
    }

    private void OnStartBtn()
    {
        Debug.Log("����� ��ʼ ��ť");
        GameController.getInstance().getGameData().ResetGameData();
        if (GameController.getInstance().getGameData()._isFirstPlaying)
        {
            SceneLoader._instance.loadScene("OpeningAnimation");  
        }
        else
        {
            SceneLoader._instance.loadScene("LevelSelect");
        }

        DelayToInvoke.DelayToInvokeBySecond(()=> { Close(); }, 1.8f);
    }

    private void OnSettingBtn()
    {
        Debug.Log("����� ���� ��ť");
        SettingWindow.Instance.Open();
    }

    private void OnTalentBtn()
    {
        Debug.Log("����� �츳 ��ť");
    }

    private void OnExitBtn()
    {
        Debug.Log("����� �˳� ��ť");
        //#if UNITY_EDITOR
        //        //unity�༭���е���ʹ��
        //        EditorApplication.isPlaying = false;
        //#else
        //        //������Ϸ��ʹ��
        //        Application.Quit();
        //#endif
    }

    private void OnModBtn()
    {
        Debug.Log("����� Mod ��ť");
        TipsWindow window = new();
        List<string> text = new();
        text.Add("����Ϸ�������κ�Mod :)");
        window.inputText = text;
        window.Open();
    }

    private void OnMoreProductBtn()
    {
        Debug.Log("����� ������Ʒ ��ť");
        TipsWindow window = new();
        List<string> text = new();
        text.Add("������Ʒ? �����ܣ����Բ����ܣ���");
        window.inputText = text;
        window.Open();
    }

    private void OnNewMsgBtn()
    {
        Debug.Log("����� ����Ϣ ��ť");
        TipsWindow window = new();
        List<string> text = new();
        text.Add("����տ���Ҳ����ذ� :)");
        window.inputText = text;
        window.Open();
    }

    private void OnCommunityBtn()
    {
        Debug.Log("����� ���� ��ť");
        TipsWindow window = new();
        List<string> text = new();
        text.Add("����? ����ʲô?");
        window.inputText = text;
        window.Open();
    }

    private void OnNameListBtn()
    {
        Debug.Log("����� ���������� ��ť");
        TipsWindow window = new();
        List<string> text = new();
        text.Add("���߻�/����/���/������: ���ɭ\n" + "������/����: ĪѸ\n" + "������/״���ٳ�: �����\n" + "��ֵ�߻�/�زĿ���: ֣��\n" + "UI���/�ճ�����: �ƿ���");
        window.inputText = text;
        window.Open();
    }
}
