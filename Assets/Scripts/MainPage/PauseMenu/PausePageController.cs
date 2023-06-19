using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePageController : PersistentSingleton<PausePageController>
{
    public GameObject _Player;
    public bool isPaused = false;
    PausePageWindow pausePageWindow;

    private void Start()
    {
        UIRoot.Init();
        pausePageWindow = new();
        pausePageWindow.Open(getAttribute(_Player));
        pausePageWindow.Close();
    }

    private void Update()
    {
        if (getCurrentScene() != "MainPage")
        {
            if (Input.GetKeyUp(KeyCode.Escape))
                if (pausePageWindow.getTransform().gameObject.activeSelf)
                {
                    pausePageWindow.Close();
                }
                else
                {
                    pausePageWindow.Open();
                }
        }
    }

    private string getAttribute(GameObject _player)
    {
        string content = "Health : " + _player.GetComponent<CharacterAttribute>().getMaxHealth() +
                             "\nSpeed : " + _player.GetComponent<CharacterAttribute>().getMoveSpeed();
        return content;
    }

    private string getCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        return currentSceneName;
    }
    public void OnPiont()
    {
        Transform property = pausePageWindow.getTransform().Find("Property");
        Transform tip = property.Find("panel");
        tip.gameObject.SetActive(true);
    }
}
