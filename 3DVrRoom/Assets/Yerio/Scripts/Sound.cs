using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;
    public AudioMixerGroup mixer;

    [Range(0f, 1f)]
    public float volume;
    [Range(-3f, 3f)]
    public float pitch;

    public bool loop;
    public bool playOnAwake;
    [Space]
    public bool isVoiceLine;
    public Line voiceLine;

    [HideInInspector]
    public AudioSource source;
}
