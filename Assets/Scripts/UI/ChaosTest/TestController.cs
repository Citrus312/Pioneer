using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TestController : MonoBehaviour
{
    public CinemachineVirtualCamera CM;
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
        if (JsonLoader.propPool.Count == 0)
        {
            JsonLoader.LoadAndDecodePropConfig();
        }
        //Debug.Log($"weaponPool  {JsonLoader.weaponPool.Count}");
        //Debug.Log($"rolePool  {JsonLoader.rolePool.Count}");
        //Debug.Log($"masterPool  {JsonLoader.monsterPool.Count}");
        for (int i = 0; i < 0; i++)
        {
            GameController.getInstance().getGameData()._weaponList.Add(55);
        }
        //GameController.getInstance().getGameData()._weaponList.Add(1);
        GameController.getInstance().getGameData()._playerID = 0;
        GameController.getInstance().initBattleScene();
        GameController.getInstance().getGameData()._difficulty = 4;
        GameController.getInstance().getGameData()._wave = 19;
        GameController.getInstance().getGameData()._scene = "Purple";
        //Debug.Log(GameController.getInstance().getGameData()._weaponList.Count);
        //Debug.Log(JsonLoader.weaponPool.Count);
        CM.Follow = GameController.getInstance().getPlayer().transform;
        GameController.getInstance().getGameData()._attr = JsonLoader.rolePool[0];
        GameController.getInstance().getPlayer().GetComponent<CharacterAttribute>().setAllPlayerAttribute(GameController.getInstance().getGameData()._attr);
        //GameController.getInstance().ModifyProp(4, 100);
        //GameController.getInstance().getPlayer().GetComponent<CharacterAttribute>().propModifyAttribute(4, 100);
        //GameController.getInstance().getPlayer().GetComponent<CharacterAttribute>().setRangedDamage(20);
        GameController.getInstance().getPlayer().GetComponent<CharacterAttribute>().setDodgeRate(100);
        //Debug.Log($"normal {GameController.getInstance().getGameData()._propCountPerQuality[0]}");
        //Debug.Log($"senior {GameController.getInstance().getGameData()._propCountPerQuality[1]}");
        //Debug.Log($"elite {GameController.getInstance().getGameData()._propCountPerQuality[2]}");
        //Debug.Log($"legendary {GameController.getInstance().getGameData()._propCountPerQuality[3]}");
        GameController.getInstance().waveStart();
    }
}
