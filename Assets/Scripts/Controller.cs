using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CharacterAttribute))]
public class Controller : MonoBehaviour
{
    protected Transform _transform;
    //2D刚体
    protected Rigidbody2D _rigidbody2D;
    //角色属性
    protected CharacterAttribute _characterAttribute;

    //动画
    public Animator _animator;

    //方向
    protected struct Direction
    {
        public float x, y;
        public Direction(float directionX, float directionY)
        {
            //如果x和y方向上都为0
            if (directionX == 0 && directionY == 0)
            {
                x = 0;
                y = 0;
            }
            //如果x方向上为0
            else if (directionX == 0)
            {
                x = 0;
                y = 1;
            }
            //如果y方向上为0
            else if (directionY == 0)
            {
                x = 1;
                y = 0;
            }
            else
            {
                float a = directionX / directionY;
                y = MathF.Sqrt(1 / (a * a + 1));
                x = MathF.Abs(a) * y;

            }
            if (directionX < 0)
                x = -x;
            if (directionY < 0)
                y = -y;
        }
    }

    protected void move(Direction direction)
    {
        _animator.SetBool("Moving", !(direction.x == 0 && direction.y == 0));
        float moveSpeed = _characterAttribute.getMoveSpeed();
        if (direction.x < 0)
        {
            Vector3 rotation = _transform.eulerAngles;
            rotation.y = -180;
            _transform.eulerAngles = rotation;
        }
        if (direction.x > 0)
        {
            Vector3 rotation = _transform.eulerAngles;
            rotation.y = 0;
            _transform.eulerAngles = rotation;
        }
        _rigidbody2D.AddForce(new Vector2(direction.x, direction.y), ForceMode2D.Impulse);
    }

    protected void Awake()
    {
        _transform = GetComponent<Transform>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _characterAttribute = GetComponent<CharacterAttribute>();
    }
}
