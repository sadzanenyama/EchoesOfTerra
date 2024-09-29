using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName ="Echoesofterra/AudioSounds")]
public class Sound : ScriptableObject
{
    public string nameOfSound; 
    public AudioClip soundClip;
}
