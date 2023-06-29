using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using UnityEngine.SceneManagement;



public class gameProcessController : PersistentSingleton<gameProcessController>
{
    CharacterAttribute playerProperty;
    GameObject _player;


    public int grade;//等级
    public int gradeCount;//可以升级的次数
    public float blood;//当前血量
    public float maxBlood;//最大生命值
    public float experience;//当前经验值
    public float maxExperience;//最大经验值
    public int level = 1;//关卡数
    public TextMeshProUGUI levelText;//关卡显示文本
    public int money;

    public float HPValue;
    public float EXPValue;
    public float HPMaxValue;
    public float EXPMaxValue;
    public TextMeshProUGUI HPValueText;
    public TextMeshProUGUI EXPValueText;

    public List<WeaponAttribute> WeaponPropList;//卡池
    public List<PropAttribute> PropPoolList;



    //倒计时需要用到的变量
    public float currentTime;
    public float totalTime;
    public TextMeshProUGUI timeText;
    void Start()
    {


        //初始化窗口
        origin();

        //初始化数据
        dataOrigin();

        //倒计时显示
        timeDisplay();



    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime <= 10)
        {
            timeText.color = Color.red;
        }
        if (currentTime == 0)
        {
            timeEnd();
        }

        roleStateUpdate();

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

    //任务状态实时更新显示
    void roleStateUpdate()
    {
        grade = GameController.getInstance().getGameData()._playerLevel;
        blood = playerProperty.getCurrentHealth();
        experience = GameController.getInstance().getGameData()._exp;
        money = GameController.getInstance().getGameData()._money;
        while (experience >= EXPMaxValue)
        {
            experience = experience - EXPMaxValue;
            GameController.getInstance().getGameData()._exp = experience;
            gradeCount++;
            grade++;
            HPMaxValue += 2;
            EXPMaxValue += 5;
        }
        HPValue = blood;
        EXPValue = experience;

        if (HPValue <= 0)
        {
            Time.timeScale = 0f;
            //死亡界面
        }
        HPValueText.text = "" + HPValue;
        EXPValueText.text = "Lv" + grade;

    }

    //数据初始化
    void dataOrigin()
    {
        JsonLoader.LoadAndDecodePropConfig();
        JsonLoader.LoadAndDecodeWeaponConfig();
        WeaponPropList = JsonLoader.weaponPool;
        PropPoolList = JsonLoader.propPool;



        playerProperty = GameController.getInstance().getPlayer().GetComponent<CharacterAttribute>();
        blood = 15;
        experience = 0;
        money = 0;
        gradeCount = 2;
        Transform roleState = roleStateWindow.Instance.getTransform().Find("roleState");
        HPValue = roleState.Find("blood").GetComponent<Slider>().value;
        EXPValue = roleState.Find("exp").GetComponent<Slider>().value;
        HPMaxValue = roleState.Find("blood").GetComponent<Slider>().maxValue;
        EXPMaxValue = roleState.Find("exp").GetComponent<Slider>().maxValue;
        HPValue = 15;
        EXPValue = 20;
        HPMaxValue = 15;
        EXPMaxValue = 20;

        HPValueText = roleStateWindow.Instance.getTransform().Find("HPValue").GetComponent<TextMeshProUGUI>();
        EXPValueText = roleStateWindow.Instance.getTransform().Find("EXPValue").GetComponent<TextMeshProUGUI>();



    }

    //窗口初始化
    void origin()
    {

        UIRoot.Init();

        GameController.getInstance().initBattleScene();

        roleStateWindow.Instance.Open();
        countDownTimerWindow.Instance.Open();
        titleWindow.Instance.Open();
        _player = GameController.getInstance().getPlayer();
        propertyWindow.Instance.inputText = getAttribute(_player);
        propertyWindow.Instance.Open();
        setAllTriggers();
        propertyWindow.Instance.Close();

        PausePageWindow.Instance.Open();
        PausePageWindow.Instance.Close();

        totalTime = 5f;
        Transform countDownTimer = countDownTimerWindow.Instance.getTransform().Find("countDownTimer");
        timeText = countDownTimer.GetComponent<TextMeshProUGUI>();

        Transform titleText = titleWindow.Instance.getTransform().Find("title");
        levelText = titleText.GetComponent<TextMeshProUGUI>();

    }



