using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPageController : PersistentSingleton<MainPageController>
{
    private void Start()
    {
        UIRoot.Init();
        MainPageWindow.Instance.Open();
        //JsonLoader.LoadAndDecodeGameData();
        //List<int> list = JsonLoader.gameData["propList"];
        //list.Add(5);
        //JsonLoader.gameData["propList"] = list;
        //JsonLoader.UpdateGameData();
    }
}
