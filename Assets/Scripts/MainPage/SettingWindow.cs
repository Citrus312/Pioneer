using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingWindow : BaseWindow
{
    private static SettingWindow instance;
    protected Slider masterSld;
    protected Slider musicSld;
    protected Slider soundSld;

    private SettingWindow()
    {
        resName = "UI/SettingWindow";
        isResident = true;
        isVisible = false;
        selfType = WindowType.SettingWindow;
        sceneType = SceneType.MainPage;
    }

    public static SettingWindow Instance
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
        masterSld = transform.Find("MasterBar").GetComponent<Slider>();
        musicSld = transform.Find("MusicBar").GetComponent<Slider>();
        soundSld = transform.Find("SoundBar").GetComponent<Slider>();
        
        base.Awake(inputText);
    }

    protected override void FillTextContent(List<string> inputText)
    {
        base.FillTextContent(inputText);
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

    protected override void RegisterUIEvent()
    {
        base.RegisterUIEvent();

        foreach (Button btn in btnList)
        {
            switch (btn.name)
            {
                case "CloseBtn":
                    btn.onClick.AddListener(() => { OnCloseBtn(); });
                    break;
                case "MasterMuteBtn":
                    btn.onClick.AddListener(() => { OnMasterMuteBtn(btn); });
                    break;
                case "MusicMuteBtn":
                    btn.onClick.AddListener(() => { OnMusicMuteBtn(btn); });
                    break;
                case "SoundMuteBtn":
                    btn.onClick.AddListener(() => { OnSoundMuteBtn(btn); });
                    break;
                default:
                    Debug.LogError("An unexpected button exists!");
                    break;
            }
        }
        masterSld.onValueChanged.AddListener(val => { OnMasterSld(); });
        musicSld.onValueChanged.AddListener((float val) => { OnMusicSld(); });
        soundSld.onValueChanged.AddListener((float val) => { OnSoundSld(); });
    }

    protected override void Update(float deltaTime)
    {
        base.Update(deltaTime);
    }

    private void OnCloseBtn()
    {
        Close();
    }

    private void OnMasterMuteBtn(Button btn)
    {
        AudioManager.Instance.MasterBtnOnClick(btn, masterSld);
    }

    private void OnMusicMuteBtn(Button btn)
    {
        AudioManager.Instance.MusicBtnOnClick(btn, musicSld);
    }

    private void OnSoundMuteBtn(Button btn)
    {
        AudioManager.Instance.SoundBtnOnClick(btn, soundSld);
    }

    private void OnMasterSld()
    {
        AudioManager.Instance.MasterSldOnClick(masterSld);
    }

    private void OnMusicSld()
    {
        AudioManager.Instance.MusicSldOnClick(musicSld);
    }

    private void OnSoundSld()
    {
        AudioManager.Instance.SoundSldOnClick(soundSld);
    }
}
