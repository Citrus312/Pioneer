using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CharacterAttribute))]
[RequireComponent(typeof(Damageable))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Controller : MonoBehaviour
{
    protected Transform _transform;
    //2D刚体
    protected Rigidbody2D _rigidbody2D;
    //角色属性
    protected CharacterAttribute _characterAttribute;
    //碰撞体
    protected CapsuleCollider2D _capsuleCollider2D;

    //动画
    public Animator _animator;

    //是否处于滑行状态
    protected bool isSkating = false;
    //滑行的方向
    protected Vector2 skatingDirection;
    //滑行状态的速度倍率
    public float skatingSpeedRatio = 2.0f;

    //进入冰面时进入滑行状态
    public virtual void inIceSurface()
    {
        isSkating = true;
    }

    //离开冰面时退出滑行状态
    public virtual void outIceSurface()
    {
        isSkating = false;
    }

    protected virtual void Awake()
    {
        _transform = GetComponent<Transform>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _characterAttribute = GetComponent<CharacterAttribute>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();

        //设置rigidbody2D的参数
        _rigidbody2D.drag = 100;
        _rigidbody2D.gravityScale = 0;
        _rigidbody2D.freezeRotation = true;
    }

    protected void move(Vector2 direction)
    {
        _animator.SetBool("Moving", !(direction.x == 0 && direction.y == 0));
        float moveSpeed = _characterAttribute.getMoveSpeed();
        if (isSkating)
            moveSpeed *= skatingSpeedRatio;
        //角色转向
        if (direction.x < 0)
        {
            // Vector3 rotation = _transform.eulerAngles;
            // rotation.y = -180;
            // _transform.eulerAngles = rotation;
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = true;
        }
        if (direction.x > 0)
        {
            // Vector3 rotation = _transform.eulerAngles;
            // rotation.y = 0;
            // _transform.eulerAngles = rotation;
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = false;
        }
        //角色移动
        // _rigidbody2D.AddForce(new Vector2(direction.x, direction.y), ForceMode2D.Impulse);
        transform.position += new Vector3(direction.x, direction.y, 0) * Time.deltaTime * moveSpeed;
    }

    //销毁character时执行，在子类重写
    public virtual void OnDie() { }
}
