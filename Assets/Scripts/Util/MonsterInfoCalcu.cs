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
            CharacterAttribute monster = JsonLoader.monsterPool[i];
            if (monster.getFirstGenWave() <= wave)
            {
                genMonsterType.Add(i);
                float baseCount = Random.Range(monster.getMinGenCount(), monster.getMaxGenCount());
                genMonsterCount.Add((int)Mathf.Ceil(baseCount * (1 + (difficulty - 1) * 0.1f) * (1 + (wave - 1) * 0.1f)));

                CharacterAttribute temp = monster;
                temp.setMaxHealth((monster.getMaxHealth() + monster.getHealthIncPerWave() * (wave - 1)) * (difficulty - 1) * 0.2f);
                temp.setMeleeDamage((monster.getMeleeDamage() + monster.getDamageIncPerWave() * (wave - 1)) * (difficulty - 1) * 0.1f);
                temp.setRangedDamage((monster.getRangedDamage() + monster.getDamageIncPerWave() * (wave - 1)) * (difficulty - 1) * 0.1f);
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
