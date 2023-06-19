using UnityEngine;

public class MonsterGenerator : Generator
{
    // 单例
    private static MonsterGenerator _instance;

    // 要生成的怪物
    public string _monsterPrefabPath;
    public GameObject _player;
    private SpriteRenderer _redCross;// 怪物生成前的信号图片

    // 在角色周围生成怪物的范围大小
    public float _distance;
    //生成怪物的时间间隔
    private float _interval;

    // 信号闪烁次数
    private int _flashCnt;

    public override void Start()
    {
        base.Start();

        _redCross = GetComponent<SpriteRenderer>(); // 必须挂一个(待解决)
        _distance = 5.0f;
        _interval = 5.0f;
        InvokeRepeating("beginGenerate", 1.0f, _interval);
    }

    public static MonsterGenerator getInstance()
    {
        if (_instance == null)
        {
            _instance = new MonsterGenerator();
        }
        return _instance;
    }

    protected void readFile()
    {
        //
    }

    protected void beginGenerate()
    {
        // 数据设置
        GameController.getInstance().getGameData();
        showRedCross();

    }

    // 红叉显现
    protected void showRedCross()
    {
        _redCross.transform.position = getSpawnLocation();
        _redCross.enabled = true;

        _flashCnt = 2;
        redCrossFlash();
        Invoke("generateMonster", 2.5f);
    }

    // 红叉闪烁
    protected void redCrossFlash()
    {
        _redCross.color = new Color(_redCross.color.r, _redCross.color.g, _redCross.color.b, 1.0f);
        Invoke("redCrossFade", 0.5f);
    }

    // 红叉透明
    protected void redCrossFade()
    {
        _redCross.color = new Color(_redCross.color.r, _redCross.color.g, _redCross.color.b, 0.1f);
        if (_flashCnt > 0)
        {
            Invoke("redCrossFlash", 0.5f);
            _flashCnt--;
        }
        else
        {
            // 红叉消失
            _redCross.color = new Color(_redCross.color.r, _redCross.color.g, _redCross.color.b, 0.0f);
            _redCross.enabled = false;
        }
    }

    protected void generateMonster() // 待补充
    {
        GameObject monster = generateObject(_monsterPrefabPath, _redCross.transform.position, 1);
    }

    // 随机获取怪物生成位置
    protected Vector3 getSpawnLocation()
    {
        Vector3 pos = _player.transform.position + new Vector3(Random.Range(-1 * _distance, _distance), Random.Range(-1 * _distance, _distance), 0);
        return pos;
    }
}