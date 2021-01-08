using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    [SerializeField] AudioSource buttonSoundSource;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip[] sounds;

    int soundsIndex = 0;
    float volume;

    private void Awake()
    {
        source.clip = sounds[soundsIndex];
        volume = source.volume;
    }

    public void SwitchSounds()
    {
        buttonSoundSource.Play();

        source.volume = volume;
        soundsIndex++;

        if(soundsIndex >= sounds.Length) { soundsIndex = 0; }

        if(soundsIndex == 2) { source.volume = 0.7f; }

        source.clip = sounds[soundsIndex];
        source.Play();
    }

    public void TurnOn()
    {
        buttonSoundSource.Play();

        if (!source.isPlaying)
            source.Play();
    }
    public void TurnOff()
    {
        buttonSoundSource.Play();

        source.Stop();
    }
}
