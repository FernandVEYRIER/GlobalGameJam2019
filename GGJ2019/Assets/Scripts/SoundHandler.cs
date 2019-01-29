using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundHandler : MonoBehaviour
{
    public AudioClip timerBip;

    private AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void StartTimerBip()
    {
        source.clip = timerBip;
        source.Play();
    }
}
