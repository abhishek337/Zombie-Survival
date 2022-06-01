using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterController character_Control;
    private Vector3 player_move;

    public float speed = 5f;
    private float gravity = 20f;
    public float jump_velocity = 10f;
    private float vertical_velocity;

    private void Awake()
    {
        character_Control = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
    }

    void movePlayer()
    {
        player_move = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        //Debug.Log("left-right : " + Input.GetAxis("Horizontal"));
        //Debug.Log("up-down : " + Input.GetAxis("Vertical"));

        player_move = transform.TransformDirection(player_move);
        player_move *= speed * Time.deltaTime;

        apply_Gravity();
        character_Control.Move(player_move);
    }

    void apply_Gravity()
    {

        vertical_velocity -= gravity * Time.deltaTime;

        Jump();

        player_move.y = vertical_velocity * Time.deltaTime;
    }

    void Jump()
    {
        if(character_Control.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            vertical_velocity = jump_velocity;
        }
    }
}
