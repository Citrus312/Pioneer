using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseWindow
{
    //窗体本身
    protected Transform transform;
    //资源名称
    protected string resName;
    //是否常驻
    protected bool isResident;
    //当前是否可见
    protected bool isVisible = false;
    //窗体类型
    protected WindowType selfType;
    //场景类型
    protected SceneType sceneType;
    //UI控件
    protected Button[] btnList;
    protected Text[] textList;
    //文本
    public List<string> inputText = new();

    //初始化
    protected virtual void AwakeWindow()
    {
        //当前窗体的按钮控件列表(在此处之后添加的按钮不会进入此列表)
        btnList = transform.GetComponentsInChildren<Button>(true);
        //当前窗体的文本控件列表
        textList = transform.GetComponentsInChildren<Text>(true);

        //注册UI事件(细节由子类实现)
        RegisterUIEvent();
        //填充文本内容(细节由子类实现)
        FillTextContent();
    }

    //UI事件的注册
    protected virtual void RegisterUIEvent() { }
    //文本内容填充
    protected virtual void FillTextContent() { }
    //添加监听游戏事件
    protected virtual void OnAddListener() { }
    //移除游戏事件
    protected virtual void OnRemoveListener() { }
    //每次打开
    protected virtual void OnEnable() { }
    //每次关闭
    protected virtual void OnDisable() { }
    //每帧更新
    protected virtual void Update() { }
    //窗体的创建
    public bool Create()
    {
        //UI资源为空，则无法创建
        if (string.IsNullOrEmpty(resName)) return false;
        //窗体引用为空，则创建实例
        if (transform == null)
        {
            //用预制件名字加载窗口预制件
            GameObject obj = Resources.Load<GameObject>(resName);
            if (obj == null)
            {
                Debug.LogError($"未找到UI预制件{selfType}");
                return false;
            }
            //实例化加载进来的窗口预制件
            transform = GameObject.Instantiate(obj).transform;
            transform.gameObject.SetActive(false);
            //将实例化完成的窗口挂载到UIRoot上
            UIRoot.setParent(transform, false, selfType == WindowType.TipsWindow);
            return true;
        }
        return true;
    }
    //开启窗体
    public virtual void Open()
    {
        //开启窗体前的窗体初始化
        if (transform == null)
        {
            //创建窗体
            if (Create())
            {
                //初始化窗体
                AwakeWindow();
            }
        }
        //检测当前窗体的激活状态
        if (!transform.gameObject.activeSelf)
        {
            //若窗体未激活则将窗口挂载到激活区，并让窗口可见
            UIRoot.setParent(transform, true, selfType == WindowType.TipsWindow);
            transform.gameObject.SetActive(true);
            isVisible = true;
            OnEnable(); //调用激活窗体时应执行的事件
            OnAddListener(); //添加事件
        }
    }
    //关闭窗体
    public void Close(bool isForceClose = false)
    {
        if (transform.gameObject.activeSelf)
        {
            OnRemoveListener(); //移除对该窗体的监听
            OnDisable(); //关闭窗体时应执行的事件
            //根据是否执行强制关闭来决定对窗体的操作
            if (!isForceClose)
            {
                //关闭一个常驻窗体会将窗体回收，关闭一个非常驻窗体则直接销毁游戏物体
                //提示窗一定不是常驻窗体
                if (isResident)
                {
                    //常驻窗体关闭时更改激活状态并将其挂载到UIRoot的回收区等待再次使用
                    transform.gameObject.SetActive(false);
                    UIRoot.setParent(transform, false, false);
                }
                else
                {
                    //非提示窗的非常驻窗口关闭时也是直接销毁游戏对象
                    GameObject.Destroy(transform.gameObject);
                    transform = null;
                }
            }
            else
            {
                //强制关闭则直接销毁窗体的游戏物体
                GameObject.Destroy(transform.gameObject);
                transform = null;
            }
        }
        //将窗体设为不可见
        isVisible = false;
    }

    //以下是获取各种窗体属性的方法
    public SceneType getSceneType()
    {
        return sceneType;
    }
    public WindowType getWindowType()
    {
        return selfType;
    }
    public Transform getTransform()
    {
        return transform;
    }
    public bool getVisible()
    {
        return isVisible;
    }
    public bool getResident()
    {
        return isResident;
    }
}
