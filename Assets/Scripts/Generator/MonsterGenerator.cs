using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerator : Generator
{
    // 单例
    private static MonsterGenerator _instance;

    // 要生成的怪物
    public string _monsterPrefabPath;

    // 怪物生成前的信号图片路径
    private string _redCrossPath;

    // 在角色周围生成怪物的范围大小
    public float _distance;
    //生成怪物的时间间隔
    private float _interval;

    // 信号闪烁次数
    private int _flashCnt;

    public void Awake()
    {
        _redCrossPath = "Assets/Prefab/RedCross.prefab";
        _distance = 5.0f;
        _interval = 3.0f;
        _instance = this;
    }

    public static MonsterGenerator getInstance()
    {
        if (_instance == null)
        {
            _instance = new MonsterGenerator();
        }
        return _instance;
    }

    //开始生成
    public void beginGenerate(string monsterPrefabPath, int num)
    {
        //设置生成的怪物预制体路径
        _monsterPrefabPath = monsterPrefabPath;

        for (int i = 0; i < num; i++)
        {
            GameObject redCross = ObjectPool.getInstance().get(_redCrossPath);
            //让红叉随机旋转一定角度
            redCross.transform.Rotate(new Vector3(0, 0, Random.Range(0, 360.0f)));
            StartCoroutine("generateMonster", redCross);
        }
    }

    // 显示红叉，闪烁后生成怪物
    protected IEnumerator generateMonster(GameObject redCross)
    {
        Vector3 pos = getSpawnLocation();
        redCross.transform.position = pos;

        _flashCnt = 2;
        SpriteRenderer spriteRenderer = redCross.GetComponent<SpriteRenderer>();

        for (int i = 0; i < _flashCnt; i++)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1.0f);
            yield return new WaitForSeconds(0.5f);

            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.1f);
            yield return new WaitForSeconds(0.5f);
        }

        ObjectPool.getInstance().remove(_redCrossPath, redCross);

        generateObject(_monsterPrefabPath, pos);
    }

    // 随机获取怪物生成位置
    protected Vector3 getSpawnLocation()
    {
        Vector3 pos = GameController.getInstance().getPlayer().transform.position + new Vector3(Random.Range(-1 * _distance, _distance), Random.Range(-1 * _distance, _distance), 0);
        return pos;
    }
}