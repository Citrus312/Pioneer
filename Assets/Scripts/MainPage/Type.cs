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
    propBagWindow,
    upgradeWindow,
    weaponBagWindow,
    propertyWindow,
    roleStateWindow,
    storeWindow,
    titleWindow,
    countDownTimerWindow,
    PauseWindow,
    TalentTreeWindow
}



//窗口所在场景类型的枚举
public enum SceneType
{
    None,
    Battle,
    MainPage,
    Select,
    gameProcess,
    Pause,
    TalentTree

}
