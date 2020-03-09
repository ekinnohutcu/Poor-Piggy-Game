using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip violin, crash;
    private AudioSource source;
    private void Start()
    {
        source = GetComponent<AudioSource>();
        PlayViolin();
    }

    public void PlayViolin()
    {
        source.PlayOneShot(violin);
    }
    public void PlayCrash()
    {
        source.PlayOneShot(crash);
    }
}
