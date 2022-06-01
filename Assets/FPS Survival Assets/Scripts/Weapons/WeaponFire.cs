using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFire : MonoBehaviour
{
    public Camera fpsCamera;
    public CameraShake cameraShake;
    private AudioSource gunShot;

    private void Awake()
    {
        gunShot = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(cameraShake.shake(0.15f, 0.2f));

            raycasting();

            gunShot.Play();
        }
    }

    void raycasting()
    {
        RaycastHit Hit;
        if(Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out Hit))
        {
            Debug.Log(Hit.transform.name);
        }
    }
}
