using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (JsonLoader.weaponPool.Count == 0)
        {
            JsonLoader.LoadAndDecodeWeaponConfig();
        }
        if (JsonLoader.rolePool.Count == 0)
        {
            JsonLoader.LoadAndDecodeRoleConfig();
        }
        if (JsonLoader.monsterPool.Count == 0)
        {
            JsonLoader.LoadAndDecodeMonsterConfig();
        }
        //Debug.Log($"weaponPool  {JsonLoader.weaponPool.Count}");
        //Debug.Log($"rolePool  {JsonLoader.rolePool.Count}");
        //Debug.Log($"masterPool  {JsonLoader.monsterPool.Count}");
        GameController.getInstance().getGameData()._weaponList.Add(12);
        GameController.getInstance().getGameData()._playerID = 1;
        GameController.getInstance().getGameData()._difficulty = 1;
        GameController.getInstance().getGameData()._wave = 1;
        //Debug.Log(GameController.getInstance().getGameData()._weaponList.Count);
        //Debug.Log(JsonLoader.weaponPool.Count);
        GameController.getInstance().initBattleScene();
        GameController.getInstance().getGameData()._attr = JsonLoader.rolePool[0];
        GameController.getInstance().getPlayer().GetComponent<CharacterAttribute>().setAllPlayerAttribute(GameController.getInstance().getGameData()._attr);
        GameController.getInstance().waveStart();
    }
}
