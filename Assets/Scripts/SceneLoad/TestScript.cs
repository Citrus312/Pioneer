using UnityEngine;

public class TestScript : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1)){
            SceneLoader._instance.loadScene("LevelSelect");
            Debug.Log("f1");
        }
        if(Input.GetKeyDown(KeyCode.F2)){
            SceneLoader._instance.loadScene("Level1");
            Debug.Log("f2");
        }
        if(Input.GetKeyDown(KeyCode.F3)){
            SceneLoader._instance.loadScene("SampleScene");
            Debug.Log("f3");
        }
        if(Input.GetKeyDown(KeyCode.F4)){
            SceneLoader._instance.loadScene("OpeningAnimation");
        }
    }
}
