using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class AIController : Controller
{
    //TODO: 后期修改成向场景询问玩家位置
    //玩家位置
    public GameObject _player;

    protected new void Awake()
    {
        //碰撞体
        CapsuleCollider2D _capsuleCollider2D;

        _transform = GetComponent<Transform>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _characterAttribute = GetComponent<CharacterAttribute>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();

        //设置rigidbody2D的参数
        _rigidbody2D.drag = 100;
        _rigidbody2D.gravityScale = 0;
        _rigidbody2D.freezeRotation = true;

        //设置碰撞体大小和位置
        _capsuleCollider2D.size = new Vector2(0.2f, 0.2f);
        _capsuleCollider2D.offset = new Vector2(0, -0.05f);

        //设置enemy标签
        gameObject.tag = "Enemy";
    }

    //计算怪物移动方向
    protected Direction getMoveDirection()
    {
        //TODO: 后期修改成向场景询问玩家位置
        Vector3 playerPos = _player.transform.position;

        Vector3 aiPos = _transform.position;
        Direction moveDirection = new Direction((playerPos - aiPos).x, (playerPos - aiPos).y);
        return moveDirection;
    }

    // Update is called once per frame
    void Update()
    {
        // move(getMoveDirection());
    }
}
