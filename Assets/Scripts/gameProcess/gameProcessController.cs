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
    public int grade;//�ȼ�
    public int blood;//Ѫ��
    public int experience;//����ֵ
    public int level=1;//�ؿ���
    public TextMeshProUGUI levelText;//�ؿ���ʾ�ı�
    public List<WeaponAttribute> WeaponPropList;//����
    public List<PropAttribute> PropPoolList;


    //����ʱ��Ҫ�õ��ı���
    public float currentTime;
    public float totalTime;
    public TextMeshProUGUI timeText;
    void Start()
    {
        JsonLoader.LoadAndDecodePropConfig();
        JsonLoader.LoadAndDecodeWeaponConfig();
        WeaponPropList = JsonLoader.weaponPool;
        PropPoolList = JsonLoader.propPool;

        //��ʼ������
        origin();

        //����ʱ��ʾ
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

    //���ڳ�ʼ��
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

    //��ȡ��Ϸ���������
    private string getAttribute(GameObject _player)
    {
        string content = "Health : " + _player.GetComponent<CharacterAttribute>().getMaxHealth() +
                             "\nSpeed : " + _player.GetComponent<CharacterAttribute>().getMoveSpeed();
        return content;
    }

    //����ʱ����
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

    //����ʱ��ʾ
    void timeDisplay()
    {
        currentTime = totalTime;
        Debug.Log(currentTime);
        InvokeRepeating("updateCountDownTimer", 1f, 1f);
    }

    //Э��
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.5f);
    }

    //����ʱ��������¼�
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
            Debug.Log("����");
            Time.timeScale = 0f;
            timeText.text = "<color=white>����</color>";
            upgradeWindow.Open();
            addListenerForupgrade();
            propertyWindow.Open();
        }
        else
        {
            Debug.Log("�̵�");
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

    //Ϊ������ť��Ӽ����¼�
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

    //������ť�¼�
    void upgradeBtnOnclik()
    {
        Debug.Log("�����ɹ�");
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
    //Ϊ������ť���ü����¼�
    void addListenerForstartBtn()
    {
        Transform startBtn = storeWindow.getTransform().Find("startButton");
        startBtn.GetComponent<Button>().onClick.AddListener(startBtnOnclick);
    }

    //������ť�¼�
    void startBtnOnclick()
    {
        Debug.Log("��ʼ��һ��");
        Time.timeScale = 1f;
        storeWindow.Close();
        propBagWindow.Instance.Close();
        weaponBagWindow.Instance.Close();
        propertyWindow.Close();
        roleStateWindow.Open();
        titleWindow.Open();
        countDownTimerWindow.Open();

        //Ѫ�������顢�ȼ�������ʱ���ؿ�����
        totalTime = totalTime + 10 >= 90 ? 90 : totalTime + 10;
        timeDisplay();
        
        level++;
        levelText.text = "��" + level + "��";

        grade = 2;

    }

    //Ϊ����ť���ü����¼�
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

    //����ť�¼�
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

    //����ͼƬ
    void loadImage(string assetPath, Transform child)
    {
        byte[] bytes = System.IO.File.ReadAllBytes(assetPath);

        Texture2D texture = new Texture2D(2, 2);
        if (texture.LoadImage(bytes))
        {
            // ����Sprite�����ӵ�Image�����
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
            child.GetComponent<Image>().sprite = sprite;
            //RectTransform size = child.GetComponent<RectTransform>();
            //size.sizeDelta = new Vector2(50, 50);

            //Debug.Log("�ɹ�����ͼƬ: ");
        }
        else
        {
            //Debug.Log("�޷���ȡ�ļ�: ");
        }
    }
}
