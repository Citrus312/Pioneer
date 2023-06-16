using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIRoot : PersistentSingleton<UIRoot>
{
    //UIRoot����
    static Transform transform;
    //������ճ�
    static Transform recyclePool;
    //ǰ̨��ʾ�л����еĴ���
    static Transform workUI;
    //��ʾ���͵Ĵ���
    static Transform noticeUI;

    //��ʼ����ǩ
    static bool isInit = false;

    //UIRoot��ʼ��
    public static void Init()
    {
        if (transform == null)
        {
            GameObject obj = Resources.Load<GameObject>("UI/UIRoot");
            transform = GameObject.Instantiate<GameObject>(obj).transform;
            DontDestroyOnLoad(transform);
        }
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

    //���ô���ĸ�����
    public static void setParent(Transform window, bool isOpen, bool isNoticeUI)
    {
        //û�г�ʼ������г�ʼ��
        if (!isInit)
        {
            Init();
        }

        //���ݴ���Ŀ���״̬��������ĸ�����
        if (isOpen)
        {
            //���������һ����ʾ��������������ΪnoticeUI
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
            //����رգ�����Ӧ�ñ����գ�����������ΪrecyclePool
            window.SetParent(recyclePool, false);
        }
    }
}
