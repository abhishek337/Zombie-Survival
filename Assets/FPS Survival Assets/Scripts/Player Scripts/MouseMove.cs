using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    [SerializeField]
    private Transform playerRot;

    [SerializeField]
    private Transform cameraRot;

    [SerializeField]
    private bool invert;

    [SerializeField]
    private float sensitivity = 5f;

    [SerializeField]
    private Vector2 default_Lookangle = new Vector2(-70f, 80f);

    private Vector2 look_angles;
    private Vector2 mouse_look;
    private Vector2 smooth_move;

    private float current_roll_angle;

    private int last_look_frame;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        lock_unlock();

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            lookAround();
        }
    }

    void lock_unlock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
           
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void lookAround()
    {
        mouse_look = new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));

        look_angles.x += sensitivity * mouse_look.x * (invert ? 1f : -1f);
        look_angles.y += mouse_look.y * sensitivity;

        look_angles.x = Mathf.Clamp(look_angles.x, default_Lookangle.x, default_Lookangle.y);

        //current_roll_angle = Mathf.Lerp(current_roll_angle, Input.GetAxisRaw("Mouse X") * roll_angle, Time.deltaTime * roll_speed);

        cameraRot.localRotation = Quaternion.Euler(look_angles.x, 0f, 0f);
        playerRot.localRotation = Quaternion.Euler(0f, look_angles.y, 0f);
    }
}
