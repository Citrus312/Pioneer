using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                    DelayToInvoke.DelayToInvokeBySecond(()=> { MainPageWindow.Instance.Open(); }, 1.8f);
                    break;
                default:
                    break;
            }
        }
    }
}
