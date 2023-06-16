using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonPause : MonoBehaviour
{
    //the ButtonPauseMenu
    public GameObject pauseMenu;
    public bool isPaused = false;

    //����UI�е��ı�
    public TextMeshProUGUI AttributeText;
    public GameObject _player;

    private void Update()
    {
        getKeyEsc();
    }

    //����esc�����ô˷���
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

    //��ͣ����ʾ����
    public void OnPause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        setUI();
    }

    //�����Continue��ʱִ�д˷���
    public void OnContinue()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    //�����Restart��ʱִ�д˷���
    public void OnRestart()
    {
        //Loading Scene0
        isPaused = !isPaused;
        SceneLoader._instance.loadScene("SampleScene");
        Time.timeScale = 1f;
    }

    //������ͣ��������е��ߺ�������ʾ
    private void setUI()
    {
        getAttributeUI();
    }

    //��������UI�е��ı�����
    private void getAttributeUI()
    {
        //�����ı��е���������
        AttributeText.text = "Health : " + _player.GetComponent<CharacterAttribute>().getMaxHealth() +
                             "\nSpeed : " + _player.GetComponent<CharacterAttribute>().getMoveSpeed();
    }
}
