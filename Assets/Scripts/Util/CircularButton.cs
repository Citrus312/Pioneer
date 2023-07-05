using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CircularButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    GameObject obj; //按钮所在位置的星球游戏物体

    private void Start()
    {
        //绑定按钮点击事件
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(() => { OnBtn(btn); });
    }
    //按钮点击事件
    public void OnBtn(Button btn)
    {
        //点击按钮时先将星球亮度恢复原状
        obj.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        string str = btn.name.Trim('B', 't', 'n');
        //根据按钮名字获取按钮索引，再计算得出窗口打开的位置
        int index = int.Parse(str);
        DifficultySelectWindow.Instance.OpenAndMove(434 + (index - 1) * 8, 248.75f);
        //根据按钮索引给全局游戏数据的_scene赋值
        switch (str)
        {
            case "1":
                GameController.getInstance().getGameData()._scene = "Purple";
                break;
            case "2":
                GameController.getInstance().getGameData()._scene = "Lava";
                break;
            case "3":
                GameController.getInstance().getGameData()._scene = "Ice";
                break;
            default:
                break;
        }
        //延迟一定时间后将星球亮度再次减半，保证鼠标进入和离开事件正常衔接
        DelayToInvoke.DelayToInvokeBySecond(() => { obj.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1f); }, 0.15f);
    }
    //按钮鼠标进入事件
    public void OnPointerEnter(PointerEventData eventData)
    {
        //鼠标进入按钮则让星球亮度减半
        obj.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
    }
    //按钮鼠标离开事件
    public void OnPointerExit(PointerEventData eventData)
    {
        //鼠标进入按钮则让星球亮度恢复原状
        obj.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }
}
