using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    protected float _minDistance = 0.2f;
    protected float _speed = 10.0f;
    public string _prefabPath;
    protected void OnTriggerEnter2D(Collider2D collider2D)
    {
        //判断是否为角色
        if (collider2D.gameObject.tag == "Player")
        {
            StartCoroutine(MoveToPlayer());
        }
    }

    IEnumerator MoveToPlayer()
    {
        GameObject _player = GameController.getInstance().getPlayer();

        float d = Vector2.Distance(transform.position, _player.transform.position);
        while (d > _minDistance)
        {
            transform.Translate((_player.transform.position - transform.position).normalized * _speed * Time.deltaTime, Space.World);
            d = Vector2.Distance(transform.position, _player.transform.position);
            yield return null;
        }
        OnDie();
        yield return null;
    }

    protected virtual void OnDie()
    {
        removeFromPool();
    }

    void removeFromPool()
    {
        // 移除
        ObjectPool.getInstance().remove(_prefabPath, gameObject);
        // 移除倒影
        GetComponent<WaterShadow>().removeWaterShadow();
    }
}
