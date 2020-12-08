using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    [SerializeField] AudioClip[] sounds;

    int soundsIndex = 0;
    AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        source.clip = sounds[soundsIndex];
    }

    public void SwitchSounds()
    {
        soundsIndex++;

        if(soundsIndex > sounds.Length - 1) { soundsIndex = 0; }

        source.clip = sounds[soundsIndex];
    }

    public void TurnOn()
    {
        source.Play();
    }
    public void TurnOff()
    {
        source.Stop();
    }
}
