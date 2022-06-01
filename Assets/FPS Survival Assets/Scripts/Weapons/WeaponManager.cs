using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private WeaponHandler[] weapons;
    private int current_index;

    // Start is called before the first frame update
    void Start()
    {
        current_index = 0;
        weapons[current_index].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            select_Weapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            select_Weapon(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            select_Weapon(2);
        }
    }

    void select_Weapon(int weapon_No)
    {
        if (current_index == weapon_No)
            return;

        weapons[current_index].gameObject.SetActive(false);
        weapons[weapon_No].gameObject.SetActive(true);
        current_index = weapon_No;
    }

    public WeaponHandler currentWeapon_Info()
    {
        return weapons[current_index];
    }
}
