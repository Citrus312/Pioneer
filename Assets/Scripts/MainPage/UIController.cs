using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : PersistentSingleton<UIController>
{
    private void Start()
    {
        UIRoot.Init();
        MainPageWindow.Instance.Open();
        JsonLoader.LoadAndDecodeGameData();
        //GameController.getInstance().getGameData()._propList.Add(5);
        //JsonLoader.UpdateGameData();
    }
}
