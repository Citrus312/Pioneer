using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInfoCalcu
{
    private static MonsterInfoCalcu instance;

    public static MonsterInfoCalcu Instance
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
    public List<int> genMonsterType = new();
    public List<int> genMonsterCount = new();
    public List<CharacterAttribute> genMonsterAttr = new();

    public void Cal()
    {
        Clear();
        for (int i = 0; i < JsonLoader.monsterPool.Count; i++)
        {
            int wave = GameController.getInstance().getGameData()._wave;
            int difficulty = GameController.getInstance().getGameData()._difficulty;
            CharacterAttribute monster = new();
            monster.setAllMonsterAttribute(JsonLoader.monsterPool[i]);
            if (monster.getBelongLevel() == "Boss" && GameController.getInstance().getGameData()._wave == 20)
            {
                genMonsterType.Add(i);
                genMonsterCount.Add(1);
                genMonsterAttr.Add(monster);
            }
            if (monster.getFirstGenWave() <= wave && monster.getBelongLevel() == GameController.getInstance().getGameData()._scene)
            {
                genMonsterType.Add(i);
                float baseCount = Random.Range(monster.getMinGenCount(), monster.getMaxGenCount());
                genMonsterCount.Add((int)Mathf.Ceil(baseCount * (1 + (difficulty - 1) * 0.1f) * (1 + (wave - 1) * 0.05f)));

                CharacterAttribute temp = monster;
                temp.setMaxHealth((monster.getMaxHealth() + monster.getHealthIncPerWave() * (wave - 1)) * ((difficulty - 1) * 0.2f + 1));
                temp.setMeleeDamage((monster.getMeleeDamage() + monster.getDamageIncPerWave() * (wave - 1)) * ((difficulty - 1) * 0.1f) + 1);
                temp.setRangedDamage((monster.getRangedDamage() + monster.getDamageIncPerWave() * (wave - 1)) * ((difficulty - 1) * 0.1f) + 1);
                Debug.Log(temp.getMeleeDamage());
                genMonsterAttr.Add(temp);
            }
        }
    }

    public void Clear()
    {
        genMonsterAttr.Clear();
        genMonsterCount.Clear();
        genMonsterType.Clear();
    }

}
