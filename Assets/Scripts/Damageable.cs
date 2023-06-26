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

    protected void Awake()
    {
        onDeath.AddListener(die);
        //该赋值仅作测试用
        //currentHealth = 20;
    }

    private void die()
    {
        //ObjectPool.getInstance().remove(,gameObject);
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        float currentHealth = GameController.getInstance().getPlayer().GetComponent<CharacterAttribute>().getCurrentHealth();
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            onDeath.Invoke();
        }
    }

    public void ResetCurrentHealth()
    {
        GameController.getInstance().getPlayer().GetComponent<CharacterAttribute>().setCurrentHealth(GameController.getInstance().getPlayer().GetComponent<CharacterAttribute>().getMaxHealth());
    }
}
