using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deceleration : ExtraEffect
{
    // 减速率
    float _decRate;
    float rawAmp;

    private void Awake()
    {
        _duration = 2.0f;
        _decRate = 50f;
    }

    protected override IEnumerator effect()
    {
        yield return null;
        _character = transform.parent.gameObject;
        // 冰冻效果
        Material m = _character.GetComponent<SpriteRenderer>().material;
        m.SetFloat("_Progress", 0.5f);
        rawAmp = _character.GetComponent<CharacterAttribute>().getMoveSpeedAmplification();
        _character.GetComponent<CharacterAttribute>().setMoveSpeedAmplification(rawAmp + (-1 * _decRate));

        yield return new WaitForSeconds(_duration);

        m.SetFloat("_Progress", 0f);
        removeFromParent();
    }

    public override void removeFromParent()
    {
        if (_character != null)
        {
            // 还原
            _character.GetComponent<CharacterAttribute>().setMoveSpeedAmplification(rawAmp);
        }
        base.removeFromParent();
    }
}
