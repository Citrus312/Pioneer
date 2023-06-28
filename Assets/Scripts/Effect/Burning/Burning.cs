using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burning : ExtraEffect
{
    // 燃烧伤害
    float _burningDamage;
    // 燃烧间隔
    float _interval;
    private void Awake() {
        _burningDamage = 1.0f;
        _interval = 1.0f;
        _duration = 5.0f;
    }

    protected override IEnumerator effect()
    {
        _character = transform.parent.gameObject;
        Damageable dm = _character.GetComponent<Damageable>();
        if(dm)
        {
            // 总时长
            float remainTime = _duration;
            while(remainTime > 0)
            {
                // 每隔一段时间就扣一次血
                dm.TakeDamage(_burningDamage);
                remainTime -= _interval;
                yield return new WaitForSeconds(_interval);
            }
        }
        removeFromParent();
    }
}
