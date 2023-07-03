using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Damageable : MonoBehaviour
{
    public class DamageableEvent : UnityEvent { }
    //public float currentHealth { get; protected set; }
    [SerializeField]
    public DamageableEvent onDeath = new DamageableEvent();
    //受击闪烁的颜色
    public Color _onHitColor;
    //闪烁的持续时间
    public float _onHitTime;
    //预制体路径
    public string _prefabPath;

    protected void Awake()
    {

        onDeath.AddListener(die);
        //初始化受击闪烁颜色和持续时间
        _onHitColor = new Color(255.0f / 255.0f, 100.0f / 255.0f, 100.0f / 255.0f, 255.0f / 255.0f);
        _onHitTime = 0.2f;
    }

    //受击闪烁
    private IEnumerator OnHit()
    {
        // 镜头震动，判断角色
        if (gameObject.tag == "Player")
        {
            CameraShake._instance.startShake();
        }

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = _onHitColor;
        yield return new WaitForSeconds(_onHitTime);
        spriteRenderer.color = Color.white;
    }

    private void die()
    {
        GetComponent<Controller>().OnDie();
        // 死亡动画
        GetComponent<Animator>().SetBool("B_isAlive", false);
        Invoke("removeFromPool", 0.5f);
    }

    void removeFromPool()
    {
        // 回收
        if (_prefabPath != null)
            ObjectPool.getInstance().remove(_prefabPath, gameObject);
        // 回收倒影
        GetComponent<WaterShadow>().removeWaterShadow();
    }

    public void TakeDamage(float damage)
    {
        GetComponent<CharacterAttribute>().setCurrentHealth(GetComponent<CharacterAttribute>().getCurrentHealth() - damage);
        //currentHealth -= damage;
        //GetComponent<CharacterAttribute>().setCurrentHealth(currentHealth);
        StartCoroutine("OnHit");
        if (GetComponent<CharacterAttribute>().getCurrentHealth() <= 0)
        {
            onDeath.Invoke();
        }
    }
}
