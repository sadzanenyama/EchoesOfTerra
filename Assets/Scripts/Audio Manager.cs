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
     
        SetMusicAndSFXVolume(); 
    }


    public float MusicAudioValues()
    {
        return musicAudioSource.volume;
    }

    public float SFXAudioValues()
    {
        return sfxAudioSource.volume;
    }
    public void Start()
    {
        SetMusicAndSFXVolume(); 
    }
    public void SetMusicAndSFXVolume()
    {
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
        musicAudioSource.volume = musicVolume != 0 ? musicVolume : 1;
        sfxAudioSource.volume = audioVolume != 0 ? audioVolume : 1;
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
        Sound sound = Array.Find(audioSounds, soundItem => soundItem.nameOfSound == name);
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
        PlayerPrefs.SetFloat("MusicVolume", musicAudioSource.volume);
    }
    public void AdjustSFXVolume(float value)
    {
        sfxAudioSource.volume = value;
        PlayerPrefs.SetFloat("AudioVolume", sfxAudioSource.volume);
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
