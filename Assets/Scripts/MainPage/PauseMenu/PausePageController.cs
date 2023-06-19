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
        content.Add("Ŀǰ�ȼ�: " + _player.GetComponent<CharacterAttribute>().getCurrentPlayerLevel());
        content.Add("�������ֵ:  " + _player.GetComponent<CharacterAttribute>().getMaxHealth());
        content.Add("��������: " + _player.GetComponent<CharacterAttribute>().getHealthRecovery());
        content.Add("%������ȡ: " + _player.GetComponent<CharacterAttribute>().getHealthSteal());
        content.Add("%�˺�:" + _player.GetComponent<CharacterAttribute>().getAttackAmplification());
        content.Add("��ս�˺�:" + _player.GetComponent<CharacterAttribute>().getMeleeDamage());
        content.Add("Զ���˺�:" + _player.GetComponent<CharacterAttribute>().getRangedDamage());
        content.Add("Ԫ���˺�:" + _player.GetComponent<CharacterAttribute>().getAbilityDamage());
        content.Add("%�����ٶ�:" + _player.GetComponent<CharacterAttribute>().getAttackSpeedAmplification());
        content.Add("%������:" + _player.GetComponent<CharacterAttribute>().getCriticalRate());
        content.Add("���̻�е:" + _player.GetComponent<CharacterAttribute>().getEngineering());
        content.Add("��Χ:" + _player.GetComponent<CharacterAttribute>().getAttackRangeAmplification());
        content.Add("����ǿ��:" + _player.GetComponent<CharacterAttribute>().getArmorStrength());
        content.Add("%���ܸ���:" + _player.GetComponent<CharacterAttribute>().getDodgeRate());
        content.Add("%���ټӳ�:" + _player.GetComponent<CharacterAttribute>().getMoveSpeedAmplification());
        content.Add("ɨ�辫��:" + _player.GetComponent<CharacterAttribute>().getScanAccuracy());
        content.Add("�ɼ�Ч��:" + _player.GetComponent<CharacterAttribute>().getCollectEfficiency());
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
