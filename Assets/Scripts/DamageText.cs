using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    //DamageText预制体路径
    private static string _damageTextPrefab = "Assets/Prefab/DamageText.prefab";

    //获取预制体路径
    public static string getDamageTextPath()
    {
        return _damageTextPrefab;
    }

    private TextMeshPro _textMeshPro;
    private Color _textColor;

    //伤害数字显示时间
    public float _showTime = 0.5f;
    //伤害数字消失计时器
    protected float _showTimer;
    //伤害数字消失速度
    protected float _disappearSpeed = 5.0f;
    //文本类型
    public enum TextType { CommonDamage, CritDamage, PlayerHurt, PlayerCure };
    //不同类型文本对应的颜色
    public Color _commonDamageColor, _critDamageColor, _playerHurtColor, _playerCureColor;

    //设置文本类型
    public void setup(TextType textType, int damage)
    {
        //刷新显示时间
        _showTimer = _showTime;
        //显示的文本
        string text = damage.ToString();
        switch (textType)
        {
            case TextType.CommonDamage:
                _textColor = _commonDamageColor;
                break;
            case TextType.CritDamage:
                _textColor = _critDamageColor;
                break;
            case TextType.PlayerHurt:
                _textColor = _playerHurtColor;
                text = "-" + text;
                break;
            case TextType.PlayerCure:
                _textColor = _playerCureColor;
                text = "+" + text;
                break;
        }
        _textMeshPro.color = _textColor;
        _textMeshPro.SetText(text);
    }

    protected void Awake()
    {
        _textMeshPro = GetComponent<TextMeshPro>();
        _textColor = _textMeshPro.color;
    }

    // Update is called once per frame
    void Update()
    {
        _showTimer -= Time.deltaTime;
        //显示时间结束，开始淡化
        if (_showTimer <= 0)
        {
            _textColor.a -= Time.deltaTime * _disappearSpeed;
            _textMeshPro.color = _textColor;
            //如果alpha值小于0则销毁对象
            if (_textColor.a < 0)
            {
                ObjectPool.getInstance().remove(_damageTextPrefab, gameObject);
            }
        }
    }
}
