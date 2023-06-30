using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomScene : MonoBehaviour
{
    public static RandomScene _instance;

    public List<string> _prefabList = new List<string>();
    // 场景大小
    public float _sceneWidth;
    public float _sceneHeight;
    // 每个格子的大小
    private float _gridSize;
    // 障碍物数量
    private int _obstacleCnt;
    // 右上、右下、左下、左上
    int[,] dirs = new int[4,2];

    private void Awake() {
        _instance = this;
        _gridSize = 5.0f;
        _obstacleCnt = 10;
        
        setUpDirs();
        setUpSceneSize();

        //randomGenerateScene();
    }

    void setUpDirs()
    {
        dirs[0,0] = 1;
        dirs[0,1] = 1;

        dirs[1,0] = 1;
        dirs[1,1] = -1;

        dirs[2,0] = -1;
        dirs[2,1] = -1;

        dirs[3,0] = -1;
        dirs[3,1] = 1;
    }
    void setUpSceneSize()
    {
        SpriteRenderer sRender = GetComponent<SpriteRenderer>();
        _sceneWidth = sRender.bounds.size.x;
        _sceneHeight = sRender.bounds.size.y;
    }

    public static RandomScene getInstance()
    {
        return _instance;
    }

    public void randomGenerateScene()
    {
        int n = (int)(_sceneHeight / 2 / _gridSize);
        int m = (int)(_sceneWidth / 2 / _gridSize);
        // 障碍物二维数组
        int[,] matrix = new int[n, m];
        int cnt = _obstacleCnt;
        while(cnt > 0){
            int randY = Random.Range(0, n), randX = Random.Range(0, m);
            if((randY == 0 && randX == 0) || matrix[randY, randX] != 0)
            {
                continue;
            }
            // 障碍物id
            int obstacleID = Random.Range(0, _prefabList.Capacity);
            // 象限
            int randDir = Random.Range(0,4);
            GameObject newObject = ObjectPool.getInstance().get(_prefabList[obstacleID]);
            // 位置偏移
            float offsetX = Random.Range(0f, _gridSize), offsetY = Random.Range(0f, _gridSize);
            // 生成
            newObject.transform.position = new Vector3(dirs[randDir,0] * (randX * _gridSize + offsetX), dirs[randDir,1] * (randY * _gridSize + offsetY), 0);
            cnt--;
        }
    }



}