using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class InitScene : MonoBehaviour
{
    public CinemachineVirtualCamera CM;
    // Start is called before the first frame update
    void Awake()
    {
        // 初始化场景
        GameController.getInstance().initBattleScene();
        // 挂载相机
        CM.Follow = GameController.getInstance().getPlayer().transform;
        // 属性赋值
        GameController.getInstance().getPlayer().GetComponent<CharacterAttribute>().setAllPlayerAttribute(GameController.getInstance().getGameData()._attr);
        GameObject manager = new GameObject("StoreManager");
        manager.AddComponent<gameProcessController>();
        gameProcessController.Instance.gameObject.SetActive(true);
        gameProcessController.Instance.Init();
        GameController.getInstance().waveStart();
    }
}