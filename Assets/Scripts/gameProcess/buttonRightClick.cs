using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class buttonRightClick : MonoBehaviour, IPointerClickHandler
{
    Transform option;
    Transform copyOption;
    Dictionary<string, int> namesCount = new Dictionary<string, int>();
    Transform father;
    Transform recycleBtn;
    Transform cancelBtn;
    Transform compositeBtn;

    public List<WeaponAttribute> WeaponPropList;//����

    private void Start()
    {
        option = weaponBagWindow.Instance.getTransform().Find("option");
        copyOption = Instantiate(option);
        copyOption.SetParent(this.transform);
        father = transform.parent;
        compositeBtn = copyOption.Find("compositeBtn");
        recycleBtn = copyOption.Find("recycleBtn");
        cancelBtn = copyOption.Find("cancelBtn");
        WeaponPropList = JsonLoader.weaponPool.GetRange(0, JsonLoader.weaponPool.Count);
        addListener();

    }
    public void OnPointerClick(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            copyOption.gameObject.SetActive(true);
            foreach (Transform child in father)
            {
                if (!namesCount.ContainsKey(child.name))
                {
                    namesCount[child.name] = 1;
                }
                else
                {
                    namesCount[child.name]++;
                }
            }
            if (namesCount[transform.name] > 1)
            {
                compositeBtn.gameObject.SetActive(true);
            }
            else
            {
                compositeBtn.gameObject.SetActive(false);
            }

            copyOption.localPosition = new Vector3(0f, 150f, 0f);
            namesCount.Clear();
        }
    }
    //����ť���Ӽ����¼�
    public void addListener()
    {
        recycleBtn.GetComponent<Button>().onClick.AddListener(recycleOnclick);
        cancelBtn.GetComponent<Button>().onClick.AddListener(cancelOnclick);
        compositeBtn.GetComponent<Button>().onClick.AddListener(compoundOnclick);
    }

    //���������е���Ʒ�Ҽ�����¼�-����
    public void recycleOnclick()
    {
        Destroy(this.gameObject);
        string n = new string(transform.name.Where(char.IsDigit).ToArray());
        int ID = int.Parse(n);
        GameController.getInstance().getGameData()._weaponList.Remove(ID);
        weaponBagWindow.Instance.ownWeaponList.Remove(ID);
        GameController.getInstance().getGameData()._money += Mathf.Ceil(JsonLoader.weaponPool[ID].getWeaponPrice() * 0.75f);
        if (weaponBagWindow.Instance.addWeapon == false)
        {
            weaponBagWindow.Instance.addWeapon = true;
        }
    }

    //���������е���Ʒ�Ҽ�����¼�-�ϳ�
    public void compoundOnclick()
    {
        int count = 0;
        string number = new string(transform.name.Where(char.IsDigit).ToArray());
        int id = int.Parse(number);

        if (id % 4 < 3)
        {
            foreach (Transform child in father)
            {
                if (child.name == transform.name)
                {
                    count++;
                }
                if (count == 2)
                {
                    Destroy(child.gameObject);
                    endofComposite(id);
                    break;
                }
            }
        }
        copyOption.gameObject.SetActive(false);

    }

    //���������е���Ʒ�Ҽ�����¼�-ȡ��
    public void cancelOnclick()
    {
        copyOption.gameObject.SetActive(false);
    }


    void endofComposite(int id)
    {
        int i = id + 1;
        string assetPathBg = "Assets/Sprites/Weapon/" + WeaponPropList[i].getWeaponBgIcon();
        transform.name = "weapon" + i;
        loadImage(assetPathBg, transform);

        weaponBagWindow.Instance.ownWeaponList.Remove(id);
        weaponBagWindow.Instance.ownWeaponList.Remove(id);
        weaponBagWindow.Instance.ownWeaponList.Add(i);
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
