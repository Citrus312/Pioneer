using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerator : Generator
{
    // 单例
    private static MonsterGenerator _instance;

    // 怪物生成前的信号图片路径
    private string _redCrossPath;

    // 在角色周围生成近战怪物的范围大小
    public float _meleeMonsterDistance;
    //在场景边界附近生成远程怪物的范围大小
    public float _rangedMonsterX;
    public float _rangedMonsterY;

    // 信号闪烁次数
    private int _flashCnt;

    public void Awake()
    {
        _redCrossPath = "Assets/Prefab/RedCross.prefab";
        _instance = this;
    }

    public static MonsterGenerator getInstance()
    {
        if (_instance == null)
        {
            _instance = new MonsterGenerator();
        }
        return _instance;
    }

    //开始生成
    public void beginGenerate(string monsterPrefabPath, int num, CharacterAttribute characterAttribute)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject redCross = ObjectPool.getInstance().get(_redCrossPath);
            //让红叉随机旋转一定角度
            redCross.transform.Rotate(new Vector3(0, 0, Random.Range(0, 360.0f)));
            StartCoroutine(generateMonster(redCross, monsterPrefabPath, characterAttribute));
        }
    }

    // 显示红叉，闪烁后生成怪物
    protected IEnumerator generateMonster(GameObject redCross, string monsterPrefabPath, CharacterAttribute characterAttribute)
    {
        Vector3 pos = getSpawnLocation(characterAttribute.getID());
        redCross.transform.position = pos;

        _flashCnt = 2;
        SpriteRenderer spriteRenderer = redCross.GetComponent<SpriteRenderer>();

        for (int i = 0; i < _flashCnt; i++)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1.0f);
            yield return new WaitForSeconds(0.5f);

            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.1f);
            yield return new WaitForSeconds(0.5f);
        }

        ObjectPool.getInstance().remove(_redCrossPath, redCross);

        GameObject monster = generateObject(monsterPrefabPath, pos);
        //设置怪物属性
        monster.GetComponent<CharacterAttribute>().setAllMonsterAttribute(characterAttribute);
        monster.GetComponent<WeaponAttribute>().setOwnerAttr(monster.GetComponent<CharacterAttribute>());
    }

    // 随机获取怪物生成位置
    protected Vector3 getSpawnLocation(int id)
    {
        // Vector3 pos = GameController.getInstance().getPlayer().transform.position + new Vector3(Random.Range(-1 * _meleeMonsterDistance, _meleeMonsterDistance), Random.Range(-1 * _meleeMonsterDistance, _meleeMonsterDistance), 0);
        // return pos;

        Vector3 pos = new Vector3();
        float height = RandomScene.getInstance().getSceneHeight();
        float width = RandomScene.getInstance().getSceneWidth();

        switch (id)
        {
            //近战怪在角色附近生成
            case 1:
            case 2:
            case 4:
            case 5:
            case 7:
            case 8:
                do
                {
                    pos = GameController.getInstance().getPlayer().transform.position + new Vector3(Random.Range(-1 * _meleeMonsterDistance, _meleeMonsterDistance), Random.Range(-1 * _meleeMonsterDistance, _meleeMonsterDistance), 0);
                } while (!(-width / 2 < pos.x && pos.x < width / 2 && -height / 2 < pos.y && pos.y < height / 2));
                break;
            //远程怪在场景边界附近生成
            case 3:
            case 6:
            case 9:
                float offsetX = Random.Range(-1 * _rangedMonsterX, _rangedMonsterX);
                float offsetY = Random.Range(-1 * _rangedMonsterY, _rangedMonsterY);
                pos.x = offsetX > 0 ? -width / 2 + offsetX : width / 2 + offsetX;
                pos.y = offsetY > 0 ? -height / 2 + offsetY : height / 2 + offsetY;
                break;
        }

        return pos;
    }
}