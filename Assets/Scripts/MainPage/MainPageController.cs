using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPageController : PersistentSingleton<MainPageController>
{
    private void Start()
    {
        UIRoot.Init();
        MainPageWindow w =  new();
        w.Open();
    }
}
