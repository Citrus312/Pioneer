using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Damageable : MonoBehaviour
{
    public class DamageableEvent : UnityEvent { }
    //public float currentHealth { get; protected set; }
    [SerializeField]
    public DamageableEvent onDeath = new DamageableEvent();
    //受击闪烁的颜色
    public Color _onHitColor;
    //闪烁的持续时间
    public float _onHitTime;
    //预制体路径
    public string _prefabPath;
    //治疗文本预制体
    public string _cureTextPrefabPath;

    protected void Awake()
    {

        onDeath.AddListener(die);
        //初始化受击闪烁颜色和持续时间
        _onHitColor = new Color(255.0f / 255.0f, 100.0f / 255.0f, 100.0f / 255.0f, 255.0f / 255.0f);
        _onHitTime = 0.2f;
    }

    //受击闪烁
    private IEnumerator OnHit()
    {
        // 镜头震动，判断角色
        if (gameObject.tag == "Player")
        {
            CameraShake._instance.startShake();
        }

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = _onHitColor;
        yield return new WaitForSeconds(_onHitTime);
        spriteRenderer.color = Color.white;
    }

    private void die()
    {
        GetComponent<Controller>().OnDie();
        // 死亡动画
        GetComponent<Animator>().SetBool("B_isAlive", false);
        Invoke("removeFromPool", 0.5f);
    }

    void removeFromPool()
    {
        // 回收
        if (_prefabPath != null)
            ObjectPool.getInstance().remove(_prefabPath, gameObject);
        // 回收倒影
        WaterShadow water = GetComponent<WaterShadow>();
        if (water != null)
            water.removeWaterShadow();
    }

    public void TakeDamage(float damage)
    {
        GetComponent<CharacterAttribute>().setCurrentHealth(GetComponent<CharacterAttribute>().getCurrentHealth() - damage);
        //currentHealth -= damage;
        //GetComponent<CharacterAttribute>().setCurrentHealth(currentHealth);
        StartCoroutine("OnHit");
        if (GetComponent<CharacterAttribute>().getCurrentHealth() <= 0)
        {
            onDeath.Invoke();
        }
    }

    //治疗
    public void cure(float cureHealth)
    {
        CharacterAttribute characterAttribute = GetComponent<CharacterAttribute>();
        //如果满血就直接返回
        if (characterAttribute.getCurrentHealth() == characterAttribute.getMaxHealth())
            return;
        cureHealth = Mathf.Min(cureHealth, characterAttribute.getMaxHealth() - characterAttribute.getCurrentHealth());
        //显示治疗
        GameObject cureTextObj = ObjectPool.getInstance().get(_cureTextPrefabPath);
        cureTextObj.transform.position = transform.position + new Vector3(Random.Range(0, 0.5f), Random.Range(0, 0.5f), 0);
        DamageText recoveryText = cureTextObj.GetComponent<DamageText>();
        recoveryText.setup(DamageText.TextType.PlayerCure, ((int)cureHealth > 0) ? (int)cureHealth : 1);
        GetComponent<CharacterAttribute>().setCurrentHealth(Mathf.Min(GetComponent<CharacterAttribute>().getCurrentHealth() + cureHealth, GetComponent<CharacterAttribute>().getMaxHealth()));
        // Debug.Log(gameObject.tag + " cure " + cureHealth + ", current health=" + characterAttribute.getCurrentHealth() + ", max health=" + characterAttribute.getMaxHealth());
    }

    //生命回复
    IEnumerator recovery()
    {
        CharacterAttribute characterAttribute = GetComponent<CharacterAttribute>();
        float cureHealth = 0;
        while (true)
        {
            yield return new WaitForSeconds(1);
            float healthRecovery = characterAttribute.getHealthRecovery();
            if (healthRecovery > 0)
                cureHealth += 0.2f + 0.1f * (healthRecovery - 1);
            if (cureHealth >= 1)
            {
                cure((int)cureHealth);
                cureHealth -= (int)cureHealth;
            }
        }
    }

    public void startRecovery()
    {
        StartCoroutine(recovery());
    }

    public void stopIEnumerator()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.white;
        StopAllCoroutines();
    }
}
