using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Weapon : RangedMonsterHit
{
    //boss的阶段状态
    private int _status;
    //技能计时器
    private float _cdTimer;
    //状态转换计时器
    private float _statusTimer;
    //技能cd时间
    public float _cd;
    //状态转换时间
    public float _statusChangeTime;

    [Header("弹幕环绕技能参数")]
    //旋转中心点
    public Transform _rotateCenter;
    //子弹数量
    public int _bulletNum = 12;
    //旋转速度
    public float _rotateSpeed;
    //扩散速度
    public float _spreadSpeed;
    //每颗子弹最终位置距离旋转中心的半径
    public float[] _spreadRadius;
    //子弹
    private List<GameObject> _bullet = new List<GameObject>();

    [Header("冲刺技能参数")]
    //蓄力时长
    public float _delayTime;
    //冲刺时长
    public float _dashTime;

    [Header("散射技能参数")]
    //散射的所有方向
    public float[] _emitAngle;

    //弹幕环绕技能
    private IEnumerator surround()
    {
        /*
            初始化12个子弹球在一个位置上
            所有子弹球共同缓慢向外扩散
            每个子弹球到达了对应的位置后停下来
            在子弹球缓慢扩散的同时所有子弹球也会共同缓慢旋转
        */

        //初始化子弹
        for (int i = 0; i < _bulletNum; i++)
        {
            //实例化子弹并加入列表
            _bullet.Add(ObjectPool.getInstance().get(_bulletPrefab));
            //设置旋转中心
            _bullet[i].transform.SetParent(_rotateCenter);
            //设置子弹初始位置
            _bullet[i].transform.localPosition = new Vector3(0, 0, 0);
            //设置子弹参数
            _bullet[i].GetComponent<Bullet>().setup(gameObject, _bulletPrefab, "Player", _pierce);
        }

        while (true)
        {
            //旋转
            _rotateCenter.Rotate(Vector3.forward * Time.deltaTime * _rotateSpeed, Space.Self);

            //扩散
            for (int i = 0; i < _bulletNum; i++)
            {
                //如果当前与旋转中心的距离小于最终位置的半径则逐渐扩散
                if (Vector2.Distance(_bullet[i].transform.localPosition, Vector2.zero) < _spreadRadius[i])
                {
                    _bullet[i].transform.localPosition = _bullet[i].transform.localPosition + Vector3.up * Time.deltaTime * _spreadSpeed;
                }
            }
            yield return null;
        }
    }

    //冲刺技能
    private IEnumerator dash(Vector2 direction)
    {
        GetComponent<Animator>().SetTrigger("T_dash");
        //设置正在冲刺禁用boss的普通移动
        GetComponent<Boss1Controller>()._isDashing = true;

        //冲刺距离设置为攻击距离
        float attackDistance = _weaponAttribute.getAttackRange();
        //计时器
        float timer = 0;

        while (timer < _delayTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        //重置计时器
        timer = 0;

        //冲刺
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + (Vector3)direction * attackDistance;
        while (timer < _dashTime)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, endPos, timer / _dashTime);
            yield return null;
        }

        //冲刺完成设置未在冲刺
        GetComponent<Boss1Controller>()._isDashing = false;
    }

    //散射技能
    private void emit()
    {
        GetComponent<Animator>().SetTrigger("T_shoot");
        for (int i = 0; i < _emitAngle.Length; i++)
        {
            Vector2 shootDirection = new Vector2(Mathf.Cos(_emitAngle[i] * Mathf.Deg2Rad), Mathf.Sin(_emitAngle[i] * Mathf.Deg2Rad));
            shoot(shootDirection);
        }
    }

    protected new void Awake()
    {
        /*
            Weapon类的Awake
        */
        _weaponAttribute = GetComponent<WeaponAttribute>();
        _damager = GetComponent<Damager>();
        _nextAttackTime = Time.time;

        /*
            RangedMonsterHit类的Awake
        */
        _attachPoint = transform;
        _bulletPrefab = "Assets/Prefab/Bullet/monster_bullet.prefab";

        /*
            Boss1Weapon的Awake
        */
        _status = 0;
        _cdTimer = 0;
        _statusTimer = 0;
        _pierce = -1;
    }

    void Start()
    {
        //开始弹幕环绕
        StartCoroutine(surround());
    }

    // Update is called once per frame
    void Update()
    {
        _cdTimer += Time.deltaTime;
        _statusTimer += Time.deltaTime;
        Vector2 attackDirection = getAttackDirection("Player");
        //如果技能cd结束了且攻击范围内有目标则释放技能
        if (_cdTimer >= _cd && attackDirection != new Vector2(0, 0))
        {
            switch (_status)
            {
                //状态0释放冲刺技能
                case 0:
                    StartCoroutine(dash(attackDirection));
                    break;
                //状态1释放散射技能
                case 1:
                    emit();
                    break;
            }
            //重置技能计时器
            _cdTimer = 0;
        }

        //如果满足阶段转换条件则转换状态
        if (_statusTimer >= _statusChangeTime && _status == 0)
        {
            _status = 1;
        }
    }
}
