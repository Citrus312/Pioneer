using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    // 玩家是否首次游玩
    public bool _isFirstPlaying;

    // 角色类型
    public int _playerID;

    // 当前选择的地图
    public string _scene;

    // 当前选择的游戏难度
    public int _difficulty;

    // 各种品质道具的数量
    public int[] _propCountPerQuality = { 0, 0, 0, 0 };

    // 拥有的道具种类
    public List<int> _propList = new List<int>();

    // 拥有的每种道具对应的数量
    public List<int> _propCount = new List<int>();

    // 拥有的武器
    public List<int> _weaponList = new List<int>();

    // 进入游戏前暂存的玩家属性
    public CharacterAttribute _attr = new();

    // 当前波次
    public int _wave = 1;

    // 当前选择的关卡
    public int _level;

    // 货币
    public float _money = 0;

    // 角色等级
    public int _playerLevel=2;

    // 经验
    public float _exp;

    // 将数据转存为字典，方便JsonLoader保存数据
    public Dictionary<string, dynamic> Data2Dict()
    {
        Dictionary<string, dynamic> dict = new();
        dict.Add("isFirstPlaying", _isFirstPlaying);
        dict.Add("playerID", _playerID);
        dict.Add("scene", _scene);
        dict.Add("difficulty", _difficulty);
        dict.Add("propList", _propList);
        dict.Add("propCount", _propCount);
        dict.Add("weaponList", _weaponList);
        dict.Add("wave", _wave);
        dict.Add("level", _level);
        dict.Add("money", _money);
        dict.Add("playerLevel", _playerLevel);
        dict.Add("exp", _exp);
        return dict;
    }

    //重置游戏数据
    public void ResetGameData()
    {
        _playerID = 0;
        _propList = new List<int>();
        _propCount = new List<int>();
        _weaponList = new List<int>();
        _wave = 0;
        _level = 0;
        _money = 0;
        _playerLevel = 0;
        _exp = 0;
    }
}
