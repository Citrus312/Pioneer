using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PropDetailDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameObject propDetail;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("PropDetail") == null)
        {
            GameObject obj = Resources.Load<GameObject>("UI/PropDetail");
            obj = GameObject.Instantiate(obj);
            obj.SetActive(false);
            propDetail = obj;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PropAttribute prop = JsonLoader.propPool[int.Parse(this.name)];

        ImageLoader.LoadImage($"Assets/Sprites/Weapon/{prop.getPropBgIcon()}", propDetail.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>());
        ImageLoader.LoadImage($"Assets/Sprites/Prop/{prop.getPropIcon()}", propDetail.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>());

        propDetail.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text = prop.getPropName();
        Text propAttrText = propDetail.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>();
        propAttrText.text = $"道具属性文本";

        propDetail.transform.GetChild(0).position = new Vector3(transform.position.x, transform.position.y + 110f, transform.position.z);
        propDetail.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        propDetail.SetActive(false);
    }
}
