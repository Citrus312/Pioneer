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
        // JsonLoader.LoadAndDecodeGameData();
        // JsonLoader.LoadAndDecodePropConfig();
        // JsonLoader.LoadAndDecodeWeaponConfig();
        //Debug.Log(JsonLoader.propPool[0].getPropName());
        //Debug.Log(JsonLoader.weaponPool[0].getWeaponName());
        //Debug.Log(GameController.getInstance().getGameData()._scene);
        //GameController.getInstance().ModifyProp(5, 2);
        //GameController.getInstance().ModifyProp(7, 3);
        //GameController.getInstance().ModifyProp(3, 1);

        //JsonLoader.UpdateGameData();
        //GameController.getInstance().getGameData()._weaponList.Add(14);
        //GameoverWindow.Instance.titleText = "胜利";
        //GameoverWindow.Instance.Open();
    }
}
