using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public class DamageableEvent : UnityEvent { }
    public float currentHealth { get; protected set; }
    [SerializeField]
    public DamageableEvent onDeath = new DamageableEvent();

    protected void Awake()
    {
        onDeath.AddListener(destroy);
        //该赋值仅作测试用
        currentHealth = 20;
    }

    private void destroy()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            onDeath.Invoke();
        }
    }
}
