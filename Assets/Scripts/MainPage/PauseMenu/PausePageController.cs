using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePageController : PersistentSingleton<PausePageController>
{
    public GameObject _Player;
    public bool isPaused = false;
    PausePageWindow pausePageWindow;

    private void Start()
    {
        UIRoot.Init();
        pausePageWindow = new();
        pausePageWindow.Open();
        pausePageWindow.Close();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            //isPaused = !isPaused;
            if (pausePageWindow.getTransform().gameObject.activeSelf)
            {
                pausePageWindow.Close();
            }
            else
            {
                pausePageWindow.Open();
            }
    }

    private string getAttribute(GameObject _player)
    {
        string content = "Health : " + _player.GetComponent<CharacterAttribute>().getMaxHealth() +
                             "\nSpeed : " + _player.GetComponent<CharacterAttribute>().getMoveSpeed();
        return content;
    }
}