    //倒计时计算
    void updateCountDownTimer()
    {

        currentTime--;
        timeText.text = "" + currentTime;
        if (currentTime <= 0f)
        {
            currentTime = 0;
            CancelInvoke();
        }
    }

    //倒计时显示
    void timeDisplay()
    {
        currentTime = totalTime;
        InvokeRepeating("updateCountDownTimer", 1f, 1f);
    }

    //协程
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.5f);
    }

    //倒计时结束后的事件
    void timeEnd()
    {

        Transform endText = countDownTimerWindow.Instance.getTransform().Find("endText");
        endText.gameObject.SetActive(true);
        StartCoroutine(wait());
        endText.gameObject.SetActive(false);
        currentTime = -1;
        if (gradeCount > 0)
        {
            Debug.Log("升级");
            Time.timeScale = 0f;
            timeText.text = "<color=white>升级</color>";
            upgradeWindow.Instance.Open();
            addListenerForupgrade();
            propertyWindow.Instance.Open();
        }
        else
        {
            Debug.Log("商店");
            weaponBagWindow.Instance.Open();
            propBagWindow.Instance.Open();
            storeWindow.Instance.Open();
            addListenerForstartBtn();
            addListenerForBuy();


            propertyWindow.Instance.Open();
            roleStateWindow.Instance.Close();
            titleWindow.Instance.Close();
            countDownTimerWindow.Instance.Close();
        }
    }

    //为升级按钮添加监听事件
    void addListenerForupgrade()
    {
        Transform child1 = upgradeWindow.Instance.getTransform().Find("cardA");
        Transform child2 = upgradeWindow.Instance.getTransform().Find("cardB");
        Transform child3 = upgradeWindow.Instance.getTransform().Find("cardC");
        Transform child4 = upgradeWindow.Instance.getTransform().Find("cardD");

        Transform btn1 = child1.Find("upgradeBtn");
        Transform btn2 = child2.Find("upgradeBtn");
        Transform btn3 = child3.Find("upgradeBtn");
        Transform btn4 = child4.Find("upgradeBtn");

        btn1.GetComponent<Button>().onClick.AddListener(upgradeBtnOnclik);
        btn2.GetComponent<Button>().onClick.AddListener(upgradeBtnOnclik);
        btn3.GetComponent<Button>().onClick.AddListener(upgradeBtnOnclik);
        btn4.GetComponent<Button>().onClick.AddListener(upgradeBtnOnclik);

    }

    //升级按钮事件
    void upgradeBtnOnclik()
    {
        Debug.Log("升级成功");
        Debug.Log(gradeCount);
        string n = upgradeWindow.Instance.name;
        float v = upgradeWindow.Instance.value;
        switch (n)
        {
            case "生命上限":
                playerProperty.setMaxHealth(playerProperty.getMaxHealth() + v);
                break;
            case "生命回复":
                playerProperty.setHealthRecovery(playerProperty.getHealthRecovery() + v);
                break;
            case "生命汲取":
                playerProperty.setHealthSteal(playerProperty.getHealthSteal() + v);
                break;
            case "输出增幅":
                playerProperty.setAttackAmplification(playerProperty.getAttackAmplification() + v);
                break;
            case "近战伤害":
                playerProperty.setMeleeDamage(playerProperty.getMeleeDamage() + v);
                break;
            case "远程伤害":
                playerProperty.setRangedDamage(playerProperty.getRangedDamage() + v);
                break;
            case "属性伤害":
                playerProperty.setAbilityDamage(playerProperty.getAbilityDamage() + v);
                break;
            case "攻速加成":
                playerProperty.setAttackSpeedAmplification(playerProperty.getAttackSpeedAmplification() + v);
                break;
            case "暴击概率":
                playerProperty.setCriticalRate(playerProperty.getCriticalRate() + v);
                break;
            case "工程机械":
                playerProperty.setEngineering(playerProperty.getEngineering() + v);
                break;
            case "攻击范围":
                playerProperty.setAttackRangeAmplification(playerProperty.getAttackRangeAmplification() + v);
                break;
            case "机甲强度":
                playerProperty.setArmorStrength(playerProperty.getArmorStrength() + v);
                break;
            case "闪避概率":
                playerProperty.setDodgeRate(playerProperty.getDodgeRate() + v);
                break;
            case "移速加成":
                playerProperty.setMoveSpeedAmplification(playerProperty.getMoveSpeedAmplification() + v);
                break;
            case "扫描精度":
                playerProperty.setScanAccuracy(playerProperty.getScanAccuracy() + v);
                break;
            case "采集效率":
                playerProperty.setCollectEfficiency(playerProperty.getCollectEfficiency() + v);
                break;
            default:
                break;
        }
        gradeCount--;

        if (gradeCount == 0)
        {
            upgradeWindow.Instance.Close();
            weaponBagWindow.Instance.Open();
            propBagWindow.Instance.Open();
            storeWindow.Instance.Open();
            addListenerForstartBtn();
            addListenerForBuy();


            roleStateWindow.Instance.Close();
            titleWindow.Instance.Close();
            countDownTimerWindow.Instance.Close();
        }
    }
    //为出发按钮设置监听事件
    void addListenerForstartBtn()
    {
        Transform startBtn = storeWindow.Instance.getTransform().Find("startButton");
        startBtn.GetComponent<Button>().onClick.AddListener(startBtnOnclick);
    }

    //出发按钮事件
    void startBtnOnclick()
    {
        HPValue = HPMaxValue;
        Debug.Log("开始下一关");
        Time.timeScale = 1f;
        storeWindow.Instance.Close();
        propBagWindow.Instance.Close();
        weaponBagWindow.Instance.Close();
        propertyWindow.Instance.Close();
        roleStateWindow.Instance.Open();
        titleWindow.Instance.Open();
        countDownTimerWindow.Instance.Open();

        //血量、经验、等级、倒计时、关卡重置
        totalTime = totalTime + 10 >= 90 ? 90 : totalTime + 10;
        timeDisplay();

        level++;
        levelText.text = "第" + level + "关";
        GameController.getInstance().getGameData()._wave = level;

        grade = 2;

    }

    //为购买按钮设置监听事件
    void addListenerForBuy()
    {
        Transform card1 = storeWindow.Instance.getTransform().Find("card_a");
        Transform card2 = storeWindow.Instance.getTransform().Find("card_b");
        Transform card3 = storeWindow.Instance.getTransform().Find("card_c");
        Transform card4 = storeWindow.Instance.getTransform().Find("card_d");

        Transform btn1 = card1.Find("Button_shop");
        Transform btn2 = card2.Find("Button_shop");
        Transform btn3 = card3.Find("Button_shop");
        Transform btn4 = card4.Find("Button_shop");

        btn1.GetComponent<Button>().onClick.AddListener(buyBtnOnclick);
        btn2.GetComponent<Button>().onClick.AddListener(buyBtnOnclick);
        btn3.GetComponent<Button>().onClick.AddListener(buyBtnOnclick);
        btn4.GetComponent<Button>().onClick.AddListener(buyBtnOnclick);
    }

    //购买按钮事件
    void buyBtnOnclick()
    {
        if (weaponBagWindow.Instance.isWeapon)
        {
            if (weaponBagWindow.Instance.addWeapon)
            {
                int w = weaponBagWindow.Instance.buyedWeapon;
                string assetPath;
                string assetPathBg;

                GameObject weapon = new GameObject("weapon" + w);
                Transform weaponBag = weaponBagWindow.Instance.getTransform().Find("weaponBag");
                weapon.transform.SetParent(weaponBag);
                weapon.AddComponent<Image>();

                GameObject image = new GameObject("image");
                image.transform.SetParent(weapon.transform);
                image.AddComponent<Image>();

                RectTransform rectWeapon = weapon.GetComponent<RectTransform>();
                rectWeapon.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                assetPathBg = "Assets/Sprites/Weapon/" + WeaponPropList[w].getWeaponBgIcon();

                if (WeaponPropList[w].getWeaponDamageType() == WeaponAttribute.WeaponDamageType.Melee)
                {
                    assetPath = "Assets/Sprites/Weapon/" + "Melee Weapon/" + WeaponPropList[w].getWeaponIcon();
                }
                else if (WeaponPropList[w].getWeaponDamageType() == WeaponAttribute.WeaponDamageType.Ranged)
                {
                    assetPath = "Assets/Sprites/Weapon/" + "Ranged Weapon/" + WeaponPropList[w].getWeaponIcon();
                }
                else
                {
                    assetPath = "Assets/Sprites/Weapon/" + "Ability Weapon/" + WeaponPropList[w].getWeaponIcon();
                }

                loadImage(assetPathBg, weapon.transform);
                loadImage(assetPath, image.transform);
            }

        }
        else
        {
            int w = propBagWindow.Instance.buyedProp;
            w = w - 40000;
            string assetPath;
            string assetPathBg;

            GameObject prop = new GameObject("prop" + w);
            Transform propBag = propBagWindow.Instance.getTransform().Find("propBag");
            Transform listContent = propBag.Find("listContent");
            prop.transform.SetParent(listContent);
            prop.AddComponent<Image>();
            GameObject image = new GameObject("image");
            image.transform.SetParent(prop.transform);
            image.AddComponent<Image>();
            RectTransform rectProp = prop.GetComponent<RectTransform>();
            rectProp.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            assetPathBg = "Assets/Sprites/Weapon/" + PropPoolList[w].getPropBgIcon();

            assetPath = "Assets/Sprites/Prop/" + PropPoolList[w].getPropIcon();

            loadImage(assetPathBg, prop.transform);
            loadImage(assetPath, image.transform);
        }

    }

    //加载图片
    void loadImage(string assetPath, Transform child)
    {
        byte[] bytes = System.IO.File.ReadAllBytes(assetPath);

        Texture2D texture = new Texture2D(2, 2);
        if (texture.LoadImage(bytes))
        {
            // 创建Sprite并附加到Image组件上
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
            child.GetComponent<Image>().sprite = sprite;
            //RectTransform size = child.GetComponent<RectTransform>();
            //size.sizeDelta = new Vector2(50, 50);

            //Debug.Log("成功加载图片: ");
        }
        else
        {
            //Debug.Log("无法读取文件: ");
        }
    }


    //fei
    //获取属性文本
    private List<string> getAttribute(GameObject _player)
    {
        List<string> content = new List<string>();
        content.Add("目前等级: " + _player.GetComponent<CharacterAttribute>().getCurrentPlayerLevel());
        content.Add("生命上限: " + _player.GetComponent<CharacterAttribute>().getMaxHealth());
        content.Add("生命回复: " + _player.GetComponent<CharacterAttribute>().getHealthRecovery());
        content.Add("生命汲取: " + _player.GetComponent<CharacterAttribute>().getHealthSteal());
        content.Add("输出增幅: " + _player.GetComponent<CharacterAttribute>().getAttackAmplification());
        content.Add("近战伤害: " + _player.GetComponent<CharacterAttribute>().getMeleeDamage());
        content.Add("远程伤害: " + _player.GetComponent<CharacterAttribute>().getRangedDamage());
        content.Add("元素伤害: " + _player.GetComponent<CharacterAttribute>().getAbilityDamage());
        content.Add("攻击速度: " + _player.GetComponent<CharacterAttribute>().getAttackSpeedAmplification());
        content.Add("暴击概率: " + _player.GetComponent<CharacterAttribute>().getCriticalRate());
        content.Add("工程机械: " + _player.GetComponent<CharacterAttribute>().getEngineering());
        content.Add("攻击范围: " + _player.GetComponent<CharacterAttribute>().getAttackRangeAmplification());
        content.Add("机甲强度: " + _player.GetComponent<CharacterAttribute>().getArmorStrength());
        content.Add("闪避概率: " + _player.GetComponent<CharacterAttribute>().getDodgeRate());
        content.Add("移速加成: " + _player.GetComponent<CharacterAttribute>().getMoveSpeedAmplification());
        content.Add("扫描精度: " + _player.GetComponent<CharacterAttribute>().getScanAccuracy());
        content.Add("采集效率: " + _player.GetComponent<CharacterAttribute>().getCollectEfficiency());
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
        Transform first = propertyWindow.Instance.getTransform().Find("Property");
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
        List<string> attributeList = getAttribute(_player);
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
