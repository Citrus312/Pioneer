using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class upgradeController : MonoBehaviour
{
    public Color firstLV = new(1f, 1f, 1f, 0.5f);
    public Color secondLV = new(0f, 1f, 0f, 0.5f);
    public Color thirdLV = new(1f, 0.647f, 0f, 0.5f);
    public Color forthLV = new(0.502f, 0f, 0.502f, 0.5f);
    public List<int> selectedCardId;//被选择的卡片id暂存列表
    public float[] probability = new float[4];//按顺序分别代表属性四个等级的概率
    public float[] valueList = new float[4];//被选择的卡片具体添加的数值，和selectedCardId对应
    public List<string> propertyIcon = new();
    public List<string> propertyName = new();
    public List<float> increaseValue = new();

    public void Start()
    {

        selectedCardId = new List<int>(new int[4]);

        cardListInit();
        //每次开启会自动刷新卡池一次
        extractCard();
        for (int i = 0; i < 4; i++)
        {
            int level = calculationLevel();
            valueList[i] = calculateValue(selectedCardId[i],level);
            drawCards(i, selectedCardId[i],level);//i为卡槽序号，ids[i]为被抽取的卡片号,calculationLevel()为按概率抽取到的等级
        }

        //为刷新按钮添加监听事件
        Transform freshBtn = transform.Find("refreshBtn");
        freshBtn.GetComponent<Button>().onClick.AddListener(OnRefreshButtonClicked);

        Transform freshButton = transform.Find("refreshBtn");
        Transform freshMoney = freshButton.Find("freshMoney");
        upgradeWindow.Instance.freshValue = GameController.getInstance().getGameData()._wave;
        freshMoney.GetComponent<TextMeshProUGUI>().text = "  刷新" + " - " + upgradeWindow.Instance.freshValue;

    }

    //所有属性的图标和名称列表
    void cardListInit()
    {
        propertyIcon.Add("生命上限.png");
        propertyIcon.Add("生命回复.png");
        propertyIcon.Add("生命汲取.png");
        propertyIcon.Add("输出增幅.png");
        propertyIcon.Add("近战伤害.png");
        propertyIcon.Add("远程伤害.png");
        propertyIcon.Add("属性伤害.png");
        propertyIcon.Add("攻速加成.png");
        propertyIcon.Add("暴击概率.png");
        propertyIcon.Add("工程机械.png");
        propertyIcon.Add("攻击范围.png");
        propertyIcon.Add("机甲强度.png");
        propertyIcon.Add("闪避概率.png");
        propertyIcon.Add("移速加成.png");
        propertyIcon.Add("扫描精度.png");
        propertyIcon.Add("采集效率.png");

        propertyName.Add("生命上限");
        propertyName.Add("生命回复");
        propertyName.Add("生命汲取");
        propertyName.Add("输出增幅");
        propertyName.Add("近战伤害");
        propertyName.Add("远程伤害");
        propertyName.Add("属性伤害");
        propertyName.Add("攻速加成");
        propertyName.Add("暴击概率");
        propertyName.Add("工程机械");
        propertyName.Add("攻击范围");
        propertyName.Add("机甲强度");
        propertyName.Add("闪避概率");
        propertyName.Add("移速加成");
        propertyName.Add("扫描精度");
        propertyName.Add("采集效率");


    }
    //抽取卡片
    void extractCard()
    {
        int count = 0;
        while (count < 4)
        {
            int randomId = Random.Range(0, 15);
            if (!selectedCardId.Contains(randomId))
            {
                selectedCardId[count] = randomId;
                count++;
            }

        }

    }
    //抽取概率计算并返回抽取后的id
    int calculationLevel()
    {
        float luck = GameController.getInstance().getPlayer().GetComponent<CharacterAttribute>().getScanAccuracy();
        probability[0] = 800f + luck * 2f + GameController.getInstance().getGameData()._wave * 10;
        probability[1] = 400f + luck * 4f + GameController.getInstance().getGameData()._wave * 20;
        probability[2] = GameController.getInstance().getGameData()._wave >= 5 ? (GameController.getInstance().getGameData()._wave - 5) * 40 + luck * 2 : 0;
        probability[3] = GameController.getInstance().getGameData()._wave >= 10 ? (GameController.getInstance().getGameData()._wave - 10) * 50 + luck * 2 : 0;
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
        return index;

    }
    //将卡片内容显示在UI上
    public void drawCards(int i, int id,int level)
    {
        string cardName = "card";
        switch (i)
        {
            case 0:
                cardName = "cardA";
                break;
            case 1:
                cardName = "cardB";
                break;
            case 2:
                cardName = "cardC";
                break;
            case 3:
                cardName = "cardD";
                break;
            default:
                break;
        }
        Transform card = transform.Find(cardName);

        Transform child1 = card.Find("icon");
        Transform child2 = card.Find("nameText");
        Transform child3 = card.Find("valueText");
        Transform child4 = card.Find("upgradeBtn");

        string assetPath = "Assets/Sprites/propertyUpgrade/" + propertyIcon[id];
        loadImage(assetPath, child1);

        TextMeshProUGUI nameText = child2.GetComponent<TextMeshProUGUI>();
        nameText.text = propertyName[id];

        TextMeshProUGUI valueText = child3.GetComponent<TextMeshProUGUI>();
        valueText.text = "+" + calculateValue(id, level);

        switch (level)
        {
            case 0:
                card.GetComponent<Image>().color = firstLV;
                break;
            case 1:
                card.GetComponent<Image>().color = secondLV;
                break;
            case 2:
                card.GetComponent<Image>().color = thirdLV;
                break;
            case 3:
                card.GetComponent<Image>().color = forthLV;
                break;

            default:
                break;
        }

    }
    //计算具体数值
    float calculateValue(int id,int level)
    {
        switch (id)
        {
            case 0:
                return 2 + 2 * level;

            case 1:
                return 2 + 1 * level;

            case 2:
                return 1 + 2 * level;

            case 3:
                return 4 + 2 * level;

            case 4:
                return 1 + 2 * level;

            case 5:
                return 1 + 2 * level;

            case 6:
                return 1 + 2 * level;

            case 7:
                return 3 + 3 * level;

            case 8:
                return 2 + 2 * level;

            case 9:
                return 2 + 1 * level;

            case 10:
                return 10 + 10 * level;

            case 11:
                return 1 + 1 * level;

            case 12:
                return 3 + 2 * level;

            case 13:
                return 2 + 2 * level;

            case 14:
                return 3 + 3 * level;

            case 15:
                return 5 + 5 * level;


            default:
                return 0;
        }
    }
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

    //刷新按钮点击事件
    void OnRefreshButtonClicked()
    {
        if (GameController.getInstance().getGameData()._money < upgradeWindow.Instance.freshValue)
        {
            Debug.Log("金矿不足");
        }
        else
        {
            extractCard();
            for (int i = 0; i < 4; i++)
            {

                int level = calculationLevel();
                valueList[i] = calculateValue(selectedCardId[i], level);
                drawCards(i, selectedCardId[i], level);//i为卡槽序号，ids[i]为被抽取的卡片号,calculationLevel()为按概率抽取到的等级
            }
            upgradeWindow.Instance.freshCount += 1;
            GameController.getInstance().getGameData()._money -= upgradeWindow.Instance.freshValue;

            Transform freshButton = transform.Find("refreshBtn");
            Transform freshMoney = freshButton.Find("freshMoney");
            upgradeWindow.Instance.freshValue = GameController.getInstance().getGameData()._wave + upgradeWindow.Instance.freshCount * (int)Mathf.Ceil(0.5f * GameController.getInstance().getGameData()._wave);
            freshMoney.GetComponent<TextMeshProUGUI>().text = "  刷新" + " - " + upgradeWindow.Instance.freshValue;
        }
        
    }

    //升级按钮点击事件
    public void OnupgradeButtonClicked(int cardId)
    {
        upgradeWindow.Instance.value = valueList[cardId];
        upgradeWindow.Instance.name = propertyName[selectedCardId[cardId]];
        extractCard();
        for (int i = 0; i < 4; i++)
        {

            int level = calculationLevel();
            valueList[i] = calculateValue(selectedCardId[i], level);
            drawCards(i, selectedCardId[i], level);//i为卡槽序号，ids[i]为被抽取的卡片号,calculationLevel()为按概率抽取到的等级
        }
    }
}
