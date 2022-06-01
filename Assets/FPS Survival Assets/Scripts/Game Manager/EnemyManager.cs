using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public static EnemyManager instance;

    [SerializeField]
    private GameObject enemy_prefab;

    public Transform[] spawn_points;

    [SerializeField]
    private int enemy_count;

    private int initial_enemyCount;

    public float wait_before_spawn = 10f;

    void Awake()
    {
        make_Instance();
    }

    private void Start()
    {
        initial_enemyCount = enemy_count;

        spawnEnemy();

        StartCoroutine("startEnemeySpawn");
    }

    void make_Instance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void spawnEnemy()
    {
        int index = 0;

        for(int i = 0; i< enemy_count; i++)
        {
            if(index >= spawn_points.Length)
            {
                index = 0;
            }

            Instantiate(enemy_prefab, spawn_points[index].position, Quaternion.identity);

            index++;
        }

        enemy_count = 0;
    }

    IEnumerator startEnemeySpawn() {
        yield return new WaitForSeconds(wait_before_spawn);

        spawnEnemy();

        StartCoroutine("startEnemeySpawn");
    }

    public void enemyDied(bool enemy)
    {
        if (enemy)
        {
            enemy_count++;

            if(enemy_count > initial_enemyCount)
            {
                enemy_count = initial_enemyCount;
            }
        }
    }

    public void stop_spawn()
    {
        StopCoroutine("startEnemeySpawn");
    }
}
