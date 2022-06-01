using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sprint_crouch : MonoBehaviour
{
    private PlayerMove playerMovement;

    private Transform look_root;

    public float sprint_speed = 10f;
    public float normal_speed = 5f;
    public float crouch_speed = 2f;

    private float stand_hight = 1.6f;
    private float crouch_height = 1f;

    private bool isCrouching;

    private PlayerFootSteps player_footsteps;

    private float sprint_volume = 1f;
    private float crouch_volume = 0.1f;
    private float walk_volumeMin = 0.2f, walk_volumeMax = 0.6f;

    private float walk_stepdistance = 0.4f;
    private float sprint_stepdistance = 0.25f;
    private float crouch_stepdistance = 0.5f;

    private PlayerUI player_Ui;

    private float sprint_value = 100f;
    public float sprint_treshold = 10f; 

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMove>();

        look_root = transform.GetChild(0);

        player_footsteps = GetComponentInChildren<PlayerFootSteps>();

        player_Ui = GetComponent<PlayerUI>();
    }

    private void Start()
    {
        player_footsteps.volume_High = walk_volumeMax;
        player_footsteps.volume_Low = walk_volumeMin;
        player_footsteps.step_distance = walk_stepdistance;
    }

    // Update is called once per frame
    void Update()
    {
        sprint();
        crouch();
    }

    void sprint()
    {
        if(sprint_value > 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching)
            {
                playerMovement.speed = sprint_speed;

                player_footsteps.step_distance = sprint_stepdistance;
                player_footsteps.volume_Low = sprint_volume;
                player_footsteps.volume_High = sprint_volume;
            }
        }       

        if(Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching)
        {
            playerMovement.speed = normal_speed;

            player_footsteps.step_distance = walk_stepdistance;
            player_footsteps.volume_High = walk_volumeMax;
            player_footsteps.volume_Low = walk_volumeMin;
           
        }

        if(Input.GetKey(KeyCode.LeftShift) && !isCrouching)
        {
            sprint_value -= Time.deltaTime * sprint_treshold;

            if(sprint_value <= 0f)
            {
                sprint_value = 0f;

                playerMovement.speed = normal_speed;
                player_footsteps.step_distance = walk_stepdistance;
                player_footsteps.volume_High = walk_volumeMax;
                player_footsteps.volume_Low = walk_volumeMin;
            }

            player_Ui.stamina_ui(sprint_value);
        }
        else
        {
            if(sprint_value != 100f)
            {
                sprint_value += (sprint_treshold / 2f) * Time.deltaTime;

                player_Ui.stamina_ui(sprint_value);

                if(sprint_value > 100f)
                {
                    sprint_value = 100f;
                }
            }
        }
    }

    void crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isCrouching)
            {
                look_root.localPosition = new Vector3(0f, stand_hight, 0f);
                playerMovement.speed = normal_speed;

                player_footsteps.step_distance = walk_stepdistance;
                player_footsteps.volume_High = walk_volumeMax;
                player_footsteps.volume_Low = walk_volumeMin;

                isCrouching = false;
            }
            else
            {
                look_root.localPosition = new Vector3(0f, crouch_height, 0f);
                playerMovement.speed = crouch_speed;

                isCrouching = true;

                player_footsteps.step_distance = crouch_stepdistance;
                player_footsteps.volume_Low = crouch_volume;
                player_footsteps.volume_High = crouch_volume;
            }
        }
    }
}
