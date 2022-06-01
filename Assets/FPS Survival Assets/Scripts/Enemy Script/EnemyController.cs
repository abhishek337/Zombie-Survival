using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
        PATROL,
        CHASE,
        ATTACK
}

public class EnemyController : MonoBehaviour
{
    private EnemyAnimator enemy_anim;
    private NavMeshAgent navmesh;
    private EnemyState enemystate;

    public float walkSpeed = 0.5f;
    public float runSpeed = 4f;

    public float chase_distance = 7f;
    private float current_chase_distance;
    public float attack_Distance = 1.8f;
    public float chase_after_attack = 2f;

    public float patrol_radius_min = 20f, patrol_radius_max = 60f;
    public float patrol_for_this_time = 15f;
    private float patrol_timer;
    public float next_attack = 2f;
    private float attack_timer;

    private Transform target;

    public GameObject attack_Point;
    private EnemyAudioControl enemy_Audio;

    // Start is called before the first frame update
    void Awake()
    {
        enemy_anim = GetComponent<EnemyAnimator>();
        navmesh = GetComponent<NavMeshAgent>();

        target = GameObject.FindWithTag("Player").transform;

        enemy_Audio = GetComponentInChildren<EnemyAudioControl>();
    }

    private void Start()
    {
        enemystate = EnemyState.PATROL;

        patrol_timer = patrol_for_this_time;
        attack_timer = next_attack;
        current_chase_distance = chase_distance;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemystate == EnemyState.PATROL)
        {
            patrol();
        }

        if(enemystate == EnemyState.CHASE)
        {
            chase();
        }

        if(enemystate == EnemyState.ATTACK)
        {
            attack();
        }
    }

    void patrol()
    {
        navmesh.isStopped = false;
        navmesh.speed = walkSpeed;

        patrol_timer += Time.deltaTime;

        if(patrol_timer > patrol_for_this_time)
        {
            setRandomDestination();

            patrol_timer = 0f;
        }

        if(navmesh.velocity.sqrMagnitude > 0)
        {
            enemy_anim.walkAnim(true);
        }
        else
        {
            enemy_anim.walkAnim(false);
        }

        if(Vector3.Distance(transform.position, target.position) <= chase_distance)
        {
            enemy_anim.walkAnim(false);

            enemystate = EnemyState.CHASE;

            enemy_Audio.playScrim_Sound();
        }
    }

    void chase()
    {
        navmesh.isStopped = false;
        navmesh.speed = runSpeed;

        navmesh.SetDestination(target.position);

        if (navmesh.velocity.sqrMagnitude > 0)
        {
            enemy_anim.runAnim(true);
        }
        else
        {
            enemy_anim.runAnim(false);
        }

        if (Vector3.Distance(transform.position, target.position) <= attack_Distance)
        {
            enemy_anim.walkAnim(false);
            enemy_anim.runAnim(false);

            enemystate = EnemyState.ATTACK;

            if(chase_distance != current_chase_distance)
            {
                chase_distance = current_chase_distance;
            }
        }
        else if(Vector3.Distance(transform.position, target.position) > chase_distance)
        {
            enemy_anim.runAnim(false);

            enemystate = EnemyState.PATROL;

            patrol_timer = patrol_for_this_time;

            if (chase_distance != current_chase_distance)
            {
                chase_distance = current_chase_distance;
            }
        }
    }

    void attack()
    {
        navmesh.velocity = Vector3.zero;
        
        navmesh.isStopped = true;

        attack_timer += Time.deltaTime;

        if(attack_timer > next_attack)
        {
            enemy_anim.attackAnim();

            attack_timer = 0;

            enemy_Audio.playAttackSound();
        }

        if(Vector3.Distance(transform.position, target.position) > attack_Distance + chase_after_attack)
        {
            enemystate = EnemyState.CHASE;
        }
    }

    void setRandomDestination()
    {
        float rand_radius = Random.Range(patrol_radius_min, patrol_radius_max);
        Vector3 rand_dir = Random.insideUnitSphere * rand_radius;
        rand_dir += transform.position;

        NavMeshHit navHit;

        NavMesh.SamplePosition(rand_dir,out navHit, rand_radius, -1);

        navmesh.SetDestination(navHit.position);
    }

    void turn_on_attackpoint()
    {
        attack_Point.SetActive(true);
    }

    void turn_off_attackpoint()
    {
        if (attack_Point.activeInHierarchy)
        {
            attack_Point.SetActive(false);
        }
    }

    public EnemyState enemy_State
    {
        get; set;
    }
}
