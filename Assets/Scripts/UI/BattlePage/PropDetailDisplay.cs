using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PropDetailDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //详细信息显示面板
    public GameObject detailDisplay;

    public void OnPointerEnter(PointerEventData eventData)
    {
        //根据物体名字获取对应的数据
        PropAttribute prop = new();
        prop.setPropAttribute(JsonLoader.propPool[int.Parse(this.name)]);

        //加载道具的背景和图标
        ImageLoader.LoadImage($"Assets/Sprites/Weapon/{prop.getPropBgIcon()}", detailDisplay.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>());
        ImageLoader.LoadImage($"Assets/Sprites/Prop/{prop.getPropIcon()}", detailDisplay.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>());
        //设置道具名字
        detailDisplay.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text = prop.getPropName();
        //设置道具的属性文本
        Text propAttrText = detailDisplay.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>();
        propAttrText.text = "";
        if (prop.getMaxHealth() != 0f)
            propAttrText.text += $"最大生命  <color={(prop.getMaxHealth() > 0 ? "green" : "red")}> {(prop.getMaxHealth() > 0 ? "+" : "")}{prop.getMaxHealth()} </color>\n";
        if (prop.getHealthRecovery() != 0f)
            propAttrText.text += $"生命回复  <color={(prop.getHealthRecovery() > 0 ? "green" : "red")}> {(prop.getHealthRecovery() > 0 ? "+" : "")}{prop.getHealthRecovery()} </color>\n";
        if (prop.getHealthSteal() != 0f)
            propAttrText.text += $"生命汲取  <color={(prop.getHealthSteal() > 0 ? "green" : "red")}> {(prop.getHealthSteal() > 0 ? "+" : "")}{prop.getHealthSteal()} </color>\n";
        if (prop.getAttackAmplification() != 0f)
            propAttrText.text += $"攻击增幅  <color={(prop.getAttackAmplification() > 0 ? "green" : "red")}> {(prop.getAttackAmplification() > 0 ? "+" : "")}{prop.getAttackAmplification()}% </color>\n";
        if (prop.getMeleeDamage() != 0f)
            propAttrText.text += $"近战伤害  <color={(prop.getMeleeDamage() > 0 ? "green" : "red")}> {(prop.getMeleeDamage() > 0 ? "+" : "")}{prop.getMeleeDamage()} </color>\n";
        if (prop.getRangedDamage() != 0f)
            propAttrText.text += $"远程伤害  <color={(prop.getRangedDamage() > 0 ? "green" : "red")}> {(prop.getRangedDamage() > 0 ? "+" : "")}{prop.getRangedDamage()} </color>\n";
        if (prop.getAbilityDamage() != 0f)
            propAttrText.text += $"属性伤害  <color={(prop.getAbilityDamage() > 0 ? "green" : "red")}> {(prop.getAbilityDamage() > 0 ? "+" : "")}{prop.getAbilityDamage()} </color>\n";
        if (prop.getAttackSpeedAmplification() != 0f)
            propAttrText.text += $"攻击速度  <color={(prop.getAttackSpeedAmplification() > 0 ? "green" : "red")}> {(prop.getAttackSpeedAmplification() > 0 ? "+" : "")}{prop.getAttackSpeedAmplification()}% </color>\n";
        if (prop.getCriticalRate() != 0f)
            propAttrText.text += $"暴击概率  <color={(prop.getCriticalRate() > 0 ? "green" : "red")}> {(prop.getCriticalRate() > 0 ? "+" : "")}{prop.getCriticalRate()}% </color>\n";
        if (prop.getEngineering() != 0f)
            propAttrText.text += $"工程机械  <color={(prop.getEngineering() > 0 ? "green" : "red")}> {(prop.getEngineering() > 0 ? "+" : "")}{prop.getEngineering()} </color>\n";
        if (prop.getAttackRangeAmplification() != 0f)
            propAttrText.text += $"攻击范围  <color={(prop.getAttackRangeAmplification() > 0 ? "green" : "red")}> {(prop.getAttackRangeAmplification() > 0 ? "+" : "")}{prop.getAttackRangeAmplification()} </color>\n";
        if (prop.getArmorStrength() != 0f)
            propAttrText.text += $"机甲强度  <color={(prop.getArmorStrength() > 0 ? "green" : "red")}> {(prop.getArmorStrength() > 0 ? "+" : "")}{prop.getArmorStrength()} </color>\n";
        if (prop.getDodgeRate() != 0f)
            propAttrText.text += $"闪避概率  <color={(prop.getDodgeRate() > 0 ? "green" : "red")}> {(prop.getDodgeRate() > 0 ? "+" : "")}{prop.getDodgeRate()}% </color>\n";
        if (prop.getMoveSpeedAmplification() != 0f)
            propAttrText.text += $"移动速度  <color={(prop.getMoveSpeedAmplification() > 0 ? "green" : "red")}> {(prop.getMoveSpeedAmplification() > 0 ? "+" : "")}{prop.getMoveSpeedAmplification()}% </color>\n";
        if (prop.getScanAccuracy() != 0f)
            propAttrText.text += $"扫描精度  <color={(prop.getScanAccuracy() > 0 ? "green" : "red")}> {(prop.getScanAccuracy() > 0 ? "+" : "")}{prop.getScanAccuracy()} </color>\n";
        if (prop.getCollectEfficiency() != 0f)
            propAttrText.text += $"采集效率  <color={(prop.getCollectEfficiency() > 0 ? "green" : "red")}> {(prop.getCollectEfficiency() > 0 ? "+" : "")}{prop.getCollectEfficiency()} </color>\n";
        //将面板移动到指定的位置
        detailDisplay.transform.GetChild(0).position = new Vector3(transform.position.x, transform.position.y + 110f, transform.position.z);
        //激活面板
        detailDisplay.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //隐藏面板
        detailDisplay.SetActive(false);
    }
}
