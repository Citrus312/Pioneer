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
    public List<int> selectedCardId;//��ѡ��Ŀ�Ƭid�ݴ��б�
    public float[] probability = new float[4];//��˳��ֱ���������ĸ��ȼ��ĸ���
    public float[] valueList = new float[4];//��ѡ��Ŀ�Ƭ������ӵ���ֵ����selectedCardId��Ӧ
    public List<string> propertyIcon = new();
    public List<string> propertyName = new();
    public List<float> increaseValue = new();

    public void Start()
    {
        //ÿ�ο������Զ�ˢ�¿���һ��
        extractCard();
        for (int i = 0; i < 4; i++)
        {
            int level = calculationLevel();
            valueList[i] = calculateValue(selectedCardId[i],level);
            drawCards(i, selectedCardId[i],level);//iΪ������ţ�ids[i]Ϊ����ȡ�Ŀ�Ƭ��,calculationLevel()Ϊ�����ʳ�ȡ���ĵȼ�
        }

        //Ϊˢ�°�ť��Ӽ����¼�
        Transform freshBtn = transform.Find("refreshBtn");
        freshBtn.GetComponent<Button>().onClick.AddListener(OnRefreshButtonClicked);

    }

    //�������Ե�ͼ��������б�
    void cardListInit()
    {
        propertyIcon.Add("��������.png");
        propertyIcon.Add("�����ظ�.png");
        propertyIcon.Add("������ȡ.png");
        propertyIcon.Add("�������.png");
        propertyIcon.Add("��ս�˺�.png");
        propertyIcon.Add("Զ���˺�.png");
        propertyIcon.Add("�����˺�.png");
        propertyIcon.Add("���ټӳ�.png");
        propertyIcon.Add("��������.png");
        propertyIcon.Add("���̻�е.png");
        propertyIcon.Add("������Χ.png");
        propertyIcon.Add("����ǿ��.png");
        propertyIcon.Add("���ܸ���.png");
        propertyIcon.Add("���ټӳ�.png");
        propertyIcon.Add("ɨ�辫��.png");
        propertyIcon.Add("�ɼ�Ч��.png");

        propertyName.Add("��������");
        propertyName.Add("�����ظ�");
        propertyName.Add("������ȡ");
        propertyName.Add("�������");
        propertyName.Add("��ս�˺�");
        propertyName.Add("Զ���˺�");
        propertyName.Add("�����˺�");
        propertyName.Add("���ټӳ�");
        propertyName.Add("��������");
        propertyName.Add("���̻�е");
        propertyName.Add("������Χ");
        propertyName.Add("����ǿ��");
        propertyName.Add("���ܸ���");
        propertyName.Add("���ټӳ�");
        propertyName.Add("ɨ�辫��");
        propertyName.Add("�ɼ�Ч��");


    }
    //��ȡ��Ƭ
    void extractCard()
    {
        int count = 0;
        while (count < 4)
        {
            int randomId = Random.Range(0,15);
            if (!selectedCardId.Contains(randomId))
            {
                selectedCardId[count] = randomId;
                count++;
            }

        }

    }
    //��ȡ���ʼ��㲢���س�ȡ���id
    int calculationLevel()
    {
        float luck = 50f;//��ʱ�滻�ɽ�ɫ������
        probability[0] = 400f + luck * 0.1f;
        probability[1] = 300f + luck * 0.2f;
        probability[2] = 200f + luck * 0.3f;
        probability[3] = 100f + luck * 0.4f;
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
    //����Ƭ������ʾ��UI��
    public void drawCards(int i, int id,int level)
    {
        string cardName = "card";
        switch (i)
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

        Transform child1 = card.Find("icon");
        Transform child2 = card.Find("nameText");
        Transform child3 = card.Find("valueText");
        Transform child4 = card.Find("upgradeBtn");

        string assetPath = "Assets/Sprites/propertyUpgrade/" + propertyIcon[id];
        loadImage(assetPath, child1);

        TextMeshProUGUI nameText = child2.GetComponent<TextMeshProUGUI>();
        nameText.text = propertyName[id];

        TextMeshProUGUI valueText = child3.GetComponent<TextMeshProUGUI>();
        valueText.text = "+" + calculateValue(id,level);


    }
    //���������ֵ
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

    //ˢ�°�ť����¼�
    void OnRefreshButtonClicked()
    {
        extractCard();
        for (int i = 0; i < 4; i++)
        {

            int level = calculationLevel();
            valueList[i] = calculateValue(selectedCardId[i], level);
            drawCards(i, selectedCardId[i], level);//iΪ������ţ�ids[i]Ϊ����ȡ�Ŀ�Ƭ��,calculationLevel()Ϊ�����ʳ�ȡ���ĵȼ�
        }
    }

    //������ť����¼�
    void OnupgradeButtonClicked(int cardId)
    {
        extractCard();
        for (int i = 0; i < 4; i++)
        {

            int level = calculationLevel();
            valueList[i] = calculateValue(selectedCardId[i], level);
            drawCards(i, selectedCardId[i], level);//iΪ������ţ�ids[i]Ϊ����ȡ�Ŀ�Ƭ��,calculationLevel()Ϊ�����ʳ�ȡ���ĵȼ�
        }

        upgradeWindow.Instance.value = valueList[cardId];
        upgradeWindow.Instance.name = propertyName[selectedCardId[cardId]];
    }
}
