using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private WeaponManager weapon_manager;

    public float firerate = 15f;
    public float nextFireRound;
    public float damage = 20f;

    private Animator cameraAnim;
    private bool Zoomed;
    private GameObject crossHair;
    private Camera mainCamera;

    private void Awake()
    {
        weapon_manager = GetComponent<WeaponManager>();

        //cameraAnim = transform.Find(Tags.Look_Root).transform.Find(Tags.Zoom_Camera).GetComponent<Animator>();
        cameraAnim = transform.Find("LookRoot").transform.Find("FirstPerson Camera").GetComponent<Animator>();

        crossHair = GameObject.FindWithTag("CrossHair");

        mainCamera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        weaponFire();
        zoomInOut();
    }

    void weaponFire()
    {
        if(weapon_manager.currentWeapon_Info().weapon_fire == weapon_Fire.Multiple)
        {
            if(Input.GetMouseButton(0) && Time.time > nextFireRound)
            {
                weapon_manager.currentWeapon_Info().shootAnimation();

                nextFireRound = Time.time + 1f / firerate;

                bullet_fire();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                weapon_manager.currentWeapon_Info().shootAnimation();

                bullet_fire();
            }
        }
    }

    void zoomInOut()
    {
        if(weapon_manager.currentWeapon_Info().weapon_aim == weapon_Aim.Aim)
        {
            if (Input.GetMouseButtonDown(1))
            {
                cameraAnim.Play("ZoomIn");
                crossHair.SetActive(false);
            }
            if (Input.GetMouseButtonUp(1))
            {
                cameraAnim.Play("ZoomOut");
                crossHair.SetActive(true);
            }
        }
    }

    void bullet_fire()
    {
        RaycastHit Hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out Hit))
        {
            Debug.Log(Hit.transform.name);
            if(Hit.transform.tag == "Enemy")
            {
                Hit.transform.GetComponent<HealthScript>().applyDamage(damage);
            }
        }
    }
}
