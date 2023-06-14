using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    //无敌时间
    protected float _invulnerabilityTime;

    //尝试伤害玩家，判断玩家是否处于无敌时间
    public bool tryDamage()
    {
        //如果无敌时间已过则允许伤害并重置无敌时间
        if (Time.time > _invulnerabilityTime)
        {
            _invulnerabilityTime = Time.time + _characterAttribute.getInvulnerabilityTime();
            return true;
        }
        //否则拒绝伤害
        else
        {
            return false;
        }
    }

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
            PlayerController类的Awake
        */
        //设置碰撞体大小和位置
        _capsuleCollider2D.size = new Vector2(0.2f, 0.2f);
        _capsuleCollider2D.offset = new Vector2(0, -0.05f);
        _capsuleCollider2D.isTrigger = true;

        //设置player标签
        gameObject.tag = "Player";

        //设置无敌时间为当前
        _invulnerabilityTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        //根据键盘输入进行移动
        move(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized);
    }
}
