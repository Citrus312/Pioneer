using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePageController : PersistentSingleton<PausePageController>
{
    public GameObject _Player;
    public bool isPaused = false;
    PausePageWindow pausePageWindow;

    private void Start()
    {
        UIRoot.Init();
        pausePageWindow = new();
        pausePageWindow.Open(getAttribute(_Player));
        pausePageWindow.Close();
    }

    private void Update()
    {
        if (getCurrentScene() != "MainPage")
        {
            if (Input.GetKeyUp(KeyCode.Escape))
                if (pausePageWindow.getTransform().gameObject.activeSelf)
                {
                    pausePageWindow.Close();
                }
                else
                {
                    pausePageWindow.Open();
                }
        }
    }

    private List<string> getAttribute(GameObject _player)
    {
        List<string> content = new List<string>();
        content.Add("目前等级: " + _player.GetComponent<CharacterAttribute>().getCurrentPlayerLevel());
        content.Add("最大生命值:  " + _player.GetComponent<CharacterAttribute>().getMaxHealth());
        content.Add("生命再生: " + _player.GetComponent<CharacterAttribute>().getHealthRecovery());
        content.Add("%生命窃取: " + _player.GetComponent<CharacterAttribute>().getHealthSteal());
        content.Add("%伤害:" + _player.GetComponent<CharacterAttribute>().getAttackAmplification());
        content.Add("近战伤害:" + _player.GetComponent<CharacterAttribute>().getMeleeDamage());
        content.Add("远程伤害:" + _player.GetComponent<CharacterAttribute>().getRangedDamage());
        content.Add("元素伤害:" + _player.GetComponent<CharacterAttribute>().getAbilityDamage());
        content.Add("%攻击速度:" + _player.GetComponent<CharacterAttribute>().getAttackSpeedAmplification());
        content.Add("%暴击率:" + _player.GetComponent<CharacterAttribute>().getCriticalRate());
        content.Add("工程机械:" + _player.GetComponent<CharacterAttribute>().getEngineering());
        content.Add("范围:" + _player.GetComponent<CharacterAttribute>().getAttackRangeAmplification());
        content.Add("机甲强度:" + _player.GetComponent<CharacterAttribute>().getArmorStrength());
        content.Add("%闪避概率:" + _player.GetComponent<CharacterAttribute>().getDodgeRate());
        content.Add("%移速加成:" + _player.GetComponent<CharacterAttribute>().getMoveSpeedAmplification());
        content.Add("扫描精度:" + _player.GetComponent<CharacterAttribute>().getScanAccuracy());
        content.Add("采集效率:" + _player.GetComponent<CharacterAttribute>().getCollectEfficiency());
        return content;
    }

    private string getCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        return currentSceneName;
    }
    public void OnPiont()
    {
        Transform property = pausePageWindow.getTransform().Find("Property");
        Transform tip = property.Find("panel");
        tip.gameObject.SetActive(true);
    }
}
