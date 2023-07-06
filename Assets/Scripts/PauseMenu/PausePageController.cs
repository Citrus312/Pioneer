using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;

public class PausePageController : PersistentSingleton<PausePageController>
{
    private GameObject _Player;

    private void Start()
    {
        //UIRoot.Init();
        _Player = GameController.getInstance().getPlayer();
        PausePageWindow.Instance.inputText = getAttribute(_Player);
        PausePageWindow.Instance.Open();
        setAllTriggers();
        Debug.Log(2);
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
