using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : PersistentSingleton<AudioManager>
{
    [SerializeField]
    AudioMixer mixer;
    //音调
    //const float pitchMin = 0.9f;
    //const float pitchMax = 1.1f;
    //静音标签
    private bool isMasterMute = false;
    private bool isMusicMute = false;
    private bool isSoundMute = false;
    //静音前的音量
    private float lastMaster;
    private float lastMusic;
    private float lastSound;

    private Texture2D texture;

    public void MasterSldOnClick(Slider slider)
    {
        mixer.SetFloat("masterVolume", slider.value);
        if (!isMasterMute)
        {
            return;
        }
        else
        {
            slider.value = slider.minValue;
        }
    }

    public void MusicSldOnClick(Slider slider)
    {
        mixer.SetFloat("musicVolume", slider.value);
        if (!isMusicMute)
        {
            return;
        }
        else
        {
            slider.value = slider.minValue;
        }
    }

    public void SoundSldOnClick(Slider slider)
    {
        mixer.SetFloat("soundVolume", slider.value);
        if (!isSoundMute)
        {
            return;
        }
        else
        {
            slider.value = slider.minValue;
        }
    }

    public void MasterBtnOnClick(Button btn ,Slider slider)
    {
        if (isMasterMute)
        {
            btn.image.sprite = Resources.Load<Sprite>("Icon/MasterOn");
            isMasterMute = false;
            slider.value = lastMaster;
        }
        else
        {
            btn.image.sprite = Resources.Load<Sprite>("Icon/MasterMute");
            lastMaster = slider.value;
            slider.value = slider.minValue;
            isMasterMute = true;
        }
    }

    public void MusicBtnOnClick(Button btn, Slider slider)
    {
        if (isMusicMute)
        {
            btn.image.sprite = Resources.Load<Sprite>("Icon/MusicOn");
            isMusicMute = false;
            slider.value = lastMusic;
        }
        else
        {
            btn.image.sprite = Resources.Load<Sprite>("Icon/MusicMute");
            lastMusic = slider.value;
            slider.value = slider.minValue;
            isMusicMute = true;
        }
    }

    public void SoundBtnOnClick(Button btn ,Slider slider)
    {
        if (isSoundMute)
        {
            btn.image.sprite = Resources.Load<Sprite>("Icon/SoundOn");
            isSoundMute = false;
            slider.value = lastSound;
        }
        else
        {
            btn.image.sprite = Resources.Load<Sprite>("Icon/SoundMute");
            lastSound = slider.value;
            slider.value = slider.minValue;
            isSoundMute = true;
        }
    }
}
