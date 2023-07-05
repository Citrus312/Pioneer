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

    public List<WeaponAttribute> WeaponPropList;//卡池

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

            copyOption.localPosition = new Vector3(150f, 180f, 0f);
        }
    }
    //给按钮添加监听事件
    public void addListener()
    {
        recycleBtn.GetComponent<Button>().onClick.AddListener(recycleOnclick);
        cancelBtn.GetComponent<Button>().onClick.AddListener(cancelOnclick);
        compositeBtn.GetComponent<Button>().onClick.AddListener(compoundOnclick);
    }

    //武器背包中的物品右键点击事件-回收
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

    //武器背包中的物品右键点击事件-合成
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

    //武器背包中的物品右键点击事件-取消
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
