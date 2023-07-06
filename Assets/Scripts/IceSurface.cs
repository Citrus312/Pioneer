/*
    挂在冰主题关卡的冰面障碍物上
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class IceSurface : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<PolygonCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Player" || collider2D.tag == "Enemy")
        {
            collider2D.GetComponent<Controller>().inIceSurface();
        }
    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Player" || collider2D.tag == "Enemy")
        {
            collider2D.GetComponent<Controller>().outIceSurface();
        }
    }
}
