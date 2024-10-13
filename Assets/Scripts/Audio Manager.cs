using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    public Sound[] musicSounds;
    public Sound[] audioSounds;
    public static AudioManager instance;
    [SerializeField]  private AudioSource musicAudioSource;
    [SerializeField]  private  AudioSource sfxAudioSource;
    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;
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
 
    public void SetMusicAndSFXVolume()
    {
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
        musicMixer.SetFloat("Volume", Mathf.Log10(musicVolume)*20);
        sfxMixer.SetFloat("VolumeSFX", Mathf.Log10(audioVolume) * 20);
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
