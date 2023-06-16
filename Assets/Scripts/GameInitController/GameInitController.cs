using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitController : MonoBehaviour
{
    private static GameInitController _instance;

    public GameObject _player;

    public GameObject _objectPool;

    public GameObject _generator;

    public GameInitController getInstance()
    {
        return _instance;
    }

    public GameInitController()
    {
        _instance = this;
        // 初始化游戏全局变量
        
        // 初始化游戏配置
        
    }

    public void initGame()
    {
        // 加载资源
        
        // 初始化场景
        
        // 初始化玩家
        initPlayer();
        // 设置游戏状态
        
    }

    private void loadResources()
    {
        // 加载场景资源
        
        // 加载玩家资源
        
        // 加载游戏配置资源
        
    }

    private void initScene()
    {
        // 初始化场景对象
        
    }

    public void initBattleScene()
    {
        Instantiate(_objectPool, Vector3.zero, Quaternion.identity);
        Instantiate(_generator, Vector3.zero, Quaternion.identity);
    }

    public bool initPlayer()
    {
        // 初始化玩家对象
        if(_player != null)
        {
            Instantiate(_player, Vector3.zero, Quaternion.identity);
            return true;
        }
        Debug.Log("Player is null!");
        return false;
    }
    void Start()
    {
        
    }
}
