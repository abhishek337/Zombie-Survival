using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator Anim;

    // Start is called before the first frame update
    void Awake()
    {
        Anim = GetComponent<Animator>();
    }

    public void walkAnim(bool walk)
    {
        Anim.SetBool("Walk", walk);
    }

    public void runAnim(bool run)
    {
        Anim.SetBool("Run", run);
    }

    public void attackAnim()
    {
        Anim.SetTrigger("Attack");
    }

    public void deathAnim()
    {
        Anim.SetTrigger("Death");
    }

    public void idle()
    {
        Anim.SetTrigger("Idle");
    }
}
