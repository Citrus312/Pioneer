using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagement : MonoBehaviour
{
    // 单例
    public static SceneManagement _sceneManager;
    public GameObject _player;
    private float sceneLength;
    private float sceneWidth;
    public float _distance = 10;

    private void Awake()
    {
        sceneLength = 10f;
        sceneWidth = 10f;
    }
    private void Update()
    {
        setSceneSize(20f, 20f);
    }

    public GameObject getPlayer()
    {
        _player = GameObject.Find("Player");
        return _player;
    }
    public static SceneManagement getSceneManager()
    {
        if (_sceneManager == null)
        {
            _sceneManager = new SceneManagement();
        }
        return _sceneManager;
    }

    public void setSceneSize(float length, float width)
    {
        sceneWidth = width;
        sceneLength = length;
        EdgeCollider2D edge = GetComponent<EdgeCollider2D>();
        Vector2[] pointsArray = { new Vector2(length/2,width/2), new Vector2(length / 2, -width / 2),
                                  new Vector2(-length / 2, -width / 2), new Vector2(-length / 2, width / 2),
                                  new Vector2(length / 2, width / 2) };
        edge.points = pointsArray;
    }

    public bool isInScene(Vector3 position)
    {
        if (position.x > -sceneLength / 2 && position.x < sceneLength / 2)
        {
            if (position.y > -sceneWidth / 2 && position.y < sceneWidth / 2) return true;
            else return false;
        }
        else return false;
    }

    public static float Rand(float u, float d)
    {
        float u1, u2, z, x;
        //Random ram = new Random();
        if (d <= 0)
        {
            return u;
        }
        Random.InitState((int)Time.time);
        u1 = Random.Range(0f, 1f);
        Random.InitState((int)Time.time);
        u2 = Random.Range(0f, 1f);
        z = Mathf.Sqrt(-2 * Mathf.Log(u1)) * Mathf.Sin(2 * Mathf.PI * u2);
        x = u + d * z;
        return x;
    }

    //利用正态分布得到生成器位置
    public Vector3 getGeneratorPos()
    {
        float dx = Mathf.Sqrt(sceneLength);
        float dy = Mathf.Sqrt(sceneWidth);
        Vector3 generatorPos;
        do
        {
            float ux = _player.transform.position.x + Random.Range(-1 * _distance, _distance);
            float uy = _player.transform.position.y + Random.Range(-1 * _distance, _distance);
            generatorPos = new Vector3(Rand(ux, dx), Rand(uy, dy), _player.transform.position.z);
        }
        while (!isInScene(generatorPos));
        return generatorPos;
    }
}
