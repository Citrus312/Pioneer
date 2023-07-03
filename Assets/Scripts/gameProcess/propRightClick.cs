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
    //����ť��Ӽ����¼�
    public void addListener()
    {
        Transform recycleBtn = copyOption.Find("recycleBtn");
        recycleBtn.GetComponent<Button>().onClick.AddListener(recycleOnclick);

        Transform cancelBtn = copyOption.Find("cancelBtn");
        cancelBtn.GetComponent<Button>().onClick.AddListener(cancelOnclick);
    }

    //�����е���Ʒ�Ҽ�����¼�-����
    public void recycleOnclick()
    {
        Destroy(this.gameObject);
       
    }

    //�����е���Ʒ�Ҽ�����¼�-ȡ��
    public void cancelOnclick()
    {
        copyOption.gameObject.SetActive(false);
    }

}
