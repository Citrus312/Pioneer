using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DisplayRoleAndWeaponDetail : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //用于显示角色名字、详细属性的文本组件
    public Text roleNameText;
    public Text roleAttrText;
    //用于显示武器名字、详细属性的文本组件
    public Text weaponNameText;
    public Text weaponAttrText;
    //用于显示角色和武器图标的图片组件
    public Image roleImg;
    public Image weaponImg;

    void Start()
    {
        //初始化详细信息显示区域
        if (!RoleAndWeaponSelectWindow.Instance.isSelectRole)
        {
            //如果还没有选定角色，则将角色详细信息显示区域的内容清空
            roleNameText = RoleAndWeaponSelectWindow.Instance.roleDisplay.Find("RoleName").GetComponent<Text>();
            roleNameText.text = "";
            roleAttrText = RoleAndWeaponSelectWindow.Instance.roleDisplay.Find("RoleAttribute").GetComponent<Text>();
            roleAttrText.text = "";
            roleImg = RoleAndWeaponSelectWindow.Instance.roleDisplay.Find("RoleImage").GetComponent<Image>();
            roleImg.color = new Color(roleImg.color.r, roleImg.color.g, roleImg.color.b, 0);
        }
        //无论是否选定角色，武器详细信息显示区域的内容都必须清空
        weaponNameText = RoleAndWeaponSelectWindow.Instance.weaponDisplay.Find("WeaponName").GetComponent<Text>();
        weaponNameText.text = "";
        weaponAttrText = RoleAndWeaponSelectWindow.Instance.weaponDisplay.Find("WeaponAttribute").GetComponent<Text>();
        weaponAttrText.text = "";
        weaponImg = RoleAndWeaponSelectWindow.Instance.weaponDisplay.Find("WeaponImage").GetComponent<Image>();
        weaponImg.color = new Color(weaponImg.color.r, weaponImg.color.g, weaponImg.color.b, 0);
    }

    //鼠标进入角色按钮或者武器按钮时触发的事件
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!RoleAndWeaponSelectWindow.Instance.isSelectRole)   //角色选择按钮的进入事件
        {
            //角色选择按钮的名字为对应角色的索引，故此处通过按钮名字获取索引，下面出现的相似用法也是这个逻辑
            int index = int.Parse(transform.name);
            //通过索引获取详细信息
            CharacterAttribute roleAttr = JsonLoader.rolePool[index];
            //将详细信息的文本赋给相应的文本组件用于显示
            roleNameText.text = roleAttr.getName();
            roleImg.sprite = transform.GetComponent<Image>().sprite;
            roleImg.color = new Color(roleImg.color.r, roleImg.color.g, roleImg.color.b, 1);
            //大于0的数值设置字体颜色为绿色，不大于0的设置字体颜色为红色
            roleAttrText.text = $"最大生命  <color={(roleAttr.getMaxHealth() > 0 ? "green" : "red")}> {roleAttr.getMaxHealth()} </color>\n" +
                                $"生命回复  <color={(roleAttr.getHealthRecovery() > 0 ? "green" : "red")}> {roleAttr.getHealthRecovery()} </color>\n" +
                                $"生命汲取  <color={(roleAttr.getHealthSteal() > 0 ? "green" : "red")}> {roleAttr.getHealthSteal()} </color>\n" +
                                $"攻击增幅  <color={(roleAttr.getAttackAmplification() > 0 ? "green" : "red")}> {roleAttr.getAttackAmplification()} </color>\n" +
                                $"近战伤害  <color={(roleAttr.getMeleeDamage() > 0 ? "green" : "red")}> {roleAttr.getMeleeDamage()} </color>\n" +
                                $"远程伤害  <color={(roleAttr.getRangedDamage() > 0 ? "green" : "red")}> {roleAttr.getRangedDamage()} </color>\n" +
                                $"属性伤害  <color={(roleAttr.getAbilityDamage() > 0 ? "green" : "red")}> {roleAttr.getAbilityDamage()} </color>\n" +
                                $"攻击速度  <color={(roleAttr.getAttackSpeedAmplification() > 0 ? "green" : "red")}> {roleAttr.getAttackSpeedAmplification()} </color>\n" +
                                $"暴击概率  <color={(roleAttr.getCriticalRate() > 0 ? "green" : "red")}> {roleAttr.getCriticalRate()} </color>\n" +
                                $"工程机械  <color={(roleAttr.getEngineering() > 0 ? "green" : "red")}> {roleAttr.getEngineering()} </color>\n" +
                                $"攻击范围  <color={(roleAttr.getAttackRangeAmplification() > 0 ? "green" : "red")}> {roleAttr.getAttackRangeAmplification()} </color>\n" +
                                $"机甲强度  <color={(roleAttr.getArmorStrength() > 0 ? "green" : "red")}> {roleAttr.getArmorStrength()} </color>\n" +
                                $"闪避概率  <color={(roleAttr.getDodgeRate() > 0 ? "green" : "red")}> {roleAttr.getDodgeRate()} </color>\n" +
                                $"移动速度  <color={(roleAttr.getMoveSpeedAmplification() > 0 ? "green" : "red")}> {roleAttr.getMoveSpeedAmplification()} </color>\n" +
                                $"扫描精度  <color={(roleAttr.getScanAccuracy() > 0 ? "green" : "red")}> {roleAttr.getScanAccuracy()} </color>\n" +
                                $"采集效率  <color={(roleAttr.getCollectEfficiency() > 0 ? "green" : "red")}> {roleAttr.getCollectEfficiency()} </color>\n";
        }
        else   //武器选择按钮的进入事件
        {
            //同上
            int index = int.Parse(transform.name);
            WeaponAttribute weaponAttr = JsonLoader.weaponPool[index];
            weaponNameText.text = weaponAttr.getWeaponName();
            weaponImg.sprite = transform.GetComponent<Image>().sprite;
            weaponImg.color = new Color(weaponImg.color.r, weaponImg.color.g, weaponImg.color.b, 1);
            weaponAttrText.text = $"<color=yellow>伤害</color>:  {weaponAttr.getWeaponDamage()}\n" +
                                  $"<color=yellow>范围</color>:  {weaponAttr.getAttackRange()} | {weaponAttr.getRawAttackRange()}\n" +
                                  $"<color=yellow>转化率</color>:  {weaponAttr.getConvertRatio()}\n" +
                                  $"<color=yellow>暴击</color>:  {weaponAttr.getCriticalBonus()}({weaponAttr.getCriticalRate() * 100}%)\n" +
                                  $"<color=yellow>攻速</color>:  {weaponAttr.getAttackSpeed()}s\n";
        }
    }
    //鼠标离开角色按钮或者武器按钮时触发的事件
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!RoleAndWeaponSelectWindow.Instance.isSelectRole)   //角色选择按钮的离开事件
        {
            //将角色详细信息显示区域的内容清空
            roleNameText.text = "";
            roleAttrText.text = "";
            roleImg.color = new Color(roleImg.color.r, roleImg.color.g, roleImg.color.b, 0);
        }
        else    //武器选择按钮的离开事件
        {
            //将武器详细信息显示区域的内容清空
            weaponNameText.text = "";
            weaponAttrText.text = "";
            weaponImg.color = new Color(weaponImg.color.r, weaponImg.color.g, weaponImg.color.b, 0);
        }
    }
}
