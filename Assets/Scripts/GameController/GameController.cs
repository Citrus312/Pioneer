using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour
{
    private static GameController _instance;

    private GameData _gameData = new GameData();

    public string _playerPrefab;
    private GameObject _player;

    public GameObject _objectPool;

    public GameObject _generator;

    public static GameController getInstance()
    {
        return _instance;
    }

    public GameController()
    {
        _instance = this;
    }

    //初始化战斗场景
    public void initBattleScene()
    {
        //初始化对象池
        Instantiate(_objectPool, Vector3.zero, Quaternion.identity);
        //初始化生成器
        Instantiate(_generator, Vector3.zero, Quaternion.identity);
        //初始化角色
        initPlayer();
    }

    public bool initPlayer()
    {
        // 初始化玩家对象
        if (_playerPrefab != null)
        {
            // _player = Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity);
            _player = ObjectPool.getInstance().get(_playerPrefab);
            _player.GetComponent<Damageable>()._prefabPath = _playerPrefab;
            _player.transform.position = Vector3.zero;
            return true;
        }
        Debug.Log("PlayerPrefab is null!");
        return false;
    }

    public GameObject getPlayer()
    {
        return _player;
    }

    public GameData getGameData()
    {
        return _gameData;
    }

    // 加钱或扣钱
    public bool updateMoney(int num)
    {
        if ((_gameData._money + num) >= 0)
        {
            _gameData._money += num;
            return true;
        }
        else return false;
    }

    // 加经验
    public void addExp(int num)
    {
        _gameData._exp += num;
    }
}
