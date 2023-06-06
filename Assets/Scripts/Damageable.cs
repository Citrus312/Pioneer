using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public class DamageableEvent : UnityEvent { }
    public float currentHealth { get; protected set; }
    [SerializeField]
    public DamageableEvent onDeath;

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            onDeath.Invoke();
        }
    }
}
