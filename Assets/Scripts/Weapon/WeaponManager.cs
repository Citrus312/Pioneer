using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    //TODO: 后期修改为通过addWeapon方法添加初始武器
    public GameObject[] _initWeapons;
    //角色持有的武器
    ArrayList _weapons = new ArrayList();

    private void Start()
    {
        addWeapon(_initWeapons[0]);
        addWeapon(_initWeapons[1]);
        // addWeapon(_initWeapons[2]);
        // addWeapon(_initWeapons[3]);
        // addWeapon(_initWeapons[4]);
        // addWeapon(_initWeapons[5]);
    }

    //添加武器
    public void addWeapon(GameObject weapon)
    {
        _weapons.Add(weapon);
        switch (_weapons.Count)
        {
            case 1:
                ((GameObject)_weapons[0]).transform.localPosition = new Vector3(0.03f, -0.1f, 0);
                break;
            case 2:
                ((GameObject)_weapons[0]).transform.localPosition = new Vector3(0.24f, -0.1f, 0);
                ((GameObject)_weapons[1]).transform.localPosition = new Vector3(-0.2f, -0.1f, 0);
                break;
            case 3:
                ((GameObject)_weapons[0]).transform.localPosition = new Vector3(0.24f, -0.1f, 0);
                ((GameObject)_weapons[1]).transform.localPosition = new Vector3(-0.2f, -0.1f, 0);
                ((GameObject)_weapons[2]).transform.localPosition = new Vector3(0, 0.11f, 0);
                break;
            case 4:
                ((GameObject)_weapons[0]).transform.localPosition = new Vector3(0.24f, -0.2f, 0);
                ((GameObject)_weapons[1]).transform.localPosition = new Vector3(-0.2f, -0.2f, 0);
                ((GameObject)_weapons[2]).transform.localPosition = new Vector3(0.24f, 0.03f, 0);
                ((GameObject)_weapons[3]).transform.localPosition = new Vector3(-0.2f, 0.03f, 0);
                break;
            case 5:
                ((GameObject)_weapons[0]).transform.localPosition = new Vector3(0.24f, -0.2f, 0);
                ((GameObject)_weapons[1]).transform.localPosition = new Vector3(-0.2f, -0.2f, 0);
                ((GameObject)_weapons[2]).transform.localPosition = new Vector3(0.24f, 0.03f, 0);
                ((GameObject)_weapons[3]).transform.localPosition = new Vector3(-0.2f, 0.03f, 0);
                ((GameObject)_weapons[4]).transform.localPosition = new Vector3(0, 0.11f, 0);
                break;
            case 6:
                ((GameObject)_weapons[0]).transform.localPosition = new Vector3(0.24f, -0.2f, 0);
                ((GameObject)_weapons[1]).transform.localPosition = new Vector3(-0.2f, -0.2f, 0);
                ((GameObject)_weapons[2]).transform.localPosition = new Vector3(0.24f, -0.03f, 0);
                ((GameObject)_weapons[3]).transform.localPosition = new Vector3(-0.2f, -0.03f, 0);
                ((GameObject)_weapons[4]).transform.localPosition = new Vector3(0.24f, 0.11f, 0);
                ((GameObject)_weapons[5]).transform.localPosition = new Vector3(-0.2f, 0.11f, 0);
                break;
        }
    }
}
