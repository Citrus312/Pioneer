using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class MainPageWindow : BaseWindow
{
    //��ʼ����ҳ����Ĳ���
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
        //��UI�еİ�ť�󶨵���¼�
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
        Debug.Log("����� ���� ��ť");
    }

    private void OnStartBtn(Button btn)
    {
        Debug.Log("����� ��ʼ ��ť");
    }

    private void OnSettingBtn(Button btn)
    {
        Debug.Log("����� ���� ��ť");
    }

    private void OnTalentBtn(Button btn)
    {
        Debug.Log("����� �츳 ��ť");
    }

    private void OnExitBtn(Button btn)
    {
        Debug.Log("����� �˳� ��ť");
#if UNITY_EDITOR
        //unity�༭���е���ʹ��
        EditorApplication.isPlaying = false;
#else
        //������Ϸ��ʹ��
        Application.Quit();
#endif
    }

    private void OnModBtn(Button btn)
    {
        Debug.Log("����� Mod ��ť");
        TipsWindow window = new TipsWindow();
        window.Open("����Ϸ�������κ�Mod :)");
    }

    private void OnMoreProductBtn(Button btn)
    {
        Debug.Log("����� ������Ʒ ��ť");
        TipsWindow window = new TipsWindow();
        window.Open("������Ʒ? �����ܣ����Բ����ܣ���");
    }

    private void OnNewMsgBtn(Button btn)
    {
        Debug.Log("����� ����Ϣ ��ť");
        TipsWindow window = new TipsWindow();
        window.Open("����տ���Ҳ����ذ� :)");
    }

    private void OnCommunityBtn(Button btn)
    {
        Debug.Log("����� ���� ��ť");
        TipsWindow window = new TipsWindow();
        window.Open("����? ����ʲô?");
    }

    private void OnNameListBtn(Button btn)
    {
        Debug.Log("����� ���������� ��ť");
        TipsWindow window = new TipsWindow();
        window.Open("���߻�/����/���/������: ���ɭ\n" + "������/����: ĪѸ\n" + "������/״���ٳ�: �����\n" + "��ֵ�߻�/�زĿ���: ֣��\n" + "UI���/�ճ�����: �ƿ���");
    }
}
