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

    protected override void Awake(List<string> inputText = null)
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

    protected override void Update(float deltaTime)
    {
        base.Update(deltaTime);
    }

    protected override void FillTextContent(List<string> inputText)
    {
        base.FillTextContent(inputText);
    }

    private void OnContinueBtn()
    {
        Debug.Log("����� ���� ��ť");
    }

    private void OnStartBtn()
    {
        Debug.Log("����� ��ʼ ��ť");
        //if (JsonLoader.localData["isFirstPlaying"])
        //{
        //    //SceneLoader._instance.
        //}
        //else
        //{

        //}
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
        window.Open(text);
    }

    private void OnMoreProductBtn()
    {
        Debug.Log("����� ������Ʒ ��ť");
        TipsWindow window = new();
        List<string> text = new();
        text.Add("������Ʒ? �����ܣ����Բ����ܣ���");
        window.Open(text);
    }

    private void OnNewMsgBtn()
    {
        Debug.Log("����� ����Ϣ ��ť");
        TipsWindow window = new();
        List<string> text = new();
        text.Add("����տ���Ҳ����ذ� :)");
        window.Open(text);
    }

    private void OnCommunityBtn()
    {
        Debug.Log("����� ���� ��ť");
        TipsWindow window = new();
        List<string> text = new();
        text.Add("����? ����ʲô?");
        window.Open(text);
    }

    private void OnNameListBtn()
    {
        Debug.Log("����� ���������� ��ť");
        TipsWindow window = new();
        List<string> text = new();
        text.Add("���߻�/����/���/������: ���ɭ\n" + "������/����: ĪѸ\n" + "������/״���ٳ�: �����\n" + "��ֵ�߻�/�زĿ���: ֣��\n" + "UI���/�ճ�����: �ƿ���");
        window.Open(text);
    }
}
