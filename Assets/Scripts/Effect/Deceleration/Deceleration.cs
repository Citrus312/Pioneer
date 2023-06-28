using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deceleration : ExtraEffect
{
    // 减速率
    float _decRate;

    private void Awake() {
        _duration = 2.0f;
        _decRate = 50f;
    }

    protected override IEnumerator effect()
    {
        _character = transform.parent.gameObject;
        float rawAmp = _character.GetComponent<CharacterAttribute>().getMoveSpeedAmplification();
        _character.GetComponent<CharacterAttribute>().setMoveSpeedAmplification(rawAmp + (-1 * _decRate));
        _character.GetComponent<CharacterAttribute>().setMoveSpeedAmplification(rawAmp);
        yield return new WaitForSeconds(_duration);
        removeFromParent();
    }
}
