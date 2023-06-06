using UnityEngine;

public class TestScript : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1)){
            SceneLoader._instance.LoadScene("Level1");
            Debug.Log("f1");
        }
        if(Input.GetKeyDown(KeyCode.F2)){
            SceneLoader._instance.LoadScene("OpeningAnimation");
            Debug.Log("f2");
        }
        if(Input.GetKeyDown(KeyCode.F3)){
            SceneLoader._instance.LoadScene("SampleScene");
            Debug.Log("f3");
        }
        if(Input.GetKeyDown(KeyCode.F4)){
            SceneLoader._instance.LoadScene("Scene D");
        }
    }
}
