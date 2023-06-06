using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    protected WeaponAttribute _weaponAttr;
    protected CharacterAttribute _ownerAttr;
    protected Weapon _weapon;

    void Start()
    {
        _weapon = GetComponentInParent<Weapon>();
        _weaponAttr = _weapon.GetComponent<WeaponAttribute>();
        _ownerAttr = _weapon.GetComponentInParent<CharacterAttribute>();
    }

    public void Damage(Collider2D targetColl)
    {
        GameObject target = targetColl.gameObject;
        Damageable damageable = target.GetComponent<Damageable>();
        CharacterAttribute targetAttr = target.GetComponent<CharacterAttribute>();
        float damage;

        float weaponDamage = _weaponAttr.getWeaponDamage();
        float criticalBonus = _weaponAttr.getCriticalBonus();
        float criticalRate = _weaponAttr.getCriticalRate();
        float armorStrength = targetAttr.getArmorStrength();
        
        float damageRedution;
        if (armorStrength >= 0)
        {
            damageRedution = armorStrength / (armorStrength + 15);
        }
        else
        {
            damageRedution = armorStrength * 0.02f;
        }

        float crit = Random.Range(0f, 1f);
        if (crit < criticalRate)
        {
            damage = weaponDamage * (1 - damageRedution) * criticalBonus;
        }
        else
        {
            damage = weaponDamage * (1 - damageRedution);
        }
        damage = damage > 1 ? damage : 1;
        
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }
    }
}
