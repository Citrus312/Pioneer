using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class propRightClick : MonoBehaviour, IPointerClickHandler
{
    Transform option;
    Transform copyOption;

    private void Start()
    {
        option = propBagWindow.Instance.getTransform().Find("option");
        copyOption = Instantiate(option);
        copyOption.SetParent(this.transform);
        addListener();
    }
    public void OnPointerClick(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            copyOption.gameObject.SetActive(true);
            copyOption.localPosition = new Vector3(150f, 100f, 0f);
        }
    }
    //给按钮添加监听事件
    public void addListener()
    {
        Transform recycleBtn = copyOption.Find("recycleBtn");
        recycleBtn.GetComponent<Button>().onClick.AddListener(recycleOnclick);

        Transform cancelBtn = copyOption.Find("cancelBtn");
        cancelBtn.GetComponent<Button>().onClick.AddListener(cancelOnclick);
    }

    //背包中的物品右键点击事件-回收
    public void recycleOnclick()
    {
        Destroy(this.gameObject);
       
    }

    //背包中的物品右键点击事件-取消
    public void cancelOnclick()
    {
        copyOption.gameObject.SetActive(false);
    }

}
