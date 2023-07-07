using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor;

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

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        if (JsonLoader.monsterPool.Count == 0)
        {
            JsonLoader.LoadAndDecodeMonsterConfig();
        }
        if (JsonLoader.propPool.Count == 0)
        {
            JsonLoader.LoadAndDecodePropConfig();
        }
        if (JsonLoader.rolePool.Count == 0)
        {
            JsonLoader.LoadAndDecodeRoleConfig();
        }
        if (JsonLoader.weaponPool.Count == 0)
        {
            JsonLoader.LoadAndDecodeWeaponConfig();
        }
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
        switch (_gameData._playerID)
        {
            case 0:
                _playerPrefab = "Assets/Prefab/Player.prefab";
                break;
            case 1:
                _playerPrefab = "Assets/Prefab/Player/Player_2.prefab";
                break;
            case 2:
                _playerPrefab = "Assets/Prefab/Player/Player_3.prefab";
                break;
        }
        // 初始化玩家对象
        if (_playerPrefab != null)
        {
            //Debug.Log(_playerPrefab);
            // _player = Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity);
            _player = ObjectPool.getInstance().get(_playerPrefab);
            _player.GetComponent<Damageable>()._prefabPath = _playerPrefab;
            _player.transform.position = Vector3.zero;
        }
        else
        {
            Debug.Log("PlayerPrefab is null!");
            return false;
        }
        ObjectPool.getInstance().remove(_playerPrefab, _player);
        return true;
    }
    //波次开始
    public void waveStart()
    {
        //初始化障碍物
        RandomScene.getInstance().randomGenerateScene();
        // _player.SetActive(true);
        _player = ObjectPool.getInstance().get(_playerPrefab);
        // 重置角色
        _player.transform.position = Vector3.zero;
        _player.GetComponent<Damageable>().startRecovery();
        _player.GetComponent<CharacterAttribute>().setCurrentHealth(_player.GetComponent<CharacterAttribute>().getMaxHealth());
        JsonLoader.UpdateGameData();
        // 加武器
        for (int i = 0; i < _gameData._weaponList.Count; i++)
        {
            int index = _gameData._weaponList[i];
            WeaponAttribute weaponAttribute = JsonLoader.weaponPool[index];
            GameObject weapon = AssetDatabase.LoadAssetAtPath<GameObject>(weaponAttribute.getWeaponPrefabPath());
            weapon = Instantiate(weapon);
            //GameObject weapon = ObjectPool.getInstance().get(weaponAttribute.getWeaponPrefabPath());
            weapon.transform.GetChild(0).GetComponent<WeaponAttribute>().setAllAttribute(weaponAttribute);
            weapon.transform.GetChild(0).GetComponent<WeaponAttribute>().setOwnerAttr(_instance._player.GetComponent<CharacterAttribute>());
            weapon.transform.SetParent(_player.transform, false);
            _player.GetComponent<WeaponManager>().addWeapon(weapon);
        }
        // 怪物
        MonsterInfoCalcu.Instance.Cal();
        // Debug.Log("genMonstreCount.Count=" + MonsterInfoCalcu.Instance.genMonsterCount.Count);
        for (int i = 0; i < MonsterInfoCalcu.Instance.genMonsterCount.Count; i++)
        {
            //生成的数量
            int num = MonsterInfoCalcu.Instance.genMonsterCount[i];
            // Debug.Log("monster num=" + num);
            //生成的怪物属性
            CharacterAttribute characterAttribute = MonsterInfoCalcu.Instance.genMonsterAttr[i];
            if (characterAttribute.getID() == 10 || characterAttribute.getID() == 11)
            {
                generateBoss(characterAttribute);
            }          
            StartCoroutine(generateMonster(characterAttribute, num));
        }
    }

    //波次结束
    public void waveEnd()
    {
        //停止所有生成怪物的协程
        StopAllCoroutines();
        MonsterGenerator.getInstance().stopGenerate();
        
        _player.GetComponent<Damageable>().stopIEnumerator();

        // 删除武器
        for (int i = 0; i < _gameData._weaponList.Count; i++)
        {
            DestroyImmediate(_player.transform.GetChild(0).gameObject);
        }
        _player.GetComponent<WeaponManager>().RemoveAllWeapon();
        _instance.updateMoney((int)Mathf.Ceil(_player.GetComponent<CharacterAttribute>().getCollectEfficiency()));
        _player.GetComponent<CharacterAttribute>().setCollectEfficiency(Mathf.Ceil(_player.GetComponent<CharacterAttribute>().getCollectEfficiency() * 1.05f));
        //回收对象池生成的所有物体
        ObjectPool.getInstance().removeAll();
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

    //生成boss
    private void generateBoss(CharacterAttribute characterAttribute)
    {
        string monsterPrefabPath = characterAttribute.getMonsterPrefabPath();
        MonsterGenerator.getInstance().beginGenerate(monsterPrefabPath, 1, characterAttribute);
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
        if ((_instance._gameData._money + num) >= 0)
        {
            _instance._gameData._money += num;
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

        //_player.GetComponent<CharacterAttribute>().propModifyAttribute(prop, count);
    }
}
