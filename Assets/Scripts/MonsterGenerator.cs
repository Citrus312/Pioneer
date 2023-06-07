using UnityEngine;
using System.Collections;

public class MonsterGenerator : Generator
{
    public GameObject _indicatorPrefab;
    private GameObject _indicator;//怪物生成前的信号图片（如土豆兄弟中的红叉）
    public float flickerInterval; // 闪烁时间间隔，单位为秒
    public int flickerCount;//闪烁次数

    private float timeElapsed = 0f; // 记录已经经过的时间
    private bool isFading = false; // 是否正在变透明
    private float alpha = 1f;

    private void Start()
    {
        _indicator = Instantiate(_indicatorPrefab, transform.position, Quaternion.identity);
    }


    void Update()
    {
        SpriteRenderer spriteRenderer = _indicator.GetComponent<SpriteRenderer>();
        _indicator.transform.position = transform.position;

        //经过的时间
        timeElapsed += Time.deltaTime;
        calculateGenerationTimer();

        //isFading状态判定
        if (!isFading)
        {
            isFading = true;
            timeElapsed = 0f;

            alpha = 1f;
            _indicator.transform.position = transform.position;
        }

        //逐渐透明至完全透明后生成物品
        if ((!isFinishGeneration()))
        {
            spriteRenderer.enabled = true;
            alpha = 1f - (timeElapsed - (int)(timeElapsed / flickerInterval) * flickerInterval) / flickerInterval;

            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
            if (timeElapsed >= flickerCount * flickerInterval)
            {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
                isFading = false;
                transform.position = getGeneratorPosition();//获取MonsterGenerator的位置
                generateItem(getGenerationTimer()); // 生成怪物
            }
        }
        else
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
        }
    }
}