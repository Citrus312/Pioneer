using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//一个临时的控制器，用于调试，后续可合并入GameController
public class UIController : PersistentSingleton<UIController>
{
    private void Start()
    {
        UIRoot.Init();
        MainPageWindow.Instance.Open();
        JsonLoader.LoadAndDecodeGameData();
        JsonLoader.LoadAndDecodePropConfig();
        JsonLoader.LoadAndDecodeWeaponConfig();
        //Debug.Log(JsonLoader.propPool[0].getPropName());
        //Debug.Log(JsonLoader.weaponPool[0].getWeaponName());
        //Debug.Log(GameController.getInstance().getGameData()._scene);
        //GameController.getInstance().getGameData()._propList.Add(5);
        //JsonLoader.UpdateGameData();
        //GameController.getInstance().getGameData()._weaponList.Add(12);

        //GameoverWindow.Instance.Open();
    }
}
