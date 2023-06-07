using UnityEngine;

public class MonsterGenerator : Generator
{
    // 单例
    private static MonsterGenerator instance;
    
    // 要生成的怪物
    public GameObject _monsterPrefab;
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
        InvokeRepeating("BeginGenerate", 1.0f, _interval);
    }

    public static MonsterGenerator getInstance()
    {
        if(instance == null)
        {
            instance = new MonsterGenerator();
        }
        return instance;
    }

    protected void BeginGenerate()
    {
        ShowRedCross();
        
    }

    // 红叉显现
    protected void ShowRedCross()
    {
        _redCross.transform.position = GetSpawnLocation();
        _redCross.enabled = true;

        _flashCnt = 2;
        RedCrossFlash();
        Invoke("GenerateMonster", 2.5f);
    }

    // 红叉闪烁
    protected void RedCrossFlash()
    {
        _redCross.color = new Color(_redCross.color.r, _redCross.color.g, _redCross.color.b, 1.0f);
        Invoke("RedCrossFade", 0.5f);
    }

    // 红叉透明
    protected void RedCrossFade()
    {
        _redCross.color = new Color(_redCross.color.r, _redCross.color.g,_redCross.color.b, 0.1f);
        if(_flashCnt > 0){
            Invoke("RedCrossFlash", 0.5f);
            _flashCnt--;
        }
        else
        {
            // 红叉消失
            _redCross.color = new Color(_redCross.color.r, _redCross.color.g,_redCross.color.b, 0.0f);
            _redCross.enabled = false;
        }
    }

    protected void GenerateMonster() // 待补充
    {
        GenerateObject(_monsterPrefab, _redCross.transform.position, 1);
    }

    // 随机获取怪物生成位置
    protected Vector3 GetSpawnLocation()
    {
        Vector3 pos = _player.transform.position + new Vector3(Random.Range(-1 * _distance, _distance), Random.Range(-1 * _distance, _distance), 0);
        return pos;
    }
}