using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController _instance;

    private GameData _gameData = new GameData();

    public GameObject _playerPrefab;
    public GameObject _player;

    public GameObject _objectPool;

    public GameObject _generator;

    public static GameController getInstance()
    {
        return _instance;
    }

    public GameController()
    {
        _instance = this;
        // 初始化游戏全局变量

        // 初始化游戏配置

    }

    public void initGame()
    {
        // 加载资源

        // 初始化玩家
        initPlayer();
        // 初始化场景
        initBattleScene();
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

        initPlayerPos();
        // 生成器初始化
        Instantiate(_generator, Vector3.zero, Quaternion.identity);
    }

    public bool initPlayer()
    {
        // 初始化玩家对象
        if (_playerPrefab != null)
        {
            _player = Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity);
            // 初始化属性
            //_player.GetComponent<CharacterAttribute>().initAttribute(_gameData._playerID);
            return true;
        }
        Debug.Log("PlayerPrefab is null!");
        return false;
    }

    private void initPlayerPos()
    {
        _player.transform.position = Vector3.zero;
    }

    public void onBattleEnd()
    {

        // 把player的attribute等等属性存入GameData

        // 并保存到本地文件
        saveGameData();
    }

    private void saveGameData()
    {
        // 

    }

    public GameObject getPlayer()
    {
        return _player;
    }

    public GameData getGameData()
    {
        return _gameData;
    }

    void Awake()
    {
        initGame();
    }
}
