using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : PersistentSingleton<AudioManager>
{
    //声音混合器和按钮图标(在场景中绑定)
    [SerializeField] AudioMixer mixer;
    [SerializeField] Sprite masterMuteSprite;
    [SerializeField] Sprite masterOnSprite;
    [SerializeField] Sprite musicMuteSprite;
    [SerializeField] Sprite musicOnSprite;
    [SerializeField] Sprite soundMuteSprite;
    [SerializeField] Sprite soundOnSprite;
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
    //主音量滑动条逻辑实现
    public void MasterSldOnClick(Slider slider)
    {
        //用混合器暴露出来的变量更改相应的音量数值
        mixer.SetFloat("masterVolume", slider.value);
        //检测主音量是否静音，是则将滑动条的数值恒定设为滑动条的最小值
        //以下两个滑动条的逻辑实现一致
        if (!isMasterMute)
        {
            return;
        }
        else
        {
            slider.value = slider.minValue;
        }
    }
    //音乐音量滑动条逻辑实现
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
    //音效音量滑动条逻辑实现
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
    //主音量静音按钮逻辑实现
    public void MasterBtnOnClick(Button btn, Slider slider)
    {
        //检测静音标签，按标签切换相应的图标，更新标签的值，并恢复静音前的音量
        //下面两个按钮的逻辑相同
        if (isMasterMute)
        {
            btn.image.sprite = masterOnSprite;
            isMasterMute = false;
            slider.value = lastMaster;
        }
        else
        {
            btn.image.sprite = masterMuteSprite;
            lastMaster = slider.value;
            slider.value = slider.minValue;
            isMasterMute = true;
        }
    }
    //音乐音量静音按钮逻辑实现
    public void MusicBtnOnClick(Button btn, Slider slider)
    {
        if (isMusicMute)
        {
            btn.image.sprite = musicOnSprite;
            isMusicMute = false;
            slider.value = lastMusic;
        }
        else
        {
            btn.image.sprite = musicMuteSprite;
            lastMusic = slider.value;
            slider.value = slider.minValue;
            isMusicMute = true;
        }
    }
    //音效音量静音按钮逻辑实现
    public void SoundBtnOnClick(Button btn, Slider slider)
    {
        if (isSoundMute)
        {
            btn.image.sprite = soundOnSprite;
            isSoundMute = false;
            slider.value = lastSound;
        }
        else
        {
            btn.image.sprite = soundMuteSprite;
            lastSound = slider.value;
            slider.value = slider.minValue;
            isSoundMute = true;
        }
    }
}
