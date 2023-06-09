using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagement : MonoBehaviour
{
    // 单例
    private static SceneManagement sceneManager;
    public GameObject _player;
    private float sceneLength;
    private float sceneWidth;

    private void Start()
    {
        sceneLength = 10f;
        sceneWidth = 10f;
    }
    private void Update()
    {
        setSceneSize(2f, 2f);
    }
    public static SceneManagement getSceneManager()
    {
        if (sceneManager == null)
        {
            sceneManager = new SceneManagement();
        }
        return sceneManager;
    }

    public GameObject getPlayer()
    {
        return _player;
    }

    public void setSceneSize(float length, float width)
    {
        sceneWidth = width;
        sceneLength = length;
        print(sceneLength + " " + sceneWidth);
        EdgeCollider2D edge = GetComponent<EdgeCollider2D>();
        Vector2[] pointsArray = { new Vector2(length/2,width/2), new Vector2(length / 2, -width / 2),
                                  new Vector2(-length / 2, -width / 2), new Vector2(-length / 2, width / 2),
                                  new Vector2(length / 2, width / 2) };
        edge.points = pointsArray;
    }
}
