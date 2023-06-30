using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameController.getInstance().initBattleScene();
        GameController.getInstance().getPlayer().GetComponent<CharacterAttribute>().setAllPlayerAttribute(GameController.getInstance().getGameData()._attr);
        //GameoverWindow.Instance.Open();
        GameController.getInstance().waveStart();
    }
}
