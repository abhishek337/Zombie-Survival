using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootSteps : MonoBehaviour
{
    private AudioSource FootSound;

    [SerializeField]
    private AudioClip[] audioClips;

    private CharacterController character_controller;

    [HideInInspector]
    public float volume_High, volume_Low;

    private float accumulated_distance;

    [HideInInspector]
    public float step_distance;

    private void Awake()
    {
        FootSound = GetComponent<AudioSource>();
        character_controller = GetComponentInParent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        playFootstepSound();
    }

    void playFootstepSound()
    {
        if (!character_controller.isGrounded)
            return;

        if(character_controller.velocity.sqrMagnitude > 0)
        {
            accumulated_distance += Time.deltaTime;

            if(accumulated_distance > step_distance)
            {
                FootSound.volume = Random.Range(volume_Low, volume_High);
                FootSound.clip = audioClips[Random.Range(0, audioClips.Length)];
                FootSound.Play();

                accumulated_distance = 0f;
            }
        }
        else
        {
            accumulated_distance = 0f;
        }
    }
}
