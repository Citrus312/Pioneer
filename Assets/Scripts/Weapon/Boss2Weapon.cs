using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Weapon : RangedMonsterHit
{
    //boss的阶段状态
    private int _status;
    //boss阶段转化计时器
    private float _statusTimer;
    //boss转化到1阶段的时间
    public float _status1Time;
    //boss转化到2阶段的时间
    public float _status2Time;
    //技能cd
    public float _cd;
    //技能cd计时器
    private float _cdTimer;
    //提示1的图片路径
    public string _hint1Path;
    //提示2的图片路径
    public string _hint2Path;
    //提示1的显示时间
    public float _hint1Time;
    //提示2的显示时间
    public float _hint2Time;
    //弹幕持续时间
    public float _bulletTime;
    //弹幕子弹数量
    private int _bulletNum;
    //弹幕位置
    private List<GameObject> _projectiles = new List<GameObject>();
    //弹幕列表中存放的预制体
    private string _prefabInList;

    [Header("随机弹幕技能参数")]
    //状态0弹幕子弹数量
    public int _status0BulletNum;
    //状态0弹幕矩形范围半边长
    public float _status0Range;
    //状态1弹幕子弹数量
    public int _status1BulletNum;
    //状态1弹幕矩形范围半边长
    public float _status1Range;
    //弹幕矩形范围半边长
    private float _projectilesRange;

    [Header("圆形弹幕技能参数")]
    //弹幕圆形半径
    public float _projectilesRadius;
    //圆形弹幕子弹数量
    public int _status2BulletNum;

    protected override void Awake()
    {
        base.Awake();
        _status = 0;
        _statusTimer = 0;
        _bulletNum = _status0BulletNum;
        _projectilesRange = _status0Range;
    }

    //随机弹幕技能
    private void randomProjectiles()
    {
        //获取角色位置
        Vector2 playerPos = GameController.getInstance().getPlayer().transform.position;

        //随机弹幕位置
        List<Vector2> projectilesPos = new List<Vector2>();
        for (int i = 0; i < _bulletNum; i++)
        {
            projectilesPos.Add(new Vector2(playerPos.x + Random.Range(-_projectilesRange, _projectilesRange), playerPos.y + Random.Range(-_projectilesRange, _projectilesRange)));
        }
        StartCoroutine(createProjectiles(projectilesPos));
    }

    //圆形弹幕技能
    private void circleProjectiles()
    {
        //获取角色位置
        Vector2 playerPos = GameController.getInstance().getPlayer().transform.position;

        //圆形弹幕位置
        List<Vector2> projectilesPos = new List<Vector2>();
        float angle = 0;
        for (int i = 0; i < _bulletNum; i++)
        {
            projectilesPos.Add(playerPos + new Vector2(_projectilesRadius * Mathf.Cos(angle * Mathf.Deg2Rad), _projectilesRadius * Mathf.Sin(angle * Mathf.Deg2Rad)));
            angle += 360.0f / _bulletNum;
        }
        StartCoroutine(createProjectiles(projectilesPos));
    }

    //弹幕技能
    private IEnumerator createProjectiles(List<Vector2> projectilesPos)
    {
        //提示弹幕位置
        for (int i = 0; i < _bulletNum; i++)
        {
            GameObject hint1 = ObjectPool.getInstance().get(_hint1Path);
            hint1.transform.position = projectilesPos[i];
            _projectiles.Add(hint1);
        }
        _prefabInList = _hint1Path;
        yield return new WaitForSeconds(_hint1Time);

        //显示提示2
        for (int i = 0; i < _bulletNum; i++)
        {
            GameObject hint1 = _projectiles[0];
            GameObject hint2 = ObjectPool.getInstance().get(_hint2Path);
            hint2.transform.position = hint1.transform.position;
            _projectiles.RemoveAt(0);
            ObjectPool.getInstance().remove(_hint1Path, hint1);
            _projectiles.Add(hint2);
        }
        _prefabInList = _hint2Path;
        yield return new WaitForSeconds(_hint2Time);

        //弹幕生成
        for (int i = 0; i < _bulletNum; i++)
        {
            GameObject hint2 = _projectiles[0];
            GameObject bullet = ObjectPool.getInstance().get(_bulletPrefab);
            bullet.transform.position = hint2.transform.position;
            bullet.GetComponent<Bullet>().setup(gameObject, _bulletPrefab, "Player", -1);
            _projectiles.RemoveAt(0);
            ObjectPool.getInstance().remove(_hint2Path, hint2);
            _projectiles.Add(bullet);
        }
        _prefabInList = _bulletPrefab;
        yield return new WaitForSeconds(_bulletTime);

        //销毁弹幕
        for (int i = 0; i < _bulletNum; i++)
        {
            GameObject bullet = _projectiles[0];
            _projectiles.RemoveAt(0);
            ObjectPool.getInstance().remove(_bulletPrefab, bullet);
        }
        _prefabInList = null;
    }

    //在销毁boss的同时销毁生成的弹幕
    public void destroyProjectiles()
    {
        if (_prefabInList == null)
            return;

        StopAllCoroutines();

        while (_projectiles.Count != 0)
        {
            GameObject obj = _projectiles[0];
            _projectiles.RemoveAt(0);
            ObjectPool.getInstance().remove(_prefabInList, obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        _statusTimer += Time.deltaTime;
        _cdTimer += Time.deltaTime;
        Vector2 attackDirection = getAttackDirection("Player");
        //如果技能cd结束了则释放技能
        if (_cdTimer >= _cd)
        {
            switch (_status)
            {
                //状态0和状态1释放随机弹幕技能
                case 0:
                case 1:
                    randomProjectiles();
                    break;
                //状态2释放圆形弹幕技能
                case 2:
                    circleProjectiles();
                    break;
            }
            //重置技能计时器
            _cdTimer = 0;
        }

        //阶段转换
        if (_statusTimer >= _status1Time && _status == 0)
        {
            _status = 1;
            _bulletNum = _status1BulletNum;
            _projectilesRange = _status1Range;
        }
        if (_statusTimer >= _status2Time && _status == 1)
        {
            _status = 2;
            _bulletNum = _status2BulletNum;
            //移速和伤害加成增加150%
            CharacterAttribute characterAttribute = GetComponent<CharacterAttribute>();
            characterAttribute.setMoveSpeedAmplification(characterAttribute.getMoveSpeedAmplification() + 1.5f);
            characterAttribute.setAttackAmplification(characterAttribute.getAttackAmplification() + 1.5f);
        }
    }
}
