using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySelectWindow : BaseWindow
{
    private static DifficultySelectWindow instance;

    private DifficultySelectWindow()
    {
        resName = "UI/DifficultySelectWindow";
        isResident = true;
        isVisible = false;
        selfType = WindowType.DifficultySelectWindow;
        sceneType = SceneType.Select;
    }

    public static DifficultySelectWindow Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new();
            }
            return instance;
        }
    }

    protected override void AwakeWindow()
    {
        base.AwakeWindow();
    }

    protected override void FillTextContent()
    {
        base.FillTextContent();
    }

    protected override void OnAddListener()
    {
        base.OnAddListener();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnRemoveListener()
    {
        base.OnRemoveListener();
    }

    protected override void RegisterUIEvent()
    {
        base.RegisterUIEvent();

        foreach (Button btn in btnList)
        {
            switch (btn.name)
            {
                case "Lv1Btn":
                    btn.onClick.AddListener(() => { OnLv1Btn(); });
                    break;
                case "Lv2Btn":
                    btn.onClick.AddListener(() => { OnLv2Btn(); });
                    break;
                case "Lv3Btn":
                    btn.onClick.AddListener(() => { OnLv3Btn(); });
                    break;
                case "Lv4Btn":
                    btn.onClick.AddListener(() => { OnLv4Btn(); });
                    break;
                default:
                    break;
            }
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    public void OnLv1Btn()
    {
        GameController.getInstance().getGameData()._difficulty = 1;
        //SceneLoader._instance.loadScene(GameController.getInstance().getGameData()._scene);
    }

    public void OnLv2Btn()
    {
        GameController.getInstance().getGameData()._difficulty = 2;
        //SceneLoader._instance.loadScene(GameController.getInstance().getGameData()._scene);
    }

    public void OnLv3Btn()
    {
        GameController.getInstance().getGameData()._difficulty = 3;
        //SceneLoader._instance.loadScene(GameController.getInstance().getGameData()._scene);
    }

    public void OnLv4Btn()
    {
        GameController.getInstance().getGameData()._difficulty = 4;
        //SceneLoader._instance.loadScene(GameController.getInstance().getGameData()._scene);
    }

    public void OpenAndMove(float x, float y)
    {
        Instance.Open();
        transform.position = new Vector3(x, y, transform.position.z);
    }
}
