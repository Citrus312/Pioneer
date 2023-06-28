using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CancelSelect : MonoBehaviour
{
    void Update()
    {
        //监听Esc键是否按下
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //检测角色武器选择窗口的角色选择状态
            //若已选择，则返回角色选择
            //若未选择，则返回上一场景
            if (RoleAndWeaponSelectWindow.Instance.isSelectRole)
            {
                //清空武器详细信息显示区域
                RoleAndWeaponSelectWindow.Instance.weaponDisplay.Find("WeaponName").GetComponent<Text>().text = "";
                RoleAndWeaponSelectWindow.Instance.weaponDisplay.Find("WeaponAttribute").GetComponent<Text>().text = "";
                Image weaponImg = RoleAndWeaponSelectWindow.Instance.weaponDisplay.Find("WeaponImage").GetComponent<Image>();
                weaponImg.color = new Color(weaponImg.color.r, weaponImg.color.g, weaponImg.color.b, 0);
                //清空角色详细信息显示区域
                RoleAndWeaponSelectWindow.Instance.roleDisplay.Find("RoleName").GetComponent<Text>().text = "";
                RoleAndWeaponSelectWindow.Instance.roleDisplay.Find("RoleAttribute").GetComponent<Text>().text = "";
                Image roleImg = RoleAndWeaponSelectWindow.Instance.roleDisplay.Find("RoleImage").GetComponent<Image>();
                roleImg.color = new Color(roleImg.color.r, roleImg.color.g, roleImg.color.b, 0);
                //清空可用武器列表，因为不同角色的可用武器列表可能不同
                RoleAndWeaponSelectWindow.Instance.weaponContentList.Clear();
                //销毁滚动区域内的所有武器显示按钮
                Transform scrollAreaContent = transform.Find("ScrollSelectArea").GetChild(0).GetChild(0);
                Transform[] allChildren = scrollAreaContent.GetComponentsInChildren<Transform>(true);
                foreach (Transform child in allChildren)
                {
                    if (child != scrollAreaContent)
                    {
                        DestroyImmediate(child.gameObject);
                    }
                }
                //重新显示角色选择滚动窗口内容
                RoleAndWeaponSelectWindow.Instance.DisplayRoleScrollContent();
                //更新角色选择状态
                RoleAndWeaponSelectWindow.Instance.isSelectRole = false;
            }
            else
            {
                //返回上一场景
                SceneLoader._instance.loadScene("LevelSelect");
                //延迟关闭角色武器选择窗口，以流畅衔接过场动画
                DelayToInvoke.DelayToInvokeBySecond(() => { RoleAndWeaponSelectWindow.Instance.Close(); }, 0.4f);
            }
        }
    }
}
