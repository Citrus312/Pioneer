using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class buttondisplay : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    public Color normalColor = new(1f, 1f, 1f,0.5f);
    public Color highLightColor = new(0f, 0f, 0f, 0.5f);

    public Image image;



    // Start is called before the first frame update

    void Start()
    {
        image = GetComponent<Image>();
        image.color = normalColor;
        
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = highLightColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = normalColor;
    }
}
