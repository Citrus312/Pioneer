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
    
    //初始化
    protected virtual void Awake(string inputText = "")
    {
        btnList = transform.GetComponentsInChildren<Button>(true);
        textList = transform.GetComponentsInChildren<Text>(true);

        //注册UI事件(细节由子类实现)
        RegisterUIEvent();
        //填充文本内容(细节由子类实现)
        FillTextContent(inputText);
    }
   
    //UI事件的注册
    protected virtual void RegisterUIEvent() { }
    //文本内容填充
    protected virtual void FillTextContent(string inputText) { }
    //添加监听游戏事件
    protected virtual void OnAddListener() { }
    //移除游戏事件
    protected virtual void OnRemoveListener() { }
    //每次打开
    protected virtual void OnEnable() { }
    //每次关闭
    protected virtual void OnDisable() { }
    //每帧更新
    protected virtual void Update(float deltaTime) { }
    //窗体的创建
    public bool Create()
    {
        //UI资源为空，则无法创建
        if (string.IsNullOrEmpty(resName)) return false;
        //窗体引用为空，则创建实例
        if (transform == null)
        {
            GameObject obj = Resources.Load<GameObject>(resName);
            if (obj == null)
            {
                Debug.LogError($"未找到UI预制件{selfType}");
                return false;
            }
            transform = GameObject.Instantiate(obj).transform;
            transform.gameObject.SetActive(false);
            UIRoot.setParent(transform, false, selfType == WindowType.TipsWindow);
            return true;
        }
        return true;
    }
    //开启窗体
    public void Open(string inputText = "")
    {
        //开启窗体前的窗体初始化
        if (transform == null)
        {
            if (Create())
            {
                Awake(inputText);
            }
        }
        if (!transform.gameObject.activeSelf)
        {
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
                    transform.gameObject.SetActive(false);
                    UIRoot.setParent(transform, false, false);
                }
                else
                {
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
