using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentTreeController : MonoBehaviour
{
    private void Start()
    {
        UIRoot.Init();
        TalentTreeWindow.Instance.Open();

    }
}
