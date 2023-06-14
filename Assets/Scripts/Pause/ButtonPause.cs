using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPause : MonoBehaviour
{
    //the ButtonPauseMenu
    public GameObject ingameMenu;
    public bool isPaused = false;

    private void Update()
    {
        getKeyEsc();
    }

    private void getKeyEsc()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            isPaused = !isPaused;
        if (isPaused) OnPause();
        else
        {
            Time.timeScale = 1f;
            ingameMenu.SetActive(false);
        }
    }

    public void OnPause()
    {
        Time.timeScale = 0;
        ingameMenu.SetActive(true);
    }

    public void OnContinue()//�����Continue��ʱִ�д˷���
    {
        isPaused = !isPaused;
        ingameMenu.SetActive(false);
        Time.timeScale = 1f;
        Debug.Log("aaaaaaaaaaaaaaaaaaaaaaa");
    }

    public void OnRestart()//�����Restart��ʱִ�д˷���
    {
        //Loading Scene0
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

}
