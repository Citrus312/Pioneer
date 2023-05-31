using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Controller : MonoBehaviour
{
    //角色对象
    public GameObject character;

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
        float moveSpeed = character.GetComponent<Attribute>().getMoveSpeed();
        Vector3 position = character.transform.position;
        position += new Vector3(direction.x * moveSpeed, direction.y * moveSpeed, 0);
        character.transform.position = position;
    }
}
