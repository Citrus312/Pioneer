using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRoot : PersistentSingleton<UIRoot>
{
    //UIRoot本身
    static Transform transform;
    //窗体回收池
    static Transform recyclePool;
    //前台显示中或工作中的窗体
    static Transform workUI;
    //提示类型的窗体
    static Transform noticeUI;

    //初始化标签
    static bool isInit = false;

    //UIRoot初始化
    public static void Init()
    {
        if (transform == null)
        {
            //载入预制体并实例化
            GameObject obj = Resources.Load<GameObject>("UI/UIRoot");
            transform = GameObject.Instantiate<GameObject>(obj).transform;
            //保持UIRoot在切换场景时不被销毁
            DontDestroyOnLoad(transform);
        }
        //获取三个分区
        if (recyclePool == null)
        {
            recyclePool = transform.Find("recyclePool");
        }
        if (workUI == null)
        {
            workUI = transform.Find("workUI");
        }
        if (noticeUI == null)
        {
            noticeUI = transform.Find("noticeUI");
        }
        isInit = true;
    }

    //设置窗体的父对象
    public static void setParent(Transform window, bool isOpen, bool isNoticeUI)
    {
        //没有初始化则进行初始化
        if (!isInit)
        {
            Init();
        }

        //根据窗体的开关状态决定窗体的父对象
        if (isOpen)
        {
            //如果窗体是一个提示窗，父对象设置为noticeUI
            if (isNoticeUI)
            {
                window.SetParent(noticeUI, false);
            }
            else
            {
                window.SetParent(workUI, false);
            }
        }
        else
        {
            //窗体关闭，窗体应该被回收，父对象设置为recyclePool
            window.SetParent(recyclePool, false);
        }
    }
}
