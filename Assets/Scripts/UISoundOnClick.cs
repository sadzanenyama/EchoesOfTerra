using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UISoundOnClick : MonoBehaviour
{
    public AudioClip sound;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.ignoreListenerPause = false;
    }

    public void PlaySound()
    {
        audioSource.PlayOneShot(sound, 0.5f);
    }
}
