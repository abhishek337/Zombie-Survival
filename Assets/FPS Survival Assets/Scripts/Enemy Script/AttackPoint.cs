using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPoint : MonoBehaviour
{
    public float Damage = 20f;
    public float radius = 1f;
    public LayerMask layerMask;

    // Update is called once per frame
    void Update()
    {

        Collider[] collider = Physics.OverlapSphere(transform.position, radius, layerMask);

        if(collider.Length > 0)
        {
            print("We Hit : " + collider[0].gameObject.tag);
            collider[0].gameObject.GetComponent<HealthScript>().applyDamage(Damage);
            gameObject.SetActive(false);
        }

    }
}
