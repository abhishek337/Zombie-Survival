using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioControl : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip scrim_sound, die_sound;

    [SerializeField]
    private AudioClip[] attack_Clip;

    void Awake()
    {
         audioSource =  GetComponent<AudioSource>();
    }

    public void playScrim_Sound()
    {
        audioSource.clip = scrim_sound;
        audioSource.Play();
    }

    public void playDie_Sound()
    {
        audioSource.clip = die_sound;
        audioSource.Play();
    }

    public void playAttackSound()
    {
        audioSource.clip = attack_Clip[Random.Range(0, attack_Clip.Length)];
        audioSource.Play();
    }
}
