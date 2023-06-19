using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;

public class PausePageController : PersistentSingleton<PausePageController>
{
    public GameObject _Player;
    public bool isPaused = false;


    private void Start()
    {
        UIRoot.Init();
        PausePageWindow.Instance.inputText = getAttribute(_Player);
        PausePageWindow.Instance.Open();
        setAllTriggers();
        PausePageWindow.Instance.Close();
    }

    private void Update()
    {
        if (getCurrentScene() != "MainPage")
        {
            if (Input.GetKeyUp(KeyCode.Escape))
                if (PausePageWindow.Instance.getTransform().gameObject.activeSelf)
                {
                    PausePageWindow.Instance.Close();
                }
                else
                {
                    PausePageWindow.Instance.Open();
                }
        }
    }

    //获取属性文本
    private List<string> getAttribute(GameObject _player)
    {
        List<string> content = new List<string>();
        content.Add("目前等级: " + _player.GetComponent<CharacterAttribute>().getCurrentPlayerLevel());
        content.Add("最大生命值:  " + _player.GetComponent<CharacterAttribute>().getMaxHealth());
        content.Add("生命再生: " + _player.GetComponent<CharacterAttribute>().getHealthRecovery());
        content.Add("%生命窃取: " + _player.GetComponent<CharacterAttribute>().getHealthSteal());
        content.Add("%伤害:" + _player.GetComponent<CharacterAttribute>().getAttackAmplification());
        content.Add("近战伤害:" + _player.GetComponent<CharacterAttribute>().getMeleeDamage());
        content.Add("远程伤害:" + _player.GetComponent<CharacterAttribute>().getRangedDamage());
        content.Add("元素伤害:" + _player.GetComponent<CharacterAttribute>().getAbilityDamage());
        content.Add("%攻击速度:" + _player.GetComponent<CharacterAttribute>().getAttackSpeedAmplification());
        content.Add("%暴击率:" + _player.GetComponent<CharacterAttribute>().getCriticalRate());
        content.Add("工程机械:" + _player.GetComponent<CharacterAttribute>().getEngineering());
        content.Add("范围:" + _player.GetComponent<CharacterAttribute>().getAttackRangeAmplification());
        content.Add("机甲强度:" + _player.GetComponent<CharacterAttribute>().getArmorStrength());
        content.Add("%闪避概率:" + _player.GetComponent<CharacterAttribute>().getDodgeRate());
        content.Add("%移速加成:" + _player.GetComponent<CharacterAttribute>().getMoveSpeedAmplification());
        content.Add("扫描精度:" + _player.GetComponent<CharacterAttribute>().getScanAccuracy());
        content.Add("采集效率:" + _player.GetComponent<CharacterAttribute>().getCollectEfficiency());
        return content;
    }

