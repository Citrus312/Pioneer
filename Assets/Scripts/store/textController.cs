using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class textController : MonoBehaviour
{
    public List<WeaponAttribute> WeaponPropList;//卡池
    public List<int> selectedCardId;//被选择的卡片id暂存列表
    public List<int> lockedCardIndex;//被锁定的卡片列表
    public List<bool> isLocked ;
    public Color normalColor = new(1f, 1f, 1f, 0.5f);
    public Color highLightColor = new(0f, 0f, 0f, 0.5f);
    Transform weaponBag;
    Transform propBag;

    public void Start()
    {
        //初始化
        isLocked = new List<bool>(new bool[4]);

        lockedCardIndex = new List<int>(new int[4]);
        selectedCardId = new List<int>(new int[4]);
        for (int i=0;i<4;i++)
        {
            lockedCardIndex[i] = -1;
            selectedCardId[i] = -1;
        }

        weaponBag = transform.Find("weaponBag");
        propBag = transform.Find("propBag");

        //加载json文件将数据放入卡池
        JsonLoader.LoadAndDecodeWeaponConfig();
        WeaponPropList = JsonLoader.weaponPool;


        //每回合会自动刷新卡池一次
        extractCard();
        for(int i=0;i<4;i++)
        {
            drawCards(i,selectedCardId[i]);//i为卡槽序号，ids[i]为被抽取的卡片号
        }

    }
    //抽取卡片id
    void extractCard()
    {
        //List<int> ids = new(4);
        int count = 0;
        for(int i=0;i<4;i++)
        {
            if(lockedCardIndex[i]!=-1)
            {
                selectedCardId[i] = lockedCardIndex[i];
                count++;
            }
        }
        while(count<4)
        {
            int randomId = Random.Range(0, WeaponPropList.Count);
            if(!selectedCardId.Contains(randomId))
            {
                for(int k=0;k<4;k++)
                {
                    if(selectedCardId[k]==-1)
                    {
                        selectedCardId[k] = randomId;
                        count++;
                        break;
                    }
                }
            }
            
        }
       
    }


    //将卡片内容显示在UI上
    public void drawCards(int i,int id)
    {
        string cardName="card";
        switch(i)
        {
            case 0:
                cardName = "card_a";
                break;
            case 1:
                cardName = "card_b";
                break;
            case 2:
                cardName = "card_c";
                break;
            case 3:
                cardName = "card_d";
                break;
            default:
                break;
        }
        Transform card = transform.Find(cardName);
       
        //获取子物体
        Transform child1 = card.Find("propText");
        Transform child2 = card.Find("bgIcon");
        Transform child2_ = card.Find("icon");
        Transform child3 = card.Find("titleText");
        Transform child4 = card.Find("Button_shop");
        Transform child4_child = child4.Find("buttonText");
        //武器属性文本
        TextMeshProUGUI myText = child1.GetComponent<TextMeshProUGUI>();
        myText.text = "<color=yellow>伤害</color>:  " + WeaponPropList[id].getWeaponDamage() + "\n"
            + "<color=yellow>范围</color>:  " + WeaponPropList[id].getAttackRange() + " | " + WeaponPropList[id].getRawAttackRange() + "\n"
            + "<color=yellow>转化率</color>:  " + WeaponPropList[id].getConvertRatio() + "\n"
            + "<color=yellow>暴击</color>:  " + WeaponPropList[id].getCriticalBonus() + "(" + WeaponPropList[id].getCriticalRate() * 100 + "%)\n"
            + "<color=yellow>攻速</color>:  " + WeaponPropList[id].getAttackSpeed() + "S\n";

        string assetPath1 = "Assets/Sprites/Weapon/" + WeaponPropList[id].getWeaponBgIcon();
        string assetPath2 = "";
        //武器图片
        if (WeaponPropList[id].getWeaponDamageType()==WeaponAttribute.WeaponDamageType.Melee)
        {
            assetPath2 = "Assets/Sprites/Weapon/" +"Melee Weapon/"+ WeaponPropList[id].getWeaponIcon();
        }
        else if(WeaponPropList[id].getWeaponDamageType() == WeaponAttribute.WeaponDamageType.Ranged)
        {
            assetPath2 = "Assets/Sprites/Weapon/" + "Ranged Weapon/" + WeaponPropList[id].getWeaponIcon();
        }
        else 
        {
            assetPath2 = "Assets/Sprites/Weapon/" + "Ability Weapon/" + WeaponPropList[id].getWeaponIcon();
        }
        
        //string assetPath2 = "Assets/Sprites/Weapon/" + WeaponPropList[id].getWeaponIcon();
        loadImage(assetPath1, child2);
        loadImage(assetPath2, child2_);


        //武器名字介绍文本
        TextMeshProUGUI myText1 = child3.GetComponent<TextMeshProUGUI>();
        myText1.text = WeaponPropList[id].getWeaponName();



        //购买按钮文本显示
        TextMeshProUGUI myText2 = child4_child.GetComponent<TextMeshProUGUI>();
        myText2.text = "" + WeaponPropList[id].getWeaponPrice();
    }


    //加载图片资源
    void loadImage(string assetPath,Transform child)
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


    //刷新按钮点击事件
   public void OnRefreshButtonClicked()
    {
        //清空卡槽列表
        for (int i = 0; i < 4; i++)
        {
            selectedCardId[i] = -1;
        }

        //确保将每个卡片显示出来，消除上一个阶段购买产生的影响
        Transform child1 = transform.Find("card_a");
        Transform child2 = transform.Find("card_b");
        Transform child3 = transform.Find("card_c");
        Transform child4 = transform.Find("card_d");

        child1.gameObject.SetActive(true);
        child2.gameObject.SetActive(true);
        child3.gameObject.SetActive(true);
        child4.gameObject.SetActive(true);

        //将按钮的颜色调为初始值
        Transform Button1 = child1.Find("Button_shop");
        Transform Button2 = child2.Find("Button_shop");
        Transform Button3 = child3.Find("Button_shop");
        Transform Button4 = child4.Find("Button_shop");

        Image image1 = Button1.GetComponent<Image>();
        Image image2 = Button2.GetComponent<Image>();
        Image image3 = Button3.GetComponent<Image>();
        Image image4 = Button4.GetComponent<Image>();

        image1.color = normalColor;
        image2.color = normalColor;
        image3.color = normalColor;
        image4.color = normalColor;

        extractCard();//抽取初始刷新的卡片id
        for (int i = 0; i < 4; i++)
        {
            drawCards(i, selectedCardId[i]);//i为卡槽序号，ids[i]为被抽取的卡片号
        }
        
        
    }

    //锁定按钮点击事件
    public void OnCardLockedButtonClicked(int cardID)
    {
        string cardName = "card";
        switch (cardID)
        {
            case 0:
                cardName = "card_a";
                break;
            case 1:
                cardName = "card_b";
                break;
            case 2:
                cardName = "card_c";
                break;
            case 3:
                cardName = "card_d";
                break;
            default:
                break;
        }
        GameObject card = GameObject.Find(cardName);
        Transform lockIcon = card.transform.Find("lockIcon");

        isLocked[cardID] = !isLocked[cardID];
        if (isLocked[cardID])
        {

            lockedCardIndex[cardID] = selectedCardId[cardID];
            lockIcon.gameObject.SetActive(true);
        }
            
        else
        {

            lockedCardIndex[cardID] = -1;
            lockIcon.gameObject.SetActive(false);
        }
    }


    //购买按钮点击事件
    public void OnCardShopButtonClicked(int cardID)
    {
        string cardName = "card";
        switch (cardID)
        {
            case 0:
                cardName = "card_a";
                break;
            case 1:
                cardName = "card_b";
                break;
            case 2:
                cardName = "card_c";
                break;
            case 3:
                cardName = "card_d";
                break;
            default:
                break;
        }
        GameObject card = GameObject.Find(cardName);
        card.SetActive(false);

        GameObject weapon = new GameObject("weapon");
        weapon.transform.SetParent(weaponBag.transform);
        weapon.AddComponent<Image>();
        RectTransform rectWeapon = weapon.GetComponent<RectTransform>();
        rectWeapon.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        string assetPath = "Assets/Sprites/Weapon/" + WeaponPropList[selectedCardId[cardID]].getWeaponIcon();
        loadImage(assetPath, weapon.transform);

    }
}
