using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleAndWeaponSelectWindow : BaseWindow
{
    private static RoleAndWeaponSelectWindow instance;
    //用于角色选择滚动窗口显示的内容
    public List<GameObject> roleContentList = new();
    //用于武器选择滚动窗口显示的内容
    public List<GameObject> weaponContentList = new();
    //用于显示鼠标当前指到的角色具体信息的区域
    public Transform roleDisplay;
    //用于显示鼠标当前指到的武器具体信息的区域
    public Transform weaponDisplay;
    //角色选择状态
    public bool isSelectRole = false;

    //初始化窗体参数
    private RoleAndWeaponSelectWindow()
    {
        resName = "UI/RoleAndWeaponSelectWindow";
        isResident = true;
        isVisible = false;
        selfType = WindowType.RoleAndWeaponSelectWindow;
        sceneType = SceneType.Select;
        //加载角色和武器数据配置，用于调试
        if (JsonLoader.rolePool.Count == 0)
        {
            JsonLoader.LoadAndDecodeRoleConfig();
        }
        if (JsonLoader.weaponPool.Count == 0)
        {
            JsonLoader.LoadAndDecodeWeaponConfig();
        }
        if (JsonLoader.propPool.Count == 0)
        {
            JsonLoader.LoadAndDecodePropConfig();
        }
        //往角色选择内容中添加相应数量的按钮
        for (int i = 0; i < JsonLoader.rolePool.Count; i++)
        {
            GameObject obj = new GameObject($"{i}");
            Button btn = obj.AddComponent<Button>();
            //按钮的image组件需要另外添加
            btn.image = btn.gameObject.AddComponent<Image>();
            roleContentList.Add(obj);
        }
        //等待按钮添加完成后，给按钮赋予对应角色的图标
        DelayToInvoke.DelayToInvokeByFrame(() =>
        {
            for (int i = 0; i < roleContentList.Count; i++)
            {
                Button btn = roleContentList[i].GetComponent<Button>();
                ImageLoader.LoadImage("Assets/Sprites/Player/" + JsonLoader.rolePool[i].getIcon(), btn.image);
            }
        }, 3);
        //等待以上操作完成后给每个按钮添加上控制脚本
        DelayToInvoke.DelayToInvokeByFrame(() =>
        {
            for (int i = 0; i < roleContentList.Count; i++)
            {
                roleContentList[i].AddComponent<DisplayRoleAndWeaponDetail>();
            }
        }, 6);
    }
    //实例的自动属性
    public static RoleAndWeaponSelectWindow Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new();
            }
            return instance;
        }
    }

    protected override void AwakeWindow()
    {
        base.AwakeWindow();
        //在窗体创建完成后再获取窗体的UI组件
        roleDisplay = transform.Find("RoleDisplay");
        weaponDisplay = transform.Find("WeaponDisplay");
    }
    protected override void FillTextContent()
    {
        base.FillTextContent();
    }
    protected override void OnAddListener()
    {
        base.OnAddListener();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    protected override void OnRemoveListener()
    {
        base.OnRemoveListener();
    }
    protected override void RegisterUIEvent()
    {
        base.RegisterUIEvent();
        //等待上述对按钮的操作完成后为每个按钮绑定点击事件
        DelayToInvoke.DelayToInvokeByFrame(() =>
        {
            for (int i = 0; i < roleContentList.Count; i++)
            {
                Button btn = roleContentList[i].GetComponent<Button>();
                btn.onClick.AddListener(() => { OnSelectBtn(btn); });
            }
        }, 6);
    }
    protected override void Update()
    {
        base.Update();
    }
    //显示角色滚动窗口的内容
    public void DisplayRoleScrollContent()
    {
        //将角色选择内容列表里的游戏物体依次挂载到滚动区域的Content子物体下
        foreach (GameObject obj in roleContentList)
        {
            if (transform != null)
            {
                obj.transform.SetParent(transform.Find("ScrollSelectArea").GetChild(0).GetChild(0));
                Image img = obj.GetComponent<Image>();
                img.color = new Color(img.color.r, img.color.g, img.color.b, 1f);
            }
        }
    }
    //显示武器滚动窗口的内容
    public void DisplayWeaponScrollContent()
    {
        //将武器选择内容列表里的游戏物体依次挂载到滚动区域的Content子物体下
        foreach (GameObject obj in weaponContentList)
        {
            if (transform != null)
            {
                obj.transform.SetParent(transform.Find("ScrollSelectArea").GetChild(0).GetChild(0));
            }
        }
    }
    //选择完角色后用于获取选定角色可用的武器
    public List<int> FetchUsableWeapon()
    {
        //获取选定角色可以使用的武器分类
        List<WeaponAttribute.WeaponCategory> list = JsonLoader.rolePool[GameController.getInstance().getGameData()._playerID].getWeaponCategory();
        //最后获得的存有选定角色能使用的所有武器的索引
        List<int> result = new();
        //由于每种武器有四种品质，故索引每次递增4
        for (int i = 0; i < JsonLoader.weaponPool.Count; i += 4)
        {
            //判断当前索引对应的武器选定的角色能否使用
            foreach (WeaponAttribute.WeaponCategory category in JsonLoader.weaponPool[i].getWeaponCategory())
            {
                if (list.Contains(WeaponAttribute.WeaponCategory.All))
                {
                    //All表示可以使用任何武器
                    result.Add(i);
                    break;
                }
                else if (list.Contains(category))
                {
                    result.Add(i);
                    break;
                }
            }
        }
        return result;
    }

    //以下是按钮的点击事件
    public void OnSelectBtn(Button btn)
    {
        if (!isSelectRole)  //角色选择按钮的逻辑
        {
            //更新角色选择状态
            isSelectRole = true;
            //按钮在生成时使用了对应角色的索引作为名字，方便获取点击选择的角色信息
            GameController.getInstance().getGameData()._playerID = int.Parse(btn.name);
            //获取可用的武器索引列表
            List<int> weaponIndex = FetchUsableWeapon();
            //由于角色选择内容不会发生改变，故此处只解绑而非销毁
            //将其重新挂载到场景中并透明，避免多次解绑导致物体大量复制
            Transform scrollAreaContent = transform.Find("ScrollSelectArea").GetChild(0).GetChild(0);
            Transform[] allChildren = scrollAreaContent.GetComponentsInChildren<Transform>(true);
            foreach (Transform child in allChildren)
            {
                if (child != scrollAreaContent)
                {
                    child.SetParent(GameObject.Find("Background").transform);
                    Image img = child.GetComponent<Image>();
                    img.color = new Color(img.color.r, img.color.g, img.color.b, 0f);
                }
            }
            //根据可用武器索引列表生成对应按钮
            foreach (int index in weaponIndex)
            {
                GameObject obj = new GameObject($"{index}");
                Button button = obj.AddComponent<Button>();
                button.image = button.gameObject.AddComponent<Image>();
                weaponContentList.Add(obj);
            }
            //等待按钮添加完成后给按钮赋予相应武器的图标
            DelayToInvoke.DelayToInvokeByFrame(() =>
            {
                for (int i = 0; i < weaponIndex.Count; i++)
                {
                    Button button = weaponContentList[i].GetComponent<Button>();
                    string prePath = "";
                    //根据武器的攻击类型确定武器图片的路径
                    switch (JsonLoader.weaponPool[weaponIndex[i]].getWeaponDamageType())
                    {
                        case WeaponAttribute.WeaponDamageType.Melee:
                            prePath = "Assets/Sprites/Weapon/Melee Weapon/";
                            break;
                        case WeaponAttribute.WeaponDamageType.Ranged:
                            prePath = "Assets/Sprites/Weapon/Ranged Weapon/";
                            break;
                        case WeaponAttribute.WeaponDamageType.Ability:
                            prePath = "Assets/Sprites/Weapon/Ability Weapon/";
                            break;
                        default:
                            break;
                    }
                    ImageLoader.LoadImage(prePath + JsonLoader.weaponPool[weaponIndex[i]].getWeaponIcon(), button.image);
                }
            }, 3);
            //给每个武器按钮添加控制脚本
            DelayToInvoke.DelayToInvokeByFrame(() =>
            {
                for (int i = 0; i < weaponContentList.Count; i++)
                {
                    weaponContentList[i].AddComponent<DisplayRoleAndWeaponDetail>();
                }
                DisplayWeaponScrollContent();
            }, 6);
            //给每个武器按钮绑定点击事件
            DelayToInvoke.DelayToInvokeByFrame(() =>
            {
                foreach (GameObject btnObj in weaponContentList)
                {
                    Button weaponBtn = btnObj.GetComponent<Button>();
                    weaponBtn.onClick.AddListener(() => { OnSelectBtn(weaponBtn); });
                }
            }, 6);
        }
        else if (isSelectRole)  //武器选择按钮的逻辑
        {
            //更新gameData中需要更新的数据
            GameController.getInstance().getGameData()._isFirstPlaying = false;
            for (int i = 0; i < 1; i++)
            {
                GameController.getInstance().getGameData()._weaponList.Add(int.Parse(btn.name));
            }
            int ID = GameController.getInstance().getGameData()._playerID;
            CharacterAttribute attr = JsonLoader.rolePool[ID];
            GameController.getInstance().getGameData()._attr.setAllPlayerAttribute(attr);
            //已经选择完角色的前提下点击武器按钮直接切换场景
            SceneLoader._instance.loadScene(GameController.getInstance().getGameData()._scene);
            DelayToInvoke.DelayToInvokeBySecond(() => { RoleAndWeaponSelectWindow.Instance.Close(); }, 0.8f);
        }
    }
}
