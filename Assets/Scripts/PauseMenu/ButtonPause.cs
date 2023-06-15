using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonPause : MonoBehaviour
{
    //the ButtonPauseMenu
    public GameObject pauseMenu;
    public bool isPaused = false;

    //属性UI中的文本
    public TextMeshProUGUI AttributeText;
    public GameObject _player;

    private void Update()
    {
        getKeyEsc();
    }

    //按下esc键调用此方法
    private void getKeyEsc()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            isPaused = !isPaused;
        if (isPaused) OnPause();
        else
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
    }

    //暂停并显示界面
    public void OnPause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        setUI();
    }

    //点击“Continue”时执行此方法
    public void OnContinue()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    //点击“Restart”时执行此方法
    public void OnRestart()
    {
        //Loading Scene0
        isPaused = !isPaused;
        SceneLoader._instance.loadScene("SampleScene");
        Time.timeScale = 1f;
    }

    //更新暂停界面的所有道具和属性显示
    private void setUI()
    {
        getAttributeUI();
    }

    //更新属性UI中的文本内容
    private void getAttributeUI()
    {
        //更新文本中的所有属性
        AttributeText.text = "Health : " + _player.GetComponent<CharacterAttribute>().getMaxHealth() +
                             "\nSpeed : " + _player.GetComponent<CharacterAttribute>().getMoveSpeed();
    }
}
