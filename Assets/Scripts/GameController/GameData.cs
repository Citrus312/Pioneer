using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    // 角色类型
    public int _playerID;

    // 拥有的道具
    public Dictionary<int, int> _propList;

    // 拥有的武器
    public List<int> _weaponList;

    // 当前波次
    public int _wave;

    // 当前选择的关卡
    public int _level;

    // 货币
    public int _money;

    // 角色等级
    public int _playerLevel;

    // 经验
    public int _exp;

}
