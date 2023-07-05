using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomScene : MonoBehaviour
{
    public static RandomScene _instance;

    public List<string> _obstacleList = new List<string>();
    public List<string> _terrainList = new List<string>();
    // 场景大小
    private float _sceneWidth;
    private float _sceneHeight;
    // 每个格子的大小
    public float _gridSize = 4.0f;
    // 障碍物数量
    public int _obstacleCnt = 8;
    // 右上、右下、左下、左上
    private int[,] dirs = new int[4, 2];

    private void Awake()
    {
        _instance = this;

        setUpDirs();
        setUpSceneSize();
    }

    void setUpDirs()
    {
        dirs[0, 0] = 1;
        dirs[0, 1] = 1;

        dirs[1, 0] = 1;
        dirs[1, 1] = -1;

        dirs[2, 0] = -1;
        dirs[2, 1] = -1;

        dirs[3, 0] = -1;
        dirs[3, 1] = 1;
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
        // 障碍物数组
        int[,,] matrix = new int[4, n, m];
        // 总量取较小值
        int cnt = _obstacleCnt <= (4 * n * m) ? _obstacleCnt : (4 * n * m);
        while (cnt > 0)
        {
            // 象限
            int randDir = Random.Range(0, 4);
            // 数组索引
            int randY = Random.Range(0, n), randX = Random.Range(0, m);
            if ((randY == 0 && randX == 0) || matrix[randDir, randY, randX] != 0)
            {
                continue;
            }
            if (Random.Range(0, 2) == 0) cnt -= generateObstacle(randDir, randY, randX, matrix);
            else cnt -= generateTerrain(randDir, randY, randX, matrix);
        }
    }

    private int generateObstacle(int randDir, int randY, int randX, int[,,] matrix)
    {
        if (_obstacleList.Capacity == 0) return 0;

        // 障碍物id
        int idx = Random.Range(0, _obstacleList.Capacity);
        matrix[randDir, randY, randX] = idx + 1;
        GameObject newObject = ObjectPool.getInstance().get(_obstacleList[idx]);
        // 位置偏移
        float minOffset = getSize(newObject) / 2;
        float maxOffset = _gridSize - (getSize(newObject) / 2);
        float offsetX = Random.Range(minOffset, maxOffset);
        float offsetY = Random.Range(minOffset, maxOffset);
        // 生成
        newObject.transform.position = new Vector3(dirs[randDir, 0] * (randX * _gridSize + offsetX), dirs[randDir, 1] * (randY * _gridSize + offsetY), 0);

        return 1;
    }

    private int generateTerrain(int randDir, int randY, int randX, int[,,] matrix)
    {
        if (_terrainList.Capacity == 0) return 0;

        // 地形id
        int idx = Random.Range(0, _terrainList.Capacity);
        matrix[randDir, randY, randX] = idx + 1;
        GameObject newObject = ObjectPool.getInstance().get(_terrainList[idx]);

        // 位置偏移
        float minOffset = getSize(newObject) / 2;
        float maxOffset = _gridSize - (getSize(newObject) / 2);
        float offsetX = Random.Range(minOffset, maxOffset);
        float offsetY = Random.Range(minOffset, maxOffset);
        // 生成
        newObject.transform.position = new Vector3(dirs[randDir, 0] * (randX * _gridSize + offsetX), dirs[randDir, 1] * (randY * _gridSize + offsetY), 0);

        // 旋转
        float minRotationAngle = 0f, maxRotationAngle = 360f;
        newObject.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(minRotationAngle, maxRotationAngle));
        return 1;
    }

    float getSize(GameObject obj)
    {
        return obj.GetComponent<ObstacleSize>().size;
    }

    public float getSceneWidth()
    {
        return _sceneWidth;
    }

    public float getSceneHeight()
    {
        return _sceneHeight;
    }
}