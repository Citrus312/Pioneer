using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class textController : MonoBehaviour
{
    public List<WeaponAttribute> WeaponPropList;//武器库
    public List<PropAttribute> PropPoolList;//道具库
    public List<int> selectedCardId;//被选择的卡片id暂存列表
    public List<int> lockedCardIndex;//被锁定的卡片列表
    public List<bool> isLocked ;
    public Color normalColor = new(1f, 1f, 1f, 0.5f);
    public Color highLightColor = new(0f, 0f, 0f, 0.5f);


    public float luck ;
    public float[] probability = new float[8];//按顺序分别代表武器和道具的四个等级的概率

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

        

        //加载json文件将数据放入卡池
        JsonLoader.LoadAndDecodeWeaponConfig();
        JsonLoader.LoadAndDecodePropConfig();
        WeaponPropList = JsonLoader.weaponPool;
        PropPoolList = JsonLoader.propPool;


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
            int randomId = calculation();
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

    //抽取概率计算并返回抽取后的id
    int calculation()
    {
        int randomId = 0;
        luck = 50f;//到时替换成角色的属性
        probability[0] = 400f + luck * 0.1f;
        probability[1] = 300f + luck * 0.2f;
        probability[2] = 200f + luck * 0.3f;
        probability[3] = 100f + luck * 0.4f;
        probability[4] = 400f + luck * 0.1f;
        probability[5] = 300f + luck * 0.2f;
        probability[6] = 200f + luck * 0.3f;
        probability[7] = 100f + luck * 0.4f;

        //归一化
        float sum = 0;
        for (int i = 0; i < probability.Length; i++)
        {
            sum += probability[i];
        }
        for (int i = 0; i < probability.Length; i++)
        {
            probability[i] = probability[i] / sum;
        }

        float randomValue = Random.Range(0f, 1f);
        float cumulativeProbability = 0f;
        int index = 0;
        for (int i = 0; i < probability.Length; i++)
        {
            cumulativeProbability += probability[i];
            if (cumulativeProbability >= randomValue)
            {
                index = i;
                break;
            }
        }

        int kindOfWeapon = WeaponPropList.Count / 4;
        int kindOfProp = PropPoolList.Count / 4;
        int temp1 = Random.Range(0, kindOfWeapon - 1);
        int temp2= Random.Range(0, kindOfProp - 1);
        switch (index)
        {
            case 0:
                randomId = 4 * temp1 + 0;
                break;
            case 1:
                randomId = 4 * temp1 + 1;
                break;
            case 2:
                randomId = 4 * temp1 + 2;
                break;
            case 3:
                randomId = 4 * temp1 + 3;
                break;
            case 4:
                randomId = 4 * temp2 + 40000;
                break;
            case 5:
                randomId = 4 * temp2 + 40001;
                break;
            case 6:
                randomId = 4 * temp2 + 40002;
                break;
            case 7:
                randomId = 4 * temp2 + 40003;
                break;
            default:
                break;
        }

        return randomId;
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
        if(id<40000)
        {

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
            if (WeaponPropList[id].getWeaponDamageType() == WeaponAttribute.WeaponDamageType.Melee)
            {
                assetPath2 = "Assets/Sprites/Weapon/" + "Melee Weapon/" + WeaponPropList[id].getWeaponIcon();
            }
            else if (WeaponPropList[id].getWeaponDamageType() == WeaponAttribute.WeaponDamageType.Ranged)
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
        else
        {
            id = id - 40000;
            //道具属性文本
            TextMeshProUGUI myText = child1.GetComponent<TextMeshProUGUI>();
            float[] value = new float[16];
            Debug.Log(id);
            Debug.Log(PropPoolList[id].getMaxHealth());
            value[0] = PropPoolList[id].getMaxHealth();
            value[1] = PropPoolList[id].getHealthRecovery();
            value[2] = PropPoolList[id].getHealthSteal();
            value[3] = PropPoolList[id].getAttackAmplification();
            value[4] = PropPoolList[id].getMeleeDamage();
            value[5] = PropPoolList[id].getRangedDamage();
            value[6] = PropPoolList[id].getAbilityDamage();
            value[7] = PropPoolList[id].getAttackSpeedAmplification();
            value[8] = PropPoolList[id].getCriticalRate();
            value[9] = PropPoolList[id].getEngineering();
            value[10] = PropPoolList[id].getAttackRangeAmplification();
            value[11] = PropPoolList[id].getArmorStrength();
            value[12] = PropPoolList[id].getDodgeRate();
            value[13] = PropPoolList[id].getMoveSpeedAmplification();
            value[14] = PropPoolList[id].getScanAccuracy();
            value[15] = PropPoolList[id].getCollectEfficiency();
            string[] propText = new string[16];
            string[] propName = new string[16];
            propName[0] = "生命上限";
            propName[1] = "生命回复";
            propName[2] = "生命汲取";
            propName[3] = "输出增幅";
            propName[4] = "近战伤害";
            propName[5] = "远程伤害";
            propName[6] = "属性伤害";
            propName[7] = "攻速加成";
            propName[8] = "暴击概率";
            propName[9] = "工程机械";
            propName[10] = "攻击范围";
            propName[11] = "机甲强度";
            propName[12] = "闪避概率";
            propName[13] = "移速加成";
            propName[14] = "扫描精度";
            propName[15] = "采集效率";
            for (int count = 0; count < 16; count++)
            {
                if (value[count] == 0.0f)
                    propText[count] = "";
                else if(value[count]>0.0f)
                {
                    propText[count] = "<color=yellow>" + propName[count] + "</color>:  +" + value[count] + "\n";
                }
            }
            myText.text = propText[0] + propText[1] + propText[2] + propText[3] + propText[4] + propText[5] + propText[6] + propText[7] + propText[8] + propText[9]
                            + propText[10] + propText[11] + propText[12] + propText[13] + propText[14] + propText[15];

            string assetPath1 = "Assets/Sprites/Weapon/" + PropPoolList[id].getPropBgIcon();
            string assetPath2 = "Assets/Sprites/Prop/" + PropPoolList[id].getPropIcon();
            

           
            loadImage(assetPath1, child2);
            loadImage(assetPath2, child2_);


            //武器名字介绍文本
            TextMeshProUGUI myText1 = child3.GetComponent<TextMeshProUGUI>();
            myText1.text = PropPoolList[id].getPropName();



            //购买按钮文本显示
            TextMeshProUGUI myText2 = child4_child.GetComponent<TextMeshProUGUI>();
            myText2.text =""+ PropPoolList[id].getPropPrice();
        }
          

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
        if(selectedCardId[cardID]<40000) //40000以下的武器，40000以上的是道具
        {
            weaponBagWindow.Instance.isWeapon = true;
            if (weaponBagWindow.Instance.ownWeaponList.Count == 6)
            {
                Debug.Log("装备武器已达上限，购买失败");
                weaponBagWindow.Instance.addWeapon = false;
            }
            else
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
                Debug.Log("购买成功");
                weaponBagWindow.Instance.buyedWeapon = selectedCardId[cardID];
                weaponBagWindow.Instance.ownWeaponList.Add(selectedCardId[cardID]);
            }
        }
        else
        {
            weaponBagWindow.Instance.isWeapon = false;
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
            Debug.Log("购买成功");
            propBagWindow.Instance.buyedProp = selectedCardId[cardID];
            propBagWindow.Instance.ownPropList.Add(selectedCardId[cardID]);

        }

    }
}
