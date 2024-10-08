using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] musicSounds;
    public Sound[] audioSounds;
    public static AudioManager instance;
    [SerializeField]  private AudioSource musicAudioSource;
    [SerializeField]  private  AudioSource sfxAudioSource;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(gameObject);
;
        }
    }

    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(musicSounds, soundItem => soundItem.nameOfSound == name);
        if (sound == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            musicAudioSource.clip = sound.soundClip;
            musicAudioSource.Play();

        }
    }
    public void PlayAudioSFX(string name)
    {
        Sound sound = Array.Find(musicSounds, soundItem => soundItem.nameOfSound == name);
        if (sound == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            sfxAudioSource.clip = sound.soundClip;
            sfxAudioSource.Play();

        }
    }

    public void AdjustMusicVolume(float value)
    {
        musicAudioSource.volume = value; 
    }
    public void AdjustSFXVolume(float value)
    {
        sfxAudioSource.volume = value;
    }

    public void ToggleMusic()
    {
        musicAudioSource.mute = !musicAudioSource.mute;
    }
    public void ToggleSFX()
    {
        sfxAudioSource.mute = !sfxAudioSource.mute;
    }
}
