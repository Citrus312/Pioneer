using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    protected int _val = 1;
    protected int _exp = 1;
    protected float _minDistance = 0.2f;
    public float _speed = 5.0f;
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
        while(d > _minDistance)
        {
            transform.Translate((_player.transform.position - transform.position) * _speed * Time.deltaTime);
            d = Vector2.Distance(transform.position, _player.transform.position);
            yield return new WaitForSeconds(0.01f);
        }
        OnDie();
        yield return null;
    }

    void OnDie()
    {
        // 添加数据
        GameController.getInstance().updateMoney(_val);
        GameController.getInstance().addExp(_exp);
        ObjectPool.getInstance().remove(_prefabPath, gameObject);
    }
}
