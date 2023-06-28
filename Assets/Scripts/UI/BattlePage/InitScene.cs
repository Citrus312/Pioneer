using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("weapon" + GameController.getInstance().getGameData()._weaponList.Count);
        GameController.getInstance().initBattleScene();
        GameController.getInstance().getPlayer().GetComponent<CharacterAttribute>().setAllPlayerAttribute(GameController.getInstance().getGameData()._attr);
        GameController.getInstance().waveStart();
    }
}
