using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(MeleeMonsterHit))]
public class AIController : Controller
{
    //击退时间
    [SerializeField] protected float beatBackTime = 0.1f;
    //击退速度
    [SerializeField] protected float beatBackTimeSpeed = 5.0f;

    //击中怪物，怪物开始击退
    public void OnHit(Vector2 direction)
    {
        StartCoroutine(beatBack(direction));
    }

    //怪物被击退
    private IEnumerator beatBack(Vector2 direction)
    {
        //计时器
        float timer = 0;
        while (timer < beatBackTime)
        {
            timer += Time.deltaTime;
            transform.position += new Vector3(direction.x, direction.y, 0) * Time.deltaTime * beatBackTimeSpeed;
            yield return null;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        //设置碰撞体大小和位置
        // _capsuleCollider2D.size = new Vector2(0.2f, 0.2f);
        // _capsuleCollider2D.offset = new Vector2(0, -0.05f);

        //设置enemy标签
        gameObject.tag = "Enemy";

        //设置击退参数
        // beatBackTime = 0.05f;
        // beatBackTimeSpeed = 30.0f;
    }

    //计算怪物移动方向
    protected Vector2 getMoveDirection()
    {
        //向场景询问玩家位置
        Vector3 playerPos = GameController.getInstance().getPlayer().transform.position;

        Vector3 aiPos = _transform.position;
        Vector2 moveDirection = new Vector2((playerPos - aiPos).x, (playerPos - aiPos).y).normalized;
        return moveDirection;
    }

    // Update is called once per frame
    void Update()
    {
        move(getMoveDirection());
    }

    public override void OnDie()
    {
        // 掉落物品
        CharacterAttribute monsterAttribute = GetComponent<CharacterAttribute>();
        DropItemGenerator.getInstance().dropItem(gameObject.transform.position, (int)monsterAttribute.getLootCount(), monsterAttribute.getDropRate());
        base.OnDie();
    }
}
