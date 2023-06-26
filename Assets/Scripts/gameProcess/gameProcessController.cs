using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class gameProcessController : PersistentSingleton<PausePageController>
{
    public GameObject player;
    public propertyWindow propertyWindow;
    //public propBagWindow propBagWindow;
    //public weaponBagWindow weaponBagWindow;
    public roleStateWindow roleStateWindow;
    public titleWindow titleWindow;
    public countDownTimerWindow countDownTimerWindow;
    public upgradeWindow upgradeWindow;
    public storeWindow storeWindow;
    public int grade;//等级
    public int blood;//血量
    public int experience;//经验值
    public int level=1;//关卡数
    public TextMeshProUGUI levelText;//关卡显示文本
    public List<WeaponAttribute> WeaponPropList;//卡池
    public List<PropAttribute> PropPoolList;


    //倒计时需要用到的变量
    public float currentTime;
    public float totalTime;
    public TextMeshProUGUI timeText;
    void Start()
    {
        JsonLoader.LoadAndDecodePropConfig();
        JsonLoader.LoadAndDecodeWeaponConfig();
        WeaponPropList = JsonLoader.weaponPool;
        PropPoolList = JsonLoader.propPool;

        //初始化窗口
        origin();

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
    }

    //窗口初始化
    void origin()
    {
        
        UIRoot.Init();
        propertyWindow = new();
        //propBagWindow = new();
        //weaponBagWindow = new();
        roleStateWindow = new();
        titleWindow = new();
        countDownTimerWindow = new();
        //upgradeWindow = new();
        storeWindow = new();

        roleStateWindow.Open();
        countDownTimerWindow.Open();
        titleWindow.Open();

        totalTime = 5f;
        Transform countDownTimer = countDownTimerWindow.getTransform().Find("countDownTimer");
        timeText = countDownTimer.GetComponent<TextMeshProUGUI>();

        Transform titleText = titleWindow.getTransform().Find("title");
        levelText = titleText.GetComponent<TextMeshProUGUI>();

    }

    //获取游戏人物的属性
    private string getAttribute(GameObject _player)
    {
        string content = "Health : " + _player.GetComponent<CharacterAttribute>().getMaxHealth() +
                             "\nSpeed : " + _player.GetComponent<CharacterAttribute>().getMoveSpeed();
        return content;
    }

    //倒计时计算
    void updateCountDownTimer()
    {
        Debug.Log(currentTime);
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
        Debug.Log(currentTime);
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
        grade = 2;
        Transform endText = countDownTimerWindow.getTransform().Find("endText");
        endText.gameObject.SetActive(true);
        StartCoroutine(wait());
        endText.gameObject.SetActive(false);
        currentTime = -1;
        Debug.Log(grade);
        if (grade > 0)
        {
            Debug.Log("升级");
            Time.timeScale = 0f;
            timeText.text = "<color=white>升级</color>";
            upgradeWindow.Open();
            addListenerForupgrade();
            propertyWindow.Open();
        }
        else
        {
            Debug.Log("商店");
            weaponBagWindow.Instance.Open();
            propBagWindow.Instance.Open();
            storeWindow.Open();
            addListenerForstartBtn();
            addListenerForBuy();
            
            
            propertyWindow.Open();
            roleStateWindow.Close();
            titleWindow.Close();
            countDownTimerWindow.Close();
        }
    }

    //为升级按钮添加监听事件
    void addListenerForupgrade()
    {
        Transform child1 = upgradeWindow.getTransform().Find("cardA");
        Transform child2 = upgradeWindow.getTransform().Find("cardB");
        Transform child3 = upgradeWindow.getTransform().Find("cardC");
        Transform child4 = upgradeWindow.getTransform().Find("cardD");

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
        grade--;
        if(grade==0)
        {
            upgradeWindow.Close();
            weaponBagWindow.Instance.Open();
            propBagWindow.Instance.Open();
            storeWindow.Open();
            addListenerForstartBtn();
            addListenerForBuy();
            
            
            roleStateWindow.Close();
            titleWindow.Close();
            countDownTimerWindow.Close();
        }
    }
    //为出发按钮设置监听事件
    void addListenerForstartBtn()
    {
        Transform startBtn = storeWindow.getTransform().Find("startButton");
        startBtn.GetComponent<Button>().onClick.AddListener(startBtnOnclick);
    }

    //出发按钮事件
    void startBtnOnclick()
    {
        Debug.Log("开始下一关");
        Time.timeScale = 1f;
        storeWindow.Close();
        propBagWindow.Instance.Close();
        weaponBagWindow.Instance.Close();
        propertyWindow.Close();
        roleStateWindow.Open();
        titleWindow.Open();
        countDownTimerWindow.Open();

        //血量、经验、等级、倒计时、关卡重置
        totalTime = totalTime + 10 >= 90 ? 90 : totalTime + 10;
        timeDisplay();
        
        level++;
        levelText.text = "第" + level + "关";

        grade = 2;

    }

    //为购买按钮设置监听事件
    void addListenerForBuy()
    {
        Transform card1 = storeWindow.getTransform().Find("card_a");
        Transform card2 = storeWindow.getTransform().Find("card_b");
        Transform card3 = storeWindow.getTransform().Find("card_c");
        Transform card4 = storeWindow.getTransform().Find("card_d");

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
}
