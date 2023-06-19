using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(MeleeMonsterHit))]
public class Boss1Controller : AIController
{
    //是否正在释放冲刺技能
    public bool _isDashing = false;

    protected new void Awake()
    {
        /*
            Controller类的Awake
        */
        _transform = GetComponent<Transform>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _characterAttribute = GetComponent<CharacterAttribute>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();

        //设置rigidbody2D的参数
        _rigidbody2D.drag = 100;
        _rigidbody2D.gravityScale = 0;
        _rigidbody2D.freezeRotation = true;

        /*
            AIController类的Awake
        */
        //设置碰撞体大小和位置
        _capsuleCollider2D.size = new Vector2(0.2f, 0.2f);
        _capsuleCollider2D.offset = new Vector2(0, -0.05f);

        //设置enemy标签
        gameObject.tag = "Enemy";
    }

    // Update is called once per frame
    void Update()
    {
        //如果不在冲刺则移动
        if (!_isDashing)
            move(getMoveDirection());
    }
}
