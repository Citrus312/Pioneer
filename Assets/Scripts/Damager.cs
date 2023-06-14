using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Damager : MonoBehaviour
{
    protected WeaponAttribute _weaponAttr;
    protected CharacterAttribute _ownerAttr;
    protected Weapon _weapon;
    //伤害显示预制体
    protected GameObject _damageTextPrefab;

    void Awake()
    {
        _weapon = GetComponentInParent<Weapon>();
        _weaponAttr = _weapon.GetComponent<WeaponAttribute>();
        _ownerAttr = _weapon.GetComponentInParent<CharacterAttribute>();
        _damageTextPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/DamageText.prefab");
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

        //显示伤害
        DamageText damageText = Instantiate(_damageTextPrefab, target.transform.position, Quaternion.identity).GetComponent<DamageText>();
        if (target.tag == "Player")
        {
            damageText.setup(DamageText.TextType.PlayerHurt, (int)damage);
        }
        else if (target.tag == "Enemy")
        {
            //暴击
            if (crit < criticalRate)
            {
                damageText.setup(DamageText.TextType.CritDamage, (int)damage);
            }
            //不暴击
            else
            {
                damageText.setup(DamageText.TextType.CommonDamage, (int)damage);
            }
        }

        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }
    }
}
