using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//用于场景间的回退上一场景
public class BackToScene : MonoBehaviour
{
    [SerializeField]
    public string sceneName;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (DifficultySelectWindow.Instance.getTransform() != null)
            {
                DifficultySelectWindow.Instance.Close();
            }
            SceneLoader._instance.loadScene(sceneName);
            switch (sceneName)
            {
                case "MainPage":
                    MainPageWindow.Instance.Open();
                    break;
                default:
                    break;
            }
        }
    }
}