    //获取当前场景名称
    private string getCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        return currentSceneName;
    }

    private void setAllTriggers()
    {
        setEventTrigger("CurrentPlayerLevel");
        setEventTrigger("MaxHealth");
        setEventTrigger("HealthRecovery");
        setEventTrigger("HealthSteal");
        setEventTrigger("AttackAmplification");
        setEventTrigger("MeleeDamage");
        setEventTrigger("RangedDamage");
        setEventTrigger("AbilityDamage");
        setEventTrigger("AttackSpeedAmplification");
        setEventTrigger("CriticalRate");
        setEventTrigger("Engineering");
        setEventTrigger("AttackRangeAmplification");
        setEventTrigger("ArmorStrength");
        setEventTrigger("DodgeRate");
        setEventTrigger("MoveSpeedAmplification");
        setEventTrigger("ScanAccuracy");
        setEventTrigger("CollectEfficiency");
    }

    //private List<string> nameList()
    //{
    //    List<string> name = new List<string>();
    //    name.Add("CurrentPlayerLevel");
    //    name.Add("MaxHealth");
    //    name.Add("HealthRecovery");
    //    name.Add("HealthSteal");
    //    name.Add("AttackAmplification");
    //    name.Add("MeleeDamage");
    //    name.Add("RangedDamage");
    //    name.Add("AbilityDamage");
    //    name.Add("AttackSpeedAmplification");
    //    name.Add("CriticalRate");
    //    name.Add("Engineering");
    //    name.Add("AttackRangeAmplification");
    //    name.Add("ArmorStrength");
    //    name.Add("DodgeRate");
    //    name.Add("MoveSpeedAmplification");
    //    name.Add("ScanAccuracy");
    //    name.Add("CollectEfficiency");
    //    return name;
    //}

    ////创建EventTrigger中PointerEnter和PointerExit的回调函数
    ////first,second为对应第一次，第二次所查找的Text对象
    //public void setEventTrigger(List<string> secondToFind)
    //{
    //    for (int i = 0; i < secondToFind.Count; i++)
    //    {
    //        Transform first = pausePageWindow.getTransform().Find("Property");
    //        Transform second = first.Find(secondToFind[i]);
    //        int index = i; // 需要将 i 的值赋值给临时变量 index，使得下面 lambda 表达式中使用的 index 值唯一、不变并且符合作用于的变量
    //        second.GetComponent<EventTrigger>().triggers.Add(
    //            new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter, callback = new EventTrigger.TriggerEvent() });
    //        second.GetComponent<EventTrigger>().triggers.Add(
    //            new EventTrigger.Entry { eventID = EventTriggerType.PointerExit, callback = new EventTrigger.TriggerEvent() });

    //        second.GetComponent<EventTrigger>().triggers[0].callback.AddListener((eventData) => { openPanel(second, second.name); });
    //        second.GetComponent<EventTrigger>().triggers[1].callback.AddListener((eventData) => { closePanel(second); });
    //    }
    //}

    //创建EventTrigger中PointerEnter和PointerExit的回调函数
    //first,second为对应第一次，第二次所查找的Text对象
    public void setEventTrigger(string secondToFind)
    {
        Transform first = PausePageWindow.Instance.getTransform().Find("Property");
        Transform second = first.Find(secondToFind);
        second.GetComponent<EventTrigger>().triggers.Add(
                new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter, callback = new EventTrigger.TriggerEvent() });
        second.GetComponent<EventTrigger>().triggers.Add(
            new EventTrigger.Entry { eventID = EventTriggerType.PointerExit, callback = new EventTrigger.TriggerEvent() });

        second.GetComponent<EventTrigger>().triggers[0].callback.AddListener((eventData) => { openPanel(second, second.name); });
        second.GetComponent<EventTrigger>().triggers[1].callback.AddListener((eventData) => { closePanel(second); });
    }

    //public void setEventTrigger(string secondToFind)
    //{

    //    //首先找到属性词条的text
    //    Transform first = pausePageWindow.getTransform().Find("Property");
    //    Transform second = first.Find(secondToFind);

    //    // 获取 UI Text 对象上的 Event Trigger 组件
    //    EventTrigger eventTrigger = second.GetComponent<EventTrigger>();

    //    // 创建一个 Entry 并将其事件设置为 “PointerEnter”
    //    EventTrigger.Entry entryEnter = new EventTrigger.Entry();
    //    entryEnter.eventID = EventTriggerType.PointerEnter;

    //    // 添加一个回调函数到 Entry 中
    //    entryEnter.callback.AddListener((data) => { openPanel(second, secondToFind); });

    //    // 将 Entry 添加到 Event Trigger 的事件列表中
    //    eventTrigger.triggers.Add(entryEnter);

    //    // 创建一个 Entry 并将其事件设置为 “PointerExit”
    //    EventTrigger.Entry entryExit = new EventTrigger.Entry();
    //    entryExit.eventID = EventTriggerType.PointerExit;

    //    // 添加一个回调函数到 Entry 中
    //    entryExit.callback.AddListener((data) => { closePanel(second); });

    //    // 将 Entry 添加到 Event Trigger 的事件列表中
    //    eventTrigger.triggers.Add(entryExit);


    //}

    //打开属性文本的子窗口（具体描述属性）
    public void openPanel(Transform transform, string parentName)
    {
        List<string> attributeList = getAttribute(_Player);
        switch (parentName)
        {
            case "CurrentPlayerLevel":
                transform.Find("Panel").GetComponentInChildren<Text>().text = attributeList[0];
                break;
            case "MaxHealth":
                transform.Find("Panel").GetComponentInChildren<Text>().text = attributeList[1];
                break;
            case "HealthRecovery":
                transform.Find("Panel").GetComponentInChildren<Text>().text = attributeList[2];
                break;
            case "HealthSteal":
                transform.Find("Panel").GetComponentInChildren<Text>().text = attributeList[3];
                break;
            case "AttackAmplification":
                transform.Find("Panel").GetComponentInChildren<Text>().text = attributeList[4];
                break;
            case "MeleeDamage":
                transform.Find("Panel").GetComponentInChildren<Text>().text = attributeList[5];
                break;
            case "RangedDamage":
                transform.Find("Panel").GetComponentInChildren<Text>().text = attributeList[6];
                break;
            case "AbilityDamage":
                transform.Find("Panel").GetComponentInChildren<Text>().text = attributeList[7];
                break;
            case "AttackSpeedAmplification":
                transform.Find("Panel").GetComponentInChildren<Text>().text = attributeList[8];
                break;
            case "CriticalRate":
                transform.Find("Panel").GetComponentInChildren<Text>().text = attributeList[9];
                break;
            case "Engineering":
                transform.Find("Panel").GetComponentInChildren<Text>().text = attributeList[10];
                break;
            case "AttackRangeAmplification":
                transform.Find("Panel").GetComponentInChildren<Text>().text = attributeList[11];
                break;
            case "ArmorStrength":
                transform.Find("Panel").GetComponentInChildren<Text>().text = attributeList[12];
                break;
            case "DodgeRate":
                transform.Find("Panel").GetComponentInChildren<Text>().text = attributeList[13];
                break;
            case "MoveSpeedAmplification":
                transform.Find("Panel").GetComponentInChildren<Text>().text = attributeList[14];
                break;
            case "ScanAccuracy":
                transform.Find("Panel").GetComponentInChildren<Text>().text = attributeList[15];
                break;
            case "CollectEfficiency":
                transform.Find("Panel").GetComponentInChildren<Text>().text = attributeList[16];
                break;
            default:
                break;
        }
        transform.Find("Panel").gameObject.SetActive(true);
    }

    //关闭属性文本的子窗口（具体描述属性）
    public void closePanel(Transform transform)
    {
        transform.Find("Panel").gameObject.SetActive(false);
    }




}
