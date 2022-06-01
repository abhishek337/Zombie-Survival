using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour
{
    EnemyAnimator enemyAnim;
    EnemyController enemyControl;
    NavMeshAgent navMesh;

    public float health = 100f;

    public bool is_Player, is_Enemy;

    private bool is_Dead;

    private EnemyAudioControl enemyAudio;

    private PlayerUI player_ui;

    // Start is called before the first frame update
    void Awake()
    {
        if (is_Enemy)
        {
            enemyAnim = GetComponent<EnemyAnimator>();
            enemyControl = GetComponent<EnemyController>();
            navMesh = GetComponent<NavMeshAgent>();

            enemyAudio = GetComponentInChildren<EnemyAudioControl>();
        }

        if (is_Player)
        {
            player_ui = GetComponent<PlayerUI>();
        }

    }

    public void applyDamage(float damage)
    {
        if (is_Dead)
            return;

        health -= damage;

        if (is_Player)
        {
            player_ui.health_ui(health);
        }

        if (is_Enemy)
        {
            if(enemyControl.enemy_State == EnemyState.PATROL)
            {
                enemyControl.chase_distance = 50f;
            }
        }

        if(health <=0)
        {
            Death();
            is_Dead = true;
        }

        void Death()
        {
            if (is_Enemy)
            {
                navMesh.velocity = Vector3.zero;
                navMesh.isStopped = true;
                enemyControl.enabled = false;

                StartCoroutine(playDeathSound());
                enemyAnim.deathAnim();

                EnemyManager.instance.enemyDied(true);
            }

            if (is_Player)
            {
                GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
                enemy.GetComponent<EnemyAnimator>().enabled = false;

                GetComponent<PlayerMove>().enabled = false;
                GetComponent<PlayerAttack>().enabled = false;
                GetComponent<WeaponManager>().currentWeapon_Info().gameObject.SetActive(false);

                EnemyManager.instance.stop_spawn();

            }

            if(tag == "Player")
            {
                Invoke("Restart", 1.0f);
            }
            else if(tag == "Enemy")
            {
                Invoke("TurnOftheGameObject", 2.0f);
            }
        }        
    }

    void Restart()
    {
        SceneManager.LoadScene("OverScene");
    }

    void TurnOftheGameObject()
    {
        gameObject.SetActive(false);
    }

    IEnumerator playDeathSound()
    {
        yield return new WaitForSeconds(0.3f);
        enemyAudio.playDie_Sound();
    }
}
