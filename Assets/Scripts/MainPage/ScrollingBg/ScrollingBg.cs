using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class ScrollingBg : MonoBehaviour 
{
 
    [Range(0f,5f)] //将rollSpeed在Inspector面板中设置成滑动条的样式
    public float rollSpeed = 1f;//滚动速度
    private float right; //右边界
    private float left; //左边界
    private float distance; //左右边界距离
    public Camera m_OrthographicCamera; // 相机
    
    // Use this for initialization
    void Start () 
    {
        //计算左右边界。Bounds是当图形的边界框
        SpriteRenderer sRender = GetComponent<SpriteRenderer>();
        if(m_OrthographicCamera == null)
        {
            Debug.Log("没有赋值相机");
            return;
        }
        right = transform.position.x + (sRender.bounds.extents.x - m_OrthographicCamera.orthographicSize * m_OrthographicCamera.aspect);
        left = transform.position.x - (sRender.bounds.extents.x - m_OrthographicCamera.orthographicSize * m_OrthographicCamera.aspect); 
        Debug.Log("left:" + left + " right:" + right);
        Debug.Log(sRender.bounds.extents.x);
        distance = sRender.bounds.extents.x;
    }
    
    // Update is called once per frame
    void Update () 
    {
        //使背景图片向右移动
        transform.localPosition += rollSpeed * Vector3.right * Time.deltaTime;
        
        //判断是否到达右边界
        if(transform.position.x > right)
        {
            //如果到达，将背景图片的位置向后（x轴方向）调整一个背景图片长度的距离
            transform.position -= new Vector3(distance, 0, 0);
        }
    }
}