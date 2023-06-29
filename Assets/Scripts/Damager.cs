using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Damager : MonoBehaviour
{
    //���������� ���������ߵ����� ��������
    protected WeaponAttribute _weaponAttr;
    protected CharacterAttribute _ownerAttr;
    protected Weapon _weapon;
    //伤害显示预制体
    protected string _damageTextPrefab;

    void Awake()
    {
        _weapon = GetComponentInParent<Weapon>();
        _weaponAttr = _weapon.GetComponent<WeaponAttribute>();
        _ownerAttr = _weapon.GetComponentInParent<CharacterAttribute>();
        _damageTextPrefab = DamageText.getDamageTextPath();
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
        //�����ܻ�������˺�����
        float damageRedution;
        if (armorStrength >= 0)
        {
            damageRedution = armorStrength / (armorStrength + 15);
        }
        else
        {
            damageRedution = armorStrength * 0.02f;
        }
        //���㹥�����Ƿ񴥷�����
        float crit = Random.Range(0f, 1f);
        if (crit < criticalRate)
        {
            damage = weaponDamage * (1 - damageRedution) * criticalBonus;
        }
        else
        {
            damage = weaponDamage * (1 - damageRedution);
        }
        //����˺��ж�
        damage = damage > 1 ? damage : 1;

        //显示伤害
        //DamageText damageText = Instantiate(_damageTextPrefab, target.transform.position, Quaternion.identity).GetComponent<DamageText>();
        GameObject damageTextObj = ObjectPool.getInstance().get(_damageTextPrefab);
        damageTextObj.transform.position = target.transform.position + new Vector3(Random.Range(0, 0.2f), Random.Range(0, 0.2f), 0);
        DamageText damageText = damageTextObj.GetComponent<DamageText>();
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

        //Ӧ�ü�������˺�
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }
    }
}
