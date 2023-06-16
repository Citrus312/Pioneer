using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPageController : PersistentSingleton<MainPageController>
{
    private void Start()
    {
        UIRoot.Init();
        MainPageWindow.Instance.Open();
        JsonLoader.LoadAndDecodeLocalData();
        //JsonLoader.localData["isFirstPlaying"] = false;
        //JsonLoader.UpdateLocalData();
    }
}
