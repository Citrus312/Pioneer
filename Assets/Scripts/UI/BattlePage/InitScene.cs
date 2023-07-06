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
        Debug.Log("InitScene Awake");
        GameObject.Find("Manager").GetComponent<AudioSource>().clip = GameController.getInstance().battleMusic;
        GameObject.Find("Manager").GetComponent<AudioSource>().Play();
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
        textController.Instance.Start();
        GameController.getInstance().waveStart();
    }
}