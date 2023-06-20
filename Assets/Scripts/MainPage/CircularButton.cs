using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircularButton : MonoBehaviour
{
    private void Start()
    {
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(() => { OnBtn(btn); });
    }

    public void OnBtn(Button btn)
    {
        string str = btn.name.Trim('B', 't', 'n');
        int index = int.Parse(str);
        DifficultySelectWindow.Instance.OpenAndMove(434 + (index - 1) * 8, 248.75f);
        switch (str)
        {
            case "1":
                GameController.getInstance().getGameData()._scene = "BattleScene1";
                break;
            case "2":
                GameController.getInstance().getGameData()._scene = "BattleScene1";
                break;
            case "3":
                GameController.getInstance().getGameData()._scene = "BattleScene1";
                break;
            default:
                break;
        }
    }
}
