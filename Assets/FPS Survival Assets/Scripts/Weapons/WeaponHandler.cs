using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum weapon_Aim
{
    None,
    Aim
}

public enum weapon_Fire
{
    Single,
    Multiple
}

public class WeaponHandler : MonoBehaviour
{
    private Animator anim;

    public weapon_Aim weapon_aim;

    [SerializeField]
    private GameObject muzzleFlash;

    [SerializeField]
    private AudioSource fire_Sound, reload_sound;

    public weapon_Fire weapon_fire;

    public GameObject attack_Point;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void shootAnimation()
    {
        anim.SetTrigger("Fire");
    }

    void turnOnMuzzleFlash()
    {
        muzzleFlash.SetActive(true);
    }

    void turnOffMuzzleFlash()
    {
        muzzleFlash.SetActive(false);
    }

    void gun_Firesound()
    {
        fire_Sound.Play();
    }

    void gun_ReloadSound()
    {
        reload_sound.Play();
    }

    void set_Aim()
    {
        attack_Point.SetActive(true);
    }

    void off_Aim()
    {
        if (attack_Point.activeInHierarchy)
        {
            attack_Point.SetActive(true);
        }
    }
}
