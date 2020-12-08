using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    [SerializeField] AudioClip[] sounds;

    int soundsIndex = 0;
    AudioSource source;
    float volume;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        source.clip = sounds[soundsIndex];
        volume = source.volume;
    }

    public void SwitchSounds()
    {
        source.volume = volume;
        soundsIndex++;

        if(soundsIndex >= sounds.Length) { soundsIndex = 0; }

        if(soundsIndex == 2) { source.volume = 0.7f; }

        source.clip = sounds[soundsIndex];
        source.Play();
    }

    public void TurnOn()
    {
        if (!source.isPlaying)
            source.Play();
    }
    public void TurnOff()
    {
        source.Stop();
    }
}
