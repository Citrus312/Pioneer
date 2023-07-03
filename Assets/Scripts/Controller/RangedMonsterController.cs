using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedMonsterController : AIController
{
    //怪物进入冰面
    public override void inIceSurface()
    {
        base.inIceSurface();
        //如果与玩家距离近于攻击距离则远离玩家
        if (Vector2.Distance(transform.position, GameController.getInstance().getPlayer().transform.position) < GetComponent<WeaponAttribute>().getAttackRange() * 0.02f)
            skatingDirection = -skatingDirection;
    }

    protected override void move(Vector2 direction)
    {
        _animator.SetBool("Moving", !(direction.x == 0 && direction.y == 0));
        float moveSpeed = _characterAttribute.getMoveSpeed();
        if (isSkating > 0)
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

    // Update is called once per frame
    void Update()
    {
        if (_dontMove)
            return;
        //如果处于滑行状态则直接向滑行方向移动
        if (isSkating > 0)
            move(skatingDirection);
        else
        {
            Vector2 direction = getMoveDirection();
            //如果与玩家距离近于攻击距离则远离玩家
            if (Vector2.Distance(transform.position, GameController.getInstance().getPlayer().transform.position) < GetComponent<WeaponAttribute>().getAttackRange() * 0.02f)
                direction = -direction;
            move(direction);
        }
    }
}
