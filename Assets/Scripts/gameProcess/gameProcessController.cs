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
    public int level=1;//关卡数
    public TextMeshProUGUI levelText;//关卡显示文本
    public float money;

    public float HPValue;
    public float EXPValue;
    public float HPMaxValue;
    public float EXPMaxValue;
    public TextMeshProUGUI HPValueText;
    public TextMeshProUGUI EXPValueText;

    public List<WeaponAttribute> WeaponPropList;//卡池
    public List<PropAttribute> PropPoolList;

    Transform stateBg;//由当前血量值决定的效果图
    
    //倒计时需要用到的变量
    public float currentTime;
    public float totalTime;
    public TextMeshProUGUI timeText;
    void Start()
    {

        test();
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
        if(currentTime<=10)
        {
            timeText.color = Color.red;
        }
         if(currentTime==0)
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
        Transform moneyValue = storeWindow.Instance.getTransform().Find("moneyValue");
        Transform roleState = roleStateWindow.Instance.getTransform().Find("roleState");
        Transform moneyValue2 = roleState.Find("moneyValue");
        moneyValue.GetComponent<TextMeshProUGUI>().text = "" + money;
        moneyValue2.GetComponent<TextMeshProUGUI>().text = "" + money;

        while(experience>=EXPMaxValue)
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

        if(HPValue<=0)
        {
            Time.timeScale = 0f;
            //死亡界面
        }
        else if(HPValue<= HPMaxValue*2/3 && HPValue> HPMaxValue*1/4)
        {
            stateBg.GetComponent<RectTransform>().localScale = new Vector3(2f, 2f, 2f);
        }
        else if(HPValue<=HPMaxValue*1/4)
        {
            stateBg.GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        HPValueText.text =""+ HPValue;
        EXPValueText.text = "Lv" + grade;
         
    }

    //test初始化
    void test()
    {
        if (JsonLoader.weaponPool.Count == 0)
        {
            JsonLoader.LoadAndDecodeWeaponConfig();
        }
        if (JsonLoader.rolePool.Count == 0)
        {
            JsonLoader.LoadAndDecodeRoleConfig();
        }
        if (JsonLoader.monsterPool.Count == 0)
        {
            JsonLoader.LoadAndDecodeMonsterConfig();
        }
        if (JsonLoader.propPool.Count == 0)
        {
            JsonLoader.LoadAndDecodePropConfig();
        }
        GameController.getInstance().getGameData()._weaponList.Add(12);
        GameController.getInstance().getGameData()._playerID = 1;
        GameController.getInstance().getGameData()._difficulty = 1;
        GameController.getInstance().getGameData()._wave = 1;
        GameController.getInstance().getGameData()._money = 1000;
        GameController.getInstance().initBattleScene();
        GameController.getInstance().getGameData()._attr = JsonLoader.rolePool[0];
        GameController.getInstance().getPlayer().GetComponent<CharacterAttribute>().setAllPlayerAttribute(GameController.getInstance().getGameData()._attr);
        GameController.getInstance().waveStart();
    }
    //数据初始化
    void dataOrigin()
    {
        if(JsonLoader.propPool.Count == 0)        
            JsonLoader.LoadAndDecodePropConfig();       
        if(JsonLoader.weaponPool.Count==0)
            JsonLoader.LoadAndDecodeWeaponConfig();
        WeaponPropList = JsonLoader.weaponPool;
        PropPoolList = JsonLoader.propPool;

        GameController.getInstance().getGameData()._money = 1000f;

        playerProperty = GameController.getInstance().getPlayer().GetComponent<CharacterAttribute>();
        blood = 15;
        experience = 0;
        money = 50;
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
        EXPValueText= roleStateWindow.Instance.getTransform().Find("EXPValue").GetComponent<TextMeshProUGUI>();

        stateBg = roleStateWindow.Instance.getTransform().Find("stateBg");
        stateBg.GetComponent<RectTransform>().localScale =new Vector3(2f, 2f, 2f);

       

        totalTime = 5f;//第一关时间
    }

    //窗口初始化
    void origin()
    {
        
        UIRoot.Init();

        

        PausePageWindow.Instance.Open();
        PausePageWindow.Instance.Close();
       
        storeWindow.Instance.Open();
        storeWindow.Instance.Close();
        upgradeWindow.Instance.Open();
        upgradeWindow.Instance.Close();
        weaponBagWindow.Instance.Open();
        weaponBagWindow.Instance.Close();
        propBagWindow.Instance.Open();
        propBagWindow.Instance.Close();
        roleStateWindow.Instance.Open();
        roleStateWindow.Instance.Close();




        roleStateWindow.Instance.Open();
        countDownTimerWindow.Instance.Open();
        titleWindow.Instance.Open();

        _player = GameController.getInstance().getPlayer();
      
        propertyWindow.Instance.inputText = getAttribute(_player);
        propertyWindow.Instance.Open();
        setAllTriggers();
        propertyWindow.Instance.Close();

        addListenerForupgrade();
        addListenerForstartBtn();
        addListenerForBuy();

        
        Transform countDownTimer = countDownTimerWindow.Instance.getTransform().Find("countDownTimer");
        timeText = countDownTimer.GetComponent<TextMeshProUGUI>();

        Transform titleText = titleWindow.Instance.getTransform().Find("title");
        levelText = titleText.GetComponent<TextMeshProUGUI>();

    }

    //倒计时计算
    void updateCountDownTimer()
    {
        
        currentTime--;
        timeText.text = "" + currentTime ;
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
           
            Time.timeScale = 0f;
            timeText.text = "<color=white>升级</color>";
            upgradeWindow.Instance.Open();
            
            propertyWindow.Instance.Open();
        }
        else
        {
            Debug.Log("商店");
            
            storeWindow.Instance.Open();
            weaponBagWindow.Instance.Open();
            propBagWindow.Instance.Open();

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

        if(gradeCount==0)
        {
            propertyWindow.Instance.Close();
            upgradeWindow.Instance.Close();
            storeWindow.Instance.Open();
            weaponBagWindow.Instance.Open();
            propBagWindow.Instance.Open();
            propertyWindow.Instance.Open();
            
            
            
            
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

        if(weaponBagWindow.Instance.isWeapon)
        {
            if (weaponBagWindow.Instance.addWeapon)
            {
                int w = weaponBagWindow.Instance.buyedWeapon;
                if(WeaponPropList[w].getWeaponPrice() > GameController.getInstance().getGameData()._money)
                {
                    Debug.Log("钱不够");
                   // Debug.Log(GameController.getInstance().getGameData()._money);
                }
                else
                {
                    GameController.getInstance().getGameData()._money -= WeaponPropList[w].getWeaponPrice();
                    //Debug.Log(GameController.getInstance().getGameData()._money);
                    string assetPath;
                    string assetPathBg;
                    GameObject weapon = new GameObject("weapon" + w);//背景图
                    Transform weaponBag = weaponBagWindow.Instance.getTransform().Find("weaponBag");
                    weapon.transform.SetParent(weaponBag);
                    weapon.AddComponent<Image>();
                    weapon.AddComponent<buttonRightClick>();                  
                    GameObject image = new GameObject(""+w);//武器图
                    image.transform.SetParent(weapon.transform);
                    //image.AddComponent<Image>();

                    GameObject id = new GameObject("id");
                    id.transform.SetParent(image.transform);
                    id.AddComponent<Image>();
                    id.AddComponent<WeaponDetailDisplay>();
                    if (GameObject.Find("DetailPanel") == null)
                    {
                        GameObject obj = Resources.Load<GameObject>("UI/DetailPanel");
                        obj = GameObject.Instantiate(obj);
                        obj.SetActive(false);
                        id.GetComponent<WeaponDetailDisplay>().detailDisplay = obj;

                    }


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
                    loadImage(assetPath, id.transform);
                }
                
            }
           
        }
        else
        {
            int w = propBagWindow.Instance.buyedProp;
            w = w - 40000;
            if (PropPoolList[w].getPropPrice() > GameController.getInstance().getGameData()._money)
            {
                Debug.Log("钱不够");
                //Debug.Log(GameController.getInstance().getGameData()._money);
            }
            else
            {
                GameController.getInstance().getGameData()._money -= PropPoolList[w].getPropPrice();
                //Debug.Log(GameController.getInstance().getGameData()._money);
                string assetPath;
                string assetPathBg;
                Transform propBag = propBagWindow.Instance.getTransform().Find("PropDisplay");
                Transform viewport = propBag.Find("Viewport");
                Transform listContent = viewport.Find("Content");
                if (propBagWindow.Instance.isExist)
                {
                    Transform existedProp = listContent.Find("prop" + w);
                    Transform count = existedProp.Find("count");
                    string text = count.GetComponent<TextMeshProUGUI>().text;
                    int c = int.Parse(text);
                    c += 1;
                    count.GetComponent<TextMeshProUGUI>().text = c.ToString();
                    count.gameObject.SetActive(true);

                }
                else
                {
                    GameObject prop = new GameObject("prop" + w);//背景图
                   
          
                    prop.transform.SetParent(listContent);
                    prop.AddComponent<Image>();
                    //prop.AddComponent<propRightClick>();




                    GameObject item = new GameObject("count");//数量标签
                    item.transform.SetParent(prop.transform);
                    item.AddComponent<TextMeshProUGUI>();
                    item.GetComponent<TextMeshProUGUI>().text = "" + 0;
                    item.SetActive(false);
                    item.transform.localPosition = new Vector3(150f, -40f, 0f);
                    item.GetComponent<TextMeshProUGUI>().fontSize = 40;
                    string fontPath = "Assets/TextMesh Pro/Resources/Fonts & Materials/" + "fontFirst";
                    TMP_FontAsset font = Resources.Load<TMP_FontAsset>(fontPath);
                    item.GetComponent<TMP_Text>().font = font;

                    GameObject image = new GameObject("" + w);//道具图
                    image.transform.SetParent(prop.transform);
                    image.AddComponent<Image>();
                    RectTransform rectProp = prop.GetComponent<RectTransform>();
                    rectProp.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    image.transform.localPosition = new Vector3(0f, 0f, 0f);
                    assetPathBg = "Assets/Sprites/Weapon/" + PropPoolList[w].getPropBgIcon();

                    assetPath = "Assets/Sprites/Prop/" + PropPoolList[w].getPropIcon();

                    loadImage(assetPathBg, prop.transform);
                    loadImage(assetPath, image.transform);


                    image.AddComponent<PropDetailDisplay>();
                    
                    //Debug.Log(image.GetComponent<PropDetailDisplay>().detailDisplay);
                    if(GameObject.Find("DetailPanel")==null)
                    {
                        GameObject obj = Resources.Load<GameObject>("UI/DetailPanel");
                        obj = GameObject.Instantiate(obj);
                        obj.SetActive(false);
                        image.GetComponent<PropDetailDisplay>().detailDisplay = obj;

                    }
                }
            }
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
                transform.Find("Panel").GetComponentInChildren<Text>().text = "角色的等级";
                break;
            case "MaxHealth":
                transform.Find("Panel").GetComponentInChildren<Text>().text = "角色的最大生命值";
                break;
            case "HealthRecovery":
                transform.Find("Panel").GetComponentInChildren<Text>().text = $"每秒回复{0.2+0.1*(_player.GetComponent<CharacterAttribute>().getHealthSteal()-1)}点生命值";
                break;
            case "HealthSteal":
                transform.Find("Panel").GetComponentInChildren<Text>().text = $"每次攻击有{_player.GetComponent<CharacterAttribute>().getHealthSteal()}%的概率回复1点生命值";
                break;
            case "AttackAmplification":
                transform.Find("Panel").GetComponentInChildren<Text>().text = $"造成的伤害增加{_player.GetComponent<CharacterAttribute>().getAttackAmplification()}%";
                break;
            case "MeleeDamage":
                transform.Find("Panel").GetComponentInChildren<Text>().text = $"近战伤害增加{_player.GetComponent<CharacterAttribute>().getMeleeDamage()}";
                break;
            case "RangedDamage":
                transform.Find("Panel").GetComponentInChildren<Text>().text = $"远程伤害增加{_player.GetComponent<CharacterAttribute>().getRangedDamage()}";
                break;
            case "AbilityDamage":
                transform.Find("Panel").GetComponentInChildren<Text>().text = $"属性伤害增加{_player.GetComponent<CharacterAttribute>().getAbilityDamage()}";
                break;
            case "AttackSpeedAmplification":
                transform.Find("Panel").GetComponentInChildren<Text>().text = $"攻击速度增加{_player.GetComponent<CharacterAttribute>().getAttackSpeedAmplification()}%";
                break;
            case "CriticalRate":
                transform.Find("Panel").GetComponentInChildren<Text>().text = $"每次攻击有{_player.GetComponent<CharacterAttribute>().getAttackSpeedAmplification()*100}%的概率产生暴击";
                break;
            case "Engineering":
                transform.Find("Panel").GetComponentInChildren<Text>().text = "提升机器人道具的属性";
                break;
            case "AttackRangeAmplification":
                transform.Find("Panel").GetComponentInChildren<Text>().text = $"攻击范围叠加{_player.GetComponent<CharacterAttribute>().getAttackRangeAmplification()}";
                break;
            case "ArmorStrength":
                transform.Find("Panel").GetComponentInChildren<Text>().text = _player.GetComponent<CharacterAttribute>().getArmorStrength() >= 0 ? 
                                                                              $"减少{_player.GetComponent<CharacterAttribute>().getArmorStrength() * 100 / (_player.GetComponent<CharacterAttribute>().getArmorStrength() + 15)}%受到的伤害" :
                                                                              $"增加{_player.GetComponent<CharacterAttribute>().getArmorStrength() * 2}%受到的伤害";
                break;
            case "DodgeRate":
                transform.Find("Panel").GetComponentInChildren<Text>().text = $"有{_player.GetComponent<CharacterAttribute>().getDodgeRate()}%的概率避免此次伤害";
                break;
            case "MoveSpeedAmplification":
                transform.Find("Panel").GetComponentInChildren<Text>().text = $"移动速度增加{_player.GetComponent<CharacterAttribute>().getMoveSpeedAmplification()}%";
                break;
            case "ScanAccuracy":
                transform.Find("Panel").GetComponentInChildren<Text>().text = "提高高品质武器和道具的刷新概率";
                break;
            case "CollectEfficiency":
                transform.Find("Panel").GetComponentInChildren<Text>().text = $"每一波次结束给予{_player.GetComponent<CharacterAttribute>().getCollectEfficiency()}金矿";
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
