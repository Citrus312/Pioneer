using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class InitScene : MonoBehaviour
{
    public CinemachineVirtualCamera CM;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("weapon" + GameController.getInstance().getGameData()._weaponList.Count);
        // 初始化场景
        GameController.getInstance().initBattleScene();
        // 挂载相机
        CM.Follow = GameController.getInstance().getPlayer().transform;
        // 属性赋值
        GameController.getInstance().getPlayer().GetComponent<CharacterAttribute>().setAllPlayerAttribute(GameController.getInstance().getGameData()._attr);
        GameController.getInstance().waveStart();
        GameoverWindow.Instance.Open();
    }
}
