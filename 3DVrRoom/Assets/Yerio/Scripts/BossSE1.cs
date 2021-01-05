using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSE1 : MonoBehaviour
{
    [SerializeField] AudioClip[] lineSounds;
    [SerializeField] Line[] lines;
    [SerializeField] Door doorToUnlock;

    AudioManager audioManager;
    SubtitleManager subtitleManager;
    AudioSource audioSource;
    private void Awake()
    {
        subtitleManager = FindObjectOfType<SubtitleManager>();
        audioSource = GetComponent<AudioSource>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void StartBeginningSequence()
    {
        StartCoroutine(BeginningSequence());
    }

    void CallLine(int index)
    {
        audioSource.clip = lineSounds[index];
        audioSource.Play();
        subtitleManager.SetupSubtitle(lines[index].line, lines[index].name, GetLineLength(index));
    }
    
    float GetLineLength(int index) { return lines[index].lineLength; }

    IEnumerator BeginningSequence()
    {
        yield return new WaitForSeconds(4.5f);

        CallLine(0);

        yield return new WaitForSeconds(GetLineLength(0) + 0.5f);

        CallLine(1);

        yield return new WaitForSeconds(GetLineLength(1) + 0.5f);

        CallLine(2);

        yield return new WaitForSeconds(GetLineLength(2) + 0.5f);

        audioManager.PlaySound("SE_1.5");

        doorToUnlock.UnlockDoor();
    }
}
