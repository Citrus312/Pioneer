using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour
{
    private static GameController _instance;

    private GameData _gameData = new GameData();

    public GameObject _playerPrefab;
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

    private void Start()
    {
        JsonLoader.LoadAndDecodeGameData();
        JsonLoader.LoadAndDecodeMonsterConfig();
        JsonLoader.LoadAndDecodePropConfig();
        JsonLoader.LoadAndDecodeRoleConfig();
        JsonLoader.LoadAndDecodeWeaponConfig();
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
        _gameData._wave = 1;
    }

    public bool initPlayer()
    {
        // 初始化玩家对象
        if (_playerPrefab != null)
        {
            _player = Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity);
            // _player = ObjectPool.getInstance().get(_playerPrefab);
            _player.GetComponent<Damageable>()._prefabPath = null;
            _player.transform.position = Vector3.zero;
        }
        else
        {
            Debug.Log("PlayerPrefab is null!");
            return false;
        }

        //为玩家对象添加武器
        for (int i = 0; i < _gameData._weaponList.Count; i++)
        {
            int index = _gameData._weaponList[i];
            WeaponAttribute weaponAttribute = JsonLoader.weaponPool[index];
            GameObject weapon = ObjectPool.getInstance().get(weaponAttribute.getWeaponPrefabPath());
            weapon.transform.GetChild(0).GetComponent<WeaponAttribute>().setAllAttribute(weaponAttribute);
            weapon.transform.SetParent(_player.transform, false);
            _player.GetComponent<WeaponManager>().addWeapon(weapon);
        }

        return true;
    }

    //波次开始
    public void waveStart()
    {
        _player.SetActive(true);
        MonsterInfoCalcu.Instance.Cal();
        for (int i = 0; i < MonsterInfoCalcu.Instance.genMonsterCount.Count; i++)
        {
            //生成的数量
            int num = MonsterInfoCalcu.Instance.genMonsterCount[i];
            Debug.Log(num);
            //生成的怪物属性
            CharacterAttribute characterAttribute = MonsterInfoCalcu.Instance.genMonsterAttr[i];
            StartCoroutine(generateMonster(characterAttribute, num));
        }
    }

    //波次结束
    public void waveEnd()
    {
        //停止所有生成怪物的协程
        StopAllCoroutines();
        //回收对象池生成的所有物体
        ObjectPool.getInstance().removeAll();
        //将玩家对象取消激活
        _player.SetActive(false);
    }

    //生成怪物
    private IEnumerator generateMonster(CharacterAttribute characterAttribute, int num)
    {
        string monsterPrefabPath = characterAttribute.getMonsterPrefabPath();
        //生成的间隔
        float interval = characterAttribute.getInterval();
        while (true)
        {
            yield return new WaitForSeconds(interval);
            MonsterGenerator.getInstance().beginGenerate(monsterPrefabPath, num, characterAttribute);
        }
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

    private void AppendPropList(int val)
    {
        int index = _gameData._propList.FindIndex(item => item.Equals(val));
        if (index == -1)
        {
            _gameData._propList.Add(val);
        }
    }

    private void ModifyPropCount(int prop, int modifyCount)
    {
        int index = _gameData._propList.FindIndex(item => item.Equals(prop));
        if (index >= _gameData._propCount.Count)
        {
            _gameData._propCount.Add(modifyCount);
        }
        else
        {
            if (_gameData._propCount[index] + modifyCount < 0)
            {
                Debug.LogError($"_propList[{index}]对应的道具数量被修改为负数");
            }
            else
            {
                _gameData._propCount[index] += modifyCount;
            }
        }
    }

    public void ModifyProp(int prop, int count)
    {
        AppendPropList(prop);
        ModifyPropCount(prop, count);
    }
}
