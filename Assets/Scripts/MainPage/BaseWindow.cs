using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseWindow
{
    //���屾��
    protected Transform transform;
    //��Դ����
    protected string resName;
    //�Ƿ�פ
    protected bool isResident;
    //��ǰ�Ƿ�ɼ�
    protected bool isVisible = false;
    //��������
    protected WindowType selfType;
    //��������
    protected SceneType sceneType;
    //UI�ؼ�
    protected Button[] btnList;
    protected Text[] textList;
    
    //��ʼ��
    protected virtual void Awake(string inputText = "")
    {
        btnList = transform.GetComponentsInChildren<Button>(true);
        textList = transform.GetComponentsInChildren<Text>(true);

        //ע��UI�¼�(ϸ��������ʵ��)
        RegisterUIEvent();
        //����ı�����(ϸ��������ʵ��)
        FillTextContent(inputText);
    }
   
    //UI�¼���ע��
    protected virtual void RegisterUIEvent() { }
    //�ı��������
    protected virtual void FillTextContent(string inputText) { }
    //��Ӽ�����Ϸ�¼�
    protected virtual void OnAddListener() { }
    //�Ƴ���Ϸ�¼�
    protected virtual void OnRemoveListener() { }
    //ÿ�δ�
    protected virtual void OnEnable() { }
    //ÿ�ιر�
    protected virtual void OnDisable() { }
    //ÿ֡����
    protected virtual void Update(float deltaTime) { }
    //����Ĵ���
    public bool Create()
    {
        //UI��ԴΪ�գ����޷�����
        if (string.IsNullOrEmpty(resName)) return false;
        //��������Ϊ�գ��򴴽�ʵ��
        if (transform == null)
        {
            GameObject obj = Resources.Load<GameObject>(resName);
            if (obj == null)
            {
                Debug.LogError($"δ�ҵ�UIԤ�Ƽ�{selfType}");
                return false;
            }
            transform = GameObject.Instantiate(obj).transform;
            transform.gameObject.SetActive(false);
            UIRoot.setParent(transform, false, selfType == WindowType.TipsWindow);
            return true;
        }
        return true;
    }
    //��������
    public void Open(string inputText = "")
    {
        //��������ǰ�Ĵ����ʼ��
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
            OnEnable(); //���ü����ʱӦִ�е��¼�
            OnAddListener(); //����¼�
        }
    }
    //�رմ���
    public void Close(bool isForceClose = false)
    {
        if (transform.gameObject.activeSelf)
        {
            OnRemoveListener(); //�Ƴ��Ըô���ļ���
            OnDisable(); //�رմ���ʱӦִ�е��¼�
            //�����Ƿ�ִ��ǿ�ƹر��������Դ���Ĳ���
            if (!isForceClose)
            {
                //�ر�һ����פ����Ὣ������գ��ر�һ���ǳ�פ������ֱ��������Ϸ����
                //��ʾ��һ�����ǳ�פ����
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
                //ǿ�ƹر���ֱ�����ٴ������Ϸ����
                GameObject.Destroy(transform.gameObject);
                transform = null;
            }
        }
        //��������Ϊ���ɼ�
        isVisible = false;
    }
    //�����ǻ�ȡ���ִ������Եķ���
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
