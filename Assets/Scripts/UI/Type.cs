using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//窗口类型的枚举
public enum WindowType
{
    TipsWindow,
    MainPageWindow,
    SettingWindow,
    DifficultySelectWindow,
    RoleAndWeaponSelectWindow,
    PauseWindow,
    GameoverWindow
}
//窗口所在场景类型的枚举
public enum SceneType
{
    None,
    Battle,
    MainPage,
    Pause,
    Select
}
